using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Framework;

namespace IntegWeb.Previdencia.Aplicacao.BLL
{

    public class LAY_CAMPO
    {
        public string nome { get; set; }
        public string nome_amigavel { get; set; }
        public short pos  { get; set; }
        public short tam  { get; set; }
    }

    public class LAYOUT_UTIL
    {

        public static string GetStringNull(string pDADOS, LAY_CAMPO lay)
        {
            if (pDADOS.Substring(lay.pos, lay.tam).Trim().Length == 0)
            {
                return null;
            }
            else
            {
                return pDADOS.Substring(lay.pos, lay.tam).Trim();
            }
        }

        public static string csv_GetStringNull(string[] pDADOS, LAY_CAMPO lay)
        {
            if (lay.pos > 0)
            {
                if (pDADOS[lay.pos - 1].Trim().Length == 0)
                {
                    return null;
                }
                else
                {
                    return pDADOS[lay.pos - 1].Trim();
                }
            }
            else
            {
                return null;
            }
        }

        public static int GetInt(string pDADOS, LAY_CAMPO lay)
        {
            return int.Parse(pDADOS.Substring(lay.pos, lay.tam));
        }

        public static int csv_GetInt(string[] pDADOS, LAY_CAMPO lay)
        {
            return int.Parse(pDADOS[lay.pos - 1]);
        }

        public static int? GetIntNull(string pDADOS, LAY_CAMPO lay)
        {
            return Util.String2Int32(pDADOS.Substring(lay.pos, lay.tam));
        }

        public static short GetShort(string pDADOS, LAY_CAMPO lay)
        {
            return short.Parse(pDADOS.Substring(lay.pos, lay.tam));
        }

        public static short? GetShortNull(string pDADOS, LAY_CAMPO lay)
        {
            return Util.String2Short(pDADOS.Substring(lay.pos, lay.tam));
        }

        public static long GetLong(string pDADOS, LAY_CAMPO lay)
        {
            return long.Parse(pDADOS.Substring(lay.pos, lay.tam));
        }

        public static long? GetLongNull(string pDADOS, LAY_CAMPO lay)
        {
            return Util.String2Int64(pDADOS.Substring(lay.pos, lay.tam));
        }

        public static decimal GetDecimal(string pDADOS, LAY_CAMPO lay)
        {
            return decimal.Parse(pDADOS.Substring(lay.pos, lay.tam-2) + "," + pDADOS.Substring(lay.pos + lay.tam - 2, 2));
        }

        public static decimal? GetDecimalNull(string pDADOS, LAY_CAMPO lay, int Decimais = 2)
        {
            return Util.String2Decimal(pDADOS.Substring(lay.pos, lay.tam - Decimais) + "," + pDADOS.Substring(lay.pos + lay.tam - Decimais, Decimais));
        }

        public static decimal csv_GetDecimal(string[] pDADOS, LAY_CAMPO lay)
        {
            if (lay.pos > 0)
            {
                if (lay.tam > 0)
                {
                    return decimal.Parse(pDADOS[lay.pos - 1].Substring(0, lay.tam - 2) + "," + pDADOS[lay.pos - 1].Substring(lay.tam - 2, 2));
                }
                else
                {
                    decimal dRet = 0;
                    decimal.TryParse(pDADOS[lay.pos - 1], out dRet);
                    return dRet;
                }
            }
            else
            {
                return 0;
            }

        }

        public static decimal csv_GetDecimal(string pDADOS, LAY_CAMPO lay)
        {
            string source = pDADOS.Substring(lay.pos, lay.tam);
            if (lay.pos > 0)
            {
                if (source.IndexOf(",") > -1)
                {
                    decimal dRet = 0;
                    decimal.TryParse(source, out dRet);
                    return dRet;
                }
                else
                {
                    return decimal.Parse(source.Substring(0, source.Length - 2) + "," + source.Substring(source.Length - 2, 2));
                }
            }
            else
            {
                return 0;
            }
        }

        public static DateTime GetData(string pDADOS, LAY_CAMPO lay)
        {
            return ConverteData(pDADOS.Substring(lay.pos, lay.tam));
        }

        public static DateTime? GetDataNull(string pDADOS, LAY_CAMPO lay)
        {
            return ConverteDataNull(pDADOS.Substring(lay.pos, lay.tam));
        }

        public static DateTime? ConverteDataNull(string data_YYYYMMDD)
        {
            DateTime dData;

            if ((data_YYYYMMDD.Length>=8) && DateTime.TryParse(data_YYYYMMDD.Substring(6, 2) + "/" + data_YYYYMMDD.Substring(4, 2) + "/" + data_YYYYMMDD.Substring(0, 4), out dData))
            {
                return dData;
            }
            else
            {
                return null;
            }
        }

        public static DateTime ConverteData(string data_YYYYMMDD)
        {
            return DateTime.Parse(data_YYYYMMDD.Substring(6, 2) + "/" + data_YYYYMMDD.Substring(4, 2) + "/" + data_YYYYMMDD.Substring(0, 4));
        }
    }
}
