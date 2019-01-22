using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Framework
{
    public static class ConfigAplication
    {
        private static string path = ConfigurationManager.AppSettings["Config"];

        public static string GetConnectString()
        {
            if (GetOwner().Equals("D"))
                return ConfigurationManager.ConnectionStrings["NEWDEV"].ConnectionString;
            else if (GetOwner().Equals("T"))
                return ConfigurationManager.ConnectionStrings["NEWTST"].ConnectionString;
            else //P
                return ConfigurationManager.ConnectionStrings["PROD"].ConnectionString;
        }

        public static string GetOwner()
        {
            return path;
        }
    }
}
