// =====================================================================================
// Chamada para executar as mascaras
function mascara(o, f) {
    v_obj = o;
    v_fun = f;
    setTimeout("execmascara()", 100);
}
// =====================================================================================
//
function execmascara() {
    v_obj.value = v_fun(v_obj.value);
}
// =====================================================================================
// Mascara que só permite digitar números
function soNumeros(v) {
    return v.replace(/\D/g, "");
}
// =====================================================================================
// Mascara para Data
function data(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/^(\d{2})(\d)/, "$1/$2");
    v = v.replace(/^(\d{2})\/(\d{2})(\d)/, "$1/$2/$3");
    return v;
}
// =====================================================================================
// Mascara para valores Inteiros
function intNumeros(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{1})(\d{15})$/, "$1.$2");
    v = v.replace(/(\d{1})(\d{12})$/, "$1.$2");
    v = v.replace(/(\d{1})(\d{9})$/, "$1.$2");  
    v = v.replace(/(\d{1})(\d{6})$/, "$1.$2");   
    v = v.replace(/(\d{1})(\d{3})$/, "$1.$2"); 
   return v;
}

// =====================================================================================
// Mascara para Telefone
function telefone(v) {
    v = v.replace(/\D/g, "");
    //v = v.replace(/^(\d\d)(\d)/g,"($1) $2");

    v = v.replace(/(\d{4})(\d)/, "$1-$2");
    return v;
}
// Mascara para Telefone com DDD
function telefoneddd(v) {
    v = v.replace(/\D/g, "")
    //    v = v.replace(/^(\d\d)(\d)/g, "($1) $2") 
    //    v = v.replace(/^(\d\d)(\d)/g, "($1)")
    //v = v.replace(/^(\d{2})(\d)/, "($1)");
    v = v.replace(/(d)(d{4})$/, "$1-$2");
    return v
}

// =====================================================================================
// Mascara pra CPF
function cpf(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d)/, "$1.$2");
    v = v.replace(/(\d{3})(\d{1,2})$/, "$1-$2");
    console.log(v);
    return v;
}
// =====================================================================================
// Mascara para CEP
function cep(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/D/g, "");
    v = v.replace(/^(\d{5})(\d)/, "$1-$2");
    return v;
}
// =====================================================================================
// Mascara para CNPJ
function cnpj(v) {
    v = v.replace(/\D/g, "");
    v = v.replace(/^(\d{2})(\d)/, "$1.$2");
    v = v.replace(/^(\d{2})\.(\d{3})(\d)/, "$1.$2.$3");
    v = v.replace(/\.(\d{3})(\d)/, ".$1/$2");
    v = v.replace(/(\d{4})(\d)/, "$1-$2");
    return v;
}
// =====================================================================================
// Mascara para Maiuscula
function maiusculo(v) {
    v = v.toUpperCase();
    return v;
}
// =====================================================================================
// Mascara para Minuscula
function minusculo(v) {
    v = v.toLowerCase();
    return v;
}

/*
// =====================================================================================
// Mascara para Moeda
function moeda(v){
v=v.replace(/^(\d{2})\.(\d{3})(\d)/,"$1.$2.$3") //Coloca ponto entre o quinto e o sexto dígitos
return v
}

*/
// =====================================================================================
// Mascara para Moeda
function moeda(v) {
    v = v.replace(/\D/g, "")  //permite digitar apenas números
    v = v.replace(/[0-9]{12}/, "inválido")   //limita pra máximo 999.999.999,99
    v = v.replace(/(\d{1})(\d{8})$/, "$1.$2")  //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{5})$/, "$1.$2")  //coloca ponto antes dos últimos 5 digitos
    v = v.replace(/(\d{1})(\d{2})$/, "$1,$2")    //coloca virgula antes dos últimos 2 digitos
    return v;
}
// =====================================================================================
// Mascara para quatro digitos
function soDecimais(v) {
    v = v.replace(/\D/g, "")  //permite digitar apenas números
    v = v.replace(/[0-9]{12}/, "inválido")   //limita pra máximo 999.999.999,99
    v = v.replace(/(\d{1})(\d{10})$/, "$1.$2")  //coloca ponto antes dos últimos 8 digitos
    v = v.replace(/(\d{1})(\d{7})$/, "$1.$2")  //coloca ponto antes dos últimos 5 digitos
    v = v.replace(/(\d{1})(\d{4})$/, "$1,$2")    //coloca virgula antes dos últimos 2 digitos
    return v;
}
// =====================================================================================
function cartao(v) {
    v = v.replace(/\D/g, "");


    v = v.replace(/(\d{1})(\d{19})$/, "$1.$2")  //coloca ponto antes dos últimos 19 digitos
    v = v.replace(/(\d{1})(\d{15})$/, "$1.$2")  //coloca ponto antes dos últimos 15 digitos
    v = v.replace(/(\d{1})(\d{11})$/, "$1.$2")    //coloca ponto antes dos últimos 11 digitos
    v = v.replace(/(\d{1})(\d{7})$/, "$1.$2")    //coloca ponto antes dos últimos 7 digitos
    v = v.replace(/(\d{1})(\d{3})$/, "$1.$2")    //coloca ponto antes dos últimos 3 digitos



//    v = v.replace(/(\d{4})(\d)/, "$1.$2");
//    v = v.replace(/(\d{4})(\d)/, "$1.$2");
//    v = v.replace(/(\d{4})(\d)/, "$1.$2");
//    v = v.replace(/(\d{4})(\d)/, "$1.$2");

    return v;
}

function soLetras(v) {
    return v.replace(/\d/g, "") 
}  

function validaCNPJ(obj) {
    s = obj.value;
    s = s.replace(".", "");
    s = s.replace(".", "");
    s = s.replace("/", "");
    s = s.replace("-", "");
    if (isNaN(s)) {
        return false;
    }
    var i;
    var c = s.substr(0, 12);
    var dv = s.substr(12, 2);
    var d1 = 0;
    for (i = 0; i < 12; i++) {
        d1 += c.charAt(11 - i) * (2 + (i % 8));
    }
    if (d1 == 0)
        return false;
    d1 = 11 - (d1 % 11);
    if (d1 > 9) d1 = 0;
    if (dv.charAt(0) != d1) {
        return false;
    }
    d1 *= 2;
    for (i = 0; i < 12; i++) {
        d1 += c.charAt(11 - i) * (2 + ((i + 1) % 8));
    }
    d1 = 11 - (d1 % 11);
    if (d1 > 9)
        d1 = 0;
    if (dv.charAt(1) != d1) {
        return false;
    }
    return true;
}
function MxLength(obj, max) {
    if (obj.value.length > max) {
        alert('A mensagem de ter no maximo ' + max + ' caracteres.');
        obj.value = obj.value.substring(0, max);
    }
    return obj;
}

// =====================================================================================
// Mascara para Telefone

function FormataTelefone(campo, teclapres) {
    var tecla = teclapres.keyCode;
    var vr = new String(campo.value);
    vr = vr.replace(".", "");
    vr = vr.replace("/", "");
    vr = vr.replace("-", "");
    vr = vr.replace("*", "");
    vr = vr.replace(",", "");
    vr = vr.replace("+", "");
    vr = vr.replace("-", "");
    vr = vr.replace(":", "");
    vr = vr.remove("(", "");
    vr = vr.remove(")", "");
    vr = vr.remove("-", "");
    tam = vr.length + 1;
    if (tecla != 9) {
        if (tam == 5)
            campo.value = vr.substr(0, 4) + '-';
    }
}
// =====================================================================================

function fnIsDate(camp_data) {
    /* ####### Função para Validação de Datas ############ */
    if (camp_data.value == '') return;
    var NumValid = "0123456789";
    var Campo = camp_data;
    var DateValue = "";
    var DateTemp = "";
    var Seperator = "/";
    var Dia;
    var Mes;
    var Ano;
    var leap = 0;
    var err = 0;
    var i;
    var Arruma;
    err = 0;
    Arruma = Campo.value.split("/");
    try {
        if (Arruma[0].length == 1) {
            DateValue = '0' + "" + Arruma[0];
        }
        else {
            DateValue = Arruma[0];
        }
        if (Arruma[1].length == 1) {
            DateValue = DateValue + "/" + '0' + "" + Arruma[1];
        }
        else {
            DateValue = DateValue + "/" + Arruma[1];
        }
        DateValue = DateValue + "/" + Arruma[2];
    }
    catch (e) {
        err = 1;
    }
    /* Deleta todos os caracteres exceto 0..9 */
    for (i = 0; i < DateValue.length; i++) {
        if (NumValid.indexOf(DateValue.substr(i, 1)) >= 0) {
            DateTemp = DateTemp + DateValue.substr(i, 1);
        }
    }
    DateValue = DateTemp;
    /* Sempre modifica a data para 8 digitos*/
    /* Se o ano for digitado com 2 digitos assume 20xx */
    if (DateValue.length == 6) {
        if (DateValue.substr(4, 2) > 50) {
            DateValue = DateValue.substr(0, 4) + '19' + DateValue.substr(4, 2);
        }
        else {
            DateValue = DateValue.substr(0, 4) + '20' + DateValue.substr(4, 2);
        }
    }
    else if (DateValue.length == 4) {
        var lngAno = "";
        var myDate = new Date();
        lngAno = myDate.getFullYear();
        DateValue = DateValue.substr(0, 4) + "" + lngAno
    }
    if (DateValue.length != 8) {
        err = 1;
    }
    /* ano é considerado inválido se for = 0000 */
    Ano = DateValue.substr(4, 4);
    if (Ano == 0) {
        err = 1;
    }
    /* Validação do Mês */
    Mes = DateValue.substr(2, 2);
    if ((Mes < 1) || (Mes > 12)) {
        err = 1;
    }
    /* Validação do Dia */
    Dia = DateValue.substr(0, 2);
    if (Dia < 1) {
        err = 1;
    }
    /* Validação do ano bissexto referente ao mês de fevereiro */
    if ((Ano % 4 == 0) || (Ano % 100 == 0) || (Ano % 400 == 0)) {
        leap = 1;
    }

    if ((Mes == 2) && (leap == 1) && (Dia > 29)) {
        err = 1;
    }

    if ((Mes == 2) && (leap != 1) && (Dia > 28)) {
        err = 1;
    }

    /* Validação dos outros meses */
    if ((Dia > 31) && ((Mes == "01") || (Mes == "03") || (Mes == "05") || (Mes == "07") || (Mes == "08") || (Mes == "10") || (Mes == "12"))) {
        err = 1;
    }
    if ((Dia > 30) && ((Mes == "04") || (Mes == "06") || (Mes == "09") || (Mes == "11"))) {
        err = 1;
    }

    /* Se não houver erro escreve a data completa no campo input com os separadores (ex. 07/01/2004) */
    if (err == 0) {
        Campo.value = Dia + Seperator + Mes + Seperator + Ano;
    }

    /* Escreve mensagem de erro se err != 0 */
    else {
        alert("Por favor, preencha a data corretamente.\n\nFormato dd/mm/aaaa.");
        Campo.select();
        Campo.focus();
        return false;
    }
    tempData = '<%=Format(Now,"yyyyMMdd")%>';
    if (Number(Ano + "" + Mes + "" + Dia) > Number(tempData)) {
        alert("Por favor, preencha a data corretamente.\n\nFormato dd/mm/aaaa.");
        Campo.select();
        Campo.focus();
        return false;
    }
}

function mskDate(X, Y, Z, setFocus) {
    var myX = '';
    myX = myX + X;
    if (myX.length == 2) {
        myX = myX + '/';
        Y.value = myX;
    }
    if (myX.length == 5) {
        myX = myX + '/';
        Y.value = myX;
    }
    if (myX.length == 10) {
        if (setFocus == true) {
            Z.focus();
        }
    }
}

function fnOnlyNumber(key) {
    if (document.all) {
        var aKey = key.keyCode;
    }
    else {
        var aKey = key.which;
    }

    if (!(aKey == 0 || aKey == 37 || aKey == 9 || aKey == 8 || (aKey > 47 && aKey < 58))) {
        if (document.all) {
            key.returnValue = false;
        }
        else {
            return false;
        }
    }
}

function Contar(Campo, txtContador) {
    document.getElementById(txtContador).value = 1000 - Campo.value.length;
}

function mascaraTelefone9Digitos(campo) {
    function trata(valor, isOnBlur) {

        valor = valor.replace(/\D/g, "");
        valor = valor.replace(/^(\d{2})(\d)/g, "($1) $2");

        if (isOnBlur) {
            valor.replace("-", "");
            valor = valor.replace(/(\d)(\d{4})$/, "$1-$2");
        }

        return valor;
    }

    campo.onkeypress = function (evt) {

        var code = (window.event) ? window.event.keyCode : evt.which;
        var valor = this.value

        if (code > 57 || (code < 48 && code != 8)) {
            return false;
        } else {
            this.value = trata(valor, false);
        }
    }

    campo.onblur = function () {

        var valor = this.value;
        if (valor.length < 13) {
            this.value = ""
        } else {
            this.value = trata(this.value, true);
        }
    }

    campo.maxLength = 14;
}

function mascaraTelefone(campo) {
    function trata(valor, isOnBlur) {

        valor = valor.replace(/\D/g, "");
        valor = valor.replace(/^(\d{2})(\d)/g, "($1) $2");

        if (isOnBlur) {
            valor.replace("-", "");
            valor = valor.replace(/(\d)(\d{4})$/, "$1-$2");
        }

        return valor;
    }

    campo.onkeypress = function (evt) {

        var code = (window.event) ? window.event.keyCode : evt.which;
        var valor = this.value

        if (code > 57 || (code < 48 && code != 8)) {
            return false;
        } else {
            this.value = trata(valor, false);
        }
    }

    campo.onblur = function () {

        var valor = this.value;
        if (valor.length < 13) {
            this.value = ""
        } else {
            this.value = trata(this.value, true);
        }
    }

    campo.maxLength = 13;
}



function check_cpf(numcpf) {

    numcpf = soNumeros(numcpf);
    numcpf = numcpf.replace(/\./g, '').replace(/-/g, '');
    x = 0;
    soma = 0;
    dig1 = 0;
    dig2 = 0;
    texto = "";
    numcpf1 = "";
    len = numcpf.length; x = len - 1;
    // var numcpf = "12345678909";
    for (var i = 0; i <= len - 3; i++) {
        y = numcpf.substring(i, i + 1);
        soma = soma + (y * x);
        x = x - 1;
        texto = texto + y;
    }
    dig1 = 11 - (soma % 11);
    if (dig1 == 10) dig1 = 0;
    if (dig1 == 11) dig1 = 0;
    numcpf1 = numcpf.substring(0, len - 2) + dig1;
    x = 11; soma = 0;
    for (var i = 0; i <= len - 2; i++) {
        soma = soma + (numcpf1.substring(i, i + 1) * x);
        x = x - 1;
    }
    dig2 = 11 - (soma % 11);
    if (dig2 == 10) dig2 = 0;
    if (dig2 == 11) dig2 = 0;
    //alert ("Digito Verificador : " + dig1 + "" + dig2);
    if ((dig1 + "" + dig2) == numcpf.substring(len, len - 2)) {
        return true;
    }
    alert("Numero do CPF invalido !!!");
    return false;
}

function FormataTelefoneBanco(campo) {    
    var vr = new String(campo.value);
    vr = vr.remove("(", "");
    vr = vr.remove(")", "");
    vr = vr.remove("-", "");
    }

