using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.ENUM;
using System;
using System.Linq;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class EmpregadoAtualizacaoDAL
    {
        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        public bool VerificaEmpregado(long pCPF)
        {
            return m_DbContext.EMPREGADO_ATUALIZACAO.Any(e => e.NUM_CPF == pCPF);
        }

        public EmpregadoTipo GetEmpregadoTipo(long pCPF, DateTime pNascimento)
        {
            if (m_DbContext.EMPREGADO.Any(e => e.NUM_CPF_EMPRG == pCPF && e.DAT_NASCM_EMPRG == pNascimento))
                return EmpregadoTipo.Empregado;
            else if (m_DbContext.REPRES_UNIAO_FSS.Any(r => r.NUM_CPF_REPRES == pCPF && r.DAT_NASCM_REPRES == pNascimento))
                return EmpregadoTipo.Representante;
            else if (m_DbContext.DEPENDENTE.Any(r => r.NUM_CPF_DPDTE == pCPF && r.DAT_NASCT_DPDTE == pNascimento))
                return EmpregadoTipo.Dependente;

            return EmpregadoTipo.Nenhum;
        }

        public Resultado ValidaEmail(long cpf, Guid guid)
        {
            Resultado res = new Resultado();
            try
            {
                var empregado = (from emp in m_DbContext.EMPREGADO_ATUALIZACAO
                                 where emp.NUM_CPF == cpf && emp.GUI_EMAIL == guid
                                 select emp).FirstOrDefault();

                if (empregado == null)
                {
                    res.Erro("Email não encontrado na atualização cadastral");
                }
                else if (empregado.CHR_EMAIL_CONFIRM == "1")
                {
                    res.Erro("Já foi realizado a validação para este email.");
                }
                else 
                {
                    empregado.CHR_EMAIL_CONFIRM = "1";
                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Email validado com sucesso.");
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

        public Resultado SaveAtualizacao(EMPREGADO_ATUALIZACAO empregado)
        {
            Resultado res = new Resultado();
            try
            {
                if (VerificaEmpregado(empregado.NUM_CPF))
                {
                    res.Alert("Já foi realizada a atualização para este CPF.");
                }
                else
                {
                    m_DbContext.EMPREGADO_ATUALIZACAO.Add(empregado);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Atualização cadastral realizada com sucesso!");
                    }
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }

        public string GetNome(long cpf, EmpregadoTipo empregadoTipo)
        {
            string nome = string.Empty;

            if (empregadoTipo == EmpregadoTipo.Empregado)
            {
                nome = (from emp in m_DbContext.EMPREGADO
                        where emp.NUM_CPF_EMPRG == cpf
                        select emp.NOM_EMPRG).FirstOrDefault();
            }
            else if (empregadoTipo == EmpregadoTipo.Representante)
            {
                nome = (from repres in m_DbContext.REPRES_UNIAO_FSS
                        where repres.NUM_CPF_REPRES == cpf
                        select repres.NOM_REPRES).FirstOrDefault();
            }
            else if (empregadoTipo == EmpregadoTipo.Dependente)
            {
                nome = (from dep in m_DbContext.DEPENDENTE
                        where dep.NUM_CPF_DPDTE == cpf
                        select dep.NOM_DPDTE).FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(nome))
                nome = nome.Split(' ')[0];
            else
                nome = string.Empty;
            nome = Util.GetCapitalize(nome);

            return nome;
        }

        public Resultado SincAtualizacao()
        {
            Resultado res = new Resultado();
            try
            {
                //Atualização de Empregado
                var empregado = from emp in m_DbContext.EMPREGADO
                                join atualiza in m_DbContext.EMPREGADO_ATUALIZACAO
                                on emp.NUM_CPF_EMPRG
                                equals atualiza.NUM_CPF
                                where atualiza.TIP_EMPREGADO == 1 && emp.DAT_NASCM_EMPRG == atualiza.DAT_NASCM
                                select new { emp = emp, atualiza = atualiza };

                foreach (var registro in empregado)
                {
                    registro.emp.COD_DDD_EMPRG = registro.atualiza.COD_DDD;
                    registro.emp.NUM_TELEF_EMPRG = registro.atualiza.NUM_TELEF;
                    registro.emp.COD_DDDCEL_EMPRG = registro.atualiza.COD_DDDCEL;
                    registro.emp.NUM_CELUL_EMPRG = registro.atualiza.NUM_CELUL;
                    registro.emp.COD_EMAIL_EMPRG = registro.atualiza.COD_EMAIL;
                }

                //Atualização de Representante
                var representante = from repres in m_DbContext.REPRES_UNIAO_FSS
                                    join atualiza in m_DbContext.EMPREGADO_ATUALIZACAO
                                    on repres.NUM_CPF_REPRES
                                    equals atualiza.NUM_CPF
                                    where atualiza.TIP_EMPREGADO == 2 && repres.DAT_NASCM_REPRES == atualiza.DAT_NASCM
                                    select new { repres = repres, atualiza = atualiza };

                foreach (var registro in representante)
                {
                    registro.repres.NUM_TELEF_REPRES = registro.atualiza.NUM_TELEF;
                    registro.repres.COD_DDDCEL_REPRES = registro.atualiza.COD_DDDCEL;
                    registro.repres.NUM_CELUL_REPRES = registro.atualiza.NUM_CELUL;
                    registro.repres.COD_EMAIL_REPRES = registro.atualiza.COD_EMAIL;
                }

                //Atualização de Dependente
                var dependente = from dep in m_DbContext.DEPENDENTE
                                join atualiza in m_DbContext.EMPREGADO_ATUALIZACAO
                                on dep.NUM_CPF_DPDTE
                                equals atualiza.NUM_CPF
                                where atualiza.TIP_EMPREGADO == 3 && dep.DAT_NASCT_DPDTE == atualiza.DAT_NASCM
                                select new { dep = dep, atualiza = atualiza };

                foreach (var registro in dependente)
                {
                    registro.dep.NUM_TELEF_DPDTE = registro.atualiza.NUM_TELEF;
                    registro.dep.COD_DDDCEL_DPDTE = registro.atualiza.COD_DDDCEL;
                    registro.dep.NUM_CELUL_DPDTE = registro.atualiza.NUM_CELUL;
                    registro.dep.COD_EMAIL_DPDTE = registro.atualiza.COD_EMAIL;
                }

                int rows_updated = m_DbContext.SaveChanges();

                if (rows_updated > 0)
                {
                    res.Sucesso("Atualização cadastral realizada com sucesso");
                }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                res.Erro(Util.GetEntityValidationErrors(ex));
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }
            return res;
        }
    }
}