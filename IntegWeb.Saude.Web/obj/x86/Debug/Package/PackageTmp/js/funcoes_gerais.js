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

function isDate(txtDate) {
    var currVal = txtDate;
    if (currVal == '')
        return false;

    //Declare Regex 
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?

    if (dtArray == null)
        return false;

    //Checks for mm/dd/yyyy format.
    dtDay = dtArray[1];
    dtMonth = dtArray[3];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}

