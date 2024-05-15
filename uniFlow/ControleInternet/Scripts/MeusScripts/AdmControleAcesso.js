$(document).ready(function () {
    let path = $(location).attr("pathname");

    if (path.toLowerCase() === '/aluno/controleacesso') {
        
        carregaControleAcesso();
                
    }
});


function carregaControleAcesso() {

    requisicaoAssincrona("POST", "../Aluno/GetControleAcesso", "", SucessoCarregarControle, ErroRequisicaoAjax);
}

function SucessoCarregarControle(json) {

    let retorno = json.retorno;

    if (retorno != undefined) {
        let tabela = document.getElementById("tableControleAcesso");
        let corpoTabela = tabela.getElementsByTagName("tbody")[0];
        corpoTabela.innerHTML = '';


        retorno.forEach(elemento => {
            console.log(elemento);

            let nomeAluno = elemento.NomeCompleto;

            let novaLinha = corpoTabela.insertRow();
            let celulaRA = novaLinha.insertCell(0);
            let celulaNome = novaLinha.insertCell(1);
            let celulaSerie = novaLinha.insertCell(2);
            let celulaDataEntrada = novaLinha.insertCell(3);
            let celulaDataSaida = novaLinha.insertCell(4);
            let celulaAcoes = novaLinha.insertCell(5);

            celulaRA.innerHTML = elemento.RA;
            celulaNome.innerHTML = elemento.NomeCompleto;
            celulaSerie.innerHTML = elemento.SerieDescricao;
            celulaDataEntrada.innerHTML = elemento.DataHoraEntrada;
            celulaDataSaida.innerHTML = elemento.DataHoraSaida;
            celulaAcoes.innerHTML = `<a id="btnEntradaAluno" onclick="entradaAluno(${elemento.RA})"><i class="icon-user-check text-success"></i>
            <a id="btnSaidaAluno"onclick="saidaAluno(${elemento.RA}, '${nomeAluno}')"><i class="icon-user-minus text-danger-600"></i></a>`;

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

function entradaAluno(RA) {
    let sessaoUsuario = localStorage.getItem("USUARIO");
    let usuario = JSON.parse(sessaoUsuario);

    let parametro = {
        RA: RA,
        CPF: usuario.CPF,
    };

    requisicaoAssincrona("POST", "../Aluno/EntradaAluno", parametro, SucessoEntradaSaidaAluno, ErroRequisicaoAjax);
}
function SucessoEntradaSaidaAluno(json) {
    carregaControleAcesso();
}

function saidaAluno(RA, nomeAluno) {
    let sessaoUsuario = localStorage.getItem("USUARIO");
    let usuario = JSON.parse(sessaoUsuario);

    let parametro = {
        RA: RA,
        CPF: usuario.CPF,
    };

    requisicaoAssincrona("POST", "../Aluno/SaidaAluno", parametro, SucessoEntradaSaidaAluno, ErroRequisicaoAjax);
}

function FiltrarTabelaControleAcesso() {
    let inputText = $('#txtPesquisarControleAcesso').val().toLowerCase();
    let selectedOption = parseInt($('#txtTipoDePesquisaControleAcesso').val());
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


    $('#tableControleAcesso tbody tr').filter(function () {

        let cellText = $(this).find('td').eq(columnIndex).text().toLowerCase();

        if (cellText.indexOf(inputText) > -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function colunaIndex(nomeColuna) {
    return $('#tableControleAcesso thead th').filter(function () {
        return $(this).text().toLowerCase() === nomeColuna;
    }).index();
}
