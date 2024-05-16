$(document).ready(function () {
    cadastrarUsuarioLogin();
    LoginUsuario();
    ChamaTelaTrocarSenha();
    ChamaTelaDadosPessoais();
    CancelarTrocaSenha();
    AcessaEndPointTrocaSenha();
    AcessaEndPointAtualizaDadosUsuario();
    CancelarAtualizaDadosUsuario();
    //PreencheCamposCadastrarUsuario();
});

function cadastrarUsuarioLogin() {
    $("#btnRegister").off("click");
    $("#btnRegister").on("click", function () {
        let Usuario = {
            Nome: $("#txtNameRegister").val(),
            Senha: $("#txtPassWordRegister").val(),
            Email: $("#txtEmailRegister").val(),
            CPF: $("#txtCPFRegister").val(),
        };

        localStorage.setItem("EmailCadastrado", Usuario);

        requisicaoAssincrona("POST", "../Usuario/CadastrarUsuario", Usuario, SucessoCadUser, ErroCadUser);
    });
}

function LoginUsuario() {
    $("#btnEntrar").off("click");
    $("#btnEntrar").on("click", function () {
        let Usuario = {
            Email: $("#txtEmailLogin").val(),
            Senha: $("#txtSenhaLogin").val()
        };

        requisicaoAssincrona("POST", "../Principal/Login", Usuario, SucessoLoginUser, ErroLoginUser);
    });
}

function AcessaEndPointTrocaSenha() {
    $("#btnTrocaSenha").off("click");
    $("#btnTrocaSenha").on("click", function () {
        let Usuario = {
            Email: $("#txtEmail").val(),
            SenhaAtual: $("#txtSenhaAtual").val(),
            NovaSenha: $("#txtNovaSenha").val()
        };

        requisicaoAssincrona("POST", "../Principal/TrocaSenha", Usuario, SucessoTrocaSenha, ErroTrocaSenha);
    });
}

function ChamaTelaTrocarSenha() {
    $("#ChamaTrocaSenha").off("click");
    $("#ChamaTrocaSenha").on("click", function () {
        let objRet = JSON.parse(localStorage.getItem("USUARIO"));
        window.location.assign("../Principal/TrocaSenhaUsuario");
    });
}

function ChamaTelaDadosPessoais() {
    $("#ChamaDadosPessoais").off("click");
    $("#ChamaDadosPessoais").on("click", function () {
        window.location.assign("../Principal/CadastroUsuario");
    });
}

function CancelarTrocaSenha() {
    $("#btnCancelaTrocaSenha").off("click");
    $("#btnCancelaTrocaSenha").on("click", function () {
        window.location.assign("../Principal/Principal");
    });
}

function SucessoCadUser(json) {
    alert(json.retorno);

    voltarTelaLogin();
};

function ErroCadUser(json) {
    alert("Erro");
    voltarTelaLogin();
}

function SucessoLoginUser(json) {

    localStorage.removeItem("USUARIO");
    localStorage.setItem("USUARIO", json.retorno);
    let objRet = JSON.parse(json.retorno);
    if (objRet != null) {
        
        alert("Bem vindo, " + objRet.Nome + "!");
        window.location.assign("../Principal/Index");
    } else {
        alert("Usuário ou senha incorretos!");
        voltarTelaLogin();
    }
};

function ErroLoginUser(json) {
    alert("Erro Login");
    voltarTelaLogin();
}

function SucessoTrocaSenha(json) {
    alert(json.retorno);
    if (json.retorno == "Senha alterada com sucesso") {
        window.location.assign("../Principal/Index");
    } else {
        window.location.assign("/Principal/TrocaSenhaUsuario");
    }
};

function ErroTrocaSenha(json) {
    alert("Erro");
    window.location.assign("/Principal/TrocaSenhaUsuario");
}

function voltarTelaLogin() {
    window.location.assign("/");
}

function SucessoAlteraCadastro(json) {
    alert(json.retorno);

    window.location.assign("/Principal/Principal");
};

function ErroAlteraCadastro(json) {
    alert("Erro");
    window.location.assign("/Principal/CadastroUsuario");
}

function PreencheCamposCadastrarUsuario() {
    let objRet = JSON.parse(localStorage.getItem("USUARIO"));
    if (objRet.Nome != null) {
        $("#txtNomeDeUsuario").val(objRet.Nome);
        $("#txtNomeCompleto").val(objRet.NomeCompleto);
        $("#txtEmailCadastro").val(objRet.Email);
        $("#txtTelefoneCadastro").val(objRet.Telefone);
        $("#txtCidadeEstado").val(objRet.CidadeEstado);
    }
}

function AcessaEndPointAtualizaDadosUsuario() {
    $("#btnConfirmarCadastro").off("click");
    $("#btnConfirmarCadastro").on("click", function () {
        let objRet = JSON.parse(localStorage.getItem("USUARIO"));
        if (objRet.Nome != null) {

            let cadastroUsuario = {
                NomeCompleto: $("#txtNomeCompleto").val(),
                Nome: $("#txtNomeDeUsuario").val(),
                Email: $("#txtEmailCadastro").val(),
                Telefone: $("#txtTelefoneCadastro").val(),
                CidadeEstado: $("#txtCidadeEstado").val()
            }

            requisicaoAssincrona("POST", "../Principal/AtualizaDadosUsuario", cadastroUsuario, SucessoAlteraCadastro, ErroAlteraCadastro);
        };

    });
}

function CancelarAtualizaDadosUsuario() {
    $("#btnCancelarCadastro").off("click");
    $("#btnCancelarCadastro").on("click", function () {
        window.location.assign("../Principal/Principal");
    });
}