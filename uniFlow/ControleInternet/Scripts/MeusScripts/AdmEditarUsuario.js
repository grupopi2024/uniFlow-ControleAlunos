$(document).ready(function () {
    let path = $(location).attr("pathname");

    if (path.toLowerCase() === '/usuario/editarusuario') {
        verificaUsuarioAdm();
        verificaFuncaoUsuario();
    }
});

function verificaFuncaoUsuario() {

}

function formataMascaraEditarTelefoneUsuario() {
    $("#txtTelefoneEditarUsuario").mask('(00) 0 0000-0000', { reverse: false });
}

function edicaoUsuario() {
    let txtSenha = $("#txtSenhaEditarUsuario").val();
    let txtConfirmaSenha = $("#txtConfirmarSenhaEditarUsuario").val();
    let slcFuncao = document.getElementById('slcFuncaoEditarUsuario')

    if (txtSenha != txtConfirmaSenha) {
        alert("Senhas diferentes");
        return;
    }

    let Usuario = {
        NomeCompleto: $("#txtNomeCompletoEditarUsuario").val(),
        Nome: $("#txtNomeEditarUsuario").val(),
        Telefone: $("#txtTelefoneEditarUsuario").val(),
        Email: $("#txtEmailEditarUsuario").val(),
        CPF: $("#txtCpfEditarUsuario").val(),
        Senha: $("#txtSenhaEditarUsuario").val(),
        Funcao: slcFuncao.value,
    };

    requisicaoAssincrona("POST", "../Usuario/EdicaoUsuario", Usuario, sucessoEdicaoUsuario, erroEditarUsuarios);
};

function sucessoEdicaoUsuario() {
    alert("Usuário editado com sucesso");
    limparCamposTelaEditarUsuario();
    window.location.assign("../Usuario/ListarUsuario");
}

function erroEditarUsuarios() {
    alert("Erro ao editar usuários");
}

function limparCamposTelaEditarUsuario() {
    let txtNomeCompletoEditarUsuario = document.getElementById("txtNomeCompletoEditarUsuario");
    let txtNomeEditarUsuario = document.getElementById("txtNomeEditarUsuario");
    let txtTelefoneEditarUsuario = document.getElementById("txtTelefoneEditarUsuario");
    let txtEmailEditarUsuario = document.getElementById("txtEmailEditarUsuario");
    let txtSenhaEditarUsuario = document.getElementById("txtSenhaEditarUsuario");
    let slcFuncaoEditarUsuario = document.getElementById("slcFuncaoEditarUsuario");
    let txtConfirmarSenhaEditarUsuario = document.getElementById("txtConfirmarSenhaEditarUsuario");
    let txtCpfEditarUsuario = document.getElementById("txtCpfEditarUsuario");


    txtNomeCompletoEditarUsuario.value = "";
    txtNomeEditarUsuario.value = "";
    txtTelefoneEditarUsuario.value = "";
    txtEmailEditarUsuario.value = "";
    txtSenhaEditarUsuario.value = "";
    slcFuncaoEditarUsuario.value = "";
    txtConfirmarSenhaEditarUsuario.value = "";
    txtCpfEditarUsuario.value = "";
}