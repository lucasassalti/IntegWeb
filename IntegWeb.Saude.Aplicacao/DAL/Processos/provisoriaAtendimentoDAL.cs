using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using System.Data.Objects;
using System.Data;
using System.Data.OracleClient;


namespace IntegWeb.Saude.Aplicacao.DAL.Processos
{
    public class provisoriaAtendimentoDAL
    {
        private SAUDE_EntityConn m_DbContext = new SAUDE_EntityConn();

        public class SAU_TBL_Provisoria_Atendimento_view
        {
            public string NOM_PARTICIP { get; set; }
            public string NUM_SUB_MATRIC { get; set; }
            public string DES_PLANO { get; set; }
            public DateTime? DAT_CANCELAMENTO { get; set; }
            public DateTime? DAT_VALIDADECI { get; set; }
            public DateTime? DAT_PARTO { get; set; }
            public DateTime DAT_NASCM_EMPRG { get; set; }
            public DateTime DAT_ADESAO { get; set; }
            public int COD_MOD_PLANO { get; set; }
            public int COD_PROGRAMA { get; set; }
            public string NOM_MAE { get; set; }
            public string NUM_CPF { get; set; }
            public int NUM_SEQ_PARTICIP { get; set; }
            public int COD_PLANO { get; set; }
            public DateTime? DAT_CAREN_APTO { get; set; }
            public int? NUM_SEQ_CONTRATO { get; set; }
            public string DES_ABREVIADA { get; set; }
            public string DCR_ESTCV { get; set; }
            public string COD_SEXO { get; set; }
            public string DS_GRAU { get; set; }
        }

        public List<SAU_TBL_Provisoria_Atendimento_view> GetDataPgto(int startRowIndex, int maximumRows, int plano, int empresa, int rgtEmpregado, int? representante, int? dependente, string sortParameter)
        {
            return GetWhere(plano, empresa, rgtEmpregado, representante, dependente);
        }

        public List<SAU_TBL_Provisoria_Atendimento_view> GetWhere(int plano, int empresa, int rgtEmpregado, int? representante, int? dependente)
        {
            ConexaoOracle objConexao = new ConexaoOracle();
            SAU_TBL_Provisoria_Atendimento_view obj;
            List<SAU_TBL_Provisoria_Atendimento_view> lista = new List<SAU_TBL_Provisoria_Atendimento_view>();

            try
            {
                objConexao.AdicionarParametro("V_NUM_PARAMETRO", plano);
                objConexao.AdicionarParametro("V_NUM_EMPR", empresa);
                objConexao.AdicionarParametro("V_NUM_REG", rgtEmpregado);
                objConexao.AdicionarParametro("V_NUM_REPR", representante);
                objConexao.AdicionarParametro("V_NUM_DEP", dependente);
                objConexao.AdicionarParametroCursor("srcreturn");
                
                using (OracleDataReader leitor = objConexao.ObterLeitor("own_funcesp.SAU_PRC_SQL_S_BENEFICIARIOS"))
                {

                    while (leitor.Read())
                    {
                        obj = new SAU_TBL_Provisoria_Atendimento_view();

                        MapearProcedure(leitor, ref obj, plano);

                        lista.Add(obj);
                    }
                }

                return lista;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
                {
                objConexao.Dispose();
            }
        }

        private void MapearProcedure(OracleDataReader leitor, ref SAU_TBL_Provisoria_Atendimento_view obj, int plano)
        {
            switch (plano)
            {                  
                case 1111:

                    obj.NOM_PARTICIP = leitor["NOM_PARTICIP"].ToString();
                    obj.NUM_SUB_MATRIC = leitor["NUM_SUB_MATRIC"].ToString();
                    obj.DES_PLANO = leitor["DES_PLANO"].ToString();
                    obj.DAT_CANCELAMENTO = Util.String2Date(leitor["DAT_CANCELAMENTO"].ToString());
                    obj.DAT_VALIDADECI = Util.String2Date(leitor["DAT_VALIDADECI"].ToString());
                    obj.DAT_PARTO = Util.String2Date(leitor["DAT_PARTO"].ToString());
                    obj.DAT_NASCM_EMPRG = Convert.ToDateTime(leitor["DAT_NASCM_EMPRG"].ToString());
                    obj.DAT_ADESAO = Convert.ToDateTime(leitor["DAT_ADESAO"].ToString());
                    obj.COD_MOD_PLANO = int.Parse(leitor["COD_MOD_PLANO"].ToString());
                    obj.COD_PROGRAMA = int.Parse(leitor["COD_PROGRAMA"].ToString());
                    obj.NOM_MAE = leitor["NOM_MAE"].ToString();
                    obj.NUM_CPF = leitor["NUM_CPF"].ToString();
                    obj.NUM_SEQ_PARTICIP = int.Parse(leitor["NUM_SEQ_PARTICIP"].ToString());
                    obj.COD_PLANO = int.Parse(leitor["COD_PLANO"].ToString());                   
                    break;

                case 1112:

                    obj.NOM_PARTICIP = leitor["NOM_PARTICIP"].ToString();
                    obj.NUM_SUB_MATRIC = leitor["NUM_SUB_MATRIC"].ToString();
                    obj.DES_PLANO = leitor["DES_PLANO"].ToString();
                    obj.DAT_CANCELAMENTO = Util.String2Date(leitor["DAT_CANCELAMENTO"].ToString());
                    obj.DAT_VALIDADECI = Util.String2Date(leitor["DAT_VALIDADECI"].ToString());
                    obj.DAT_NASCM_EMPRG = Convert.ToDateTime(leitor["DAT_NASCM_EMPRG"].ToString());
                    obj.DAT_ADESAO = Convert.ToDateTime(leitor["DAT_ADESAO"].ToString());
                    obj.DAT_PARTO = Util.String2Date(leitor["DAT_PARTO"].ToString());
                    obj.DAT_CAREN_APTO = Util.String2Date(leitor["DAT_CAREN_APTO"].ToString());
                    obj.NOM_MAE = leitor["NOM_MAE"].ToString();
                    obj.NUM_CPF = leitor["NUM_CPF"].ToString();
                    obj.COD_PLANO = int.Parse(leitor["COD_PLANO"].ToString());
                    obj.NUM_SEQ_PARTICIP = int.Parse(leitor["NUM_SEQ_PARTICIP"].ToString());
                    obj.NUM_SEQ_CONTRATO = int.Parse(leitor["NUM_SEQ_CONTRATO"].ToString());
                    break;

                case 1113:

                    obj.NOM_PARTICIP = leitor["NOM_PARTICIP"].ToString();
                    obj.NUM_SUB_MATRIC = leitor["NUM_SUB_MATRIC"].ToString();
                    obj.DES_PLANO = leitor["DES_PLANO"].ToString();
                    obj.DES_ABREVIADA = leitor["DES_ABREVIADA"].ToString();
                    obj.DAT_CANCELAMENTO = Util.String2Date(leitor["DAT_CANCELAMENTO"].ToString());
                    obj.DAT_VALIDADECI = Util.String2Date(leitor["DAT_VALIDADECI"].ToString());
                    obj.DAT_NASCM_EMPRG = Convert.ToDateTime(leitor["DAT_NASCM_EMPRG"].ToString());
                    obj.DCR_ESTCV = leitor["DCR_ESTCV"].ToString();
                    obj.COD_SEXO = leitor["COD_SEXO"].ToString();
                    obj.DAT_ADESAO = Convert.ToDateTime(leitor["DAT_ADESAO"].ToString());
                    obj.DS_GRAU = leitor["DS_GRAU"].ToString();
                    obj.DAT_PARTO = Util.String2Date(leitor["DAT_PARTO"].ToString());
                    obj.NOM_MAE = leitor["NOM_MAE"].ToString();
                    obj.NUM_CPF = leitor["NUM_CPF"].ToString();
                    obj.NUM_SEQ_PARTICIP = int.Parse(leitor["NUM_SEQ_PARTICIP"].ToString());
                    obj.NUM_SEQ_CONTRATO = int.Parse(leitor["NUM_SEQ_CONTRATO"].ToString());
                    obj.COD_MOD_PLANO = int.Parse(leitor["COD_MOD_PLANO"].ToString());
                    obj.COD_PROGRAMA = int.Parse(leitor["COD_PROGRAMA"].ToString());
                    break;

                case 1114:

                    obj.NOM_PARTICIP = leitor["NOM_PARTICIP"].ToString();
                    obj.NUM_SUB_MATRIC = leitor["NUM_SUB_MATRIC"].ToString();
                    obj.DES_PLANO = leitor["DES_PLANO"].ToString();
                    obj.DAT_CANCELAMENTO = Util.String2Date(leitor["DAT_CANCELAMENTO"].ToString());
                    obj.DAT_VALIDADECI = Util.String2Date(leitor["DAT_VALIDADECI"].ToString());
                    obj.DAT_PARTO = Util.String2Date(leitor["DAT_PARTO"].ToString());
                    obj.DAT_NASCM_EMPRG = Convert.ToDateTime(leitor["DAT_NASCM_EMPRG"].ToString());
                    obj.DAT_ADESAO = Convert.ToDateTime(leitor["DAT_ADESAO"].ToString());
                    obj.NOM_MAE = leitor["NOM_MAE"].ToString();
                    obj.NUM_CPF = leitor["NUM_CPF"].ToString();
                    break;
            }
        }

        public SAU_TBL_Provisoria_Atendimento_view GetDadosIniciais(int cod_emprs, int num_rgto_emprg, int? num_idntf_rptant)
        {
            IEnumerable<SAU_TBL_Provisoria_Atendimento_view> IEnum = m_DbContext.Database.SqlQuery<SAU_TBL_Provisoria_Atendimento_view>("select * from OWN_FUNCESP.VI_CONSULTA_ASS_MED_TITULAR_P WHERE NUM_RGTRO_EMPRG=" + num_rgto_emprg + " AND COD_EMPRS=" + cod_emprs, 0);
            SAU_TBL_Provisoria_Atendimento_view query = IEnum.FirstOrDefault();

            return query;
        }



        //public int GetDataCount(int cod_emprs, int num_rgto_emprg, int? num_idntf_rptant)
        //{
        //    return GetWhere(cod_emprs, num_rgto_emprg, num_idntf_rptant).SelectCount();
        //}

    }
}
