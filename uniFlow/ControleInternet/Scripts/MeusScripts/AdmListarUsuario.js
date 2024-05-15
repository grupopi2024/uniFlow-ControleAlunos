$(document).ready(function () {
    let path = $(location).attr("pathname");

    if (path.toLowerCase() === '/usuario/listarusuario') {
        
        verificaUsuarioAdm();
        carregaUsuarioListagem();
        }
});



function carregaUsuarioListagem() {

    requisicaoAssincrona("POST", "../Usuario/GetLista", "", SucessoCarregar, ErroRequisicaoAjax);
}

function SucessoCarregar(json) {

    let retorno = json.retorno;

    if (retorno != undefined) {
        let tabela = document.getElementById("tableUsuarioListagem");
        let corpoTabela = tabela.getElementsByTagName("tbody")[0];
        corpoTabela.innerHTML = '';


        retorno.forEach(elemento => {
            console.log(elemento);

            let nomeUsuario = elemento.Nome;

            let novaLinha = corpoTabela.insertRow();
            let celulaCPF = novaLinha.insertCell(0);
            let celulaNome = novaLinha.insertCell(1);
            let celulaTelefone = novaLinha.insertCell(2);
            let celulaAcoes = novaLinha.insertCell(3);

            celulaCPF.innerHTML = elemento.CPF;
            celulaNome.innerHTML = elemento.Nome;
            celulaTelefone.innerHTML = elemento.Telefone;
            celulaAcoes.innerHTML = `<a id="btnEditarUsuario" onclick="editarUsuario(${elemento.CPF})"><i class="icon-pencil7 text-primary"></i></a>
            <a id="btnExcluirUsuario"onclick="ExcluirUsuario(${elemento.CPF}, '${nomeUsuario}')"><i class="icon-trash text-danger-600"></i></a>`;

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

            window.location.assign("../Usuario/ListarUsuario");
        }
    });
}

function editarUsuario(CPF) {
    let parametro = {
        CPF: CPF
    };

    requisicaoAssincrona("POST", "../Usuario/ObterUsuarioCPF", parametro, SucessoEditarUsuario, ErroRequisicaoAjax);
}

function SucessoEditarUsuario(json) {
    window.location.assign("../Usuario/EditarUsuario");
    //editarAdmUsuario(json);
}

function ExcluirUsuario(CPF, nomeUsuario) {
    let sessaoUsuario = localStorage.getItem("USUARIO");
    let usuario = JSON.parse(sessaoUsuario);

    swal({
        title: `Deseja realmente excluir o produto: ${nomeUsuario}?`,
        text: "Todas as informações deste produto serão perdidas!",
        type: "warning",
        showCancelButton: true,
        cancelButtonText: "Não excluir",
        confirmButtonColor: "#FF7043",
        confirmButtonText: "Sim, excluir!"
    },
        function (result) {
            if (result) {
                if (CPF == usuario.CPF) {
                    $.blockUI({ timeout: 10 });
                    setTimeout(function () {
                    swal({
                        title: "Erro",
                        text: "Você não pode excluir a si mesmo!",
                        type: "error",
                        confirmButtonColor: "#FF7043",
                        confirmButtonText: "OK"
                    })
                    }, 500);
                } else {
                    let parametro = {
                        CPF: CPF
                    };
                    requisicaoAssincrona("POST", "../Usuario/ExcluirUsuario", parametro, SucessoExcluirUsuario, ErroRequisicaoAjax);
                }
            }
        }
    );
}

function SucessoExcluirUsuario(json) {
    let retorno = json.retorno;

    if (retorno == "") {
        carregaUsuarioListagem();

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
    let inputText = $('#txtPesquisarListagemUsuario').val().toLowerCase();
    let selectedOption = parseInt($('#txtTipoDePesquisaListagemUsuario').val());
    let columnIndex = 1;

    switch (selectedOption) {
        case 0:
            columnIndex = 1;
            break;
        case 1:
            columnIndex = 0;
            break;
        default:
            columnIndex = 1;
            break;
    }


    $('#tableUsuarioListagem tbody tr').filter(function () {

        let cellText = $(this).find('td').eq(columnIndex).text().toLowerCase();

        if (cellText.indexOf(inputText) > -1) {
            $(this).show();
        } else {
            $(this).hide();
        }
    });
}

function colunaIndex(nomeColuna) {
    return $('#tableUsuarioListagem thead th').filter(function () {
        return $(this).text().toLowerCase() === nomeColuna;
    }).index();
}
