function calcularTotalHospedagem() {

    var diaria = parseFloat(document.getElementById('diaria-hotel').value);
    var diasDeHospedagem = parseFloat(calcularDiasDeHospedagem());
    var valorTotal = parseFloat(diaria * diasDeHospedagem).toFixed(2);

    if (valorTotal > 0) {
        document.getElementById('valor-total').value = valorTotal;
    }
    else {
        document.getElementById('valor-total').value = '';
    }
}

function calcularDiasDeHospedagem() {

    var dataInicio = new Date(document.getElementById('data-inicio').value);
    var dataTermino = new Date(document.getElementById('data-termino').value);

    dataInicio.toLocaleDateString('pt-BR', { timeZone: 'UTC' });
    dataTermino.toLocaleDateString('pt-BR', { timeZone: 'UTC' });

    var milissegundosEntreAsDatas = Math.abs(dataInicio.getTime() - dataTermino.getTime());
    var milissegundosPorDia = (1 * 24 * 60 * 60 * 1000);

    return Math.ceil(milissegundosEntreAsDatas / milissegundosPorDia);
}

function permitirSomenteNumeros(evt) {
    var evento = evt || window.event;
    var chave = evento.keyCode || evento.which;
    chave = String.fromCharCode(chave);

    var regex = /^[0-9.]+$/;
    if (!regex.test(chave)) {
        evento.returnValue = false;
        if (evento.preventDefault) evento.preventDefault();
    }
}