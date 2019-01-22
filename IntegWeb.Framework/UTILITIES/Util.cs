using IntegWeb.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Globalization;
using System.Threading;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.Entity;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Configuration;

namespace IntegWeb.Framework
{
    public static class Util
    {
        public static string GetUrlPortal()
        {
            string config = ConfigurationManager.AppSettings["Config"] ?? string.Empty;
            string url = string.Empty;

            if (config == "D")
                url = "localhost:61022";
            else if (config == "T")
                url = "integportal.funcesp.com.br/Homolog";
            else
                url = "integweb.funcesp.com.br/Prod";

            return url;
        }

        public static string GetCapitalize(string termo)
        {
            return Regex.Replace(termo.ToLower(), @"(^\w)|(\s\w)", palavra => palavra.Value.ToUpper());
        }

        public static string LimparCNPJ(string cnpj)
        {
            return cnpj.Replace(".", "").Replace("/", "").Replace("-", "");
        }

        public static string LimparCPF(string cpf)
        {
            return cpf.Replace(".", "").Replace("/", "").Replace("-", "");
        }

        public static string FormatarCNPJ(string cnpj)
        {
            return String.Format(@"{0:00\.000\.000\/0000\-00}", Convert.ToInt64(cnpj));
        }

        public static string FormatarCPF(string cpf)
        {
            return String.Format(@"{0:000\.000\.000\-00}", Convert.ToInt64(cpf));
        }

        public static string FormatarData(DateTime data)
        {
            return data.ToString("dd/MM/yyyy");
        }

        public static string FormatarValorMoeda(decimal valor)
        {
            return String.Format("{0:F}", valor);
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

        public static bool ValidaEmail(string email)
        {
            bool result = false;
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex rg = new Regex(strRegex);

            email = PreparaEmail(email);

            string[] aEmail = email.Split(',');
            //int i = 0;
            for (int i = 0; i < aEmail.Length; i++)
            //while (rg.IsMatch(aEmail[i]) && (i < aEmail.Length))
            {
                email = aEmail[i].ToString().Trim();
                result = rg.IsMatch(email);
                if (!result) break;
            }

            return result;
        }

        public static DateTime PrimeiroDiaMes(DateTime source)
        {
            DateTime dtRet = new DateTime(source.Year, source.Month, 1);
            return dtRet;
        }

        public static DateTime UltimoDiaMes(DateTime source)
        {
            int UltimoDiaDoMes = DateTime.DaysInMonth(source.Year, source.Month);
            DateTime dtRet = new DateTime(source.Year, source.Month, UltimoDiaDoMes);
            return dtRet;
        }

        public static DateTime? ToDateTimeCustom(String source, String customformat)
        {
            DateTime? dtRet = null;
            try
            {
                dtRet = DateTime.ParseExact(source, customformat, null);
            }
            catch { };
            return dtRet;
        }

        public static string String2Limit(string DCR_CRITICA, int startIndex, int length)
        {
            return DCR_CRITICA.PadRight(length, ' ').Substring(startIndex, length).Trim();
        }

        public static DateTime? String2Date(string valor)
        {
            DateTime dtReturn;
            if (DateTime.TryParse(valor, out dtReturn))
            {
                return dtReturn;
            }
            else
            {
                return null;
            }
        }

        public static Decimal? String2Decimal(string valor)
        {
            Decimal dcReturn;
            if (Decimal.TryParse(valor, out dcReturn))
            {
                return dcReturn;
            }
            else
            {
                return null;
            }
        }

        public static Int32? String2Int32(string valor)
        {
            Int32 iReturn;
            if (Int32.TryParse(valor, out iReturn))
            {
                return iReturn;
            }
            else
            {
                return null;
            }
        }

        public static short? String2Short(string valor)
        {
            short iReturn;
            if (short.TryParse(valor, out iReturn))
            {
                return iReturn;
            }
            else
            {
                return null;
            }
        }

        public static Int64? String2Int64(string valor)
        {
            Int64 iReturn;
            if (Int64.TryParse(valor, out iReturn))
            {
                return iReturn;
            }
            else
            {
                return null;
            }
        }

        public static string Date2String(DateTime? valor, string mascara = "dd/MM/yyyy")
        {
            if (valor != null)
            {
                return DateTime.Parse(valor.ToString()).ToString(mascara);
            }
            else
            {
                return "";
            }
        }

        public static string DataRow2String(DataRow dr, string nome_coluna)
        {
            if (dr.Table.Columns.IndexOf(nome_coluna) > -1)
            {
                return (dr[nome_coluna] ?? "").ToString();
            }
            else
            {
                return "";
            }
        }

        public static long? TryParseLong(string text)
        {
            int value;
            if (int.TryParse(text, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public static short? TryParseShort(string text)
        {
            short value;
            if (short.TryParse(text, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        public static int IndexOf(short[] arr, short value)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == value)
                {
                    return i;
                }
            }
            return -1;
        }

        public static string UTF8_XML_DECODE(string InnerXml)
        {
            string strRetorno = InnerXml;
            strRetorno = RemoverAcentuacao(strRetorno);
            strRetorno = strRetorno.Replace("\"", "&quot;");
            strRetorno = strRetorno.Replace("'", "&apos;");
            strRetorno = strRetorno.Replace("&", "&amp;");
            strRetorno = strRetorno.Replace("<", "&lt;");
            strRetorno = strRetorno.Replace(">", "&gt;");
            return strRetorno.Trim();
        }

        public static string RemoverAcentuacao(string text)
        {
            return new string(text
                .Normalize(NormalizationForm.FormD)
                .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                .ToArray());
        }

        public static Resultado XmlValidator(string sXML, string sXSD)
        {
            Resultado res = new Resultado();

            XmlSchemaSet Xss = new XmlSchemaSet();
            Xss.Add("", XmlReader.Create(new StringReader(sXSD)));

            XDocument doc1 = XDocument.Parse(sXML);
            string err = "";
            bool errors = false;
            doc1.Validate(Xss, (o, e) =>
            {
                err += e.Message;
                errors = true;
            });

            if (errors)
            {
                res.Erro(err);
            }
            else
            {
                res.Sucesso(string.Empty);
            }

            return res;
        }

        public static void LogError(Exception ex)
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

            string path = System.Web.HttpContext.Current.Server.MapPath("~\\UploadFile");
            if (!Directory.Exists(path)) path = System.Web.HttpContext.Current.Server.MapPath("~\\");
            var fullpath = Path.Combine(path, "error.log");

            String2File(message, fullpath);
        }

        public static void Log(String titulo, String texto)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            message += Environment.NewLine;
            message += "--" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "--[ " + titulo + " ]-------------------------------------------------------" + Environment.NewLine;
            message += texto;

            string path = System.Web.HttpContext.Current.Server.MapPath("~\\UploadFile");
            if (!Directory.Exists(path)) path = System.Web.HttpContext.Current.Server.MapPath("~\\");
            var fullpath = Path.Combine(path, "trace.log");

            String2File(message, fullpath);

        }

        public static void String2File(String message, String fullpath, bool newline = true)
        {
            FileStream fs = new FileStream(fullpath, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            if (newline)
            {
                sw.WriteLine(message);
            }
            else
            {
                sw.Write(message);
            }
            sw.Flush();
            sw.Close();
            fs.Close();
        }

        public static byte[] File2Memory(string filePath)
        {
            int trys = 0;
            byte[] bytes = new byte[0];
            bool fail = true;

            while (fail && trys < 3)
            {
                try
                {
                    using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                    }

                    fail = false;
                }
                catch
                {
                    Thread.Sleep(500);
                    trys++;
                }
            }

            System.IO.File.Delete(filePath);
            return bytes;
        }

        public static String HashGenerico(String sequencia)
        {

            string _ret = "";
            byte[] HashValue;

            //string MessageString = "This is the original message!";

            //Create a new instance of the UnicodeEncoding class to 
            //convert the string into an array of Unicode bytes.
            UnicodeEncoding UE = new UnicodeEncoding();

            //Convert the string into an array of bytes.
            byte[] MessageBytes = UE.GetBytes(sequencia);

            //Create a new instance of the SHA1Managed class to create 
            //the hash value.
            SHA1Managed SHhash = new SHA1Managed();

            //Create the hash value from the array of bytes.
            HashValue = SHhash.ComputeHash(MessageBytes);

            //Display the hash value to the console. 
            foreach (byte b in HashValue)
            {
                _ret += String.Format("{0} ", b);
            }

            return _ret;

        }

        public static String HashMD5_to_hex(String sequencia)
        {
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(sequencia));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
                //sBuilder.Append(String.Format("{0} ", data[i]));
            }
            return sBuilder.ToString();
        }

        public static long HashMD5_to_long(String sequencia)
        {
            long _ret = 0;
            MD5 md5Hash = MD5.Create();
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(sequencia));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                long dig;
                long.TryParse(data[i].ToString(), out dig);
                _ret += dig;
                //sBuilder.Append(String.Format("{0} ", data[i]));
            }
            return _ret;
        }

        public static long HashSHA_to_long(String sequencia)
        {

            long _ret = 0;
            byte[] HashValue;

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] MessageBytes = UE.GetBytes(sequencia);

            //SHA1Managed SHhash = new SHA1Managed();
            SHA256Managed SHhash = new SHA256Managed();

            HashValue = SHhash.ComputeHash(MessageBytes);

            foreach (byte b in HashValue)
            {
                long dig;
                long.TryParse(b.ToString(), out dig);
                _ret += dig;
            }

            return _ret;

        }

        public static string Object2XML(object o)
        {
            string ret = null;
            try
            {
                XmlSerializer serializer = new XmlSerializer(o.GetType());
                StringWriter sw = new StringWriter();
                XmlTextWriter tw = new XmlTextWriter(sw);
                serializer.Serialize(tw, o);
                ret = sw.ToString();
                sw.Close();
                tw.Close();
            }
            catch (Exception ex)
            {
                //Handle Exception Code
            }
            return ret;
        }

        public static Object Object2XML(string XmlOfAnObject, Type ObjectType)
        {
            Object AnObject = null;
            StringReader StrReader = new StringReader(XmlOfAnObject);
            XmlSerializer serializer = new XmlSerializer(ObjectType);
            XmlTextReader XmlReader = new XmlTextReader(StrReader);
            try
            {
                AnObject = serializer.Deserialize(XmlReader);
                return AnObject;
            }
            catch (Exception exp)
            {
                //Handle Exception Code
            }
            finally
            {
                XmlReader.Close();
                StrReader.Close();
            }
            return AnObject;
        }

        public static byte[] ToByteArray(this Stream stream)
        {
            stream.Position = 0;
            byte[] buffer = new byte[stream.Length];
            for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
                totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
            return buffer;
        }

        //public static void ProcessXcopy(string SolutionDirectory, string TargetDirectory)
        //{
        //    // Use ProcessStartInfo class
        //    ProcessStartInfo startInfo = new ProcessStartInfo();
        //    startInfo.CreateNoWindow = false;
        //    startInfo.UseShellExecute = false;
        //    //Give the name as Xcopy
        //    startInfo.FileName = "xcopy";
        //    //make the window Hidden
        //    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //    //Send the Source and destination as Arguments to the process
        //    startInfo.Arguments = "\"" + SolutionDirectory + "\"" + " " + "\"" + TargetDirectory + "\"" + @" /e /y /I /EXCLUDE:XcopyExcludes.txt";
        //    try
        //    {
        //        // Start the process with the info we specified.
        //        // Call WaitForExit and then the using statement will close.
        //        using (Process exeProcess = Process.Start(startInfo))
        //        {
        //            exeProcess.WaitForExit();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }
        //}

        public static string carrega_resource(string resourceName)
        {
            string result;
            //Assembly assembly = Assembly.GetExecutingAssembly();
            Assembly assembly = Assembly.GetCallingAssembly();
            Stream stream = assembly.GetManifestResourceStream(resourceName);
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }
            stream.Dispose();
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

        //public static int GetMaxPK2<TEntity>(this IQueryable<TEntity> source, string PK_property)
        //{
        //    int maxPK = 0;
        //    string command = "Max";
        //    var type = typeof(TEntity);
        //    var property = String.IsNullOrEmpty(PK_property) ? type.GetProperties().First() : type.GetProperty(PK_property);
        //    var parameter = Expression.Parameter(type, "p");
        //    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //    var orderByExpression = Expression.Lambda(propertyAccess, parameter);

        //    var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType },
        //                                  source.Expression, Expression.Quote(orderByExpression));

        //    Expression callExpr = Expression.Call(
        //        Expression.Constant("sample string"), typeof(String).GetMethod("ToUpper", new Type[] { }));

        //    string teste = Expression.Lambda<Func<String>>(callExpr).Compile()();

        //    //var teste = source.Provider.CreateQuery<TEntity>(resultExpression);

        //    return maxPK;

        //}

        //public static IQueryable<T> Where2<T>(
        //    this IQueryable<T> source, string columnName, string keyword)
        //{
        //    var arg = Expression.Parameter(typeof(T), "p");

        //    var body = Expression.Call(
        //        Expression.Property(arg, columnName),
        //        "Contains",
        //        null,
        //        Expression.Constant(keyword));

        //    var predicate = Expression.Lambda<Func<T, bool>>(body, arg);

        //    return source.Where(predicate);
        //}

        //public static IQueryable<T> GetMaxPK<T>(
        //    this IQueryable<T> source, string columnName, string keyword)
        //{
        //    var arg = Expression.Parameter(typeof(T), "p");

        //    var body = Expression.Call(
        //        Expression.Property(arg, columnName),
        //        "Max",
        //        null,
        //        Expression.Constant(keyword));

        //    var predicate = Expression.Lambda<Func<T, int>>(body, arg);

        //    return null; // source.Max(predicate);
        //}

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

        // Validação aceita números de 0 - 9 e vírgula ','. Utilizado para validar Double.
        public static Boolean validarCampoNumerico(String variavel)
        {
            Regex regExpCampoNumerico = new Regex(@"^[0-9]*(?:\,[0-9]*)?$");
            Match match = regExpCampoNumerico.Match(variavel);

            if (match.Success)
                return true;
            else
                return false;
        }

        #endregion

    }

    /// <summary>
    /// Classe para concatenação de arquivos PDF. 
    /// Utiliza a biblioteca iTextSharp.
    /// </summary>
    /// 
    /// PdfMerge merge = new PdfMerge();
    /// merge.Add("primeiro.pdf");
    /// merge.Add("pasta/segundo.pdf");
    /// merge.Add("terceiro.pdf");
    /// merge.Save("concatenado.pdf");
    /// 
    public class PdfMerge
    {
        /// <summary>
        /// Lista de arquivos a serem concatenados
        /// </summary>
        private List<string> pdfList;

        /// <summary>
        /// Objeto que representa um documento (pdf) do iTextSharp
        /// </summary>
        private Document document;

        /// <summary>
        /// Objeto responsável por salvar o pdf em disco.
        /// </summary>
        private PdfWriter writer;

        /// <summary>
        /// Construtor
        /// </summary>
        public PdfMerge()
        {
            pdfList = new List<string>();
        }

        /// <summary>
        /// Adiciona o arquivo que será concatenado ao PDF final.
        /// </summary>
        /// Caminho para o arquivo PDF
        public void Add(string filePath)
        {
            pdfList.Add(filePath);
        }

        /// <summary>
        /// Adiciona uma lista de arquivos pdf para serem concatenados.
        /// </summary>
        /// Lista contendo o caminho para os arquivos
        public void AddList(List<string> files)
        {
            pdfList.AddRange(files);
        }

        /// <summary>
        /// Concatena os arquivos de entrada, gerando um novo arquivo PDF.
        /// </summary>
        public void Save(string pathToDestFile)
        {
            PdfReader reader = null;
            PdfContentByte cb = null;
            int index = 0;
            try
            {
                // Percorre a lista de arquivos a serem concatenados.
                foreach (string file in pdfList)
                {
                    // Cria o PdfReader para ler o arquivo
                    reader = new PdfReader(pdfList[index]);
                    // Obtém o número de páginas deste pdf
                    int numPages = reader.NumberOfPages;

                    if (index == 0)
                    {
                        // Cria o objeto do novo documento
                        document = new Document(reader.GetPageSizeWithRotation(1));
                        // Cria um writer para gravar o novo arquivo
                        writer = PdfWriter.GetInstance(document, new FileStream(pathToDestFile, FileMode.Create));
                        // Abre o documento
                        document.Open();
                        cb = writer.DirectContent;
                    }

                    // Adiciona cada página do pdf origem ao pdf destino.
                    int i = 0;
                    while (i < numPages)
                    {
                        i++;
                        document.SetPageSize(reader.GetPageSizeWithRotation(i));
                        document.NewPage();
                        PdfImportedPage page = writer.GetImportedPage(reader, i);
                        int rotation = reader.GetPageRotation(i);
                        if (rotation == 90 || rotation == 270)
                            cb.AddTemplate(page, 0, -1f, 1f, 0, 0, reader.GetPageSizeWithRotation(i).Height);
                        else
                            cb.AddTemplate(page, 1f, 0, 0, 1f, 0, 0);
                    }
                    index++;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (document != null)
                    document.Close();
            }
        }
    }
}
