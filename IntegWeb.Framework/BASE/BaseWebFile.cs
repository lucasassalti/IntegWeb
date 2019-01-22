using IntegWeb.Entidades.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace IntegWeb.Framework.Base
{
    public class BaseWebFile : BasePage
    {
        public void ExportToFile()
        {
            if (Session["DtRelatorio"] != null)
            {
                Dictionary<string, DataTable> dt = (Dictionary<string, DataTable>)Session["DtRelatorio"];
                if (dt.Count > 0)
                {
                    ExportarArquivo(dt.Keys.ToArray()[0].ToString(),
                                    dt[dt.Keys.ToArray()[0].ToString()]);
                }
            }
        }

        public void ExportToFile(string dwFile)
        {
            if (Session[dwFile] != null)
            {
                ArquivoDownload adArquivo = (ArquivoDownload)Session[dwFile];

                if (adArquivo.dados != null)
                {
                    switch (adArquivo.dados.GetType().Name)
                    {
                        case "DataTable":
                            ExportarArquivo(adArquivo.nome_arquivo,
                                            (DataTable)adArquivo.dados,
                                            adArquivo.opcao_arquivo);
                            break;
                        case "XmlDocument":
                            ResponseXml(adArquivo.nome_arquivo,
                                        (XmlDocument)adArquivo.dados);
                            break;
                        case "String":
                            ResponseHtml(adArquivo.nome_arquivo, (String)adArquivo.dados);
                            break;
                        case "Byte[]":
                            ResponseFile((byte[])adArquivo.dados, adArquivo.nome_arquivo, "", null);
                            break;
                    }

                }
                else
                {
                    ExportarArquivo(adArquivo.nome_arquivo,
                                    adArquivo.caminho_arquivo,
                                    adArquivo.modo_abertura);
                }
            }
        }
    }
}
