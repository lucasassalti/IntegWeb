using IntegWeb.Framework; 
using IntegWeb.Entidades.Previdencia;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using IntegWeb.Entidades;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{
    public class RecadastramentoBLL : RecadastramentoDAL
    {

        public Entidades.Resultado RetornoRecadastro(DataTable dt, string pLOG_INCLUSAO, DateTime pDTH_INCLUSAO)
        {
            Entidades.Resultado res = new Entidades.Resultado();
            
            String linha_0 = dt.Rows[0][0].ToString();
            int num_contrato = int.Parse(linha_0.Substring(1,5));
            DateTime dat_arquivo = new DateTime(int.Parse(linha_0.Substring(10,4)), int.Parse(linha_0.Substring(8,2)), int.Parse(linha_0.Substring(6,2)));

            List<PRE_TBL_RECADASTRAMENTO> lstRecadastrados = new List<PRE_TBL_RECADASTRAMENTO>();
            
            if (linha_0.Substring(0, 1) == "0")            
            {                
                for(int lin = 1; lin < dt.Rows.Count-1; lin++)
                {
                    PRE_TBL_RECADASTRAMENTO recad = new PRE_TBL_RECADASTRAMENTO();
                    String linha = dt.Rows[lin][0].ToString();
                    recad.NUM_CONTRATO = short.Parse(linha.Substring(1, 5));
                    recad.COD_EMPRS = short.Parse(linha.Substring(6, 3));
                    recad.NUM_RGTRO_EMPRG = int.Parse(linha.Substring(9, 10));
                    recad.NUM_IDNTF_RPTANT = int.Parse(linha.Substring(19, 8));

                    int ctrlRecad = 964;
                    if (linha.Substring(960, 2) == "SN" || linha.Substring(960, 2) == "NN")
                    {
                        ctrlRecad = 960;
                    }

                    if (linha.Substring(ctrlRecad, 2) == "SN")
                    {
                        recad.DAT_RECADASTRAMENTO = new DateTime(int.Parse(linha.Substring(ctrlRecad + 6, 4)), int.Parse(linha.Substring(ctrlRecad + 4, 2)), int.Parse(linha.Substring(ctrlRecad+2, 2)));
                        recad.LOG_INCLUSAO = pLOG_INCLUSAO;
                        recad.DTH_INCLUSAO = pDTH_INCLUSAO;
                        lstRecadastrados.Add(recad);
                    }
                }
            }

            int reg_count = 0;
            if (lstRecadastrados.Count > 0)
            {
                foreach (PRE_TBL_RECADASTRAMENTO recad in lstRecadastrados)
                {
                    base.UpdateDtRecadastramento(recad);
                    if (recad.TIP_RECADASTRAMENTO == 1)
                    {
                        reg_count++;
                    }
                }
            }
            else
            {
                res.Erro("Não foram encontrados registros com data de recadastramento");
            }

            if (reg_count > 0)
            {
                res.Sucesso(reg_count + " registros atualizados com sucesso!");
        }
            else
            {
                res.Sucesso("Arquivo processado com sucesso, no entanto nenhum novo registro de recadastramento foi encontrado.");
            }

            return res;
        }


        public Resultado Validar(PRE_TBL_RECADASTRAMENTO Recad)
        {
            Resultado retorno = new Resultado(true);

            if (Recad.COD_EMPRS == 0)
            {
                retorno.Erro("O campo Empresa é obrigatório");
                return retorno;
            }

            if (Recad.NUM_RGTRO_EMPRG == 0)
            {
                retorno.Erro("O campo Matrícula é obrigatório");
                return retorno;
            }

            //if (Recad.NUM_MATR_PARTF == 0)
            //{
            //    retorno.Erro("O campo Matrícula é obrigatório");
            //    return retorno;
            //}

            if (Recad.NOME == null || Recad.NOME == "")
            {
                retorno.Erro("O campo Nome é obrigatório");
                return retorno;
            }

            if (Recad.DAT_RECADASTRAMENTO == null && Recad.TIP_RECADASTRAMENTO != null)
            {
                retorno.Erro("O campo Dt. Recadastro é obrigatório");
                return retorno;
            }

            if (Recad.TIP_RECADASTRAMENTO == null && Recad.DAT_RECADASTRAMENTO != null)
            {
                retorno.Erro("O campo Tipo Atendimento é obrigatório");
                return retorno;
            }

            return retorno;

        }

    }
}
