using IntegWeb.Framework;
using IntegWeb.Entidades.Framework;
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

    public class ArqPatrocinaNotaDebitoBLL : ArqPatrocinaNotaDebitoDAL
    {

        public Resultado CarregarNotaDebito(short sANO_REF, short sMES_REF, string pGRUPO_PORTAL)
        {

            Resultado res = new Resultado();

            PRE_TBL_GRUPO_EMPRS ge = base.GetCodigoGrupoEmprs(null, pGRUPO_PORTAL);
            PRE_TBL_ARQ_NOTA_DEBITO ND = base.GetNotaDebito(sANO_REF, sMES_REF, ge.COD_GRUPO_EMPRS);
           
            if (ND != null)
            {
                res.Sucesso("Nota de Débito carregada com sucesso", ND.COD_NOTA_DEBITO);
            }
            else
            {
                res.Erro("Não foi encontrada Nota de Débito para este Ano/Mes ref.");
            }

            return res;

        }

        public Resultado ProcessarNotaDebito(short sANO_REF, short sMES_REF, string pGRUPO_PORTAL, bool Reprocessar = false)
        {

            Resultado res = new Resultado();
            PRE_TBL_GRUPO_EMPRS ge = base.GetCodigoGrupoEmprs(null, pGRUPO_PORTAL);
            PRE_TBL_ARQ_NOTA_DEBITO ND = base.GetNotaDebito(sANO_REF, sMES_REF, ge.COD_GRUPO_EMPRS);            

            if (Reprocessar && ND!=null)
            {
                base.DeleteData(ND.COD_NOTA_DEBITO, ge.COD_GRUPO_EMPRS);
            }

            if (ND == null || Reprocessar)
            {
                decimal VLR_NOTA_DEBITO = 0;
                decimal VLR_DEDUZ_SALDO_FUNDO = 0;
                List<PRE_VW_ARQ_PAT_DEMONSTRATIVO> linhas = base.GetLancamentosDemonstrativo(sANO_REF, sMES_REF, pGRUPO_PORTAL).ToList();
                if (linhas.Count() > 0)
                {
                    foreach (PRE_VW_ARQ_PAT_DEMONSTRATIVO lanc in linhas)
                    {
                        VLR_NOTA_DEBITO += Math.Round(lanc.VLR_LANCAMENTO ?? 0,2);
                        if (lanc.IND_DEDUZ_SALDO_FUNDO == 1)
                        {
                            VLR_DEDUZ_SALDO_FUNDO += lanc.VLR_LANCAMENTO ?? 0;
                        }                            
                    }
                }

                PRE_TBL_ARQ_SALDO_FUNDO SF = base.GetSaldoFundo(null, ge.COD_GRUPO_EMPRS);

                if (SF != null)
                {
                    if (SF.VLR_SALDO_FUNDO > VLR_DEDUZ_SALDO_FUNDO)
                    {
                        VLR_NOTA_DEBITO -= VLR_DEDUZ_SALDO_FUNDO;
                        SF.VLR_SALDO_FUNDO -= VLR_DEDUZ_SALDO_FUNDO;
                        res = base.SaveData(SF);
                    }
                }

                ND = new PRE_TBL_ARQ_NOTA_DEBITO();

                ND.COD_NOTA_DEBITO = 0;
                ND.COD_GRUPO_EMPRS = ge.COD_GRUPO_EMPRS;
                ND.ANO_REF = sANO_REF;
                ND.MES_REF = sMES_REF;
                ND.DCR_NOTA_DEBITO = "APORTE RESERVA MATEMÁTICA - PA";
                ND.DTH_VENCIMENTO = DateTime.Now;
                ND.VLR_NOTA_DEBITO = VLR_NOTA_DEBITO;

                res = base.SaveData(ND);

            }
            else
            {
                res.Sucesso("Nota de Débito carregada", ND.COD_NOTA_DEBITO);
            }

            return res;

        }
    }
}
