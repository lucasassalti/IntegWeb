using IntegWeb.Saude.Aplicacao.ENTITY;
using IntegWeb.Saude.Aplicacao.DAL.Faturamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using IntegWeb.Entidades;

namespace IntegWeb.Saude.Aplicacao.BLL.Faturamento
{
    public class CargaEquipSimproBLL : CargaEquipSimproDAL
    {
        public List<TB_CONVENENTE_view> GetConvenente()
        {
            return base.GetConvenente();
        }

        public List<TB_TIPO_COND_CONV_view> GetCondicaoContratual(decimal codigoConv)
        {
            return base.GetCondicaoContratual(codigoConv);
        }

        public List<TB_EQUIP_SIMPRO_view> GetEquipSimproList() 
        {
            return base.GetEquipSimproList();
        }

        public IQueryable<TB_EQUIP_SIMPRO_view> GetEquipSimpro()
        {
            return base.GetEquipSimpro();
        }
      
        public Resultado ImportaDados(DataTable dt)
        {

            return base.ImportaDados(dt);
        }

        public Resultado InserirFattblrsp(FATTBLRSP obj)
        {
            return base.InserirFattblrsp(obj);
        }

        public Resultado Savechanges()
        {
                return base.Savechanges();
        }

        public DateTime GetDatValidade(decimal codTipoConvenente, decimal codConvenente)
        {
            return base.GetDatValidade(codTipoConvenente, codConvenente);
        }

        public Resultado DeleteEquipSimpro()
        {
            return base.DeleteEquipSimpro();
        }

        public List<CADTBLRCORECURSOCODIGO_view> GetProcedimento(string procedimento) 
        {
            return base.GetProcedimento(procedimento);
        }

        public List<SAU_TBL_QTDE_MATMED_view1> GetMatMed(decimal recurso)
        {
            return base.GetMatMed(recurso);

        }

        public Resultado AtualizarMatMed(SAU_TBL_QTDE_MATMED obj) 
        {
            return base.AtualizarMatMed(obj);
        }

        public Resultado InserirMatMed(SAU_TBL_QTDE_MATMED obj) 
        {
            return base.InserirMatMed(obj);
        }

        public DateTime? GetLastFimMatMed(decimal recurso) 
        {
            return base.GetLastFimMatMed(recurso);
        }

        public DateTime GetLastInicioMatMed(decimal recurso) 
        {
            return base.GetLastInicioMatMed(recurso);
        }

        public decimal GetMaxPrioridade(FATTBLRSP obj) 
        {
            return base.GetMaxPrioridade(obj);
        }

    
    }
}
