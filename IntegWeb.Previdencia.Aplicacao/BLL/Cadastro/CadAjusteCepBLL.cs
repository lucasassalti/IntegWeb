using IntegWeb.Entidades;
using IntegWeb.Previdencia.Aplicacao.ENTITY;
using IntegWeb.Previdencia.Aplicacao.DAL;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntegWeb.Previdencia.Aplicacao.DAL.Cadastro;

namespace IntegWeb.Previdencia.Aplicacao.BLL.Cadastro
{
    public class CadAjusteCepBLL : CadAjusteCepDAL
    {
        public DataTable buscarMunicipio()
        {
            DataTable dt = new DataTable();
            List<MUNICIPIO> list = new List<MUNICIPIO>();
            list = GetDataMunicipio().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
        public DataTable buscarCep()
        {
            DataTable dt = new DataTable();
            List<TB_CEP> list = new List<TB_CEP>();
            list = GetDataCep().ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
        /// <summary>
        /// Busca CEP ou Logradouro na tabela TB_CEP
        /// </summary>
        /// <param name="stringBuscar"></param>
        /// <returns></returns>
        public DataTable buscarPorCepOuLogradouro(string stringBuscar)
        {
            DataTable dt = new DataTable();
            List<TB_CEP> list = new List<TB_CEP>();
            list = GetLikeCEPOrLogradouro(stringBuscar).ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }

        /// <summary>
        /// Busca código do municipio ou descrição do municipio na tabela MUNICIPIO
        /// </summary>
        /// <param name="stringBuscar"></param>
        /// <returns></returns>
        public DataTable buscarDescMunicOrCodMunic(string stringBuscar)
        {
            DataTable dt = new DataTable();
            List<MUNICIPIO> list = new List<MUNICIPIO>();
            list = GetLikeDescMunicOrCodMunic(stringBuscar).ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }

        public DataTable buscarDescLogradouro(string stringBuscar)
        {
            DataTable dt = new DataTable();
            List<TB_CEP> list = new List<TB_CEP>();
            list = GetLikeDescLogradouro(stringBuscar).ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }
    }
}
