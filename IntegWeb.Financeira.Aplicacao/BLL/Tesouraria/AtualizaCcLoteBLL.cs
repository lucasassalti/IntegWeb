using IntegWeb.Financeira.Aplicacao.DAL.Tesouraria;
using IntegWeb.Financeira.Aplicacao.ENTITY;
using IntegWeb.Framework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegWeb.Financeira.Aplicacao.BLL.Tesouraria
{
    public class AtualizaCcLoteBLL : AtualizaCcLoteDAL
    {

        public DataSet Validar(DataTable dt)
        {
            DataTable dtInserirHist = dt.Clone();
            DataTable dtInserir = dt.Clone();
            DataSet ds = new DataSet();
            //List<string> lstMsg = new List<string>();
            int validaNumero;
            string dataContaAtivaCorporativo = "";
            int j = 0;
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //Verifica se os campos obrigatórios estão preenchidos antes da verificação.
                if (CelulaVazia(dt.Rows[i]) == "")
                {

                    int numDep = 0;

                    if (!dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("71"))
                    {
                        if (dt.Rows[i]["Representante"].ToString() == "" || dt.Rows[i]["Representante"].ToString() == "0")
                        {
                            dataContaAtivaCorporativo = GetContaAtivaTit(Convert.ToInt16(dt.Rows[i]["Empresa"]),
                                                                         Convert.ToInt32(dt.Rows[i]["Matricula"]),
                                                                         Convert.ToInt16(dt.Rows[i]["Código do Banco"]),
                                                                         Convert.ToInt32(dt.Rows[i]["Código Agencia"]),
                                                                         dt.Rows[i]["Número da conta"].ToString(),
                                                                         dt.Rows[i]["Tipo de conta corrente"].ToString());

                        }
                        else
                        {

                            numDep = GetNumDepend(Convert.ToInt16(dt.Rows[i]["Empresa"]), Convert.ToInt32(dt.Rows[i]["Matricula"]), Convert.ToInt64(Util.LimparCPF(dt.Rows[i]["CPF"].ToString())));

                             dataContaAtivaCorporativo = GetContaAtivaCorporativo(Convert.ToInt16(dt.Rows[i]["Empresa"]),
                                                         GetNumRepr(Convert.ToInt16(dt.Rows[i]["Empresa"]), Convert.ToInt32(dt.Rows[i]["Matricula"]), Convert.ToInt32(dt.Rows[i]["Representante"].ToString())),
                                                         numDep,
                                                        Convert.ToInt32(dt.Rows[i]["Matricula"]),
                                                        Convert.ToInt16(dt.Rows[i]["Código do Banco"]),
                                                        Convert.ToInt32(dt.Rows[i]["Código Agencia"]),
                                                        dt.Rows[i]["Número da conta"].ToString(),
                                                        dt.Rows[i]["Tipo de conta corrente"].ToString());

                             if (numDep != 0)
                             {
                                 dt.Rows[i]["Representante"] = numDep;
                             }

                            

                        }

                        
                    }

                    if (String.IsNullOrEmpty(GetNomeParticipante(Convert.ToInt16(dt.Rows[i]["Empresa"].ToString()),
                                           Convert.ToInt32(dt.Rows[i]["Matricula"].ToString()),
                                           Convert.ToInt64(Util.LimparCPF(dt.Rows[i]["CPF"].ToString()))))
                                           &&
                                           String.IsNullOrEmpty(GetNomeParticipanteDep(Convert.ToInt16(dt.Rows[i]["Empresa"].ToString()),
                                           Convert.ToInt32(dt.Rows[i]["Matricula"].ToString()),
                                           Convert.ToInt64(Util.LimparCPF(dt.Rows[i]["CPF"].ToString()))))
                                            && 
                                            String.IsNullOrEmpty(GetNomeRepresentanteExt(Convert.ToInt32(dt.Rows[i]["Representante"].ToString()),
                                                                    Convert.ToInt64(Util.LimparCPF(dt.Rows[i]["CPF"].ToString())))))
                    {
                        dtInserirHist.ImportRow(dt.Rows[i]);
                        dtInserirHist.Rows[j]["Critica"] = "Erro: Dados do participante inválidos - Titular da conta: " + dt.Rows[i]["Nome do Empregado"].ToString();
                        j++;

                    }
                    //else if (GetNomeParticipanteDep(Convert.ToInt16(dt.Rows[i]["Empresa"].ToString()),
                    //                       Convert.ToInt32(dt.Rows[i]["Matricula"].ToString()),
                    //                       Convert.ToInt64(Util.LimparCPF(dt.Rows[i]["CPF"].ToString()))).Count() > 1)
                    //{

                    //}

                    else if (!String.IsNullOrEmpty(dataContaAtivaCorporativo) && !dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("71"))
                    {
                        dtInserirHist.ImportRow(dt.Rows[i]);
                        dtInserirHist.Rows[j]["Critica"] = "Erro: Dados bancários cadastrados desde: " + dataContaAtivaCorporativo.Replace("00:00:00", "") + " - Titular da conta: " + dt.Rows[i]["Nome do Empregado"].ToString();
                        j++;

                    }
                    else
                    {
                        string nome_Empregado = dt.Rows[i]["Nome do Empregado"].ToString();

                        //Valida Empresa
                        if (ValidaEmpresa(Convert.ToInt16(dt.Rows[i]["Empresa"].ToString())) == null)
                        {
                            dtInserirHist.ImportRow(dt.Rows[i]);
                            dtInserirHist.Rows[j]["Critica"] = "Erro: Código da empresa inválido - Titular da conta: " + nome_Empregado;
                            j++;
                        }  //Valida Perfil
                        else if (VerificaPerfil(Convert.ToInt16(dt.Rows[i]["Empresa"].ToString()), Convert.ToInt32(dt.Rows[i]["Matricula"])) == "Ativo" && String.IsNullOrEmpty(dt.Rows[i]["Representante"].ToString()))
                        {
                            dtInserirHist.ImportRow(dt.Rows[i]);
                            dtInserirHist.Rows[j]["Critica"] = "Erro: Não é permitido Atualizar Dados Bancários de Participante Ativo - Titular da conta: " + nome_Empregado;
                            j++;
                        }//Bloqueia bancos que a fundação não atende mais, porém, está na base.
                        else if((Convert.ToInt16(dt.Rows[i]["Código do Banco"].ToString())) == 151 ||
                                (Convert.ToInt16(dt.Rows[i]["Código do Banco"].ToString())) == 356 ||
                                 (Convert.ToInt16(dt.Rows[i]["Código do Banco"].ToString())) == 399 ||
                            (Convert.ToInt16(dt.Rows[i]["Código do Banco"].ToString())) == 409 )
                        {
                            dtInserirHist.ImportRow(dt.Rows[i]);
                            dtInserirHist.Rows[j]["Critica"] = "Erro: Banco bloqueado para essa operação - Titular da conta: " + nome_Empregado;
                            j++;
                        }//Valida Banco
                        else if(!ValidaBanco(Convert.ToInt16(dt.Rows[i]["Código do Banco"].ToString())))
                        {
                            dtInserirHist.ImportRow(dt.Rows[i]);
                            dtInserirHist.Rows[j]["Critica"] = "Erro: Banco inexistente - Titular da conta: " + nome_Empregado;
                            j++;
                        }//Valida Agencia
                        else if (ValidaAgencia(Convert.ToInt16(dt.Rows[i]["Código do Banco"].ToString()), Convert.ToInt32(dt.Rows[i]["Código Agencia"].ToString())) == null)
                        {
                            dtInserirHist.ImportRow(dt.Rows[i]);
                            dtInserirHist.Rows[j]["Critica"] = "Erro: Agência inexistente - Titular da conta: " + nome_Empregado;
                            j++;
                        }
                        else
                        {
                            dt.Rows[i]["Tipo de conta corrente"] = dt.Rows[i]["Tipo de conta corrente"].ToString().Trim();

                            //Banco do Brasil
                            if (dt.Rows[i]["Código do Banco"].ToString().Trim('0').Equals("1"))
                            {
                                // Até 8 digitos na conta
                                if (dt.Rows[i]["Número da conta"].ToString().Length > 8)
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta ultrapassa o limite de 8 dígitos - Titular da conta: " + nome_Empregado;
                                    j++;
                                }

                                else if (String.IsNullOrEmpty(dt.Rows[i]["CPF"].ToString()) || dt.Rows[i]["CPF"].ToString() == "0")
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: CPF inválido - Titular da conta:" + nome_Empregado;
                                    j++;
                                }

                                else if (dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0') != "")
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, este banco não deve possuir tipo de conta corrente -  Titular da conta: " + nome_Empregado;
                                    j++;
                                }

                                else if (!int.TryParse(dt.Rows[i]["Número da conta"].ToString(), out validaNumero))
                                {
                                    //Verifica se a letra é X
                                    int contaX = 0;
                                    if (!dt.Rows[i]["Número da conta"].ToString().ToUpper().Contains("X"))
                                    {
                                        dtInserirHist.ImportRow(dt.Rows[i]);
                                        dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, caractere incorreto -  Titular da conta: " + nome_Empregado;
                                        j++;
                                    }
                                    else
                                    {
                                        contaX = dt.Rows[i]["Número da conta"].ToString().ToUpper().Count(x => x == 'X');
                                    }

                                    if (contaX > 1)
                                    {
                                        dtInserirHist.ImportRow(dt.Rows[i]);
                                        dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, caractere incorreto -  Titular da conta: " + nome_Empregado;
                                        j++;
                                    }

                                    else if (contaX == 1)
                                    {
                                        dtInserir.ImportRow(dt.Rows[i]);
                                    }
                                }
                                else
                                {
                                    dtInserir.ImportRow(dt.Rows[i]);
                                }

                            }

                            //Santander
                            else if (dt.Rows[i]["Código do Banco"].ToString().Trim('0').Equals("33"))
                            {
                                // Tipos de contas válidas para o lançamento
                                if (dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("1") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("2") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("3") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("5") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("92") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Equals("60") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("71") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("7") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("9") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("27") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("35") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("37") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("43") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("45") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("46") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("48") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("50") ||
                                    dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("53") 
                                    )
                                {
                                    if (dt.Rows[i]["Número da conta"].ToString().Length > 7)
                                    {
                                        dtInserirHist.ImportRow(dt.Rows[i]);
                                        dtInserirHist.Rows[j]["Critica"] = "Erro: Conta ultrapassa o limite de sete dígitos - Titular da conta: " + nome_Empregado;
                                        j++;
                                    }

                                    else if (String.IsNullOrEmpty(dt.Rows[i]["CPF"].ToString()) || dt.Rows[i]["CPF"].ToString() == "0")
                                    {
                                        dtInserirHist.ImportRow(dt.Rows[i]);
                                        dtInserirHist.Rows[j]["Critica"] = "Erro: CPF inválido - Titular da conta:" + nome_Empregado;
                                        j++;
                                    }

                                    else if (dt.Rows[i]["Número da conta"].ToString().Length < 7)
                                    {
                                        dtInserirHist.ImportRow(dt.Rows[i]);
                                        dtInserirHist.Rows[j]["Critica"] = "Erro: Conta incompleta, dados bancários possuem menos que sete dígitos - Titular da conta: " + nome_Empregado;
                                        j++;
                                    }

                                    else if (!int.TryParse(dt.Rows[i]["Número da conta"].ToString(), out validaNumero))
                                    {
                                        dtInserirHist.ImportRow(dt.Rows[i]);
                                        dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, deve conter apenas números - Titular da Conta: " + nome_Empregado;
                                        j++;
                                    }
                                    // VALIDAR CONTA SALARIO
                                    else if (dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("71"))
                                    {

                                        string dataContaAtivaSalario = "";

                                        if (dt.Rows[i]["Representante"].ToString() == "" || dt.Rows[i]["Representante"].ToString() == "0")
                                        {
                                            dataContaAtivaSalario = GetContaComplemenTit(Convert.ToInt16(dt.Rows[i]["Empresa"]),
                                                                         Convert.ToInt32(dt.Rows[i]["Matricula"]),
                                                                         Convert.ToInt16(dt.Rows[i]["Código do Banco"]),
                                                                         Convert.ToInt32(dt.Rows[i]["Código Agencia"]),
                                                                         dt.Rows[i]["Número da conta"].ToString(),
                                                                         dt.Rows[i]["Tipo de conta corrente"].ToString());
                                        }
                                        else
                                        {
                                            numDep = GetNumDepend(Convert.ToInt16(dt.Rows[i]["Empresa"]), Convert.ToInt32(dt.Rows[i]["Matricula"]), Convert.ToInt64(Util.LimparCPF(dt.Rows[i]["CPF"].ToString())));

                                            dataContaAtivaSalario = GetContaAtivaComplementar(Convert.ToInt16(dt.Rows[i]["Empresa"]),
                                                                                               Convert.ToInt32(dt.Rows[i]["Representante"].ToString()),
                                                                                                //GetNumRepr(Convert.ToInt16(dt.Rows[i]["Empresa"]), Convert.ToInt32(dt.Rows[i]["Matricula"]), GetNumDepend(Convert.ToInt16(dt.Rows[i]["Empresa"]), Convert.ToInt32(dt.Rows[i]["Matricula"]), Convert.ToInt64(Util.LimparCPF(dt.Rows[i]["CPF"].ToString())))),
                                                                                                numDep, 
                                                                                                Convert.ToInt32(dt.Rows[i]["Matricula"]),
                                                                                                     Convert.ToInt16(dt.Rows[i]["Código do Banco"]),
                                                                                                     Convert.ToInt32(dt.Rows[i]["Código Agencia"]),
                                                                                                     dt.Rows[i]["Número da conta"].ToString(),
                                                                                                     dt.Rows[i]["Tipo de conta corrente"].ToString()).Replace("00:00:00", "");

                                            if (numDep != 0)
                                            {
                                                dt.Rows[i]["Representante"] = numDep;
                                            }
                                        }



                                             
                                        //}
                                        //else
                                        //{
                                        //     dataContaAtivaSalario = GetContaAtivaCorporativo(Convert.ToInt16(dt.Rows[i]["Empresa"]),
                                        //                                                            GetNumRepr(Convert.ToInt16(dt.Rows[i]["Empresa"]), Convert.ToInt32(dt.Rows[i]["Matricula"]), GetNumDepend(Convert.ToInt16(dt.Rows[i]["Empresa"]), Convert.ToInt32(dt.Rows[i]["Matricula"]), Convert.ToInt64(Util.LimparCPF(dt.Rows[i]["CPF"].ToString())))),
                                        //                                                            Convert.ToInt32(dt.Rows[i]["Matricula"]),
                                        //                                                            Convert.ToInt16(dt.Rows[i]["Código do Banco"]),
                                        //                                                            Convert.ToInt32(dt.Rows[i]["Código Agencia"]),
                                        //                                                            dt.Rows[i]["Número da conta"].ToString(),
                                        //                                                            dt.Rows[i]["Tipo de conta corrente"].ToString());

                                        //}



                                        if (!String.IsNullOrEmpty(dataContaAtivaSalario))
                                        {
                                            dtInserirHist.ImportRow(dt.Rows[i]);
                                            dtInserirHist.Rows[j]["Critica"] = "Erro: Dados bancários cadastrados desde: " + dataContaAtivaSalario.Replace("00:00:00", "") + " - Titular da conta: " + dt.Rows[i]["Nome do Empregado"].ToString();
                                            j++;
                                        }
                                        else
                                        {
                                            dtInserir.ImportRow(dt.Rows[i]);
                                        }

                                    }

                                    else
                                    {
                                        dtInserir.ImportRow(dt.Rows[i]);
                                    }
                                }
                                else if (dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("13") ||
                                          dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("55"))
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Tipo de conta corrente inválido, não é permitido cadastro de pessoa jurídica " + nome_Empregado;
                                    j++;
                                }
                                else if (dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0').Equals("96"))
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Tipo de conta corrente inválido, exclusivas para recebimento do INSS " + nome_Empregado;
                                    j++;
                                }
                                else
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Conta inválida, tipo de conta corrente incorreto " + nome_Empregado;
                                    j++;
                                }

                            }  // Caixa Econômica Federeal 
                            else if (dt.Rows[i]["Código do Banco"].ToString().Equals("104"))
                            {
                                // regras 
                                if (dt.Rows[i]["Número da conta"].ToString().Length > 11)
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta ultrapassa o limite de onze dígitos - Titular da conta: " + nome_Empregado;
                                    j++;
                                }
                                else if (dt.Rows[i]["Número da conta"].ToString().Length < 11)
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta incompleta, dados bancários possuem menos que onze dígitos - Titular da conta: " + nome_Empregado;
                                    j++;
                                }
                                else if (String.IsNullOrEmpty(dt.Rows[i]["CPF"].ToString()) || dt.Rows[i]["CPF"].ToString() == "0")
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: CPF inválido - Titular da conta:" + nome_Empregado;
                                    j++;
                                }
                                else if (!System.Text.RegularExpressions.Regex.IsMatch(dt.Rows[i]["Número da conta"].ToString(), "^[0-9]*$"))
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, deve conter apenas números - Titular da Conta: " + nome_Empregado;
                                    j++;
                                }
                                else if (dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0') != "")
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, este banco não deve possuir tipo de conta corrente -  Titular da conta: " + nome_Empregado;
                                    j++;
                                }
                                else
                                {
                                    dtInserir.ImportRow(dt.Rows[i]);
                                }
                            }

                            //Bradesco
                            else if (dt.Rows[i]["Código do Banco"].ToString().Equals("237"))
                            {
                                // Até 8 digitos na conta
                                if (dt.Rows[i]["Número da conta"].ToString().Length > 8)
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta ultrapassa o limite de oito dígitos - Titular da conta: " + nome_Empregado;
                                    j++;
                                }
                                else if (String.IsNullOrEmpty(dt.Rows[i]["CPF"].ToString()) || dt.Rows[i]["CPF"].ToString() == "0")
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: CPF inválido - Titular da conta:" + nome_Empregado;
                                    j++;
                                }
                                else if (!int.TryParse(dt.Rows[i]["Número da conta"].ToString(), out validaNumero))
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, deve conter apenas números - Titular da Conta: " + nome_Empregado;
                                    j++;
                                }
                                else if (dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0') != "")
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, este banco não deve possuir tipo de conta corrente -  Titular da conta: " + nome_Empregado;
                                    j++;
                                }
                                else
                                {
                                    dtInserir.ImportRow(dt.Rows[i]);
                                }

                            }     //Itau
                            else if (dt.Rows[i]["Código do Banco"].ToString().Equals("341"))
                            {
                                if (dt.Rows[i]["Número da conta"].ToString().Length > 8)
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta ultrapassa o limite de oito dígitos - Titular da conta: " + nome_Empregado;
                                    j++;
                                }
                                else if (String.IsNullOrEmpty(dt.Rows[i]["CPF"].ToString()) || dt.Rows[i]["CPF"].ToString() == "0")
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: CPF inválido - Titular da conta:" + nome_Empregado;
                                    j++;
                                }
                                else if (!int.TryParse(dt.Rows[i]["Número da conta"].ToString(), out validaNumero))
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, deve conter apenas números - Titular da Conta: " + nome_Empregado;
                                    j++;
                                }
                                else if (dt.Rows[i]["Tipo de conta corrente"].ToString().Trim('0') != "")
                                {
                                    dtInserirHist.ImportRow(dt.Rows[i]);
                                    dtInserirHist.Rows[j]["Critica"] = "Erro: Conta inválida, este banco não deve possuir tipo de conta corrente -  Titular da conta: " + nome_Empregado;
                                    j++;
                                }
                                else
                                {
                                    dtInserir.ImportRow(dt.Rows[i]);
                                }
                            }
                            else
                            {
                                dtInserir.ImportRow(dt.Rows[i]);
                            }
                        }

                    }

                }
                else
                {
                    dtInserirHist.ImportRow(dt.Rows[i]);
                    dtInserirHist.Rows[j]["Critica"] = "Erro: Dados incompleto, campo (" + CelulaVazia(dt.Rows[i]) + ") não preenchido - Titular da Conta: " + dt.Rows[i]["Nome do Empregado"].ToString().ToUpper();
                    j++;
                }

            }

            ds.Tables.Add(dtInserirHist);
            ds.Tables.Add(dtInserir);
            return ds;
        }

        public DataTable ListarDadosParaExcel(short? emp, DateTime? datProcessamentoIni, DateTime? datProcessamentoFim, int? matricula, int? representante, string nome, Int64? cpf, short? codBanco, int? codAgencia, string tipConta, string numConta)
        {
            DataTable dt = new DataTable();
            List<FUN_TBL_ATU_CC_HIST_view> list = new List<FUN_TBL_ATU_CC_HIST_view>();
            list = GetWhereExcel(emp, datProcessamentoIni, datProcessamentoFim, matricula, representante, nome, cpf, codBanco, codAgencia, tipConta, numConta).ToList();
            if (list != null)
            {
                dt = list.ToDataTable();
            }
            return dt;
        }

        public string CelulaVazia(DataRow dr)
        {
            string coluna = "";

            if (dr["Código do Banco"].ToString().Trim('0') == "33")
            {
                if (dr["Tipo de conta corrente"].ToString() == "")
                    coluna = "Tipo de conta corrente";
            }

            if (dr["Empresa"].ToString() == "")
                coluna = "Empresa";
            else if (dr["Matricula"].ToString() == "")
                coluna = "Matrcula";
            else if (dr["Nome do Empregado"].ToString() == "")
                coluna = "Nome do Empregado";
            else if (dr["CPF"].ToString() == "")
                coluna = "CPF";
            else if (dr["Código do Banco"].ToString() == "")
                coluna = "Código do Banco";
            else if (dr["Código Agencia"].ToString() == "")
                coluna = "Código Agencia";
            else if (dr["Número da conta"].ToString() == "")
                coluna = "Número da conta";

            return coluna;
        }
    }
}
