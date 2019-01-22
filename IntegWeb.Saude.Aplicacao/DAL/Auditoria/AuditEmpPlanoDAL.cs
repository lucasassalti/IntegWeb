using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.DAL.Auditoria
{
    public class AuditEmpPlanoDAL
    {
        public SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public AuditEmpPlanoRpt GetPlan(string pCarteira, string pEmpresa, string pMatricula, string pSub, string pNome)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            AuditEmpPlanoRpt reg = new AuditEmpPlanoRpt();
            DataTable dt = new DataTable();
            AuditEmpPlanoRpt result = new AuditEmpPlanoRpt();

            try
            {
                StringBuilder querysql = new StringBuilder();
                StringBuilder searchsql = new StringBuilder();

                querysql.Append(@" select 
                                       pp.cod_emprs, e.nom_abrvo_emprs as NOM_EMPRESA, pp.num_matricula, pp.num_sub_matric, m.nom_particip as NOM_PESSOA, p.cod_plano as COD_PLANO, p.des_plano as NOM_PLANO
                                  from tb_particip_plano pp, tb_plano p, tb_particip_assmed m, att.empresa e
                                where pp.cod_plano = p.cod_plano
                                   and pp.cod_emprs = e.cod_emprs 
                                   and pp.num_seq_particip = m.num_seq_particip
                                   and P.COD_MOD_PLANO IN ('5', '6')
                                   and P.COD_PROGRAMA IN (1, 3, 8)
                                   and PP.SIT_PARTIC_PLANO = 'A'
                                   and pp.cod_emprs = " + pEmpresa + @"
                                   and pp.num_matricula = " + pMatricula + @"
                                   and pp.num_sub_matric = " + pSub + @"
                                   and pp.dat_cancelamento is null ");

                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(querysql.ToString() + searchsql.ToString());
                adpt.Fill(dt);
                adpt.Dispose();

                if (dt.Rows.Count <= 0) {
                    querysql.Clear();
                    querysql.Append(@" select 
                                       pp.cod_emprs, e.nom_abrvo_emprs as NOM_EMPRESA, pp.num_matricula, pp.num_sub_matric, m.nom_particip as NOM_PESSOA, p.cod_plano as COD_PLANO, p.des_plano as NOM_PLANO
                                  from tb_particip_plano pp, tb_plano p, tb_particip_assmed m, att.empresa e
                                where pp.cod_plano = p.cod_plano
                                   and pp.cod_emprs = e.cod_emprs 
                                   and pp.num_seq_particip = m.num_seq_particip
                                   and P.COD_MOD_PLANO IN ('5', '6')
                                   and P.COD_PROGRAMA IN (1, 3, 8)
                                   and PP.SIT_PARTIC_PLANO = 'A'
                                   and m.nom_particip like '%" + pNome + @"%'
                                   and pp.dat_cancelamento is null ");

                    adpt = objConexao.ExecutarQueryAdapter(querysql.ToString() + searchsql.ToString());
                    adpt.Fill(dt);
                    adpt.Dispose();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
                if (dt.Rows.Count > 0)
                {
                    reg = new AuditEmpPlanoRpt();
                    reg.CARTEIRA = pCarteira;
                    reg.COD_EMPRS = pEmpresa;
                    reg.EMPRESA = dt.Rows[0]["NOM_EMPRESA"].ToString();
                    reg.MATRICULA = pMatricula;
                    reg.NUM_SUB_MATRIC = pSub;
                    reg.NOME = dt.Rows[0]["NOM_PESSOA"].ToString();
                    reg.COD_PLANO = dt.Rows[0]["COD_PLANO"].ToString();
                    reg.PLANO = dt.Rows[0]["NOM_PLANO"].ToString();
                    result = reg;
                }
            }
            return result;
        }


        public class AuditEmpPlanoRpt {
            public string CARTEIRA { get; set; }
            public string COD_EMPRS { get; set; }
            public string EMPRESA { get; set; }
            public string MATRICULA { get; set; }
            public string NUM_SUB_MATRIC { get; set; }
            public string NOME { get; set; }
            public string COD_PLANO { get; set; }
            public string PLANO { get; set; }
        }
        public class AuditEmpPlanoRptCritica
        {
            public string CARTEIRA { get; set; }
            public string NOME { get; set; }
            public string CRITICA { get; set; }

            public AuditEmpPlanoRptCritica(string p_carteira, string p_nome, string p_critica)
            {
                this.CARTEIRA = p_carteira;
                this.NOME = p_nome;
                this.CRITICA = p_critica;
            }
        }

    }
}
