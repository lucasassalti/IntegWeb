using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using IntegWeb.Framework;
using IntegWeb.Entidades.Saude.Processos;

namespace IntegWeb.Saude.Aplicacao.BLL.Processos
{
    public class PrestadorBLL
    {
        public List<Prestador> Prestadores { get; set; }
        public void LoadData(DataTable dtPrestadores)
        {
            Prestadores = new List<Prestador>();            
            foreach (DataRow drPrestador in dtPrestadores.Rows)
            {
                Prestador newPrestador = DataRow2Pretador(drPrestador);
                Prestadores.Add(newPrestador);
            }
        }

        public Prestador DataRow2Pretador(DataRow drPrestador)
        {
            Prestador newPrestador = new Prestador();
            string sCOD_CONVENENTE = Util.DataRow2String(drPrestador, "COD_CONVENENTE");
            if (String.IsNullOrEmpty(sCOD_CONVENENTE))
            {
                sCOD_CONVENENTE = Util.DataRow2String(drPrestador, "CONTRATO");
            }
            newPrestador.COD_CONVENENTE = int.Parse(sCOD_CONVENENTE);
            newPrestador.CNPJ = Util.DataRow2String(drPrestador, "NUM_CGC_CPF").Replace("/", "").Replace("-", "").Replace(".", "");
            newPrestador.CNES = Util.DataRow2String(drPrestador, "NUM_CNES");
            newPrestador.Razao_Social = Util.DataRow2String(drPrestador, "NOM_RAZAO_SOCIAL");
            newPrestador.Municipio = Util.DataRow2String(drPrestador, "DCR_MUNICI");
            newPrestador.UF = Util.DataRow2String(drPrestador, "COD_ESTADO");
            newPrestador.PJ = !String.IsNullOrEmpty(Util.DataRow2String(drPrestador, "NOM_RAZAO_SOCIAL"));

            newPrestador.RELACAO_OPERADORA = Util.DataRow2String(drPrestador, "RELACAO_OPERADORA");
            newPrestador.TIPO_CONTRATUALIZACAO = Util.DataRow2String(drPrestador, "TIPO_CONTRATUALIZACAO");
            newPrestador.REGISTRO_ANS_OPERADORA_INTERMEDIARIA = Util.DataRow2String(drPrestador, "REGISTRO_ANS_OPERADORA_INTERMEDIARIA");
            newPrestador.DATA_CONTRATUALIZACAO = Util.DataRow2String(drPrestador, "DATA_CONTRATUALIZACAO");

            string IDENTIFICACAO_CNPJ = Util.DataRow2String(drPrestador, "CNPJ_ANS");
            if (!String.IsNullOrEmpty(IDENTIFICACAO_CNPJ))
            {
                newPrestador.CHAVE_ALTERACAO = new Prestador();
                newPrestador.CHAVE_ALTERACAO.CNPJ = Util.DataRow2String(drPrestador, "CNPJ_ANS").Replace("/", "").Replace("-", "").Replace(".", "");
                newPrestador.CHAVE_ALTERACAO.CNES = Util.DataRow2String(drPrestador, "CNES_ANS");
                newPrestador.CHAVE_ALTERACAO.Municipio = Util.DataRow2String(drPrestador, "CODIBGE_ANS");
                newPrestador.CHAVE_ALTERACAO.PJ = !String.IsNullOrEmpty(Util.DataRow2String(drPrestador, "NOM_RAZAO_SOCIAL_ANS"));
            }
            newPrestador.REGISTRO_PLANO = Util.DataRow2String(drPrestador, "NUMERO_REGISTRO_PLANO_VINCULACAO");
            newPrestador.CODIGO_PLANO_OPERADORA = Util.DataRow2String(drPrestador, "CODIGO_PLANO_OPERADORA_VINCULACAO");
            return newPrestador;
        }
    }
}
