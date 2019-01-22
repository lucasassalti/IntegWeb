using IntegWeb.Framework;
using IntegWeb.Entidades;
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
using System.Net.Mail;
using System.Net.Mime;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{

    public class ArqPatrocinadoraBLL : ArqPatrocinadoraDAL
    {

        public Resultado DePara(PRE_TBL_ARQ_PATROCINA arqPatrocinadora, 
                                            DataTable dtarqPatrocinadora_linhas, 
                                                short Ano_Ref,
                                                short Mes_Ref,
                                               string Grupo_Portal,
                                               string pLOG_INCLUSAO)
        {
            Resultado res = new Resultado();
            //List<PRE_TBL_ARQ_PATROCINADORA_LINHA> lsLinhas = new List<PRE_TBL_ARQ_PATROCINADORA_LINHA>();
            arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_LINHA = new List<PRE_TBL_ARQ_PATROCINA_LINHA>();
            DateTime dtNow = DateTime.Now;
            long line_count = 1;
            long file_hash = 0;
            string cod_emprs_arquivo = "000";            


            //base.DeleteData(NomeArquivo);
            try
            {
                PRE_TBL_ARQ_PATROCINA_TIPO patTIPO = ValidaTipoArquivo(dtarqPatrocinadora_linhas);

                if (patTIPO == null)
                {
                    throw new Exception("<strong>Layout do arquivo não identificado.</strong>");
                }
                arqPatrocinadora.TIP_ARQUIVO = patTIPO.COD_TIPO;
                arqPatrocinadora.LOG_INCLUSAO = Util.String2Limit(pLOG_INCLUSAO,0,30);
                arqPatrocinadora.DTH_INCLUSAO = dtNow;
                arqPatrocinadora.ANO_REF = Ano_Ref;
                arqPatrocinadora.MES_REF = Mes_Ref;
                arqPatrocinadora.GRUPO_PORTAL = Grupo_Portal;
                arqPatrocinadora.COD_STATUS = 1; //Novo
                foreach (DataRow row in dtarqPatrocinadora_linhas.Rows)
                {
                    String _data = row["DATA"].ToString();

                    if (String.IsNullOrEmpty(_data.Trim())) { 
                        line_count++; 
                        continue;
                    } // Ignorar linhas em branco

                    short tipo_linha = ValidaTipoLinha(line_count, dtarqPatrocinadora_linhas.Rows.Count, _data, arqPatrocinadora.TIP_ARQUIVO);
                    PRE_TBL_ARQ_PATROCINA_LINHA newLINHA =
                        new PRE_TBL_ARQ_PATROCINA_LINHA(arqPatrocinadora.TIP_ARQUIVO, tipo_linha, _data);
                    //newLINHA.COD_ARQ_PAT_LINHA = 0;
                    //newLINHA.COD_ARQ_PAT = 0;
                    //hash_file = Util.HashGenerico(_data);
                    file_hash += Convert.ToInt64(newLINHA.NUM_HASH_LINHA);
                    newLINHA.NUM_LINHA = line_count;
                    line_count++;
                    cod_emprs_arquivo = (tipo_linha == 2) ? newLINHA.COD_EMPRS : cod_emprs_arquivo;
                    arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_LINHA.Add(newLINHA);
                }

                // Setar COD_EMPRS das linha de cabeçalho e rodapé
                arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_LINHA
                        .Where(l => l.TIP_LINHA!=2)
                        .ToList()
                        .ForEach(a => { a.COD_EMPRS = cod_emprs_arquivo; });

                arqPatrocinadora.NUM_HASH = file_hash;

            }
            catch (Exception ex)
            {
                arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA = new List<PRE_TBL_ARQ_PATROCINA_CRITICA>();
                arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA { COD_CRITICA = 900,
                                                                                                       DCR_CRITICA = "Ocorreu um erro no processamento do arquivo " + arqPatrocinadora.NOM_ARQUIVO + " :\\n" + ex.Message});
            }

            //arqPatrocinadora.PRE_TBL_ARQ_PATROCINADORA_TIPO = new PRE_TBL_ARQ_PATROCINADORA_TIPO(max_length);
            //arqPatrocinadora.TIP_ARQUIVO = new PRE_TBL_ARQ_PATROCINADORA_TIPO(max_length).COD_TIP_ARQUIVO;

           //Criticar(arqPatrocinadora);

            if (arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_LINHA.Count > 0)
            {
                //base.Persistir(lsDebConta);
                res.Sucesso(arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_LINHA.Count + " registro(s) importado(s).");
            }
            else
            {
                res.Erro((arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.FirstOrDefault()!=null) ? arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.FirstOrDefault().DCR_CRITICA : "Nenhum registro localizado.");
            }

            return res;

        }

        private PRE_TBL_ARQ_PATROCINA_TIPO ValidaTipoArquivo(DataTable _dt)
        {
            int line_length = 0;            
            DataRowCollection _drc = _dt.Rows;
            int max_amostra = _drc.Count - (_drc.Count > 10 ? 10 : _drc.Count); // Dez ultimas linhas
            for (int i = _drc.Count - 1; i >= 0 && i >= max_amostra; i--)
            {
                String _data = _drc[i]["DATA"].ToString();
                line_length = (_data.Length > line_length) ? _data.Length : line_length;
            }
            return base.GetTipoByTam(line_length);
        }

        private short ValidaTipoLinha(long num_linha, long total_linha, string dados_linha, short? TIP_ARQUIVO = null)
        {
            short sCOD_EMPRS;
            if (TIP_ARQUIVO!=null && TIP_ARQUIVO == 3)
            {
                short.TryParse(dados_linha.PadRight(3,'0').Substring(72,3), out sCOD_EMPRS);
            } else {
                short.TryParse(dados_linha.PadRight(3,'0').Substring(0,3), out sCOD_EMPRS);
            }
            //short.TryParse(dados_linha.Substring(0, 3), out sCOD_EMPRS);

            if (num_linha == 1 && sCOD_EMPRS == 0)
            {
                return 1;
            }
            else if (num_linha == total_linha && sCOD_EMPRS == 0)
            {
                return 3;
            }

            return 2;
        }

        public new Resultado Persistir(PRE_TBL_ARQ_PATROCINA newArqPatrocina, PRE_TBL_ARQ_PATROCINA oldArqPatrocina, string grupo_portal, string pLOG_INCLUSAO)
        {
            Resultado res = new Entidades.Resultado();
            short sCOD_ACAO = 1;
            bool trava_sobrepor = false;
            if (oldArqPatrocina!=null)
            {
                //
                //  Trava para não sobrepor arquivos que foram carregados parcialmente:
                //
                int linhas_importadas = oldArqPatrocina.PRE_TBL_ARQ_PATROCINA_LINHA.Count(c => c.DAT_IMPORTADO != null);
                if (linhas_importadas > 0)
                {
                    trava_sobrepor = true;
                    oldArqPatrocina = null;
                    newArqPatrocina.NOM_ARQUIVO = Controle_Nomes(newArqPatrocina.NOM_ARQUIVO, grupo_portal);
                    sCOD_ACAO = 7; //Renomeado e enviado
                }
                else
                {
                    //Backup log anterior:
                    oldArqPatrocina.PRE_TBL_ARQ_PATROCINA_LOG.ToList().ForEach(log =>
                        newArqPatrocina.PRE_TBL_ARQ_PATROCINA_LOG.Add(log)
                    );
                    sCOD_ACAO = 2; //Substituição
                    newArqPatrocina.COD_STATUS = 2; //Substituição
                    newArqPatrocina.NOM_ARQUIVO = oldArqPatrocina.NOM_ARQUIVO;
                }
            }

            res = base.Delete(oldArqPatrocina);

            if (res.Ok)
            {
                res = base.Persistir(newArqPatrocina);
            }

            if (res.Ok)
            {
                Registra_LOG(sCOD_ACAO, newArqPatrocina.COD_ARQ_PAT, pLOG_INCLUSAO);
            }

            if (trava_sobrepor)
            {
                res.Alert("Arquivo renomeado para <span style='font-weight: bold;'>" + newArqPatrocina.NOM_ARQUIVO + "</span> e enviado com sucesso.\\n" +
                          "<span style='font-size: smaller;'>(ALERTA: O arquivo anterior não pode ser sobreposto por conter registros já carregados parcialmente)</span>");
            }

            return res;
        }

        private string Controle_Nomes(string nome_arq, string grupo_portal)
        {
            int num_novo_nome_arq = 1;
            string novo_nome_arq = nome_arq;
            //ArqPatrocinadoraBLL apBLL = new ArqPatrocinadoraBLL();
            PRE_TBL_ARQ_PATROCINA mesmo_nome = base.GetDataByNome(novo_nome_arq, grupo_portal);
            while (mesmo_nome != null)
            {
                novo_nome_arq = nome_arq + "(" + num_novo_nome_arq + ")";
                mesmo_nome = base.GetDataByNome(novo_nome_arq, grupo_portal);
                num_novo_nome_arq++;
            };

            return novo_nome_arq;
        }

        public void Registra_LOG(short pCOD_ACAO, int pCOD_ARQ_PAT, string pLOG_INCLUSAO)
        {
            string sDCR_ACAO = String.Empty;
            switch (pCOD_ACAO)
            {
                case 1:
                default:
                    sDCR_ACAO = "Envio de arquivo";
                    break;
                case 2:
                    sDCR_ACAO = "Substituição de arquivo";
                    break;
                case 3:
                    sDCR_ACAO = "Validação solicitada";
                    break;
                case 5:
                    sDCR_ACAO = "Validação concluída";
                    break;
                case 6:
                    sDCR_ACAO = "Carregamento solicitado";
                    break;
                case 8:
                    sDCR_ACAO = "Arquivo carregado parcialmente";
                    break;
                case 9:
                    sDCR_ACAO = "Arquivo 100% carregado";
                    break;
                case 10:
                    sDCR_ACAO = "Acerto Efetuado";
                    break;
                case 11:
                    sDCR_ACAO = "Demonstrativo Gerado";
                    break;
                case 12:
                    sDCR_ACAO = "Demonstrativo Impresso";
                    break;
                case 13:
                    sDCR_ACAO = "Arquivo renomeado e enviado";
                    break;
            }

            base.LOG_InsertData(new PRE_TBL_ARQ_PATROCINA_LOG
            {
                COD_ARQ_PAT = pCOD_ARQ_PAT,
                COD_ACAO = pCOD_ACAO,
                LOG_INCLUSAO = Util.String2Limit(pLOG_INCLUSAO, 0, 30),
                DTH_INCLUSAO = DateTime.Now,
                DCR_ACAO = sDCR_ACAO
            });

        }

        //public Resultado Criticar_async(int COD_ARQ_PAT, string pLOG_INCLUSAO)
        //{
        //    Resultado res = new Resultado();
        //    try
        //    {
        //        base.UpdateStatus(COD_ARQ_PAT, 3);
        //        Registra_LOG(3, COD_ARQ_PAT, pLOG_INCLUSAO);
        //        res.Sucesso("Arquivo(s) enviado(s) para fila de validação.");
        //    }
        //    catch (Exception ex)
        //    {
        //        res.Erro("Atenção! Ocorreu um erro na tentativa de processar o arquivo.\\n" + 
        //                 "Motivo: " + Util.GetInnerException(ex));
        //    }
        //    return res;
        //}

        public Resultado Criticar_etapa_1(PRE_TBL_ARQ_PATROCINA arqPatrocinadora,
                                  short MesRef, 
                                  short AnoRef, 
                                  string sDAT_REPASSE, 
                                  string sDAT_CREDITO,
                                  string Grupo_Portal,
                                  UsuarioPortal user)
        {

            Resultado res = new Resultado();
            long ult_linha = 0;
            try
            {
                string analytics = "";                
                DateTime tempo_ini, tempo_fim;
                tempo_ini = DateTime.Now;

                base.m_DbContext.Configuration.LazyLoadingEnabled = false;
                base.m_DbContext.Configuration.ProxyCreationEnabled = false;   
             
                CriticasBLL Crit = new CriticasBLL();                
                Crit.DeleteCritica(arqPatrocinadora.COD_ARQ_PAT);

                ArqPatrocinaDemonstrativoBLL DemonstrativoBLL = new ArqPatrocinaDemonstrativoBLL();
                DemonstrativoBLL.DeleteDemonstra(arqPatrocinadora.COD_ARQ_PAT);

                List<PRE_TBL_ARQ_PATROCINA_LINHA> lsLINHAS = base.LINHA_GetAllByCOD_ARQ_PAT(arqPatrocinadora.COD_ARQ_PAT, true);

                //ArqPatrocinaCargaBLL CargaBLL = new ArqPatrocinaCargaBLL();
                FinanceiroBLL FinBLL = new FinanceiroBLL();
                
                List<CriticasBLL.CONTROLE_ARQ_FINANCEIRO> CTRL_FINANCEIRO = new List<CriticasBLL.CONTROLE_ARQ_FINANCEIRO>();

                List<PRE_TBL_ARQ_PATROCINA_CRITICA> Criticas_Impeditivas = new List<PRE_TBL_ARQ_PATROCINA_CRITICA>();

                foreach (PRE_TBL_ARQ_PATROCINA_LINHA lALVO in lsLINHAS)
                {

                    ult_linha = lALVO.NUM_LINHA;
                    switch (arqPatrocinadora.TIP_ARQUIVO)
                    {
                        case 1: //TIPO 1 - Cadastral - Empregados
                            LAY_EMPREGADO lay_emp = new LAY_EMPREGADO(short.Parse(lALVO.COD_EMPRS));
                            Crit.Empresa(arqPatrocinadora, lALVO, lsLINHAS, lay_emp.LAY_COD_EMPRS, user.ListaEmpresas);
                            //Crit.Cadastral(arqPatrocinadora, lALVO, lsLINHAS, lay_emp, FinBLL);
                            break;
                        case 2: //TIPO 2 - Afastamento
                            LAY_AFASTAMENTO lay_afa = new LAY_AFASTAMENTO();
                            Crit.Empresa(arqPatrocinadora, lALVO, lsLINHAS, lay_afa.LAY_COD_EMPRS, user.ListaEmpresas);
                            //Crit.Afastamento(arqPatrocinadora, lALVO, lsLINHAS, lay_afa);
                            break;
                        case 3: //TIPO 3 - Orgão Lotação
                            LAY_ORGAO lay_org = new LAY_ORGAO();
                            Crit.Empresa(arqPatrocinadora, lALVO, lsLINHAS, lay_org.LAY_COD_EMPRS, user.ListaEmpresas);
                            //Crit.OrgaoLotacao(arqPatrocinadora, lALVO, lsLINHAS, lay_org);
                            break;
                        case 4: //TIPO 4 - Financeiro
                            LAY_FICHA_FINANCEIRA lay_fin = new LAY_FICHA_FINANCEIRA();
                            Crit.Empresa(arqPatrocinadora, lALVO, lsLINHAS, lay_fin.LAY_COD_EMPRS, user.ListaEmpresas);
                            //Crit.Financeiro(arqPatrocinadora, lALVO, lsLINHAS, lay_fin, ref CTRL_FINANCEIRO, sDAT_REPASSE, FinBLL);
                            break;

                    }
                    //Crit.DeleteData(lALVO.COD_ARQ_PAT_LINHA);

                    if (lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Count > 0)
                    {
                        Crit.AddData(lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.ToList(), lALVO.COD_ARQ_PAT_LINHA, null);
                        Crit.SaveData();

                        Criticas_Impeditivas.AddRange(lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Where(c => c.COD_CRITICA == 4));

                        if (Criticas_Impeditivas.Where(c => c.COD_CRITICA == 4).Count() >= 2000)
                        {
                            break;
                        }
                        else
                        {
                            // Trava para muitas linhas com erro:
                            long Linhas_processadas = Int32.Parse(lsLINHAS.Count().ToString()) - ult_linha;
                            if (Criticas_Impeditivas.Count() >= Linhas_processadas && Linhas_processadas > (ult_linha/10))
                            {
                                break;
                            }
                        }
                    }
                    
                }

                Crit.AddData(arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.ToList(), null, arqPatrocinadora.COD_ARQ_PAT);
                res = Crit.SaveData();
                base.Refresh();

                tempo_fim = DateTime.Now;
                //analytics = (String.Format("{0:n}", (tempo_fim - tempo_ini).TotalSeconds)) + " sec.";
                analytics = "";

                if (!Criticas_Impeditivas.Any()) //Criticas graves! Não deixar validar arquivo
                {


                    //PRE_TBL_ARQ_PAT_DEMONSTRA
                    ArqPatrocinaDemonstrativoBLL DemonsBLL = new ArqPatrocinaDemonstrativoBLL();
                    PRE_TBL_ARQ_PAT_DEMONSTRA newDemonstrativo = new PRE_TBL_ARQ_PAT_DEMONSTRA();
                    newDemonstrativo.COD_ARQ_PAT_DEMO = 0;
                    newDemonstrativo.ANO_REF = AnoRef;
                    newDemonstrativo.MES_REF = MesRef;
                    newDemonstrativo.DAT_REPASSE = Util.String2Date(sDAT_REPASSE);
                    newDemonstrativo.DAT_CREDITO = Util.String2Date(sDAT_CREDITO);
                    newDemonstrativo.GRUPO_PORTAL = Grupo_Portal;
                    newDemonstrativo.LOG_INCLUSAO = Util.String2Limit(user.login, 0, 30);
                    newDemonstrativo.DTH_INCLUSAO = tempo_ini;

                    //res = DemonsBLL.SaveData(newDemonstrativo);
                    DemonsBLL.SaveData(newDemonstrativo);

                    if (arqPatrocinadora.COD_STATUS < 6)
                    {
                        arqPatrocinadora.COD_STATUS = 3;
                    }
                    arqPatrocinadora.MES_REF = MesRef;
                    arqPatrocinadora.ANO_REF = AnoRef;
                    arqPatrocinadora.GRUPO_PORTAL = Grupo_Portal;

                    arqPatrocinadora.NUM_QTD_VALIDOS = 0;
                    arqPatrocinadora.NUM_QTD_ERROS = 0;
                    arqPatrocinadora.NUM_QTD_ERROS_LINHAS = 0;
                    arqPatrocinadora.NUM_QTD_ALERTAS = 0;
                    arqPatrocinadora.NUM_QTD_ALERTAS_LINHAS = 0;
                    //arqPatrocinadora.NUM_QTD_IMPORTADOS = 0;
                    arqPatrocinadora.NUM_QTD_PROCESSADOS = 0;

                    //res = base.UpdateData(arqPatrocinadora);
                    base.UpdateData(arqPatrocinadora);

                    if (res.Ok)
                    {
                        Registra_LOG(3, arqPatrocinadora.COD_ARQ_PAT, user.login);
                        //res.Sucesso(lsLINHAS.Count() + " registros validados. " + analytics);
                        res.Sucesso("Arquivo(s) enviado(s) para fila de validação. " + analytics);
                    }
                    else
                    {
                        throw new Exception(res.Mensagem);
                    }
                }
                else
                {
                    res.Erro("Não é possivel validar este arquivo:\\n" + Criticas_Impeditivas.FirstOrDefault().DCR_CRITICA);
                }
            }
            catch (Exception ex)
            {

                res.Erro("Atenção! Ocorreu um erro na tentativa de validar o arquivo.\\n" +
                         "Linha: " + ult_linha.ToString() + "\\n" +
                         "Motivo: " + Util.GetInnerException(ex));

                arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                {
                    COD_CRITICA = 901,
                    DCR_CRITICA = "Atenção! Ocorreu um erro na tentativa de processar o arquivo.\\n" +
                         "Linha: " + ult_linha.ToString() + "\\n" +
                         "Motivo: " + Util.GetInnerException(ex),
                    NOM_CAMPO = "",
                    NUM_POSICAO = 1
                });
            }
            return res;

        }

        public Resultado Criticar_etapa_2(PRE_TBL_ARQ_PATROCINA arqPatrocinadora, 
                                  short MesRef, 
                                  short AnoRef, 
                                  string sDAT_REPASSE, 
                                  string sDAT_CREDITO,
                                  int LIMITE_PROC_REGISTROS = 0)
        {

            Resultado res = new Resultado();
            long ult_linha = 0;
            try
            {
                string analytics = "";                
                DateTime tempo_ini, tempo_fim;
                tempo_ini = DateTime.Now;

                base.m_DbContext.Configuration.LazyLoadingEnabled = false;
                base.m_DbContext.Configuration.ProxyCreationEnabled = false;

                bool PROCESSAMENTO_PARCIAL = false;
                int NUM_QTD_PROCESSADOS = arqPatrocinadora.NUM_QTD_PROCESSADOS ?? 0;
                int NUM_QTD_PROCESSADOS_ANTERIOR = arqPatrocinadora.NUM_QTD_PROCESSADOS ?? 0;            
                int NUM_QTD_VALIDOS = arqPatrocinadora.NUM_QTD_VALIDOS ?? 0;
                int NUM_QTD_ERROS = arqPatrocinadora.NUM_QTD_ERROS ?? 0;
                int NUM_QTD_ERROS_LINHAS = arqPatrocinadora.NUM_QTD_ERROS_LINHAS ?? 0;
                int NUM_QTD_ALERTAS = arqPatrocinadora.NUM_QTD_ALERTAS ?? 0;
                int NUM_QTD_ALERTAS_LINHAS = arqPatrocinadora.NUM_QTD_ALERTAS_LINHAS ?? 0;

                CriticasBLL Crit = new CriticasBLL();
                if (arqPatrocinadora.COD_STATUS < 4)
                {
                    Crit.DeleteCritica(arqPatrocinadora.COD_ARQ_PAT);
                    NUM_QTD_PROCESSADOS = 0;
                    NUM_QTD_PROCESSADOS_ANTERIOR = 0;
                    NUM_QTD_VALIDOS = 0;
                    NUM_QTD_ERROS = 0;
                    NUM_QTD_ERROS_LINHAS = 0;
                    NUM_QTD_ALERTAS = 0;
                    NUM_QTD_ALERTAS_LINHAS = 0;
                }

                ArqPatrocinaDemonstrativoBLL DemonstrativoBLL = new ArqPatrocinaDemonstrativoBLL();
                //DemonstrativoBLL.DeleteDemonstra(arqPatrocinadora.COD_ARQ_PAT);

                List<PRE_TBL_ARQ_PATROCINA_LINHA> lsLINHAS = base.LINHA_GetAllByCOD_ARQ_PAT(arqPatrocinadora.COD_ARQ_PAT, true);

                //ArqPatrocinaCargaBLL CargaBLL = new ArqPatrocinaCargaBLL();
                FinanceiroBLL FinBLL = new FinanceiroBLL();
                
                DateTime DAT_DEMONSTRATIVO = DateTime.Now;
                bool Demonstrativo_Gerado = false;
                int  Cod_Demonstrativo_Gerado = 0;

                List<CriticasBLL.CONTROLE_ARQ_FINANCEIRO> CTRL_FINANCEIRO = new List<CriticasBLL.CONTROLE_ARQ_FINANCEIRO>();
                List<PRE_TBL_ARQ_PATROCINA_CRITICA> Criticas_Impeditivas = new List<PRE_TBL_ARQ_PATROCINA_CRITICA>();

                int NUM_CONT_REG = 1;

                foreach (PRE_TBL_ARQ_PATROCINA_LINHA lALVO in lsLINHAS)
                {

                    if (NUM_CONT_REG <= NUM_QTD_PROCESSADOS_ANTERIOR)
                    {
                        NUM_CONT_REG++;
                        continue;
                    }

                    ult_linha = lALVO.NUM_LINHA;
                    switch (arqPatrocinadora.TIP_ARQUIVO)
                    {
                        case 1: //TIPO 1 - Cadastral - Empregados
                            LAY_EMPREGADO lay_emp = new LAY_EMPREGADO(short.Parse(lALVO.COD_EMPRS));
                            //Crit.Empresa(arqPatrocinadora, lALVO, lsLINHAS, lay_emp.LAY_COD_EMPRS, user.ListaEmpresas);
                            Crit.Cadastral(arqPatrocinadora, lALVO, lsLINHAS, lay_emp, FinBLL);
                            break;
                        case 2: //TIPO 2 - Afastamento
                            LAY_AFASTAMENTO lay_afa = new LAY_AFASTAMENTO();
                            //Crit.Empresa(arqPatrocinadora, lALVO, lsLINHAS, lay_afa.LAY_COD_EMPRS, user.ListaEmpresas);
                            Crit.Afastamento(arqPatrocinadora, lALVO, lsLINHAS, lay_afa); 
                            break;
                        case 3: //TIPO 3 - Orgão Lotação
                            LAY_ORGAO lay_org = new LAY_ORGAO();
                            //Crit.Empresa(arqPatrocinadora, lALVO, lsLINHAS, lay_org.LAY_COD_EMPRS, user.ListaEmpresas);
                            Crit.OrgaoLotacao(arqPatrocinadora, lALVO, lsLINHAS, lay_org); 
                            break;
                        case 4: //TIPO 4 - Financeiro
                            LAY_FICHA_FINANCEIRA lay_fin = new LAY_FICHA_FINANCEIRA();
                            //Crit.Empresa(arqPatrocinadora, lALVO, lsLINHAS, lay_fin.LAY_COD_EMPRS, user.ListaEmpresas);
                            Crit.Financeiro(arqPatrocinadora, lALVO, lsLINHAS, lay_fin, ref CTRL_FINANCEIRO, sDAT_REPASSE, FinBLL);
                            break;

                    }
                    //Crit.DeleteData(lALVO.COD_ARQ_PAT_LINHA);


                    if (lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Count > 0)
                    {
                        //NUM_QTD_REGISTROS = g.Sum(s => s.NUM_QTD_REGISTROS),
                        //NUM_QTD_VALIDOS = g.Sum(s => s.NUM_QTD_REGISTROS - s.NUM_QTD_ERROS_LINHAS),
                        //NUM_QTD_ERROS = g.Sum(s => s.NUM_QTD_ERROS + s.NUM_QTD_ERROS_LINHAS),
                        //NUM_QTD_ERROS_LINHAS = g.Sum(s => s.NUM_QTD_ERROS_LINHAS),
                        //NUM_QTD_ALERTAS = g.Sum(s => s.NUM_QTD_ALERTAS + s.NUM_QTD_ALERTAS_LINHAS),
                        //NUM_QTD_ALERTAS_LINHAS = g.Sum(s => s.NUM_QTD_ALERTAS_LINHAS),
                        //NUM_QTD_IMPORTADOS = g.Sum(s => s.NUM_QTD_IMPORTADOS)

                        NUM_QTD_ERROS_LINHAS += lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Count(c => c.TIP_CRITICA == 1);
                        NUM_QTD_ALERTAS_LINHAS += lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Count(c => c.TIP_CRITICA == 2);


                        Crit.AddData(lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.ToList(), lALVO.COD_ARQ_PAT_LINHA, null);
                        Crit.SaveData();

                        Criticas_Impeditivas.AddRange(lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.Where(c => c.COD_CRITICA == 4));

                        if (Criticas_Impeditivas.Where(c => c.COD_CRITICA == 4).Count() >= 2000)
                        {
                            break;
                        }
                        else
                        {
                            // Trava para muitas linhas com erro:
                            long Linhas_processadas = Int32.Parse(lsLINHAS.Count().ToString()) - ult_linha;
                            if (Criticas_Impeditivas.Count() >= Linhas_processadas && Linhas_processadas > (ult_linha / 10))
                            {
                                break;
                            }
                        }

                    }
                    else
                    {
                        NUM_QTD_VALIDOS += 1;
                        if (arqPatrocinadora.TIP_ARQUIVO == 4 && lALVO.TIP_LINHA == 2)
                        {
                            res = DemonstrativoBLL.DeParaDemonstrativo(lALVO.COD_EMPRS,
                                                                       lALVO.NUM_RGTRO_EMPRG,
                                                                       lALVO.DADOS,
                                                                       sDAT_REPASSE,
                                                                       sDAT_CREDITO,
                                                                       arqPatrocinadora.GRUPO_PORTAL,
                                                                       arqPatrocinadora.LOG_INCLUSAO,
                                                                       DAT_DEMONSTRATIVO,
                                                                       FinBLL,
                                                                       lALVO.COD_ARQ_PAT_LINHA,
                                                                       Cod_Demonstrativo_Gerado);
                            if (!Demonstrativo_Gerado && res.CodigoCriado > 0)
                            {
                                Cod_Demonstrativo_Gerado = Convert.ToInt32(res.CodigoCriado);
                                Demonstrativo_Gerado = true;
                            }
                            if (res.Ok)
                            {
                                DemonstrativoBLL.SaveChanges();
                            }
                        }
                    }

                    NUM_QTD_PROCESSADOS++;

                    if (NUM_QTD_PROCESSADOS >= NUM_QTD_PROCESSADOS_ANTERIOR + LIMITE_PROC_REGISTROS && LIMITE_PROC_REGISTROS > 0)
                    {
                        break;
                    }
                    
                }

                if (NUM_QTD_PROCESSADOS < lsLINHAS.Count && LIMITE_PROC_REGISTROS > 0)
                {
                    PROCESSAMENTO_PARCIAL = true;
                }

                Crit.Financeiro_2(arqPatrocinadora, ref CTRL_FINANCEIRO, sDAT_REPASSE); // TIPO 4 - Criticas Financeiro no arquivo

                NUM_QTD_ERROS += arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.Count(c => c.TIP_CRITICA == 1);
                NUM_QTD_ALERTAS += arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.Count(c => c.TIP_CRITICA == 2);

                Crit.AddData(arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.ToList(), null, arqPatrocinadora.COD_ARQ_PAT);
                res = Crit.SaveData();

                base.Refresh();

                if (PROCESSAMENTO_PARCIAL)
                {
                    arqPatrocinadora.COD_STATUS = 4;
                } 
                else if (arqPatrocinadora.COD_STATUS < 5)
                {
                    arqPatrocinadora.COD_STATUS = 5;
                }
                arqPatrocinadora.MES_REF = MesRef;
                arqPatrocinadora.ANO_REF = AnoRef;
                arqPatrocinadora.NUM_QTD_PROCESSADOS = NUM_QTD_PROCESSADOS;
                arqPatrocinadora.NUM_QTD_VALIDOS = NUM_QTD_VALIDOS;
                arqPatrocinadora.NUM_QTD_ERROS = NUM_QTD_ERROS;
                arqPatrocinadora.NUM_QTD_ERROS_LINHAS = NUM_QTD_ERROS_LINHAS;
                arqPatrocinadora.NUM_QTD_ALERTAS = NUM_QTD_ALERTAS;
                arqPatrocinadora.NUM_QTD_ALERTAS_LINHAS = NUM_QTD_ALERTAS_LINHAS;
                
                //arqPatrocinadora.GRUPO_PORTAL = arqPatrocinadora.GRUPO_PORTAL;
                base.UpdateData(arqPatrocinadora);

                tempo_fim = DateTime.Now;
                analytics = (String.Format("{0:n}", (tempo_fim - tempo_ini).TotalSeconds)) + " sec.";
                //analytics = "";

                if (res.Ok)
                {
                    if (!PROCESSAMENTO_PARCIAL)
                    {
                        Registra_LOG(5, arqPatrocinadora.COD_ARQ_PAT, arqPatrocinadora.LOG_INCLUSAO);
                        res.Sucesso(lsLINHAS.Count() + " registros validados com sucesso. [" + analytics + "]", 5);
                    }
                    else
                    {
                        res.Sucesso("**PROCESSAMENTO PARTICIAL: " + NUM_QTD_PROCESSADOS + " de " + lsLINHAS.Count + " registros validados. [" + analytics + "]", 4);
                    }
                }
                else
                {
                    throw new Exception(res.Mensagem);
                }

                if (Demonstrativo_Gerado)
                {
                    Registra_LOG(11, arqPatrocinadora.COD_ARQ_PAT, arqPatrocinadora.LOG_INCLUSAO);
                }

            }
            catch (Exception ex)
            {

                string Log_Erro = "Atenção! Ocorreu um erro na tentativa de validar o arquivo.\\n" +
                         "Linha: " + ult_linha.ToString() + "\\n" +
                         "Motivo: " + Util.GetInnerException(ex) + "\\n" +
                         "Stack: " + ex.StackTrace;

                res.Erro(Log_Erro);

                arqPatrocinadora.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                {
                    COD_CRITICA = 901,
                    DCR_CRITICA = Log_Erro,
                    NOM_CAMPO = "",
                    NUM_POSICAO = 1
                });

                DisparaAlerta("ArqPatrocinadoraBLL.Criticar_etapa_2 [Exception]:" + Log_Erro);

            }
            return res;

        }

        public Resultado Carregar_async(PRE_TBL_ARQ_PATROCINA arqPatrocinadora,                                   
                                        string pLOG_INCLUSAO)            
            //int COD_ARQ_PAT, string pLOG_INCLUSAO)
        {
            Resultado res = new Resultado();
            try
            {
                //base.UpdateStatus(COD_ARQ_PAT, 6);
                //res = base.UpdateData(arqPatrocinadora);
                arqPatrocinadora.COD_STATUS = 6;
                arqPatrocinadora.NUM_QTD_IMPORTADOS = 0;
                arqPatrocinadora.NUM_QTD_PROCESSADOS = 0;
                base.UpdateData(arqPatrocinadora);

                Registra_LOG(6, arqPatrocinadora.COD_ARQ_PAT, pLOG_INCLUSAO);
                res.Sucesso("Arquivo(s) enviado(s) para fila de processamento.");
            }
            catch (Exception ex)
            {
                res.Erro("Atenção! Ocorreu um erro na tentativa de carregar o arquivo.\\nMotivo: " + Util.GetInnerException(ex));
            }

            return res;

        }

        public Resultado Carregar(PRE_TBL_ARQ_PATROCINA arqCARGA,                                   
                                  string pLOG_INCLUSAO,                                  
                                  DateTime? pDTH_INCLUSAO = null,
                                  string sDAT_REPASSE = null,
                                  int LIMITE_PROC_REGISTROS = 0)
        {
            Resultado res = new Resultado();
            try
            {
                string analytics = "";
                string acertos = "";
                long LINHAS_LIDAS = 1;
                long LINHAS_CARGA_SUCESSO = 0;
                long LINHAS_ERRO = 0;
                long LINHAS_COM_CRITICA = 0;
                long LINHAS_JA_CARREGADAS = 0;
                long LINHAS_INSERIDAS = 0;
                long LINHAS_ATUALIZADAS = 0;
                DateTime tempo_ini, tempo_fim;
                tempo_ini = DateTime.Now;
                DateTime DAT_IMPORTADO = DateTime.Now;
                base.m_DbContext.Configuration.LazyLoadingEnabled = false;
                base.m_DbContext.Configuration.ProxyCreationEnabled = false;
                ArqPatrocinaCargaBLL CargaBLL = new ArqPatrocinaCargaBLL();                
                ArqPatrocinadoraBLL ArqPatro = new ArqPatrocinadoraBLL();
                List<PRE_TBL_ARQ_PATROCINA_LINHA> lsLINHAS = base.LINHA_GetAllByCOD_ARQ_PAT(arqCARGA.COD_ARQ_PAT);
                List<PRE_TBL_ARQ_PATROCINA_LINHA> lsACERTOS = base.LINHA_GetAllByCOD_ARQ_PAT(arqCARGA.COD_ARQ_PAT);
                List<PRE_TBL_ARQ_PATROCINA_CRITICA> lsCRITICAS = ArqPatro.CRITICA_LINHAS_GetBy_COD_ARQ_PAT(arqCARGA.COD_ARQ_PAT);

                bool PROCESSAMENTO_PARCIAL = false;
                int NUM_QTD_PROCESSADOS = arqCARGA.NUM_QTD_PROCESSADOS ?? 0;
                int NUM_QTD_PROCESSADOS_ANTERIOR = arqCARGA.NUM_QTD_PROCESSADOS ?? 0;

                if (arqCARGA.COD_STATUS < 7)
                {
                    NUM_QTD_PROCESSADOS = 0;
                    NUM_QTD_PROCESSADOS_ANTERIOR = 0;
                }

                arqCARGA.COD_STATUS = 7;

                PRE_TBL_ARQ_PATROCINA_CARGA Carga = new PRE_TBL_ARQ_PATROCINA_CARGA();
                //Crit.DeleteCritica(arqPatrocinadora.COD_ARQ_PAT);

                if (ArqPatro.CRITICA_GetDataCountGroup(arqCARGA.COD_ARQ_PAT, 1) > 0)
                {
                    res.Erro("Foram encontradas críticas que impedem o carregamento do arquivo.");
                }
                else
                {

                    foreach (PRE_TBL_ARQ_PATROCINA_LINHA lALVO in lsLINHAS)
                    {

                        if (lALVO.TIP_LINHA != 2) continue;

                        //LINHAS_LIDAS++;
                        if (LINHAS_LIDAS <= NUM_QTD_PROCESSADOS_ANTERIOR)
                        {
                            if (lALVO.DAT_IMPORTADO != null)
                            {
                                LINHAS_JA_CARREGADAS++;
                            }
                            LINHAS_LIDAS++;
                            continue;
                        }
                        
                        if (lALVO.DAT_IMPORTADO != null)
                        {
                            LINHAS_JA_CARREGADAS++;
                            continue;
                        }

                        List<PRE_TBL_ARQ_PATROCINA_CRITICA> lsCRITICAS_LINHA = lsCRITICAS.Where(c => c.COD_ARQ_PAT_LINHA == lALVO.COD_ARQ_PAT_LINHA).ToList();
                        List<string> NAO_ATUALIZAR = new List<string>();

                        if (lsCRITICAS_LINHA.Count > 0)
                        {
                            if (lsCRITICAS_LINHA.Any(c => c.TIP_CRITICA == 1)) // Tem erro? Ignora a linha toda
                            {                                
                                LINHAS_COM_CRITICA++; //Ignoradas
                                continue;
                            }
                            else
                            {
                                foreach (PRE_TBL_ARQ_PATROCINA_CRITICA crit in lsCRITICAS_LINHA.Where(c => c.TIP_CRITICA == 2))
                                {
                                    NAO_ATUALIZAR.Add(crit.NOM_CAMPO);
                                }
                            }
                        }

                        res = new Resultado();
                        try
                        {

                            switch (arqCARGA.TIP_ARQUIVO)
                            {
                                case 1:
                                    res = CargaBLL.Cadastral(lALVO, NAO_ATUALIZAR); //TIPO 1 - Cadastral - Empregados
                                    break;
                                case 2:
                                    res = CargaBLL.Afastamento(lALVO.COD_EMPRS, lALVO.NUM_RGTRO_EMPRG, lALVO.DADOS, NAO_ATUALIZAR); //TIPO 2 - Afastamento
                                    break;
                                case 3:
                                    res = CargaBLL.OrgaoLotacao(lALVO.COD_EMPRS, lALVO.NUM_RGTRO_EMPRG, lALVO.DADOS, NAO_ATUALIZAR); //TIPO 3 - Orgão Lotação
                                    break;
                                case 4:
                                    res = CargaBLL.Financeiro(lALVO.COD_EMPRS, lALVO.NUM_RGTRO_EMPRG, sDAT_REPASSE, lALVO.DADOS, NAO_ATUALIZAR); //TIPO 4 - Financeiro
                                    break;

                            }
                            switch (res.CodigoCriado)
                            {
                                case 1: //INSERT
                                    LINHAS_INSERIDAS++;
                                    break;
                                case 2: //UPDATE
                                    LINHAS_ATUALIZADAS++;
                                    break;
                            }
                            //Crit.DeleteData(lALVO.COD_ARQ_PAT_LINHA);
                            //Carga.AddData(lALVO.PRE_TBL_ARQ_PATROCINA_CRITICA.ToList(), lALVO.COD_ARQ_PAT_LINHA, null);
                            //if (res.Ok)
                            //{
                            res = CargaBLL.SaveChanges();
                            //}
                        }
                        catch (Exception ex)
                        {
                            res.Erro(Util.GetInnerException(ex));
                        }

                        if (res.Ok)
                        {
                            CargaBLL.LinhaCarregada(lALVO, DAT_IMPORTADO);
                            CargaBLL.SaveChanges();
                            LINHAS_CARGA_SUCESSO++;
                        }
                        else
                        {
                            LINHAS_ERRO++;
                            Carga.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                            {
                                COD_CRITICA = 900,
                                DCR_CRITICA = "Atenção! Ocorreu um erro na tentativa de carregar a linha " + lALVO.NUM_LINHA + " do arquivo.\\nMotivo:" + res.Mensagem,
                                //COD_ARQ_PAT_LINHA = lALVO.COD_ARQ_PAT_LINHA,   <-- Critica da carga
                                NOM_CAMPO = "",
                                NUM_POSICAO = 1                                
                            });
                        }

                        NUM_QTD_PROCESSADOS++;
                        if (NUM_QTD_PROCESSADOS >= NUM_QTD_PROCESSADOS_ANTERIOR + LIMITE_PROC_REGISTROS && LIMITE_PROC_REGISTROS > 0)
                        {
                            PROCESSAMENTO_PARCIAL = true;
                            break;
                        }
                    }

                    //Carrega acertos
                    if (arqCARGA.TIP_ARQUIVO == 4)
                    {
                        if (!CarregarAcertos(arqCARGA, CargaBLL, DAT_IMPORTADO).Ok)
                        {
                            acertos = "(*)";
                        };
                    }

                }

                tempo_fim = DateTime.Now;
                analytics = (String.Format("{0:n}", (tempo_fim - tempo_ini).TotalSeconds)) + " sec.";
                //analytics = "";

                arqCARGA.NUM_QTD_PROCESSADOS = Convert.ToInt32(NUM_QTD_PROCESSADOS + LINHAS_COM_CRITICA);
                arqCARGA.NUM_QTD_IMPORTADOS = Convert.ToInt32(LINHAS_CARGA_SUCESSO + LINHAS_JA_CARREGADAS);
                base.UpdateData(arqCARGA);

                if (!PROCESSAMENTO_PARCIAL)
                {
                    if (LINHAS_CARGA_SUCESSO > 0)
                    {
                        //arqCARGA.NUM_QTD_IMPORTADOS = Convert.ToInt32(LINHAS_CARGA_SUCESSO);                    
                        if (LINHAS_ERRO == 0)
                        {
                            base.UpdateStatus(arqCARGA.COD_ARQ_PAT, 9);
                            Registra_LOG(9, arqCARGA.COD_ARQ_PAT, pLOG_INCLUSAO);
                            res.Sucesso(LINHAS_CARGA_SUCESSO + " registros carregados. [" + analytics + "]", 9);
                        }
                        else
                        {
                            base.UpdateStatus(arqCARGA.COD_ARQ_PAT, 8);
                            Registra_LOG(8, arqCARGA.COD_ARQ_PAT, pLOG_INCLUSAO);
                            string sMsg = "Atenção! Os registros foram carregados parcialmente.\\nCarregados: " + LINHAS_CARGA_SUCESSO + " Ignorados: " + LINHAS_ERRO;
                            if (!res.Ok)
                            {
                                sMsg += "\\nErro:\\n" + res.Mensagem;
                            }
                            res.Alert(sMsg + " [" + analytics + "]");
                        }

                    }
                    else
                    {
                        if (LINHAS_ERRO == 0)
                        {
                            if (LINHAS_JA_CARREGADAS > 0)
                            {
                                base.UpdateStatus(arqCARGA.COD_ARQ_PAT, 9);
                                res.Alert("Atenção! Nenhum registro novo foi carregado. [" + analytics + "] " + acertos);
                            }
                            else if (LINHAS_COM_CRITICA >= lsLINHAS.Count)
                            {
                                base.UpdateStatus(arqCARGA.COD_ARQ_PAT, 8);
                                res.Alert("Atenção! Não foram encontrados registros validos para carregar. [" + analytics + "] " + acertos);
                            }
                        }
                        else
                        {
                            //Registra_LOG(5, arqCARGA.COD_ARQ_PAT, pLOG_INCLUSAO);
                            base.UpdateStatus(arqCARGA.COD_ARQ_PAT, 5);
                            res.Erro("Atenção! Nenhum registro foi carregado.\\nMotivo:\\n" + res.Mensagem + "[" + analytics + "] " + acertos);
                        }
                    }

                    //Carga.COD_ARQ_PAT_CARGA  
                    Carga.COD_ARQ_PAT = arqCARGA.COD_ARQ_PAT;
                    //Carga.COD_EMPRS
                    //Carga.DCR_EMPRS
                    Carga.DCR_ARQS = arqCARGA.NOM_ARQUIVO;
                    Carga.TIP_ARQUIVO = arqCARGA.TIP_ARQUIVO;
                    Carga.NUM_LINHAS_LIDAS = LINHAS_LIDAS - 1;
                    Carga.NUM_LINHAS_CARREGADAS = LINHAS_CARGA_SUCESSO;
                    Carga.NUM_LINHAS_ERROS = LINHAS_ERRO;
                    Carga.NUM_LINHAS_CRITICAS = LINHAS_COM_CRITICA;
                    Carga.NUM_LINHAS_JA_CARREGADAS = LINHAS_JA_CARREGADAS;
                    Carga.NUM_LINHAS_INSERIDAS = LINHAS_INSERIDAS;
                    Carga.NUM_LINHAS_ATUALIZADAS = LINHAS_ATUALIZADAS;
                    Carga.DCR_EMAIL_ENVIADO = "";
                    Carga.LOG_INCLUSAO = Util.String2Limit(pLOG_INCLUSAO, 0, 30);
                    Carga.DTH_INCLUSAO = pDTH_INCLUSAO ?? DateTime.Now;

                    CargaBLL = new ArqPatrocinaCargaBLL();
                    CargaBLL.RegistraCarga(Carga);
                }
                else
                {
                    res.Sucesso("**PROCESSAMENTO PARTICIAL: " + NUM_QTD_PROCESSADOS + " de " + lsLINHAS.Count + " registros carregados. [" + analytics + "]", 6);
                }
            }
            catch (Exception ex)
            {

                res.Erro("Atenção! Ocorreu um erro na tentativa de carregar o arquivo.\\nMotivo: " + Util.GetInnerException(ex));

                arqCARGA.PRE_TBL_ARQ_PATROCINA_CRITICA.Add(new PRE_TBL_ARQ_PATROCINA_CRITICA
                {
                    COD_CRITICA = 902,
                    DCR_CRITICA = "Atenção! Ocorreu um erro na tentativa de carregar o arquivo.\\nMotivo: " + Util.GetInnerException(ex),
                    NOM_CAMPO = "",
                    NUM_POSICAO = 1
                });
            }

            return res;
        }

        public async Task<string> Processar_Todos_Arquivos_Por_Status(short pStatus)
        {
            string ret = "";
            string nomArquivo = "";
            //List<PRE_VIEW_ARQ_PATROCINA> lstPendentes = base.GetByStatus(pStatus);
            //lstPendentes.ForEach(a => { a.COD_EMPRS = String.Join(",", GetCOD_EMPRSs(a.COD_ARQ_PAT)); });
            PRE_VIEW_ARQ_PATROCINA ap = base.GetByStatus(pStatus).FirstOrDefault();

            if (ap != null)
            {
                nomArquivo = ap.NOM_ARQUIVO;
            }
            
            List<PRE_VIEW_ARQ_PATROCINA> lstValidando = base.GetByStatus(4);
            if (lstValidando.Count() > 2 && pStatus == 3)
            {
                LimparArqCriticando(lstValidando);
                ret += "(V) " + nomArquivo + "-> Arquivo ignorado por execesso de validações em andamento => " + lstValidando.Count() + Environment.NewLine;
                return ret;
            }
            
            List<PRE_VIEW_ARQ_PATROCINA> lstCarregando = base.GetByStatus(7);
            if (lstCarregando.Count() > 2 && pStatus == 6)
            {
                LimparArqCarregando(lstCarregando);
                ret += "(C) " + nomArquivo + "-> Arquivo ignorado por execesso de carregamentos em andamento => " + lstCarregando.Count() + Environment.NewLine;
                return ret;
            }

            //Nenhum arquivo para processsar
            if (ap == null) return ret;

            ap.COD_EMPRS = String.Join(",", GetCOD_EMPRSs(ap.COD_ARQ_PAT));
            //foreach (PRE_VIEW_ARQ_PATROCINA ap in lstPendentes)
            //{

            PRE_TBL_ARQ_PATROCINA _ap_atualizado = GetDataByCod(ap.COD_ARQ_PAT);
            if (_ap_atualizado == null)
            {
                ret += ap.NOM_ARQUIVO + "-> Arquivo não encontrado.  " + Environment.NewLine;
                return ret;
            } else if (_ap_atualizado.COD_STATUS != pStatus)
            {
                ret += ap.NOM_ARQUIVO + "-> Arquivo ignorado por diferença de status.  " + Environment.NewLine;
                return ret;
            }

            //List<PRE_TBL_ARQ_PATROCINA_LOG> apLogs = GetLogBy(ap.COD_ARQ_PAT);
            //PRE_TBL_ARQ_PATROCINA_LOG ultLog = apLogs
            //                                    .Where(l => l.COD_ACAO == pStatus && l.DTH_INCLUSAO == apLogs.Max(m => m.DTH_INCLUSAO))
            //                                    .FirstOrDefault();

            Resultado res = new Resultado();
            short STATUS_ROLLBACK = 1;
            switch (pStatus)
            {
                case 3:
                case 4:
                    base.UpdateStatus(ap.COD_ARQ_PAT, 4);
                    res = Criticar_etapa_2(ap, short.Parse(ap.MES_REF.ToString()), short.Parse(ap.ANO_REF.ToString()), Util.Date2String(ap.DAT_REPASSE), Util.Date2String(ap.DAT_CREDITO), 1000);
                    STATUS_ROLLBACK = 1;
                    //Registra_LOG(5, ap.COD_ARQ_PAT, ap.LOG_INCLUSAO);
                    break;
                case 6:
                case 7:
                    base.UpdateStatus(ap.COD_ARQ_PAT, 7);
                    res = Carregar(ap, ap.LOG_INCLUSAO, null, Util.Date2String(ap.DAT_REPASSE), 1000);
                    STATUS_ROLLBACK = 5;
                    //Registra_LOG(7, ap.COD_ARQ_PAT, ap.LOG_INCLUSAO);
                    break;
            }

            //Enviar e-mail para patrocinadora em caso de processamento 100% concluído
            if (res.CodigoCriado != 4 && res.CodigoCriado != 6)
            {
                if (res.Ok)
                {
                    enviar_email_patrocinadora_processamento_concluido(ap, res.Mensagem, res.Ok);
                }
                else
                {
                    //enviar_email_patrocinadora_processamento_concluido(ap, "Alerta: Foram encontradas inconsistências que impedem o processamento total ou parcial do arquivo.", false);
                    enviar_email_patrocinadora_processamento_concluido(ap, res.Mensagem, false);
                }
            }

            ret += ap.NOM_ARQUIVO + "->   Código: " + ap.COD_ARQ_PAT + "   Status: " + ap.DCR_STATUS + "   Empresas: " + ap.COD_EMPRS + (ap.DAT_REPASSE!=null ? "   Dat. Repasse: " + ap.DAT_REPASSE : "")  + "\n";
            ret += "Resultado:   res.Ok=" + res.Ok + "   res.Mensagem=" + res.Mensagem + Environment.NewLine;

            if (!res.Ok && !res.Alerta)
            {
                //Rollback de status:
                base.m_DbContext = new PREV_Entity_Conn();
                base.UpdateStatus(ap.COD_ARQ_PAT, STATUS_ROLLBACK);
                DisparaAlerta(ret);
            }

            return ret;
        }

        private void LimparArqCarregando(List<PRE_VIEW_ARQ_PATROCINA> lstCarregando)
        {
            //PRE_VIEW_ARQ_PATROCINA mais_novo = lstCarregando.FirstOrDefault(a => a.DTH_INCLUSAO == lstCarregando.Min(b => b.DTH_INCLUSAO));
            foreach(PRE_VIEW_ARQ_PATROCINA ArqPat in lstCarregando)
            {
                List<PRE_TBL_ARQ_PATROCINA_LOG> apLgs = GetLogBy(ArqPat.COD_ARQ_PAT);
                List<PRE_TBL_ARQ_PATROCINA_LOG> apLgs_carregar = apLgs.Where(l => l.COD_ACAO == 6).ToList();
                PRE_TBL_ARQ_PATROCINA_LOG log_solicitado = apLgs_carregar.FirstOrDefault(m => m.DTH_INCLUSAO == apLgs_carregar.Max(n => n.DTH_INCLUSAO));
                if (DateTime.Now.Subtract(log_solicitado.DTH_INCLUSAO).TotalHours > 4)
                {
                    base.UpdateStatus(ArqPat.COD_ARQ_PAT, 8);
                }
            }
        }

        private void LimparArqCriticando(List<PRE_VIEW_ARQ_PATROCINA> lstCarregando)
        {
            //PRE_VIEW_ARQ_PATROCINA mais_novo = lstCarregando.FirstOrDefault(a => a.DTH_INCLUSAO == lstCarregando.Min(b => b.DTH_INCLUSAO));
            foreach (PRE_VIEW_ARQ_PATROCINA ArqPat in lstCarregando)
            {
                List<PRE_TBL_ARQ_PATROCINA_LOG> apLgs = GetLogBy(ArqPat.COD_ARQ_PAT);
                List<PRE_TBL_ARQ_PATROCINA_LOG> apLgs_carregar = apLgs.Where(l => l.COD_ACAO == 3).ToList();
                PRE_TBL_ARQ_PATROCINA_LOG log_solicitado = apLgs_carregar.FirstOrDefault(m => m.DTH_INCLUSAO == apLgs_carregar.Max(n => n.DTH_INCLUSAO));
                if (log_solicitado != null && DateTime.Now.Subtract(log_solicitado.DTH_INCLUSAO).TotalHours > 4)
                {
                    base.UpdateStatus(ArqPat.COD_ARQ_PAT, 1);
                }
            }
        }


        private Resultado CarregarAcertos(PRE_TBL_ARQ_PATROCINA arqCARGA,
                                          ArqPatrocinaCargaBLL CargaBLL,
                                          DateTime DAT_IMPORTADO)
        {
            Resultado res = new Resultado();
            ArqPatrocinaDemonstrativoBLL apdBLL = new ArqPatrocinaDemonstrativoBLL();
            res.Sucesso("Nenhum acerto encontrado.");
            foreach (String Cod_Emprs in GetCOD_EMPRSs(arqCARGA.COD_ARQ_PAT))
            {
                foreach (PRE_VW_ARQ_PAT_DEMONSTRATIVO DEMO_DETALHE in apdBLL.GetLancamentos(arqCARGA.GRUPO_PORTAL, arqCARGA.MES_REF ?? 0, arqCARGA.ANO_REF ?? 0, short.Parse(Cod_Emprs)))
                {
                    FICHA_FINANCEIRA newFicha = new FICHA_FINANCEIRA();

                    DateTime DAT_REPASSE;
                    DateTime.TryParse(DEMO_DETALHE.DAT_REPASSE.ToString(), out DAT_REPASSE);
                    newFicha.COD_EMPRS = DEMO_DETALHE.COD_EMPRS ?? 0;
                    newFicha.NUM_RGTRO_EMPRG = DEMO_DETALHE.NUM_RGTRO_EMPRG ?? 0;
                    newFicha.COD_VERBA = DEMO_DETALHE.COD_VERBA ?? 0;
                    newFicha.ANO_COMPET_VERFIN = DEMO_DETALHE.ANO_REF ?? 0;
                    newFicha.MES_COMPET_VERFIN = DEMO_DETALHE.MES_REF ?? 0;
                    newFicha.VLR_VERFIN = DEMO_DETALHE.VLR_ACERTO_PATROCINADORA;
                    newFicha.ANO_PAGTO_VERFIN = short.Parse(DAT_REPASSE.Year.ToString());
                    newFicha.MES_PAGTO_VERFIN = short.Parse(DAT_REPASSE.Month.ToString());
                    newFicha.DAT_PAGTO_VERFIN = DAT_REPASSE;
                    res = CargaBLL.Financeiro(DEMO_DETALHE.COD_EMPRS ?? 0, DEMO_DETALHE.NUM_RGTRO_EMPRG ?? 0, newFicha);

                    PRE_TBL_ARQ_PAT_DEMONSTRA_DET demo_det = apdBLL.GetLancamento(DEMO_DETALHE.COD_DEMO_DET, DEMO_DETALHE.TIP_LANCAMENTO);
                    demo_det.DTH_IMPORTADO = DAT_IMPORTADO;
                    res = CargaBLL.SaveData(demo_det);

                }
            }

            return res;
        }

        public List<PRE_VIEW_ARQ_RECEBIDO_CONTROLE> GetDataControle(int startRowIndex, int maximumRows, short? ano, short? mes, string sortParameter)
        {

            List<PRE_VIEW_ARQ_RECEBIDO_CONTROLE> ls_RECEBIDOS_CONTROLE = base.GetDataControle2(startRowIndex, maximumRows, null, sortParameter);

            foreach (PRE_VIEW_ARQ_RECEBIDO_CONTROLE rec_ctrl in ls_RECEBIDOS_CONTROLE)
            {
                List<PRE_TBL_ARQ_PATROCINA> ls_Arquivos = base.GetDataByGrupoPortal(rec_ctrl.GRUPO_PORTAL, ano, mes);

                rec_ctrl.QTD_CADASTRAL_VALIDADO = ls_Arquivos.Where(a => a.TIP_ARQUIVO == 1 && a.COD_STATUS == 5).Count();
                rec_ctrl.QTD_AFASTAMENTO_VALIDADO = ls_Arquivos.Where(a => a.TIP_ARQUIVO == 2 && a.COD_STATUS == 5).Count();
                rec_ctrl.QTD_ORGAO_LOTACAO_VALIDADO = ls_Arquivos.Where(a => a.TIP_ARQUIVO == 3 && a.COD_STATUS == 5).Count();
                rec_ctrl.QTD_FINANCEIRO_VALIDADO = ls_Arquivos.Where(a => a.TIP_ARQUIVO == 4 && a.COD_STATUS == 5).Count();

                rec_ctrl.QTD_CADASTRAL_CARREGADO = ls_Arquivos.Where(a => a.TIP_ARQUIVO == 1 && a.COD_STATUS >= 8 && a.COD_STATUS <= 9).Count();
                rec_ctrl.QTD_AFASTAMENTO_CARREGADO = ls_Arquivos.Where(a => a.TIP_ARQUIVO == 2 && a.COD_STATUS >= 8 && a.COD_STATUS <= 9).Count();
                rec_ctrl.QTD_ORGAO_LOTACAO_CARREGADO = ls_Arquivos.Where(a => a.TIP_ARQUIVO == 3 && a.COD_STATUS >= 8 && a.COD_STATUS <= 9).Count();
                rec_ctrl.QTD_FINANCEIRO_CARREGADO = ls_Arquivos.Where(a => a.TIP_ARQUIVO == 4 && a.COD_STATUS >= 8 && a.COD_STATUS <= 9).Count();

                string dcr_cadastral = String.Format("Total de Arquivos: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 1).Count().ToString());
                dcr_cadastral += String.Format("Arq. Validados: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 1 && a.COD_STATUS == 5).Count().ToString());
                dcr_cadastral += String.Format("Arq. Carregados: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 1 && a.COD_STATUS >= 8 && a.COD_STATUS <= 9).Count().ToString());                
                rec_ctrl.DCR_QTD_CADASTRAL = dcr_cadastral;

                string dcr_afastamento = String.Format("Total de Arquivos: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 2).Count().ToString());
                dcr_afastamento += String.Format("Arq. Validados: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 2 && a.COD_STATUS == 5).Count().ToString());
                dcr_afastamento += String.Format("Arq. Carregados: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 2 && a.COD_STATUS >= 8 && a.COD_STATUS <= 9).Count().ToString());
                rec_ctrl.DCR_QTD_AFASTAMENTO = dcr_afastamento;

                string dcr_orgao_lotacao = String.Format("Total de Arquivos: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 3).Count().ToString());
                dcr_orgao_lotacao += String.Format("Arq. Validados: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 3 && a.COD_STATUS == 5).Count().ToString());
                dcr_orgao_lotacao += String.Format("Arq. Carregados: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 3 && a.COD_STATUS >= 8 && a.COD_STATUS <= 9).Count().ToString());
                rec_ctrl.DCR_QTD_ORGAO_LOTACAO = dcr_orgao_lotacao;

                string dcr_financeiro = String.Format("Total de Arquivos: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 4).Count().ToString());
                dcr_financeiro += String.Format("Arq. Validados: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 4 && a.COD_STATUS == 5).Count().ToString());
                dcr_financeiro += String.Format("Arq. Carregados: {0}" + Environment.NewLine, ls_Arquivos.Where(a => a.TIP_ARQUIVO == 4 && a.COD_STATUS >= 8 && a.COD_STATUS <= 9).Count().ToString());
                rec_ctrl.DCR_QTD_FINANCEIRO = dcr_financeiro;

            }

            return ls_RECEBIDOS_CONTROLE;

        }

        private void enviar_email_patrocinadora_processamento_concluido(PRE_VIEW_ARQ_PATROCINA arqPatrocinadora, string sRESULTADO, bool Ok)
        {
            Email mail_util = new Email();
            string corpo_email = Util.carrega_resource("IntegWeb.Previdencia.Aplicacao.MODELOS.email_patrocinadora_processamento_concluido.html");
            Stream assinatura = Util.carrega_resource_stream("IntegWeb.Previdencia.Aplicacao.MODELOS.assinatura_email_portal.jpg");

            PRE_TBL_GRUPO_EMPRS ge = base.GetCodigoGrupoEmprs(null, arqPatrocinadora.GRUPO_PORTAL);

            ArqParametrosDAL paramDAL = new ArqParametrosDAL();
            List<ArqParametrosDAL.PRE_TBL_ARQ_PARAM_view> lstPAram = paramDAL.GetWhere("EMAIL_PATROCINADORA", ge.COD_GRUPO_EMPRS, null, null).ToList();
            string emailDestinatario = "guilherme.provenzano@funcesp.com.br";

            if (lstPAram.FirstOrDefault() != null)
            {
                emailDestinatario = lstPAram.FirstOrDefault().DCR_PARAM;
            }

            sRESULTADO = sRESULTADO.Split('[')[0]; // Remove o analytics

            corpo_email = corpo_email.Replace("{BOM_DIA_TARDE}", mail_util.Bom_Dia_Tarde_Noite());
            corpo_email = corpo_email.Replace("{NOM_ARQUIVO}", arqPatrocinadora.NOM_ARQUIVO);
            corpo_email = corpo_email.Replace("{DCR_TIPO}", arqPatrocinadora.DCR_TIPO);
            corpo_email = corpo_email.Replace("{MES_REF}", (arqPatrocinadora.MES_REF ?? 0).ToString("00"));
            corpo_email = corpo_email.Replace("{ANO_REF}", (arqPatrocinadora.ANO_REF ?? 0).ToString("0000"));

            sRESULTADO = sRESULTADO.Replace("\\n", "<br>");

            if (Ok)
            {
                corpo_email = corpo_email.Replace("{RESULTADO}", "<span style='color: #006600;'>" + sRESULTADO + "</span>");
            }
            else
            {
                corpo_email = corpo_email.Replace("{RESULTADO}", "<span style='color: #CC0000;'>" + sRESULTADO + "</span>");
            }

            mail_util.EnviaEmail(emailDestinatario, "Portal Funcesp <atendimento@funcesp.com.br>",
                                 "Portal Funcesp - Processamento do arquivo concluído 100%", corpo_email, "", assinatura, true);
        }

        private void DisparaAlerta(string _log)
        {
            Email mail_util = new Email();
            _log = _log.Replace("\\n", "<br>");
            mail_util.EnviaEmail("guilherme.provenzano@funcesp.com.br", "Robô Troca de Arquivos <atendimento@funcesp.com.br>", "** Alerta de erro no processamento do Troca de Arquivos **", _log, "");
        }

    }
}
