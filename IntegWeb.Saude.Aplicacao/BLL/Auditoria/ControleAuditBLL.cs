using IntegWeb.Entidades.Saude.Auditoria;
using IntegWeb.Saude.Aplicacao.DAL.Auditoria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao.BLL.Auditoria
{
    public class ControleAuditBLL
    {
        public bool ImportaDados(DataTable dt, AuditControle obj)
        {
            //Deletar o mês ano atual se houver
            Deletar(obj);
        
            dt.Columns.Add("RESPONSAVEL");
            dt.Columns.Add("ID_EMPAUDIT");
            dt.Columns.Add("MESANO");

            DataColumn dc = new DataColumn();
            dc.DataType = Type.GetType("System.DateTime");
            dc.ColumnName = "DT_INCLUSAO";
            dt.Columns.Add(dc);

            foreach (DataRow dr in dt.Rows)
            {
                dr["DT_INCLUSAO"] = DateTime.Now;
                dr["RESPONSAVEL"] = obj.responsavel;
                dr["MESANO"] = obj.mesano;
                dr["ID_EMPAUDIT"] = obj.id_empaudit;
            }

            DataTable dtCloned = dt.Clone();
            dtCloned.Columns[4].DataType = typeof(DateTime);
            dtCloned.Columns[5].DataType = typeof(DateTime);
            dtCloned.Columns[6].DataType = typeof(Decimal);
            dtCloned.Columns[7].DataType = typeof(Decimal);
            dtCloned.Columns[8].DataType = typeof(Decimal);
            dtCloned.Columns[9].DataType = typeof(Decimal);


            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }

            return new ControleAuditDAL().ImportaDados(dtCloned);

        }

        public bool Deletar(AuditControle OBJ)
        {
            return new ControleAuditDAL().Deletar(OBJ);
        }

        public DataTable ListaProcesso(AuditControle objM)
        {
            return new ControleAuditDAL().ListaProcesso(objM); 
        }
    }
}
