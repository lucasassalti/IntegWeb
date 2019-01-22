using IntegWeb.Entidades;
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Framework;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Previdencia.Aplicacao.DAL.Cadastro
{
    public class CadAjusteCepDAL
    {
        public class CustomerTBCEPInfo
        {
            public string TipoLogradouro { get; set; }
        }

        public PREV_Entity_Conn m_DbContext = new PREV_Entity_Conn();

        #region Município

        public List<MUNICIPIO> GetData(int startRowIndex, int maximumRows, string pCidade, string pUF, string pCodMunic, string pCodIBGE, string sortParameter)
        {
            return GetWhere(pCidade, pUF, pCodMunic, pCodIBGE)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<MUNICIPIO> GetWhere(string pCidade, string pUF, string pCodMunic, string pCodIBGE)
        {

            if (!string.IsNullOrEmpty(pCidade))
            {
                pCidade = pCidade.ToUpper();
            }

            if (!string.IsNullOrEmpty(pUF))
            {
                pUF = pUF.ToUpper();
            }

            IQueryable<MUNICIPIO> query;
            query = from r in m_DbContext.MUNICIPIO
                    where (r.COD_MUNICI == pCodMunic || pCodMunic == null)                       
                       && (r.DCR_MUNICI.ToUpper().Trim().Contains(pCidade.ToUpper().Trim()) || pCidade == null)
                       && (r.COD_ESTADO == pUF || pUF == null)
                       && (r.COD_MUNICI_IBGE == pCodIBGE || pCodIBGE == null)
                    select r;
            return query;
        }

        public int GetDataCount(string pCidade, string pUF, string pCodMunic, string pCodIBGE)
        {
            return GetWhere(pCidade, pUF, pCodMunic, pCodIBGE).SelectCount();
        }

        public MUNICIPIO GetMunicipio(string pCodMunic, string pCodIBGE)
        {
            return GetWhere(null, null, pCodMunic, pCodIBGE).FirstOrDefault();
        }

        public List<ESTADO> GetEstado()
        {
            IQueryable<ESTADO> query;

            query = from u in m_DbContext.ESTADO
                    select u;

            return query.ToList();
        }

        public IQueryable<MUNICIPIO> GetDataMunicipio()
        {
            IQueryable<MUNICIPIO> query;

            query = from u in m_DbContext.MUNICIPIO
                    select u;

            return query.Take(100);
        }

        public Resultado InserirMunicipio(MUNICIPIO obj)
        {
            Resultado res = new Resultado();

            try
            {

                var atualiza = m_DbContext.MUNICIPIO.FirstOrDefault(p => p.COD_MUNICI_IBGE == obj.COD_MUNICI_IBGE);

                if (atualiza == null)
                {
                    m_DbContext.MUNICIPIO.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Esse Código de Municipio já existe! ");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado AtualizaTabelaMunicipio(MUNICIPIO obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.MUNICIPIO.FirstOrDefault(p => p.COD_MUNICI_IBGE == obj.COD_MUNICI_IBGE);

                if (atualiza != null)
                {
                    atualiza.DCR_MUNICI = obj.DCR_MUNICI;
                    atualiza.DCR_RSUMD_MUNICI = obj.DCR_RSUMD_MUNICI;

                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro Atualizado com Sucesso"); //D:\po\IntegWeb\IntegWeb.Previdencia.Web\Includes\
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }


        public Resultado ExcluirMunicipio(MUNICIPIO obj)
        {
            Resultado res = new Resultado();

            try
            {
                var delete = m_DbContext.MUNICIPIO.FirstOrDefault(uref => uref.COD_ESTADO == obj.COD_ESTADO
                                                                    && uref.COD_MUNICI == obj.COD_MUNICI
                                                                    && uref.COD_MUNICI_IBGE == obj.COD_MUNICI_IBGE
                                                                    );
                if (delete != null)
                {
                    m_DbContext.MUNICIPIO.Remove(delete);
                    int rows_update = m_DbContext.SaveChanges();
                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registros excluido.", rows_update));
                    }
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));

            }

            return res;
        }

        public IQueryable<MUNICIPIO> GetLikeDescMunicOrCodMunic(string stringWhere)
        {
            IQueryable<MUNICIPIO> query;
            string stringLike = stringWhere.Trim().ToUpper();

            query = from u in m_DbContext.MUNICIPIO
                    where u.DCR_MUNICI.Contains(stringLike) ||
                           u.COD_MUNICI.Contains(stringLike)
                    select u;

            return query.Take(1000);
        }


        public DataTable SelectLikeDescMunicOrCodMunic(string stringWhere)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                string stringLike = stringWhere.Trim().ToUpper();
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select * FROM MUNICIPIO ");
                querysql.Append(" where ");
                querysql.Append(" DCR_MUNICI like '%" + stringLike + "%'");
                querysql.Append(" OR COD_MUNICI like '%" + stringLike + "%'");

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString());
                adpt.Fill(dt);
                adpt.Dispose();
            }

            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        #endregion

        public List<TB_CEP> GetDataCep(int startRowIndex, int maximumRows, string pUF, string pMunicipio, string pBairro, string pLogradouro, string pCEP, string sortParameter)
        {
            return GetWhereCep(pUF, pMunicipio, pBairro, pLogradouro, pCEP)
                  .GetData(startRowIndex, maximumRows, sortParameter).ToList();
        }

        public IQueryable<TB_CEP> GetWhereCep(string pUF, string pMunicipio, string pBairro, string pLogradouro, string pCEP)
        {
            if (!string.IsNullOrEmpty(pUF))
            {
                pUF = pUF.ToUpper();
            }

            if (!string.IsNullOrEmpty(pMunicipio))
            {
                pMunicipio = pMunicipio.ToUpper();
            }

            if (!string.IsNullOrEmpty(pBairro))
            {
                pBairro = pBairro.ToUpper();
            }

            if (!string.IsNullOrEmpty(pLogradouro))
            {
                pLogradouro = pLogradouro.ToUpper();
            }

            IQueryable<TB_CEP> query;
            query = from c in m_DbContext.TB_CEP
                    where (c.COD_ESTADO == pUF || pUF == null)
                       && (c.DES_BAIRRO.ToUpper().Trim().Contains(pBairro.ToUpper().Trim()) || pBairro == null)
                       && (c.NOM_LOGRADOURO.ToUpper().Trim().Contains(pLogradouro.ToUpper().Trim()) || pLogradouro == null)
                       && (c.NUM_CEP == pCEP || pCEP == null)
                    select c;
            return query;
        }

        public int GetDataCepCount(string pUF, string pMunicipio, string pBairro, string pLogradouro, string pCEP)
        {
            return GetWhereCep(pUF, pMunicipio, pBairro, pLogradouro, pCEP).SelectCount();
        }

        public IQueryable<TB_CEP> GetDataCep()
        {
            IQueryable<TB_CEP> query;

            query = from u in m_DbContext.TB_CEP
                    select u;

            return query.Take(100);
        }

        public List<CustomerTBCEPInfo> GetCEP()
        {
            var results = m_DbContext.TB_CEP.Select(x => new CustomerTBCEPInfo()
                {
                    TipoLogradouro = x.TIP_LOGRADOURO
                }).ToList().Distinct();

            return results.ToList();
        }

        public Resultado AtualizaTabelaCep(TB_CEP obj)
        {
            Resultado res = new Resultado();

            try
            {
                var atualiza = m_DbContext.TB_CEP.FirstOrDefault(p => (p.NUM_CEP == obj.NUM_CEP) && (p.COD_ESTADO == obj.COD_ESTADO) && (p.COD_MUNICI == obj.COD_MUNICI));

                if (atualiza != null)
                {
                    atualiza.NUM_CEP = obj.NUM_CEP;
                    atualiza.COD_ESTADO = obj.COD_ESTADO;
                    atualiza.COD_MUNICI = obj.COD_MUNICI;
                    atualiza.TIP_LOGRADOURO = obj.TIP_LOGRADOURO;
                    atualiza.NOM_LOGRADOURO = obj.NOM_LOGRADOURO;
                    atualiza.DES_BAIRRO = obj.DES_BAIRRO;
                    atualiza.DES_MUNICI = obj.DES_MUNICI;
                    atualiza.PAICOD = obj.PAICOD;
                    

                    int rows_updated = m_DbContext.SaveChanges();

                    if (rows_updated > 0)
                    {
                        res.Sucesso("Registro Atualizado com Sucesso");
                    }
                }
            }
            catch (Exception ex)
            {
                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado InseriCEP(TB_CEP obj)
        {
            Resultado res = new Resultado();

            try
            {

                var atualiza = m_DbContext.TB_CEP.FirstOrDefault(p => p.NUM_CEP == obj.NUM_CEP);

                if (atualiza == null)
                {
                    m_DbContext.TB_CEP.Add(obj);
                    int rows_insert = m_DbContext.SaveChanges();

                    if (rows_insert > 0)
                    {
                        res.Sucesso("Inclusão Feita com Sucesso");
                    }
                }
                else
                {
                    res.Alert("Esse Código de Municipio já existe! ");
                }
            }
            catch (Exception ex)
            {

                res.Erro(Util.GetInnerException(ex));
            }

            return res;
        }

        public Resultado ExcluirCep(TB_CEP obj)
        {
            Resultado res = new Resultado();

            try
            {
                var delete = m_DbContext.TB_CEP.FirstOrDefault(uref => uref.COD_ESTADO == obj.COD_ESTADO
                                                                    && uref.COD_MUNICI == obj.COD_MUNICI
                                                                    && uref.NUM_CEP == obj.NUM_CEP
                                                                    );

                if (delete != null)
                {
                    m_DbContext.TB_CEP.Remove(delete);
                    int rows_update = m_DbContext.SaveChanges();
                    if (rows_update > 0)
                    {
                        res.Sucesso(String.Format("{0} registros excluido.", rows_update));
                    }
                }
            }
            catch (Exception ex)
            {

                res.Erro("Existem regiões que utilizam esse CEP. Favor verificar as regiões desse CEP. ");

            }

            return res;
        }

        //Util.GetInnerException(ex)


        public IQueryable<TB_CEP> GetLikeCEPOrLogradouro(string stringWhere)
        {
            IQueryable<TB_CEP> query;
            string stringLike = stringWhere.Trim().ToUpper();

            query = from u in m_DbContext.TB_CEP
                    where u.NUM_CEP.Contains(stringLike) ||
                           u.NOM_LOGRADOURO.Contains(stringLike)
                    select u;

            return query.Take(100);
        }

        public DataTable SelectLikeCEPOrLogradouro(string stringWhere)
        {
            DataTable dt = new DataTable();
            ConexaoOracle objConexao = new ConexaoOracle();

            try
            {
                string stringLike = stringWhere.Trim().ToUpper();
                StringBuilder querysql = new StringBuilder();

                querysql.Append(" Select * FROM TB_CEP ");
                querysql.Append(" where ");
                querysql.Append(" NUM_CEP like '%" + stringLike + "%'");
                querysql.Append(" OR NOM_LOGRADOURO like '%" + stringLike + "%'");

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString());
                adpt.Fill(dt);
                adpt.Dispose();
            }

            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }
            return dt;
        }

        public IQueryable<TB_CEP> GetLikeDescLogradouro(string stringWhere)
        {
            IQueryable<TB_CEP> query;
            string stringLike = stringWhere.Trim().ToUpper();

            query = from u in m_DbContext.TB_CEP
                    where u.NOM_LOGRADOURO.Contains(stringLike)
                    select u;

            return query.Take(100);
        }

    }
}

