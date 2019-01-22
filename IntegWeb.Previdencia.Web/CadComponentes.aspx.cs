using IntegWeb.Previdencia.Aplicacao.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace IntegWeb.Previdencia.Web
{
    public partial class CadComponentes : System.Web.UI.Page
    {
        CadComponentesDAL dal = new CadComponentesDAL();
        JavaScriptSerializer serial = new JavaScriptSerializer();

        private DataTable _data;

        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //Verifica se existem valores na requisição
                string eventArgs = Request["__EVENTARGUMENT"];
                List<IDictionary> _params = null;

                //Seta os Args da ViewState via JS
                if (!string.IsNullOrEmpty(eventArgs) &&
                    !eventArgs.ToLower().Contains("page"))
                {
                    //Deserializa os valores para leitura
                    _params = serial.Deserialize<List<IDictionary>>(eventArgs);

                    if (!ReferenceEquals(_params[0]["Key"], null))
                        ViewState["Args"] = serial.Serialize(new { Key = _params[0]["Key"] });
                }

                _data = (ViewState["DataFilter"] ?? ViewState["DataDefault"]) as DataTable;

                //Verifica se existem parâmetros serializados no ViewState
                if (!ReferenceEquals(ViewState["Args"], null))
                {
                    //Deserializa os paramêtros para utilização
                    IDictionary args = serial.Deserialize<IDictionary>(ViewState["Args"].ToString());

                    //Obtém o parâmetro Key
                    string key = args["Key"].ToString();

                    switch (key)
                    {
                        #region CreateForm
                        case "createForm":
                            if (!ReferenceEquals(_params, null))
                            {
                                //Reseta os filtros
                                _data = ViewState["DataDefault"] as DataTable;

                                //Cria uma nova linha
                                DataRow newRow = _data.NewRow();

                                //Insere os valores na linha criada de acordo com as células do GridView
                                for (int i = 0; i < _data.Columns.Count; i++)
                                {
                                    string coluna = _data.Columns[i].ColumnName;
                                    string value = _params[i][coluna].ToString();

                                    newRow.SetField<string>(i, value);
                                }

                                //Insere a nova linha no GridView
                                _data.Rows.InsertAt(newRow, 0);
                                dal.SaveData(ref _data, ddlTabelas.SelectedValue);

                                gvData.DataSource = _data;
                                gvData.DataBind();

                                hdnResponse.Value = "Dados salvos com sucesso!";

                                ViewState["Args"] = serial.Serialize(new { Key = "default" });
                                HideForm();
                            }
                            else
                                CreateForm();
                            break;
                        #endregion

                        case "clearFilter":
                            _data = ViewState["DataDefault"] as DataTable;
                            ViewState["DataFilter"] = null;
                            ViewState["Args"] = serial.Serialize(new { Key = "default" });
                            HideForm();
                            FillTable(_data);
                            break;

                        case "default":
                            FillTable(_data);
                            break;

                        case "rowEditing":
                            //Não chama o método Filltable, para não perder os valores atualizados
                            break;
                    }

                    if (!ReferenceEquals(_data, null))
                    {
                        CreateFilters();
                        CreateGridControls();
                    }
                }
            }
        }
        #endregion

        #region Methods
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string tabela = ddlTabelas.SelectedValue;
            _data = dal.GetData(tabela);

            ViewState["DataDefault"] = _data;
            ViewState["DataFilter"] = null;
            ViewState["Args"] = serial.Serialize(new { Key = "default" });
            Page_Load(sender, e);
        }

        protected void btnFilter_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            DropDownList ddl = btn.Parent.FindControl("ddlFilter") as DropDownList;
            string column = ddl.SelectedValue;

            TextBox txt = btn.Parent.FindControl("txtFilter") as TextBox;
            string value = txt.Text.Trim();

            DataTable data = ViewState["DataDefault"] as DataTable;

            try
            {
                DataRow[] rows = data.Select(string.Format("{0} = '{1}'", column, value));

                if (rows.Any())
                {
                    data = rows.CopyToDataTable();
                    ViewState["DataFilter"] = data;
                    ViewState["Args"] = serial.Serialize(new { Key = "default" });
                    Page_Load(sender, e);
                }
                else
                    hdnResponse.Value = "Nenhum registro encontrado";
            }
            catch
            {
                hdnResponse.Value = "Operação inválida";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            ViewState["Args"] = serial.Serialize(new { Key = "createForm" });
            Page_Load(sender, e);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            HideForm();
            ViewState["Args"] = serial.Serialize(new { Key = "default" });
            Page_Load(sender, e);
        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            gvData.EditIndex = -1;

            ViewState["Args"] = serial.Serialize(new { Key = "default" });
            HideForm();

            Page_Load(sender, e);
        }

        protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ViewState["Args"] = serial.Serialize(new { Key = "rowEditing" });

            FillTable(_data);
            gvData.EditIndex = e.NewEditIndex;
            gvData.DataBind();
            CreateGridControls();

            TableRow row = gvData.Rows[e.NewEditIndex];

            for (int i = 0; i < _data.Columns.Count; i++)
            {
                string columnName = _data.Columns[i].ColumnName;
                string columnType = _data.Columns[i].DataType.Name;

                TextBox txt = row.Cells[i].Controls[0] as TextBox;
                txt.Attributes.Add("data-type", columnType);

                bool nullable = ColumnIsNullable(columnName);
                if (!nullable)
                    txt.Attributes.Add("required", "true");
            }
        }

        protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvData.EditIndex = -1;
            ViewState["Args"] = serial.Serialize(new { Key = "default" });
            Page_Load(sender, e);
        }

        protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Dictionary<string, object> oldValues = new Dictionary<string, object>();
            int index = 0;

            if (gvData.PageIndex > 1)
                index = gvData.PageIndex * gvData.PageSize + gvData.EditIndex;
            else
                index = gvData.EditIndex;

            DataRow editRow = _data.Rows[index];

            for (int i = 0; i < _data.Columns.Count; i++)
            {
                string column = _data.Columns[i].ColumnName;
                Type type = _data.Columns[i].DataType;

                GridViewRow gvRow = gvData.Rows[gvData.EditIndex];
                object value = (gvRow.Cells[i].Controls[0] as TextBox).Text;

                oldValues.Add(column, editRow[column]);
                editRow[column] = string.IsNullOrEmpty(value.ToString().Trim()) ? DBNull.Value : value;
            }

            dal.SaveData(ref _data, ddlTabelas.SelectedValue, oldValues);
            gvData.EditIndex = -1;
            hdnResponse.Value = "Dados salvos com sucesso!";

            ViewState["Args"] = serial.Serialize(new { Key = "default" });
            Page_Load(sender, e);
        }
        #endregion

        #region Private
        private void FillTable(DataTable data)
        {
            gvData.DataSource = data;
            gvData.AutoGenerateColumns = true;
            gvData.DataBind();
            ShowPanels();
        }

        private void CreateFilters()
        {
            trFilter.Controls.Clear();

            Label lblFilter = new Label();
            lblFilter.ID = "lblFilter";
            lblFilter.Text = "Pesquisar: ";

            TableCell tdLabel = new TableCell();
            tdLabel.Controls.Add(lblFilter);
            trFilter.Controls.Add(tdLabel);

            DropDownList ddlFilter = new DropDownList();
            ddlFilter.ID = "ddlFilter";
            ddlFilter.ClientIDMode = System.Web.UI.ClientIDMode.Static;

            ddlFilter.Items.Add(new ListItem { Text = "Selecione...", Value = "" });

            for (int i = 0; i < _data.Columns.Count; i++)
            {
                ListItem item = new ListItem();
                item.Text = _data.Columns[i].ColumnName;
                item.Value = _data.Columns[i].ColumnName;
                item.Attributes.Add("data-type", _data.Columns[i].DataType.Name);

                ddlFilter.Items.Add(item);
            }

            TableCell tdDropDown = new TableCell();
            tdDropDown.Controls.Add(ddlFilter);
            trFilter.Controls.Add(tdDropDown);

            TextBox txtFilter = new TextBox();
            txtFilter.ID = "txtFilter";
            txtFilter.ClientIDMode = System.Web.UI.ClientIDMode.Static;

            TableCell tdTextBox = new TableCell();
            tdTextBox.Controls.Add(txtFilter);
            trFilter.Controls.Add(tdTextBox);

            Button btnFilter = new Button();
            btnFilter.ID = "btnFilter";
            btnFilter.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            btnFilter.CssClass = "button";
            btnFilter.Text = "Enviar";
            btnFilter.Click += new EventHandler(btnFilter_Click);
            btnFilter.OnClientClick = "return filter(this)";

            Button btnReset = new Button();
            btnReset.ID = "btnFilterReset";
            btnReset.ClientIDMode = System.Web.UI.ClientIDMode.Static;
            btnReset.CssClass = "button";
            btnReset.Text = "Limpar pesquisa";

            TableCell tdButtons = new TableCell();
            tdButtons.Controls.Add(btnFilter);
            tdButtons.Controls.Add(btnReset);
            trFilter.Controls.Add(tdButtons);
        }

        private void CreateGridControls()
        {
            for (int i = 0; i < gvData.Rows.Count; i++)
            {
                if (gvData.Rows[i].Cells.Count > _data.Columns.Count) break;

                TableCell cell = new TableCell();
                LinkButton lnkEdit = new LinkButton();

                lnkEdit.Text = "Editar";
                lnkEdit.CommandName = "Edit";

                cell.Controls.Add(lnkEdit);

                if (i == gvData.EditIndex)
                {
                    lnkEdit.Text = "Salvar";
                    lnkEdit.CommandName = "Update";
                    lnkEdit.OnClientClick = "return rowValidate(this);";
                    LinkButton lnkCancel = new LinkButton();

                    lnkCancel.Text = "Cancelar";
                    lnkCancel.CommandName = "Cancel";
                    lnkCancel.Attributes.Add("style", "padding-left: 10px;");

                    cell.Controls.Add(lnkCancel);
                }
                gvData.Rows[i].Cells.Add(cell);
            }
        }

        private void CreateForm()
        {
            trForm.Visible = true;
            tblForm.Controls.Clear();
            TableRow tr = new TableRow();
            for (int i = 0; i < _data.Columns.Count; i++)
            {
                tr = new TableRow();
                string columnName = _data.Columns[i].ColumnName;
                string columnType = _data.Columns[i].DataType.Name;

                if (string.IsNullOrEmpty(columnName)) continue;

                Label lblField = new Label();
                lblField.ID = "lblNew_" + columnName;
                lblField.Text = gvData.HeaderRow.Cells[i].Text + ": ";

                TableCell tdLabel = new TableCell();
                tdLabel.Controls.Add(lblField);
                tr.Controls.Add(tdLabel);

                TextBox txtField = new TextBox();
                txtField.ID = "txtNew_" + columnName;
                txtField.Attributes.Add("data-key", columnName);
                txtField.Attributes.Add("data-type", columnType);

                TableCell tdTextBox = new TableCell();
                tdTextBox.Controls.Add(txtField);
                tr.Controls.Add(tdTextBox);

                bool isNullable = ColumnIsNullable(columnName);
                if (!isNullable)
                {
                    txtField.Attributes.Add("required", "required");

                    Label lblReq = new Label();
                    lblReq.Text = "*Campo obrigatório";
                    lblReq.Font.Italic = true;

                    TableCell tdLabelReq = new TableCell();
                    tdLabelReq.Controls.Add(lblReq);
                    tr.Controls.Add(tdLabelReq);
                }
                else
                    tdTextBox.ColumnSpan = 2;

                tblForm.Controls.Add(tr);
            }

            tr = new TableRow();
            Button btnNew = new Button();
            btnNew.Text = "Salvar";
            btnNew.ID = "btnNew";
            btnNew.CssClass = "button";
            btnNew.ClientIDMode = ClientIDMode.Static;
            btnNew.OnClientClick = "return createRow(this)";

            Button btnCancel = new Button();
            btnCancel.Text = "Cancelar";
            btnCancel.CssClass = "button";
            btnCancel.CausesValidation = false;
            btnCancel.UseSubmitBehavior = false;
            btnCancel.Click += new EventHandler(btnCancel_Click);

            TableCell tbButtons = new TableCell();
            tbButtons.Controls.Add(btnNew);
            tbButtons.Controls.Add(btnCancel);

            tr.Controls.Add(tbButtons);
            tblForm.Controls.Add(tr);
        }

        private void HideForm()
        {
            trForm.Controls.Clear();
            trForm.Visible = false;
        }

        private void ShowPanels()
        {
            pnlGrid.Visible = true;
            tblAdd.Visible = true;
            trFilter.Visible = true;
        }

        private bool ColumnIsNullable(string columnName)
        {
            IEnumerable<DataRow> list = _data.AsEnumerable();

            bool nullable = list.Any(item => string.IsNullOrEmpty(item[columnName].ToString().Trim()));
            return nullable;
        }
        #endregion
    }
}