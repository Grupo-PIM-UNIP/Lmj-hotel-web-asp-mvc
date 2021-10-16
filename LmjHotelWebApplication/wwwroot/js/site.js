function calcularDiasDeHospedagem() {

    var dataInicio = new Date(document.getElementById('data-inicio').value);
    var dataTermino = new Date(document.getElementById('data-termino').value);

    dataInicio.toLocaleDateString('pt-BR', { timeZone: 'UTC' });
    dataTermino.toLocaleDateString('pt-BR', { timeZone: 'UTC' });

    var milissegundosEntreAsDatas = Math.abs(dataInicio.getTime() - dataTermino.getTime());
    var milissegundosPorDia = (1 * 24 * 60 * 60 * 1000);

    return Math.ceil(milissegundosEntreAsDatas / milissegundosPorDia);
}

function calcularValorTotalHospedagem() {

    var valor = 150.00;
    var total = parseFloat((valor * calcularDiasDeHospedagem()).toFixed(2));

    if (total > 0) {
        document.getElementById('valor-total').value = total;
    }
    else {
        document.getElementById('valor-total').value = '';
    }
}