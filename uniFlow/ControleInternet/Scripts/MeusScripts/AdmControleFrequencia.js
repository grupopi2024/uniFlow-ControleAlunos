$(document).ready(function () {
    let path = $(location).attr("pathname");

    if (path.toLowerCase() === '/aluno/frequenciaaluno') {
        verificaUsuarioAdm();
                
    }
});


function carregaControleFrequencia() {
    let txtData = $("#txtDataParametro").val();

    let parametro = {
        Data: txtData,
    };

    requisicaoAssincrona("POST", "../Aluno/GetControleFrequencia", parametro, SucessoCarregarFrequencia, ErroRequisicaoAjax);
}

function SucessoCarregarFrequencia(json) {

    let retorno = json.retorno;

    if (retorno != undefined) {
        let tabela = document.getElementById("tableControleFrequencia");
        let corpoTabela = tabela.getElementsByTagName("tbody")[0];
        corpoTabela.innerHTML = '';


        retorno.forEach(elemento => {
            //console.log(elemento);

            let nomeAluno = elemento.NomeCompleto;

            let novaLinha = corpoTabela.insertRow();
            let celulaRA = novaLinha.insertCell(0);
            let celulaNome = novaLinha.insertCell(1);
            let celulaSerie = novaLinha.insertCell(2);
            let celulaDataEntrada = novaLinha.insertCell(3);
            let celulaDataSaida = novaLinha.insertCell(4);
            

            celulaRA.innerHTML = elemento.RA;
            celulaNome.innerHTML = elemento.NomeCompleto;
            celulaSerie.innerHTML = elemento.SerieDescricao;
            celulaDataEntrada.innerHTML = elemento.DataHoraEntrada;
            celulaDataSaida.innerHTML = elemento.DataHoraSaida;
            corpoTabela.insertRow(novaLinha, corpoTabela.firstChild);

        });

    } else {
        alert("não há registros");
    }
}

function ErroRequisicaoAjax(json) {
    swal({
        title: "Oops...",
        text: `Algo deu errado, por favor, tente novamente!`,
        confirmButtonColor: "#EF5350",
        type: "error"
    }, function (isConfirmed) {
        if (isConfirmed) {
            $.blockUI({ timeout: 10 });

            window.location.assign("../Aluno/ControleAcesso");
        }
    });
}

//function entradaAluno(RA) {
//    let sessaoUsuario = localStorage.getItem("USUARIO");
//    let usuario = JSON.parse(sessaoUsuario);

//    let parametro = {
//        RA: RA,
//        CPF: usuario.CPF,
//    };

//    requisicaoAssincrona("POST", "../Aluno/EntradaAluno", parametro, SucessoEntradaSaidaAluno, ErroRequisicaoAjax);
//}
//function SucessoEntradaSaidaAluno(json) {
//    carregaControleFrequencia();
//}

//function saidaAluno(RA, nomeAluno) {
//    let sessaoUsuario = localStorage.getItem("USUARIO");
//    let usuario = JSON.parse(sessaoUsuario);

//    let parametro = {
//        RA: RA,
//        CPF: usuario.CPF,
//    };

//    requisicaoAssincrona("POST", "../Aluno/SaidaAluno", parametro, SucessoEntradaSaidaAluno, ErroRequisicaoAjax);
//}

function FiltrarTabelaControleAcesso() {
    let inputText = $('#txtPesquisarControleFrequencia').val().toLowerCase();
    let selectedOption = parseInt($('#txtTipoDePesquisaControleFrequencia').val());
    let columnIndex = 1;

    switch (selectedOption) {
        case 0:
            columnIndex = 1;
            break;
        case 1:
            columnIndex = 0;
            break;
        case 2:
            columnIndex = 2;
            break;
        default:
            columnIndex = 1;
            break;
    }


    $('#tableControleFrequencia tbody tr').filter(function () {

        let cellText = $(this).find('td').eq(columnIndex).text().toLowerCase();

        if (cellText.indexOf(inputText) > -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function colunaIndex(nomeColuna) {
    return $('#tableControleFrequencia thead th').filter(function () {
        return $(this).text().toLowerCase() === nomeColuna;
    }).index();
}
