using IntegWeb.Entidades;
using IntegWeb.Saude.Aplicacao.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Saude.Aplicacao
{
    public class AssinaturaPeriodicoBLL 
    {
        private AssinaturaPeriodicoDAL _obj;
        private Assinatura _objM;
        private DataTable _dt;

        public DataTable ListaTodos(Assinatura obj)
        {
            _obj = new AssinaturaPeriodicoDAL();
            return _obj.SelectAll(obj);
        }

        public bool ValidaCampos(out string mensagem, Assinatura objM, bool isUpdate, out int id)
        {

            bool ret = false;
            id = 0;
        

            if (objM.valor_assinat<1)
            {
                mensagem = "Informe o valor da assinatura!";
                return false;
            }

            if(objM.qtde_assinat<1)
            {
                mensagem = "Informe a quantidade de assinatura!";
                return false;
            }

            if (objM.id_periodo<1)
            {
                mensagem = "Informe o período!";
                return false;
            }

            if (objM.id_periodico < 1)
            {
                mensagem = "Informe o periódico!";
                return false;
            }

            if (DateTime.MinValue.Equals(objM.dt_pagto_assinat))
            {
                mensagem = "Informe o data de pagamento!";
                return false;
            }
            if (DateTime.MinValue.Equals(objM.dt_inicio_assinat))
            {
                mensagem = "Informe a data início!";
                return false;
            }

            if (DateTime.MinValue.Equals(objM.dt_vecto_assinat))
            {
                mensagem = "Informe a data vencimento!";
                return false;
            }

            if (DateTime.MinValue.Equals(objM.dt_vigencia))
            {
                mensagem = "Informe a data vigência!";
                return false;
            }

            if (objM.dist_assinat < 1)
            {
                mensagem = "Informe a distribuição!";
                return false;
            }

            if (string.IsNullOrEmpty(objM.cod_assinatura))
            {
                mensagem = "Informe o código da assinatura!";
                return false;
            }

     
        
               
            _obj = new AssinaturaPeriodicoDAL();

            if (isUpdate)
                ret = _obj.Update(out mensagem, objM);
            else

                ret = _obj.Insert(out mensagem, objM ,out id);

            return ret;

        }


        public bool Deletar(out string mensagem, Assinatura objM)
        {
            _obj = new AssinaturaPeriodicoDAL();
            return _obj.Delete(out mensagem, objM);
        }

        public DataTable ListaAreaViculada(Assinatura obj)
        {
            return new AssinaturaPeriodicoDAL().SelectJoin(obj);
        }

        public bool VincularArea(out string mensagem, Assinatura objM)
        {
            return new AssinaturaPeriodicoDAL().InserAreaAssinatura( objM, out mensagem);
        }

        public bool InsertVigencia(out string mensagem, Assinatura objM)
        {
            bool ret;
            if (DateTime.MinValue.Equals(objM.dt_vigencia))
            {
                mensagem = "Informe a data vigência!";
                return false;
            }

            if (objM.valor_assinat < 1)
            {
                mensagem = "Informe o valor da assinatura!";
                return false;
            }
            _obj = new AssinaturaPeriodicoDAL();
            ret = _obj.InsertVigencia(out mensagem, objM);

            return ret;

        }

        public DataTable ListaValores(Assinatura obj)
        {
            _obj = new AssinaturaPeriodicoDAL();
            return _obj.SelectVal(obj);
        }
    }
}
