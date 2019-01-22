
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Xml.Serialization;
using System.IO;
using System.Reflection;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.Entity;
using Robot.Entidades;

namespace Robot.Framework
{
    public static class Util
    {
        public static List<ServiceController> GetAllServices(string DisplayName)
        {
            List<ServiceController> ret = new List<ServiceController>();

            // get list of Windows services
            ServiceController[] services = ServiceController.GetServices();

            // try to find service name
            foreach (ServiceController service in services)
            {
                if (!String.IsNullOrEmpty(DisplayName) &&
                    service.DisplayName.IndexOf(DisplayName) > -1)
                {
                    ret.Add(service);
                }
            }

            return ret;
        }

        public static Resultado StartService(string serviceName, int timeoutSeconds)
        {
            Resultado res = new Resultado();

            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromSeconds(timeoutSeconds);

                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                res.Sucesso("Serviço iniciado com sucesso!");
            }
            //catch (InvalidOperationException ex)
            catch (Exception ex)
            {
                res.Erro("Mensagem: " + ex.Message + "\n\nInnerException: " + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
            }

            return res;
        }

        public static Resultado StopService(string serviceName, int timeoutSeconds)
        {
            Resultado res = new Resultado();

            ServiceController service = new ServiceController(serviceName);
            try
            {
                TimeSpan timeout = TimeSpan.FromSeconds(timeoutSeconds);

                service.Stop();
                service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                res.Sucesso("Serviço parado com sucesso!");
            }
            //catch (InvalidOperationException ex)
            catch (Exception ex)
            {
                res.Erro("Mensagem: " + ex.Message + "\n\nInnerException: " + ((ex.InnerException != null) ? ex.InnerException.Message : ""));
            }

            return res;
        }

        public static string LogError(Exception ex)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += Environment.NewLine;
            //string path = System.Web.HttpContext.Current.Server.MapPath("~\\UploadFile");
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //if (!Directory.Exists(path)) path = optional_path;
            var fullpath = Path.Combine(path, "error.log");

            String2File(message, fullpath);

            return message;
        }

        public static string Log(String titulo, String texto)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "--" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "--[ " + titulo + " ]-------------------------------------------------------" + Environment.NewLine;
            message += texto;

            //string path = System.Web.HttpContext.Current.Server.MapPath("~\\UploadFile");
            string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            //if (!Directory.Exists(path)) path = System.Web.HttpContext.Current.Server.MapPath("~\\");
            var fullpath = Path.Combine(path, "trace.log");     
       
            String2File(message, fullpath);

            return message;
        }

        private static void String2File(String message, String fullpath)
        {
            FileStream fs = new FileStream(fullpath, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static string PreparaEmail(string email)
        {
            email = email.Trim();
            email = email.Replace(';', ',');
            if (email.EndsWith(","))
            {
                email = email.Remove(email.Length - 1);
            }
            return email;
        }

        public static string carrega_resource(string resourceName)
        {
            string result;
            //Assembly assembly = Assembly.GetExecutingAssembly();
            Assembly assembly = Assembly.GetCallingAssembly();
            //var resourceName = "MyCompany.MyProduct.MyFile.txt";

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        public static Stream carrega_resource_stream(string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            return assembly.GetManifestResourceStream(resourceName);
        }

        #region EntityUtil

        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty,
                                  bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = String.IsNullOrEmpty(orderByProperty) ? type.GetProperties().First(p => !p.Name.StartsWith("_")) : type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
                                          source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static IQueryable<TEntity> GetData<TEntity>(this IQueryable<TEntity> source, int startRowIndex, int maximumRows, string sortParameter)
        {
            bool sortDirectionDesc = false;
            if (sortParameter != string.Empty)
            {
                sortDirectionDesc = !sortParameter.EndsWith(" DESC");
                sortParameter = sortParameter.Replace(" DESC", "");
            }
            return (from c in source
                    select c)
                    .OrderBy(sortParameter, sortDirectionDesc)
                    .Skip(startRowIndex)
                    .Take(maximumRows);
        }

        public static int SelectCount<TEntity>(this IQueryable<TEntity> source)
        {
            return (from c in source select c).Count();
        }

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);

                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static string GetInnerException(Exception ex)
        {
            string ret = ex.Message;


            while (ex.Message.IndexOf("See the inner exception for details") > -1 ||
                   ex.Message.IndexOf("Consulte a exceção interna para obter detalhes") > -1)
            {
                ex = ex.InnerException;
            }

            ret = ex.Message;

            //if (ex.Message.IndexOf("See the inner exception for details") > -1)
            //{
            //    ret = ex.Message.Replace("See the inner exception for details.", "Details: " + ex.InnerException.Message);
            //}
            //else
            //{
            //    ret = ex.Message;
            //    //if (ex.InnerException.Message.IndexOf("See the inner exception for details") > -1)
            //    //{
            //    //    ret = ex.InnerException.Message.Replace("See the inner exception for details.", "Details: " + ex.InnerException.InnerException.Message);
            //    //}
            //}

            return ret;
        }

        public static string GetEntityValidationErrors(DbEntityValidationException ex)
        {
            string ret = ex.Message + "\\n";

            foreach (var eve in ex.EntityValidationErrors)
            {
                ret += String.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:\\n", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    ret += String.Format("- Property: \"{0}\", Error: \"{1}\"\\n", ve.PropertyName, ve.ErrorMessage);
                }
            }

            return ret;
        }

        #endregion

        #region XmlSerializerUtil

        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToXmlFile<T>(string fileName, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(Path.Combine(path, fileName), append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T ReadFromXmlFile<T>(string fileName) where T : new()
        {
            TextReader reader = null;
            try
            {
                string path = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(Path.Combine(path, fileName));
                return (T)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                return default(T);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        #endregion
    }
}

