function ValidarInteiros(evento) {
    //alert(evento.keyCode);

    if (evento.keyCode < 48 || evento.keyCode > 57) {
        //caracter invalido (letras, acentuação, pontos...)
        evento.returnValue = false;
        return false;
    }
    else {
        //caracter valido (numeros)
        return true;
    }
}

function ValidarDecimal(evento) {
    //alert(evento.keyCode);

    if ((evento.keyCode < 48 || evento.keyCode > 57) && evento.keyCode != 44 && evento.keyCode != 46) {
        //caracter invalido (letras, acentuação, pontos...)
        evento.returnValue = false;
        return false;
    }
    else {
        //caracter valido (numeros)
        return true;
    }
}

//Função para ser utilizada junto com o CustomValidator
function validaCPF(oSrc, args) {
    var Soma;
    var Resto;
    var valida = true;
    var strCPF = args.Value;
    Soma = 0;    
    strCPF = strCPF.replace(/\./g, '').replace(/-/g, '');
    for (var numeroRepetido = 0; numeroRepetido < 10; numeroRepetido++)
        if (strCPF == numeroRepetido.toString().padEnd(11, numeroRepetido.toString())) valida = false;

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) valida = false;

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) valida = false;

    args.IsValid = valida;
}

//Função para ser utilizada junto com o CustomValidator
function validaData(oSrc, args) {
    var pattern = /\d{2,4}\/?/g;
    var data = args.Value;
    args.IsValid = /\d{2}\/\d{2}\/\d{4}/g.test(data) &&
             !(new Date(data.match(pattern)[0] + data.match(pattern)[1] + data.match(pattern)[2]) == 'Invalid Date'
            && new Date(data.match(pattern)[1] + data.match(pattern)[0] + data.match(pattern)[2]) == 'Invalid Date');
}

function MascaraData(campo) {

    if (event.keyCode == 8) return;

    var valor = campo.value;

    if (valor.length == 2 || valor.length == 5) {
        valor = valor + '/';
    }

    campo.value = valor;
}

function MascaraCPF(campo) {

    if (event.keyCode == 8) return;

    var valor = campo.value;

    if (valor.length == 3 || valor.length == 7) {
        valor = valor + '.';
    }

    if (valor.length == 11) {
        valor = valor + '-';
    }

    campo.value = valor;
}

function MascaraCNPJ(campo) {

    if (event.keyCode == 8) return;

    var valor = campo.value;

    if (valor.length == 2 || valor.length == 6) {
        valor = valor + '.';
    }

    if (valor.length == 10) {
        valor = valor + '/';
    }

    if (valor.length == 15) {
        valor = valor + '-';
    }

    campo.value = valor;
}

