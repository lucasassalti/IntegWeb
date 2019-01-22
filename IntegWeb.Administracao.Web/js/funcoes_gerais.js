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

