using IntegWeb.Previdencia.Aplicacao.ENTITY;
using System.Collections;
using System.Data;
using System.Data.OracleClient;

namespace IntegWeb.Previdencia.Aplicacao.DAL
{
    public class CadComponentesDAL
    {
        private PREV_Entity_Conn entity = new PREV_Entity_Conn();
        private static string tb;
        OracleConnection conn;

        public CadComponentesDAL()
        {
            conn = new OracleConnection(entity.Database.Connection.ConnectionString);
        }

        public DataTable GetData(string table)
        {
            string query = "SELECT * FROM OWN_INTPROTHEUS.{0} ORDER BY 1 DESC";

            using (OracleDataAdapter adapter = new OracleDataAdapter(string.Format(query, table), entity.Database.Connection.ConnectionString))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                tb = table;
                return dataTable;
            }
        }

        public void SaveData(ref DataTable data, string table, IDictionary oldValues = null)
        {
            try
            {
                OpenConnection();
                DataTable dataInsert = data.GetChanges(DataRowState.Added);
                DataTable dataEdit = data.GetChanges(DataRowState.Modified);
                string query,
                    columnName,
                    paramName,
                    oldParamName,
                    where = string.Empty;

                object value,
                    oldValue;

                OracleCommand command;

                #region Insert
                if (!ReferenceEquals(dataInsert, null))
                {
                    query = string.Format("INSERT INTO OWN_INTPROTHEUS.{0} VALUES ", table);
                    using (command = new OracleCommand(query, conn))
                        for (int row = 0; row < dataInsert.Rows.Count; row++)
                        {
                            command.Parameters.Clear();
                            for (int col = 0; col < dataInsert.Columns.Count; col++)
                            {
                                columnName = dataInsert.Columns[col].ColumnName;
                                paramName = columnName;
                                value = dataInsert.Rows[row][col];

                                command.Parameters.AddWithValue(paramName, value);

                                if (col == 0)
                                    query += string.Format("(:{0}", paramName);
                                else if (col == (dataInsert.Columns.Count - 1))
                                    query += string.Format(", :{0})", paramName);
                                else
                                    query += string.Format(", :{0}", paramName);
                            }
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                }
                #endregion

                #region Update
                if (!ReferenceEquals(dataEdit, null))
                {
                    query = string.Format("UPDATE OWN_INTPROTHEUS.{0} SET ", table);

                    using (command = new OracleCommand(query, conn))
                    {
                        for (int row = 0; row < dataEdit.Rows.Count; row++)
                        {
                            where = string.Empty;

                            for (int col = 0; col < dataEdit.Columns.Count; col++)
                            {
                                columnName = dataEdit.Columns[col].ColumnName;
                                paramName = columnName;
                                oldParamName = string.Format("old_{0}", columnName);

                                value = dataEdit.Rows[row][columnName];
                                oldValue = oldValues[columnName];

                                command.Parameters.AddWithValue(paramName, value);
                                command.Parameters.AddWithValue(oldParamName, oldValue);

                                if (col == 0)
                                {
                                    query += string.Format("{0} = :{1}", columnName, paramName);
                                    where += string.Format("WHERE {0} = :{1}", columnName, oldParamName);
                                }
                                else
                                {
                                    query += string.Format(",{0} = :{1}", columnName, paramName);
                                    where += string.Format(" AND {0} = :{1}", columnName, oldParamName);
                                }
                            }
                            query = string.Join(" ", query, where);
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                    }
                }
                #endregion
            }
            finally
            {
                CloseConnection();
                data.AcceptChanges();
            }
        }

        private void OpenConnection()
        {
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

        private void CloseConnection()
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }
    }
}
