$(document).ready(function () {
    let path = $(location).attr("pathname");

    if (path.toLowerCase() === '/aluno/listaraluno') {
        
        verificaUsuarioAdm();
        carregaAlunoListagem();
        //editarUsuario();
        //excluirUsuario();
    }
});



function carregaAlunoListagem() {

    requisicaoAssincrona("POST", "../Aluno/GetLista", "", SucessoCarregarAluno, ErroRequisicaoAjax);
}

function SucessoCarregarAluno(json) {

    let retorno = json.retorno;

    if (retorno != undefined) {
        let tabela = document.getElementById("tableAlunoListagem");
        let corpoTabela = tabela.getElementsByTagName("tbody")[0];
        corpoTabela.innerHTML = '';


        retorno.forEach(elemento => {
            console.log(elemento);

            let nomeAluno = elemento.NomeCompleto;

            let novaLinha = corpoTabela.insertRow();
            let celulaRA = novaLinha.insertCell(0);
            let celulaNome = novaLinha.insertCell(1);
            let celulaSerie = novaLinha.insertCell(2);
            let celulaAcoes = novaLinha.insertCell(3);

            celulaRA.innerHTML = elemento.RA;
            celulaNome.innerHTML = elemento.NomeCompleto;
            celulaSerie.innerHTML = elemento.SerieDescricao;
            celulaAcoes.innerHTML = `<a id="btnEditarUsuario" onclick="editarAluno(${elemento.RA})"><i class="icon-pencil7 text-primary"></i></a>
            <a id="btnExcluirAluno"onclick="ExcluirAluno(${elemento.RA}, '${nomeAluno}')"><i class="icon-trash text-danger-600"></i></a>`;

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

            window.location.assign("../Aluno/ListarAluno");
        }
    });
}

function editarAluno(RA) {
    let parametro = {
        RA: RA
    };

    requisicaoAssincrona("POST", "../Aluno/ObterAlunoRA", parametro, SucessoEditarAluno, ErroRequisicaoAjax);
}
function SucessoEditarAluno(json) {
    window.location.assign("../Aluno/EditarAluno");
    
}

function ExcluirAluno(RA, nomeAluno) {
       swal({
        title: `Deseja realmente excluir o aluno: ${nomeAluno}?`,
        text: "Todas as informações deste aluno serão perdidas!",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Não excluir",
        confirmButtonColor: "#FF7043",
        confirmButtonText: "Sim, excluir!"
    },
        function (result) {
            if (result) {
                let parametro = {
                    RA: RA
                };
                requisicaoAssincrona("POST", "../Aluno/ExcluirAluno", parametro, SucessoExcluirAluno, ErroRequisicaoAjax);
            }
        }
    );
}

function SucessoExcluirAluno(json) {
    let retorno = json.retorno;

    if (retorno == "") {
        carregaAlunoListagem();

        return;
    }

    swal({
        title: "Oops...",
        text: `${retorno}!`,
        confirmButtonColor: "#EF5350",
        type: "error"
    },
        function (result) {
            if (result) {
            }
        }
    );

}

function FiltrarTabela() {
    let inputText = $('#txtPesquisarListagemAluno').val().toLowerCase();
    let selectedOption = parseInt($('#txtTipoDePesquisaListagemAluno').val());
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


    $('#tableAlunoListagem tbody tr').filter(function () {

        let cellText = $(this).find('td').eq(columnIndex).text().toLowerCase();

        if (cellText.indexOf(inputText) > -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function colunaIndex(nomeColuna) {
    return $('#tableAlunoListagem thead th').filter(function () {
        return $(this).text().toLowerCase() === nomeColuna;
    }).index();
}
