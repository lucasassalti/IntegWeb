using IntegWeb.Saude.Aplicacao.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace IntegWeb.Saude.Web
{
    public partial class ControleCassi : System.Web.UI.Page
    {
        private ControleCassiBLL bll = new ControleCassiBLL();
        private DataTable list;

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            list = bll.GetAllParticipantes();
            FillTable();
        }

        protected void Page_UnLoad(object sender, EventArgs e)
        {
            lblBaseImportacaoVal.Text = "";
            pnDevolucaoResult.Controls.Clear();
        }
        #endregion

        #region Methods
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            list = bll.GetAllParticipantes();
            FillTable(txtSearch.Text);
        }

        protected void btnImportDevolucao_Click(object sender, EventArgs e)
        {
            if (!uplDevolucao.HasFile)
            {
                lblDevolucaoVal.Text = "Nenhum arquivo selecionado";
                return;
            }

            HttpPostedFile file = uplDevolucao.PostedFile;
            string fileType = file.ContentType;
            string[] allowedFormats = { "text/plain" };
            bool isValidFormat = allowedFormats.Contains(fileType);

            if (!isValidFormat)
            {
                lblDevolucaoVal.Text = "Formato de arquivo inválido";
                return;
            }

            string path = FileUpload(sender);

            #region Leitura do Arquivo
            ControleCassiBLL.ArquivoDevolucao devolucao;

            try
            {
                string[] fileContent = File.ReadAllLines(path, Encoding.GetEncoding("iso-8859-1"));
                devolucao = new ControleCassiBLL.ArquivoDevolucao
                {
                    TipoRegistro = fileContent[0].Substring(0, 2),
                    Movimento = fileContent[0].Substring(2, 8),
                    Lote = fileContent[0].Substring(10, 6),
                    Contrato = fileContent[0].Substring(16, 10),
                    Rotina = fileContent[0].Substring(26, 10)
                };

                fileContent = fileContent.Skip(1).ToArray();
                devolucao.Participantes = new List<ControleCassiBLL.Participante>();

                for (int i = 0; i < fileContent.Length; i++)
                {
                    ControleCassiBLL.Participante participante = new ControleCassiBLL.Participante();
                    participante.TipoRegistro = fileContent[i].Substring(0, 2);

                    if (participante.TipoRegistro == "99")
                    {
                        devolucao.Total = int.Parse(fileContent[i].Substring(2, 9));
                        continue;
                    }

                    participante.MATRICULA_CASSI = fileContent[i].Substring(2, 9);
                    participante.NOME_PARTICIP = fileContent[i].Substring(11, 70);
                    participante.CARTAO_FUNCESP = fileContent[i].Substring(81, 15);
                    devolucao.Participantes.Add(participante);
                }
            }
            catch
            {
                pnDevolucaoResult.Controls.Add(new Label { Text = "Arquivo fora do layout esperado." });
                return;
            }
            #endregion

            object result = bll.FetchParticipantes(devolucao.Participantes);
            List<ControleCassiBLL.Participante> matches = result.GetType().GetProperty("Matches").GetValue(result) as List<ControleCassiBLL.Participante>;
            List<ControleCassiBLL.Participante> misMatches = result.GetType().GetProperty("MisMatches").GetValue(result) as List<ControleCassiBLL.Participante>;

            #region Matches
            if (matches.Any())
            {
                #region Edit
                List<ControleCassiBLL.Participante> edit = matches.Where(item => item.TipoRegistro == "02").ToList();
                if (edit.Any())
                {
                    devolucao.Participantes = edit;
                    bll.UpdateParticipantesBatch(devolucao);

                    BulletedList lstAlterados = new BulletedList { ID = "lstAlterados" };
                    for (int i = 0; i < edit.Count; i++)
                    {
                        lstAlterados.Items.Add(FormatarLinha(edit[i], 70));
                    }
                    pnDevolucaoResult.Controls.Add(new Label { Text = "Registros atualizados:", ForeColor = System.Drawing.Color.Blue });
                    pnDevolucaoResult.Controls.Add(lstAlterados);
                }
                #endregion

                #region Enable
                List<ControleCassiBLL.Participante> enable = matches.Where(item => item.TipoRegistro == "03").ToList();
                if (enable.Any())
                {
                    devolucao.Participantes = enable.ToList();
                    BulletedList lstHabilitados = new BulletedList { ID = "lstHabilitados" };
                    for (int i = 0; i < enable.Count; i++)
                    {
                        lstHabilitados.Items.Add(FormatarLinha(enable[i], 70));
                    }
                    pnDevolucaoResult.Controls.Add(new Label { Text = "Registros habilitados:", ForeColor = System.Drawing.Color.Blue });
                    pnDevolucaoResult.Controls.Add(lstHabilitados);
                }
                #endregion

                #region Disable
                List<ControleCassiBLL.Participante> disable = matches.Where(item => item.TipoRegistro == "90").ToList();
                if (disable.Any())
                {
                    devolucao.Participantes = disable;
                    BulletedList lstDesabilitados = new BulletedList { ID = "lstDesabilitados" };
                    for (int i = 0; i < disable.Count; i++)
                    {
                        lstDesabilitados.Items.Add(FormatarLinha(disable[i], 70));
                    }
                    pnDevolucaoResult.Controls.Add(new Label { Text = "Registros cancelados:", ForeColor = System.Drawing.Color.Blue });
                    pnDevolucaoResult.Controls.Add(lstDesabilitados);
                }
                #endregion
            }
            #endregion

            #region MisMatches
            if (misMatches.Any())
            {
                #region Insert
                List<ControleCassiBLL.Participante> insert = misMatches.Where(item => item.TipoRegistro == "01").ToList();
                if (insert.Any())
                {
                    insert.ForEach(item => item.SITUACAO = "A");
                    devolucao.Participantes = insert;
                    bll.InsertBatchParticipantes(devolucao);

                    pnDevolucaoResult.Controls.Add(new Label
                    {
                        ForeColor = System.Drawing.Color.Blue,
                        Text = "Registros inseridos:"
                    });

                    BulletedList lstInseridos = new BulletedList { ID = "lstInseridos" };
                    for (int i = 0; i < insert.Count; i++)
                    {
                        lstInseridos.Items.Add(FormatarLinha(insert[i], 70));
                    }
                    pnDevolucaoResult.Controls.Add(lstInseridos);
                }
                #endregion

                #region NotFound
                misMatches.RemoveAll(value => value.TipoRegistro == "01");

                if (misMatches.Any())
                {
                    pnDevolucaoResult.Controls.Add(new Label
                    {
                        ForeColor = System.Drawing.Color.Red,
                        Text = "Registros não encontrados:"
                    });

                    BulletedList lstLost = new BulletedList { ID = "lstLost" };
                    for (int i = 0; i < insert.Count; i++)
                    {
                        lstLost.Items.Add(FormatarLinha(insert[i], 70));
                    }
                    pnDevolucaoResult.Controls.Add(lstLost);
                }
                #endregion
            }
            #endregion
        }

        protected void btnImportBase_Click(object sender, EventArgs e)
        {
            HttpPostedFile file = uplBase.PostedFile;

            if (ReferenceEquals(file, null))
            {
                lblBaseImportacaoVal.Text = "Nenhum arquivo selecionado";
                return;
            }

            string fileType = file.ContentType;
            string[] allowedFormats = { "application/vnd.ms-excel",
                                          "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" };
            bool isValidFormat = allowedFormats.Contains(fileType);

            if (!isValidFormat)
            {
                lblDevolucaoVal.Text = "Formato de arquivo inválido";
                return;
            }

            string path = FileUpload(sender);
            if (string.IsNullOrEmpty(path)) return;

            #region MyRegion
            /*
             
            var excel = new ExcelQueryFactory(path);
            var result = (from sheet in excel.GetWorksheetNames()
                          from row in excel.WorksheetNoHeader(sheet).Skip(4)
                          where row[7].Value.ToString().ToLower().Equals("normal")
                          select ((Func<dynamic>)delegate
                          {
                              dynamic assegurado = new ExpandoObject();
                              assegurado.Nome = row[0].Value;
                              assegurado.Familia = row[2].Value;
                              assegurado.Dependente = row[3].Value;
                              assegurado.Adesao = row[4].Value;
                              assegurado.Cancelamento = row[5].Value;
                              assegurado.Cartao = row[6].Value;
                              assegurado.Situacao = row[7].Value;
                              assegurado.ValidadeInicio = row[8].Value;
                              assegurado.ValidadeFim = row[9].Value;
                              assegurado.UF = row[10].Value;
                              assegurado.Nascimento = row[11].Value;
                              assegurado.CPF = row[12].Value;
                              assegurado.Lotacao = row[13].Value;

                              return assegurado;
                          }).Invoke()).AsEnumerable();

            ViewState["fileName"] = file.FileName;
            ViewState["fileRows"] = result;*/
            #endregion

            Server.Transfer(Request.Url.LocalPath);
        }

        protected void gvParticipantes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvParticipantes.PageIndex = e.NewPageIndex;
            ViewState["IsListFiltered"] = false;
            FillTable();
        }

        protected void gvParticipantes_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sort = !ReferenceEquals(ViewState["gvParticipantesDirection"], null) ? ViewState["gvParticipantesDirection"].ToString() : string.Empty;

            if (string.IsNullOrEmpty(sort))
            {
                switch (e.SortDirection)
                {
                    case SortDirection.Ascending: sort = "DESC"; break;
                    case SortDirection.Descending: sort = "ASC"; break;
                }
            }
            else
                switch (sort)
                {
                    case "ASC": sort = "DESC"; break;
                    case "DESC": sort = "ASC"; break;
                }

            ViewState["gvParticipantesDirection"] = sort;

            DataView dtView = new DataView(list);
            dtView.Sort = string.Format("{0} {1}", e.SortExpression, sort);

            gvParticipantes.DataSource = dtView;
            gvParticipantes.DataBind();
        }

        private void FillTable(string filter = "")
        {
            //Aplica Filtros
            if (!string.IsNullOrEmpty(filter))
            {
                string exp = Regex.Replace(filter, @"[^0-9]", "");

                if (Regex.IsMatch(filter, @"[0-9]{3}.[0-9]{3}.[0-9]{3}(-|/)[0-9]{2}") ||
                    exp.Length == 11)
                {
                    list = bll.GetAllParticipantes(item => Regex.Replace(item.NUM_CPF, @"[^0-9]", "").Equals(exp));
                }
                else if (exp.Length > 0)
                    list = bll.GetAllParticipantes(item => item.CARTAO_FUNCESP.Equals(exp));
                else
                    list = bll.GetAllParticipantes(Regex.Replace(filter, @"[0-9]", "").ToLower());

                ViewState["IsListFiltered"] = true;
            }
            //Limpa Filtros
            else if (!ReferenceEquals(ViewState["IsListFiltered"], null))
            {
                bool filtered = false;

                bool.TryParse(ViewState["IsListFiltered"].ToString(), out filtered);
                if (filtered)
                {
                    list = bll.GetAllParticipantes();

                    ViewState["IsListFiltered"] = false;
                }
            }

            gvParticipantes.DataSource = list;
            gvParticipantes.DataBind();
        }

        private string FileUpload(object sender)
        {
            string path = string.Empty;

            if (Request.Files.Count > 0)
            {
                string commandArg = sender.GetType().GetProperty("CommandArgument").GetValue(sender).ToString();
                string fileKey = Request.Files.AllKeys.Where(item => item.Contains(commandArg)).FirstOrDefault();

                HttpPostedFile file = Request.Files[fileKey];

                if (!(ReferenceEquals(file, null) || file.ContentLength < 1))
                {
                    string tempPath = Path.GetTempPath();
                    path = string.Join(@"\", tempPath, file.FileName);

                    if (File.Exists(path))
                        File.Delete(path);

                    file.SaveAs(path);
                }
            }
            return path;
        }

        private string FormatarLinha(ControleCassiBLL.Participante participante, int tamanho)
        {
            string[] length = new string[(tamanho + 1) - participante.NOME_PARTICIP.Length];
            StringBuilder builder = new StringBuilder();
            builder.Append(participante.TipoRegistro ?? "00");
            builder.Append(participante.MATRICULA_CASSI);
            builder.Append(participante.NOME_PARTICIP + string.Join(" ", length));
            builder.Append(participante.CARTAO_FUNCESP);

            builder.Append(participante.SITUACAO);
            return builder.ToString();
        }
        #endregion

        protected void btnCassiMovimentacao_Click(object sender, EventArgs e)
        {
            bll.PRC_CASSI_MOVIMENTACAO();
        }
    }
}