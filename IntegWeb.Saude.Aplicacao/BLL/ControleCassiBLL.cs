using IntegWeb.Framework;
using IntegWeb.Saude.Aplicacao.DAL;
using IntegWeb.Saude.Aplicacao.ENTITY;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace IntegWeb.Saude.Aplicacao.BLL
{
    public class ControleCassiBLL : ControleCassiDAL
    {
        #region Classes
        public class ArquivoDevolucao
        {
            public string TipoRegistro { get; set; }
            public string Movimento { get; set; }
            public string Lote { get; set; }
            public string Contrato { get; set; }
            public string Rotina { get; set; }
            public int Total { get; set; }

            public List<Participante> Participantes { get; set; }

        }
        public class Participante : TB_CASSI_PARTICIP_PLANO
        {
            public string TipoRegistro { get; set; }
        }
        #endregion

        /// <summary>
        /// Select sem filtro
        /// </summary>
        /// <returns></returns>
        public new DataTable GetAllParticipantes()
        {
            return CreateDataTable(base.GetParticipantesCassi());
        }

        /// <summary>
        /// Select com filtro lambda
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public new DataTable GetAllParticipantes(Func<TB_CASSI_PARTICIP_PLANO, bool> criteria)
        {
            return CreateDataTable(base.GetParticipantesCassi(criteria));
        }

        /// <summary>
        /// Select por nome
        /// </summary>
        /// <param name="nome"></param>
        /// <returns></returns>
        public DataTable GetAllParticipantes(string nome)
        {
            string[] nomes = nome.ToLower().Split(' ').Where(val => val.Length > 2).ToArray();
            string arg = nomes[0];
            var list = base.GetParticipantesCassi(item => item.NOME_PARTICIP.ToLower().Contains(arg)).OrderBy(item => item.NOME_PARTICIP);

            if (nomes.Length > 1)
            {
                nomes = nomes.Skip(1).ToArray();
                list = (from item in list
                        from value in nomes
                        orderby item.NOME_PARTICIP
                        select item).OrderBy(item => item.NOME_PARTICIP);
            }

            return CreateDataTable(list);
        }

        /// <summary>
        /// Recupera um objeto TB_CASSI_PARTICIP_PLANO do banco utilizando filtro lambda e converte para o tipo Participante
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        public new Participante GetParticipante(Func<TB_CASSI_PARTICIP_PLANO, bool> criteria)
        {
            TB_CASSI_PARTICIP_PLANO participante = base.GetParticipante(criteria);

            return !ReferenceEquals(participante, null) ?
                new Participante
                {
                    CARTAO_CASSI = participante.CARTAO_CASSI,
                    CARTAO_FUNCESP = participante.CARTAO_FUNCESP,
                    DT_ADESAO = participante.DT_ADESAO,
                    DT_INCLUSAO = participante.DT_INCLUSAO,
                    DT_NASC = participante.DT_NASC,
                    DT_VALIDADE_FIM = participante.DT_VALIDADE_FIM,
                    DT_VALIDADE_INI = participante.DT_VALIDADE_INI,
                    MATRICULA_CASSI = participante.MATRICULA_CASSI,
                    NOME_PARTICIP = participante.NOME_PARTICIP,
                    NUM_CPF = participante.NUM_CPF,
                    SITUACAO = participante.SITUACAO,
                    TIPO_ARQUIVO = participante.TIPO_ARQUIVO
                } : null;
        }

        public bool InsertParticipante(Participante participante)
        {
            throw new NotImplementedException();
        }

        public bool UpdateParticipante(Participante participante)
        {
            throw new NotImplementedException();
        }
        
        #region Batch Methods
        /// <summary>
        /// Busca de participantes na base utilizando uma lista para comparação
        /// </summary>
        /// <param name="participantes"></param>
        /// <returns></returns>
        public object FetchParticipantes(List<Participante> participantes)
        {
            var list = (from participante in participantes
                        join dbParticipante in base.GetParticipantesCassi() on participante.CARTAO_FUNCESP equals dbParticipante.CARTAO_FUNCESP into jn
                        from dbParticipante in jn.DefaultIfEmpty()
                        group dbParticipante by new { dbParticipante, participante } into gp
                        select gp);
            return new
            {
                Matches = list.Where(item => !ReferenceEquals(item.Key.dbParticipante, null))
                              .Select(item => item.Key.participante).ToList(),

                MisMatches = list.Where(item => ReferenceEquals(item.Key.dbParticipante, null))
                                 .Select(item => item.Key.participante).ToList()
            };
        }

        /// <summary>
        /// Insere a lista de participantes cassi no banco
        /// </summary>
        /// <param name="participantes"></param>
        public void InsertBatchParticipantes(ArquivoDevolucao lote)
        {
            TB_CASSI_DEVOLUCAO _lote;
            base.GetLoteDevolucao(item => item.NUM_LOTE == int.Parse(lote.Lote), out _lote);
            if (!ReferenceEquals(_lote, null))
            {
                if (!_lote.TB_CASSI_PARTICIP_PLANO.Any())
                    _lote.TB_CASSI_PARTICIP_PLANO = lote.Participantes.Select(item => CastAs<TB_CASSI_PARTICIP_PLANO>(item)).ToList();
                else
                    _lote.TB_CASSI_PARTICIP_PLANO = _lote.TB_CASSI_PARTICIP_PLANO
                        .Concat(lote.Participantes.Select(item => CastAs<TB_CASSI_PARTICIP_PLANO>(item))).ToList();
                base.UpdateLoteDevolucao(ref _lote);
            }
            else
            {
                CultureInfo dtCulture = new CultureInfo("en-US");
                DateTime _dateTimeResult;

                _lote = new TB_CASSI_DEVOLUCAO
                {
                    NUM_LOTE = int.Parse(lote.Lote),
                    DT_INCLUSAO = DateTime.Now,
                    DT_MOVIMENTO = DateTime.TryParseExact(lote.Movimento, "yyyyMMdd", dtCulture, DateTimeStyles.None, out _dateTimeResult) ? _dateTimeResult : DateTime.MinValue,
                    NUM_CONTRATO = int.Parse(lote.Contrato),
                    NUM_ROTINA = int.Parse(lote.Rotina),
                    TIPO_REGISTRO = short.Parse(lote.TipoRegistro),
                    TB_CASSI_PARTICIP_PLANO = lote.Participantes.Select(item => CastAs<TB_CASSI_PARTICIP_PLANO>(item)).ToList()
                };

                base.InsertLoteDevolucao(ref _lote);
            }
        }

        /// <summary>
        /// Atualiza os participantes cassi do banco com os dados da lista
        /// </summary>
        /// <param name="participantes"></param>
        public void UpdateParticipantesBatch(ArquivoDevolucao lote)
        {
            TB_CASSI_DEVOLUCAO _lote;
            base.GetLoteDevolucao(item => item.NUM_LOTE == int.Parse(lote.Lote), out _lote);

            if (ReferenceEquals(_lote, null)) return;

            IEnumerable<TB_CASSI_PARTICIP_PLANO> list = (from participante in lote.Participantes
                                                         join dbParticipante in _lote.TB_CASSI_PARTICIP_PLANO
                                                         on participante.CARTAO_FUNCESP equals dbParticipante.CARTAO_FUNCESP
                                                         select ((Func<TB_CASSI_PARTICIP_PLANO>)delegate
                                                         {
                                                             dbParticipante.CARTAO_CASSI = participante.CARTAO_CASSI;
                                                             dbParticipante.DT_ADESAO = participante.DT_ADESAO;
                                                             dbParticipante.DT_INCLUSAO = participante.DT_INCLUSAO;
                                                             dbParticipante.DT_NASC = participante.DT_NASC;
                                                             dbParticipante.DT_VALIDADE_FIM = participante.DT_VALIDADE_FIM;
                                                             dbParticipante.DT_VALIDADE_INI = participante.DT_VALIDADE_INI;
                                                             dbParticipante.MATRICULA_CASSI = participante.MATRICULA_CASSI;
                                                             dbParticipante.NOME_PARTICIP = participante.NOME_PARTICIP;
                                                             dbParticipante.NUM_CPF = participante.NUM_CPF;
                                                             dbParticipante.SITUACAO = participante.SITUACAO;
                                                             dbParticipante.TIPO_ARQUIVO = participante.TIPO_ARQUIVO;
                                                             return dbParticipante;
                                                         }).Invoke());
            base.UpdateLoteDevolucao(ref _lote);
        }

        /// <summary>
        /// Altera a situação dos participantes da lista no banco para "A"
        /// </summary>
        /// <param name="participantes"></param>
        public void BulkEnableParticipantesCassi(List<Participante> participantes)
        {
            List<TB_CASSI_PARTICIP_PLANO> list = (from p in participantes
                                                  join _p in base.GetParticipantesCassi(item => item.SITUACAO == "C")
                                                      on p.CARTAO_FUNCESP equals _p.CARTAO_FUNCESP
                                                  select _p).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SITUACAO = "A";
            }

            base.BulkUpdateParticipanteCassi(list);
        }

        /// <summary>
        /// Altera a situação dos participantes da lista no banco para "C"
        /// </summary>
        /// <param name="participantes"></param>
        public void BulkDisableParticipantesCassi(List<Participante> participantes)
        {
            List<TB_CASSI_PARTICIP_PLANO> list = (from p in participantes
                                                  join _p in base.GetParticipantesCassi(item => item.SITUACAO == "A")
                                                      on p.CARTAO_FUNCESP equals _p.CARTAO_FUNCESP
                                                  select _p).ToList();
            for (int i = 0; i < list.Count; i++)
            {
                list[i].SITUACAO = "C";
            }

            base.BulkUpdateParticipanteCassi(list);
        }
        #endregion

        #region Private
        /// <summary>
        /// "Converte" os dados para DataTable
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private DataTable CreateDataTable(IEnumerable<TB_CASSI_PARTICIP_PLANO> list)
        {
            if (ReferenceEquals(list, null))
                return null;

            DataTable dt = new DataTable();

            dt.Columns.AddRange(new DataColumn[]{
                new DataColumn("Nome", typeof(string)),
                new DataColumn("CPF", typeof(string)),
                new DataColumn("Nascimento", typeof(string)),
                new DataColumn("Cartão Funcesp", typeof(string)),
                new DataColumn("Cartão Cassi", typeof(string)),
                new DataColumn("Matricula Cassi", typeof(string)),
                new DataColumn("Status", typeof(string)),
                new DataColumn("Adesão", typeof(string)),
                new DataColumn("Validade", typeof(string)),                
            });

            foreach (var item in list)
            {
                //Validações
                string dtNascimento = item.DT_NASC.HasValue ? Util.FormatarData(item.DT_NASC.Value) : string.Empty;
                string dtValidadeInicio = item.DT_VALIDADE_INI.HasValue ? Util.FormatarData(item.DT_VALIDADE_INI.Value) : string.Empty;
                string dtValidadeFim = item.DT_VALIDADE_FIM.HasValue ? Util.FormatarData(item.DT_VALIDADE_FIM.Value) : string.Empty;
                string nomeParticip = item.NOME_PARTICIP ?? string.Empty;
                string numCPF = !string.IsNullOrEmpty(item.NUM_CPF) ? Framework.Util.FormatarCPF(item.NUM_CPF) : string.Empty;
                string cartaoFUNCESP = item.CARTAO_FUNCESP ?? string.Empty;
                string cartaoCASSI = item.CARTAO_CASSI ?? string.Empty;
                string matriculaCASSI = item.MATRICULA_CASSI ?? string.Empty;
                string situacao = !string.IsNullOrEmpty(item.SITUACAO) ? (item.SITUACAO.Equals("A") ? "Ativo" : "Cancelado") : string.Empty;
                string dtAdesao = Framework.Util.FormatarData(item.DT_ADESAO);

                //Criação das Células
                dt.Rows.Add(new object[] {
                    nomeParticip,
                    numCPF,
                    dtNascimento,
                    cartaoFUNCESP,
                    cartaoCASSI,
                    matriculaCASSI,
                    situacao,
                    dtAdesao,
                    string.Format("{0} - {1}", dtValidadeInicio, dtValidadeFim)
                });

            }

            return dt;
        }

        /// <summary>
        /// Converte o objeto para o type especificado
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        private T CastAs<T>(object item) where T : new()
        {
            T type = new T();

            PropertyInfo[] objectProperties = item.GetType().GetProperties();
            PropertyInfo[] typeProperties = type.GetType().GetProperties();

            IEnumerable<string> matchProps = from objectProperty in objectProperties
                                             join typeProperty in typeProperties on objectProperty.Name equals typeProperty.Name
                                             select typeProperty.Name;

            foreach (string property in matchProps)
            {
                object propertyValue = item.GetType().GetProperty(property).GetValue(item);
                type.GetType().GetProperty(property).SetValue(type, propertyValue);
            }

            return type;
        }

        #endregion
    }
}