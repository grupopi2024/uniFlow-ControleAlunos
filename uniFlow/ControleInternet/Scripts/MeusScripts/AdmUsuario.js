$(document).ready(function () {
    let path = $(location).attr("pathname");

    if (path.toLowerCase() === '/usuario/cadastrousuario') {
        ObterUsuarios();
        verificaUsuarioAdm();
        //EditarUsuario();
    }
});

function editarUsuario() {
    
    $('#btnEditarUsuario').off('click');//id componente
    $('#btnEditarUsuario').on('click', function () {
        alert("editar");

    });

}

function formataMascaraCadastroTelefoneUsuario() {
    $("#txtTelefoneCadastroUsuario").mask('(00) 0 0000-0000', { reverse: false });
}


function cadastrarUsuario(id) {
    
    let txtSenha = $("#txtSenhaCadastroUsuario").val();
    let txtConfirmaSenha = $("#txtConfirmarSenhaCadastroUsuario").val();
    let slcFuncao = document.getElementById('slcFuncaoCadastroUsuario')

    if (txtSenha != txtConfirmaSenha) {
        alert("Senhas diferentes");
        return;
    }

    let Usuario = {
        NomeCompleto: $("#txtNomeCompletoCadastroUsuario").val(),
        Nome: $("#txtNomeCadastroUsuario").val(),
        Telefone: $("#txtTelefoneCadastroUsuario").val(),
        Email: $("#txtEmailCadastroUsuario").val(),
        CPF: $("#txtCpfCadastroUsuario").val(),
        Senha: $("#txtSenhaCadastroUsuario").val(),
        Funcao: slcFuncao.value,
    };

    localStorage.setItem("EmailCadastrado", Usuario);

    requisicaoAssincrona("POST", "../Usuario/CadastrarUsuario", Usuario, sucessoCadastroUsuario, erroObterUsuarios);
};

function ObterUsuarios() {
    requisicaoAssincrona("POST", "../Usuario/ObterUsuarios", "", sucessoObterUsuarios, erroObterUsuarios);
};

function sucessoObterUsuarios(json) {
    let objRet = JSON.parse(json.retorno);
    let htmlDinamico = [];
    $.each(objRet, function (i, obj) {

        let idUsuario = JSON.stringify(obj._id);
        var conteudo =
            " <div class='testeCardUsuario panel panel-body col-md-3'>                                         " +
            "     <div class='media'>                                                                          " +
            "         <div class='media-left'>                                                                 " +
            "             <a href='../assets/images/placeholder.jpg'>                                           " +
            "                 <img src='../assets/images/placeholder.jpg' class='img-circle img-lg' alt=''>     " +
            "             </a>                                                                                 " +
            "         </div>                                                                                   " +
            "                                                                                                  " +
            "         <div class='media-body'>                                                                 " +
            "             <h6 class='media-heading'>" + obj.Nome + "</h6>                                      " +
            "             <span class='text-muted'>" + obj.Email + "</span>                                    " +
            "         </div>                                                                                   " +
            "                                                                                                  " +
            "         <div class='media-right media-middle'>                                                   " +
            "             <ul class='icons-list'>                                                              " +
            "                 <li class='dropdown'>                                                            " +
            "                     <a href='#' class='dropdown-toggle' data-toggle='dropdown'>                  " +
            "                         <i class='icon-menu7'></i>                                               " +
            "                     </a>                                                                         " +
            "                                                                                                  " +
            "                     <ul class='dropdown-menu dropdown-menu-right'>                               " +
            "                         <li><a onclick='editarUsuario(" + idUsuario + ")'><i class='icon-add pull-right'></i>Editar</a></li>       " +
            "                         <li class='divider'></li>                                                " +
            "                         <li><a onclick='excluirUsuario(" + idUsuario + ")'><i class='icon-trash pull-right'></i>Excluir</a></li>    " +
            "                     </ul>                                                                        " +
            "                 </li>                                                                            " +
            "             </ul>                                                                                " +
            "         </div>                                                                                   " +
            "     </div>                                                                                       " +
            " </div>																							";
        htmlDinamico.push(conteudo);
    });

    $("#cardUsuario").html(htmlDinamico);
}

function sucessoCadastroUsuario() {
    alert("Usuário cadastrado com sucesso");
    limparCamposTelaCadastroUsuario();
}
function erroObterUsuarios() {
    alert("Erro ao buscar usuários");
}
function limparCamposTelaCadastroUsuario() {
    let txtNomeCompletoCadastroUsuario = document.getElementById("txtNomeCompletoCadastroUsuario");
    let txtNomeCadastroUsuario = document.getElementById("txtNomeCadastroUsuario");
    let txtTelefoneCadastroUsuario = document.getElementById("txtTelefoneCadastroUsuario");
    let txtEmailCadastroUsuario = document.getElementById("txtEmailCadastroUsuario");
    let txtSenhaCadastroUsuario = document.getElementById("txtSenhaCadastroUsuario");
    let slcFuncaoCadastroUsuario = document.getElementById("slcFuncaoCadastroUsuario");
    let txtConfirmarSenhaCadastroUsuario = document.getElementById("txtConfirmarSenhaCadastroUsuario");
    let txtCpfCadastroUsuario = document.getElementById("txtCpfCadastroUsuario");


    txtNomeCompletoCadastroUsuario.value = "";
    txtNomeCadastroUsuario.value = "";
    txtTelefoneCadastroUsuario.value = "";
    txtEmailCadastroUsuario.value = "";
    txtSenhaCadastroUsuario.value = "";
    slcFuncaoCadastroUsuario.value = "";
    txtConfirmarSenhaCadastroUsuario.value = "";
    txtCpfCadastroUsuario.value = "";
}

function editarUsuario(id) {

    let usuario = {
        _id: id
    };

    requisicaoAssincrona("POST", "../Principal/BuscarDadosPessoaisById", usuario, SucessoBuscaUsuarioId, ErroBuscaUsuarioId);
}

function SucessoBuscaUsuarioId(json) {
    window.location.assign("/Principal/CadastroUsuario");
};
function ErroBuscaUsuarioId(json) {
    alert("Erro ao buscar usuário");
    window.location.assign("/Principal/CadastroUsuario");
}

function excluirUsuario(id) {
    let usuario = {
        _id: id
    };

    requisicaoAssincrona("POST", "../Usuario/ExcluirUsuario", usuario, sucessoExcluirUsuario, erroExcluirUsuario);
}

function sucessoExcluirUsuario() {
    ObterUsuarios();
}

function erroExcluirUsuario() {
    alert("Erro ao excluir usuário");
}

function cancelarCadastrarUsuario() {
    window.location.assign("../Principal/Index");
}