using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Objects; 
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MySql.Data.MySqlClient;
using IntegWeb.Entidades;
using IntegWeb.Framework;
using IntegWeb.Intranet.Aplicacao.ENTITY;

namespace Intranet.Aplicacao.DAL
{
    public class ConsultaExtratoCRMDAL 
    {
        public List<Intranet.Aplicacao.BLL.DocumentoPDF> executar_comando_Select(String SQL, String Servidor)
        {
            // Criar conexao para executar comando SQL
            MySqlConnection conn = new MySqlConnection("Persist Security Info=False;server=10.190.35.143;database=fcesp;uid=sys_crmged;pwd='teste092014';Connection Timeout=120");
            if (Servidor == "10.190.35.57")
            {
                conn = new MySqlConnection("Persist Security Info=False;server=10.190.35.57;database=fcesp;uid=sys_crmged;pwd='V37qcw11';Connection Timeout=120");
            }
            
            MySqlCommand cmd = new MySqlCommand(SQL, conn);
            conn.Open();


            // Executar o comando SQL criado a partir dos filtros anteriores
            MySqlDataReader dr = cmd.ExecuteReader();

            // Criar variaveis de controle            
            int contador = 0;

            // Obter o resultado do comando SQL e adicionar ao array
            List<Intranet.Aplicacao.BLL.DocumentoPDF> documentos = new List<Intranet.Aplicacao.BLL.DocumentoPDF>();

            while (dr.Read())
            {
                documentos.Add(new Intranet.Aplicacao.BLL.DocumentoPDF());

                documentos[contador].TipoDocumento = dr.GetString(0);
                documentos[contador].Empresa = dr.GetString(1);
                documentos[contador].Matricula = dr.IsDBNull(2) ? null : dr.GetString(2);
                documentos[contador].Digito = dr.IsDBNull(3) ? null : dr.GetString(3);
                documentos[contador].Nome = dr.IsDBNull(4) ? null : dr.GetString(4);
                documentos[contador].Ano = dr.GetString(5);
                documentos[contador].Mes = dr.IsDBNull(6) ? null : dr.GetString(6);
                documentos[contador].Plano = dr.IsDBNull(7) ? null : dr.GetString(7);
                documentos[contador].DocumentoArquivo = dr.GetString(8);
                contador++;

            }

            dr.Close();
            conn.Close();

            return documentos;
        }

        public String Gerar_Comando_Select(ref Intranet.Aplicacao.BLL.Filtro_Pesquisa filtro, List<String> Lista_conversao_nomes)
        {
            String SQL = "";
            //String Prefixo_Storage = Consulta_GED_CRM.Properties.Settings.Default.Prefixo_Storage;
            String Prefixo_Storage = "/storage01/fcesp/";
            if (filtro.TipoDocumento == 1)
            {
                filtro.IdIdocs = "tp_pagsupl";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'PAGSUPL - Aviso Pagamento - Suplementado' as Tipo, codigodaempresa, numerodamatricula, digito, nomedoparticipante, anodeemissaododocumento, mesdeemissao, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_pagsupl   ";
                SQL = SQL + " where (anodeemissaododocumento * 100 + mesdeemissao) between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and codigodaempresa = '" + filtro.CodigoEmpresa + "' and  numerodamatricula = '" + filtro.Registro;
                SQL = SQL + "' and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";

                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }
            if (filtro.TipoDocumento == 2)
            {
                filtro.IdIdocs = "tp_contrib";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'CONTRIB - Extrato Previdenciário'  as Tipo, codigodaempresa, numerodamatricula, digito, nomedoparticipante, anodeemissao, mesdeemissao, nomedoplano, idocs_path";
                SQL = SQL + " from tp_contrib     ";
                SQL = SQL + " where (anodeemissao * 100 + mesdeemissao) between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and codigodaempresa = '" + filtro.CodigoEmpresa + "' and  numerodamatricula = '" + filtro.Registro;
                SQL = SQL + "' and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            if (filtro.TipoDocumento == 3)
            {

                filtro.IdIdocs = "tp_irrfapo";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'CIRRFAPO - Informe Rendimento Assistidos' as Tipo, numerodocpf as empresa, '' as Matricula, '' as digito, nomedoparticipante as nome, anodebase, '' as mes, '' as Plano, idocs_path";
                SQL = SQL + " from tp_irrfapo ";
                SQL = SQL + " where anodebase between " + filtro.PesquisaAnoInicio + " and " + filtro.PesquisaAnoFim + " ";
                SQL = SQL + " and trim(numerodocpf) = trim(lpad('" + filtro.CPF + "',11,'0'))";

                //Colocar ordem
                SQL = SQL + " order by 4";

            }
            if (filtro.TipoDocumento == 4)
            {
                filtro.IdIdocs = "tp_cobvinc";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'COBVINC - Extrato Cobrança - Autopatrocinados' as Tipo, substr( identificacao, 1, 3) empresa, substr( identificacao, 4, 8 ) matricula, null, nomedoparticipante, anodacobranca, mesdacobranca, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_cobvinc     ";
                SQL = SQL + " where anodacobranca * 100 + mesdacobranca  between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and substr( identificacao, 1, 10 )   = " + filtro.CodigoEmpresa + filtro.Registro.Substring(filtro.Registro.Length - 7);

                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                //SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }
            if (filtro.TipoDocumento == 5)
            {
                filtro.IdIdocs = "tp_cobrsep";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'COBRSEP - Extrato Cobrança - Seguros e Pecúlio' as Tipo, substr( identificacao, 1, 3) empresa, substr( identificacao, 4, 8 ) matricula, null, nomedoparticipante, anodacobranca, mesdacobranca, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_cobrsep";
                SQL = SQL + " where anodacobranca * 100 + mesdacobranca  between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and substr( identificacao, 1, 10 )   = " + filtro.CodigoEmpresa + filtro.Registro.Substring(filtro.Registro.Length - 7);

                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                //SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            if (filtro.TipoDocumento == 6)
            {
                filtro.IdIdocs = "tp_revisao";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'REVISAO - Carta Revisão INSS' as Tipo, codigodaempresa, numerodamatricula, digito, nomedoparticipante, anodeemissao, mesdeemissao, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_revisao    ";
                SQL = SQL + " where (anodeemissao * 100 + mesdeemissao) between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and codigodaempresa = '" + filtro.CodigoEmpresa + "' and  numerodamatricula = '" + filtro.Registro;
                SQL = SQL + "' and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            //if (TipoDocumento == 7)
            //    falta 	7	Não Utiliza	
            //    SQL = SQL + " select 'PAGCOMPC - Aviso Pagamento - Complementados' as Tipo,  codigodaempresa,  numerodamatricula,  digito,  nomedoparticipante,    anodeemissaododocumento,  mesdeemissao, '' as NomePlano, CONCAT(  '" + Prefixo_Storage + "',  'tp_pagcompc/'  , idocs_path )  as idocs_path   from tp_pagcompc  " + where + " union all  ";
            //    SQL = SQL + " select 'Pag antigos'                                 as Tipo,  codigodaempresa,  numerodamatricula,  digito,  nomedoparticipante,    anodeemissaododocumento,  mesdeemissao, '' as NomePlano, CONCAT(  '" + Prefixo_Storage + "',  'tp_pagantigos/', idocs_path )  as idocs_path   from tp_pagantigos " + where + "  ";

            if (filtro.TipoDocumento == 8)
            {
                filtro.IdIdocs = "tp_cobsaud";
                // Consultar a tabela do MySQL com a lista de arquivos
                //SQL = SQL + " select 'COBSAUD - Extrato Cobrança - Saúde' as Tipo, substr( identificacao, 1, 3) empresa, substr( identificacao, 4, 8 ) matricula, null, nomedoparticipante, anodacobranca, mesdacobranca,  '' as NomePlano, CONCAT('" + Prefixo_Storage + "', 'tp_cobrsep/', idocs_path) as idocs_path";
                SQL = SQL + " select 'COBSAUD - Extrato Cobrança - Saúde' as Tipo, substr( identificacao, 1, 3) empresa, substr( identificacao, 4, 8 ) matricula, null, nomedoparticipante, anodacobranca, mesdacobranca, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_cobsaud";
                SQL = SQL + " where anodacobranca * 100 + mesdacobranca  between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                //SQL = SQL + " and substr( identificacao, 1, 10 ) = " + filtro.CodigoEmpresa + filtro.Registro.Substring(3, 7);
                SQL = SQL + " and identificacao like '" + filtro.CodigoEmpresa + filtro.Registro.Substring(3, 7) + "%' ";
                SQL = SQL + " and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7";
            }

            if (filtro.TipoDocumento == 9)
            {
                filtro.IdIdocs = "tp_utilamh";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'UTILAMH - Extrato Mensal Utilização - AMH' as Tipo, codigodaempresa, numerodamatricula, digito, nomedoparticipante, anodeemissao, mesdeemissao, '' as NomePlano, idocs_path, idocs_arquivo";
                SQL = SQL + " from tp_utilamh    ";
                SQL = SQL + " where (anodeemissao * 100 + mesdeemissao) between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and codigodaempresa = '" + filtro.CodigoEmpresa + "' and  numerodamatricula = '" + filtro.Registro;
                SQL = SQL + "' and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            if (filtro.TipoDocumento == 10)
            {
                filtro.IdIdocs = "tp_utilpes";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'UTILPES - Extrato Mensal Utilização - PES' as Tipo, codigodaempresa, numerodamatricula, digito, nomedoparticipante, anodeemissao, mesdeemissao, '' as NomePlano, idocs_path";
                //SQL = SQL + " select 'UTILPES - Extrato Mensal Utilização - PES'   as Tipo,  codigodaempresa,  numerodamatricula,  digito,  nomedoparticipante,    anodeemissao,             mesdeemissao, '' as NomePlano, CONCAT(  '" + Prefixo_Storage + "',  'tp_utilpes/'   , idocs_path )  as idocs_path ";
                SQL = SQL + " from tp_utilpes      ";
                SQL = SQL + " where (anodeemissao * 100 + mesdeemissao) between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and codigodaempresa = '" + filtro.CodigoEmpresa + "' and  numerodamatricula = '" + filtro.Registro;
                SQL = SQL + "' and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            if (filtro.TipoDocumento == 11)
            {
                filtro.IdIdocs = "tp_credree";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'CREDREE - Crédito Reembolso' as Tipo, codigodaempresa, numerodamatricula, digito, nomedoparticipante, anodeemissao, mesdeemissao, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_credree";
                SQL = SQL + " where (anodeemissao * 100 + mesdeemissao) between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and codigodaempresa = '" + filtro.CodigoEmpresa + "' and  numerodamatricula = '" + filtro.Registro;
                SQL = SQL + "' and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            if (filtro.TipoDocumento == 12)
            {
               

                filtro.IdIdocs = "tp_utilanu";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'UTILANU - Extrato Anual Utilização' as Tipo, codigodaempresa, numerodamatricula, digito, nomedoparticipante, anobase, '' as mes, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_utilanu";
                SQL = SQL + " where (anobase) between '" + filtro.PesquisaAnoInicio + "' and '" + filtro.PesquisaAnoFim + "' ";
                SQL = SQL + " and codigodaempresa = '" + filtro.CodigoEmpresa + "' and  numerodamatricula = '" + filtro.Registro;
                SQL = SQL + "' and ( nomedoparticipante = '" + retornaString40Caracteres(filtro.ParticipanteNome.Replace("'", " ")) + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            if (filtro.TipoDocumento == 13)
            //falta 	CP17	
            {
                filtro.IdIdocs = "tp_cejadmn";
                // Consultar a tabela do MySQL com a lista de arquivos
                //SQL = SQL + " select 'CEJADMN - Carta de Cobrança ExtraJudicial Adm' as Tipo, identificacao, null, null, nomedoparticipante, anodacobranca, mesdacobranca, idocs_path";
                SQL = SQL + " select 'CEJADMN - Carta de Cobrança ExtraJudicial Adm' as Tipo, substr( identificacao, 1, 3) empresa, substr( identificacao, 4, 8 ) matricula, null, nomedoparticipante, anodacobranca, mesdacobranca,  '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_cejadmn";
                SQL = SQL + " where anodacobranca * 100 + mesdacobranca  between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and substr( identificacao, 1, 10 )   = " + filtro.CodigoEmpresa + filtro.Registro;
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";

            }

            if (filtro.TipoDocumento == 14)
            {
                filtro.IdIdocs = "tp_irrfcre";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'IRRFCRE - Informe Rendimento Credenciados' as Tipo, numerodocnpjcpf, null, null, null, anobase, '' as mes, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_irrfcre";
                SQL = SQL + " where (anobase) between '" + filtro.PesquisaAnoInicio + "' and '" + filtro.PesquisaAnoFim + "' ";
                SQL = SQL + " and trim(lpad(numerodocnpjcpf,14,'0')) = lpad(trim('" + filtro.cnpj + "'),14,'0')";
                //SQL = SQL + " and nomedocredenciado = '" + filtro.nom_convenente + "' ";

                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }
            //

            if (filtro.TipoDocumento == 15)
            {
                filtro.IdIdocs = "tp_terquit";
                //2ª Termo de Quitação Serviços Prestados - 'TERQUIT - Termo Quitação Serviços Prestados'
                // tabela tp_terquit
                // baseado no Contrato
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'TERQUIT - Termo Quitação Serviços Prestados' as Tipo, numerodocontratodocredenciado, null, null, null, anodeemissao, mesdeemissao, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_terquit";
                SQL = SQL + " where (anodeemissao * 100 + mesdeemissao) between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and numerodocontratodocredenciado = '" + filtro.contrato + "' ";
                SQL = SQL + " order by 1, 6, 7 ";

            }

            if (filtro.TipoDocumento == 16)
            {
                filtro.IdIdocs = "tp_pagcompc";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'PAGCOMPC - Aviso Pagamento - Complementados' as Tipo, codigodaempresa, numerodamatricula, digito, nomedoparticipante, anodeemissaododocumento, mesdeemissao, '' as NomePlano, idocs_path  ";
                SQL = SQL + " from tp_pagcompc";
                SQL = SQL + " where (anodeemissaododocumento * 100 + mesdeemissao) between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and codigodaempresa = '" + filtro.CodigoEmpresa + "' and  numerodamatricula = '" + filtro.Registro;
                SQL = SQL + "' and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            if (filtro.TipoDocumento == 17)
            {
                filtro.IdIdocs = "tp_cejsaud";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'CEJSAUD - Carta Cobrança Extrajudicial - Saúde (PES)' as Tipo, substr( identificacao, 1, 3) empresa, substr( identificacao, 4, 8 ) matricula, null, nomedoparticipante, anodacobranca, mesdacobranca, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_cejsaud";
                SQL = SQL + " where anodacobranca * 100 + mesdacobranca  between " + filtro.PesquisaAnoInicio + filtro.PesquisaMesInicio.PadLeft(2, '0') + " and " + filtro.PesquisaAnoFim + filtro.PesquisaMesFim.PadLeft(2, '0') + " ";
                SQL = SQL + " and substr( identificacao, 1, 10 ) = " + filtro.CodigoEmpresa + filtro.Registro.Substring(3, 7);
                SQL = SQL + " and ( nomedoparticipante = '" + filtro.ParticipanteNome.Replace("'", " ") + "' ";
                // adicionar nome antigo (nome abreviado) do participante atraves da tabela DE/PARA
                if (Lista_conversao_nomes.Count != 0)
                {
                    foreach (String ParticipanteNome in Lista_conversao_nomes)
                    {
                        SQL = SQL + " or  nomedoparticipante = '" + ParticipanteNome.Replace("'", " ") + "' ";
                    }
                }
                SQL = SQL + " ) ";
                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7";
            }

            if (filtro.TipoDocumento == 18)
            {
                filtro.IdIdocs = "tp_credpcc";
                // Consultar a tabela do MySQL com a lista de arquivos
                SQL = SQL + " select 'CREDPCC - Extrato Credenciados - Outros Impostos' as Tipo, numerodocnpjcpf, null, null, null, anobase, '' as mes, '' as NomePlano, idocs_path";
                SQL = SQL + " from tp_credpcc";
                SQL = SQL + " where (anobase) between '" + filtro.PesquisaAnoInicio + "' and '" + filtro.PesquisaAnoFim + "' ";
                SQL = SQL + " and trim(lpad(numerodocnpjcpf,14,'0')) = lpad(trim('" + filtro.cnpj + "'),14,'0')";
                //SQL = SQL + " and nomedocredenciado = '" + filtro.nom_convenente + "' ";

                //Colocar ordem
                SQL = SQL + " order by 1, 6, 7 ";
            }

            //Retornar valor
            return SQL;
        }

        public string retornaString40Caracteres(string nomeParticipante) {

            //Valida a quantidade de caracters
            if (nomeParticipante.Length > 40)
            {
                return nomeParticipante.Substring(0, 40);
            }
            else {

                return nomeParticipante;
            }
        }

        public List<String> Obter_lista_conversao_nomes(Intranet.Aplicacao.BLL.Filtro_Pesquisa filtro)
        {
            // Variavel com o array DE/PARA 
            List<String> Lista_conversao_nomes = new List<String>();

            //Consultar tabela de Representante Oracle para fazer o DE/PARA de nome
            String strString = "";
            strString = strString + " select distinct A.COD_EMPRS, A.NUM_RGTRO_EMPRG, A.NUM_IDNTF_RPTANT, A.NOME_ATU, A.NOME_ANT, A.DATA_ATU ";
            strString = strString + "   from opportunity.TB_NOME_ALTERADO A ";
            strString = strString + "  where A.COD_EMPRS = '" + filtro.CodigoEmpresa + "' ";
            strString = strString + "    and A.NUM_RGTRO_EMPRG = '" + filtro.Registro + "' ";
            strString = strString + "    and A.NUM_IDNTF_RPTANT in ";
            strString = strString + "        (select A.NUM_IDNTF_RPTANT ";
            strString = strString + "           from opportunity.TB_NOME_ALTERADO A ";
            strString = strString + "          where A.COD_EMPRS = '" + filtro.CodigoEmpresa + "' ";
            strString = strString + "            and A.NUM_RGTRO_EMPRG = '" + filtro.Registro + "' ";
            //strString = strString + "            and A.NOME_ATU = '" + filtro.ParticipanteNome + "' ) ";
            strString = strString + "            and A.NOME_ATU = '" + filtro.ParticipanteNome.Replace("'", "") + "' ) ";

            //SqlDataSource sdsDinamico = new SqlDataSource("System.Data.OracleClient", Consulta_GED_CRM.Properties.Settings.Default.Conexao_Oracle, strString);
            //System.Data.DataView dv = (System.Data.DataView)sdsDinamico.Select(DataSourceSelectArguments.Empty);

            //Conexao objConexao = new Conexao();
            ConexaoOracle objConexao = new ConexaoOracle();
            DataTable dt = new DataTable();
            try
            {
                objConexao.ExecutarQueryAdapter(strString);
                OracleDataAdapter adpt = objConexao.ExecutarQueryAdapter(strString);
                adpt.Fill(dt);
                adpt.Dispose();

                foreach (DataRow datarow in dt.Rows)
                {
                    Lista_conversao_nomes.Add(datarow[0].ToString());
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Problemas contate o administrador do sistema: //n" + ex.Message);
            }
            finally
            {
                objConexao.Dispose();
            }


            return Lista_conversao_nomes;
        }
    }
}