function requisicaoAssincrona(tipoRequisicao, caminhoMetodo, parametroJson, funcaoJsSucesso, funcaoJsErro) {

    $.ajax({
        type: tipoRequisicao,
        url: caminhoMetodo,
        async: true,
        data: JSON.stringify(parametroJson),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (Json) {
            funcaoJsSucesso(Json);
        },
        error: function (Json) {
            funcaoJsErro(Json);
        }
    });
}
function verificaUsuarioAdm() {
    
    let sessaoUsuario = localStorage.getItem("USUARIO");
    let usuario = JSON.parse(sessaoUsuario);

    if (usuario.Funcao != 0) {
        alert("Você não tem permissão para acessar essa página! ");
        window.location.assign("../Principal/Index");
    };
}
function editarAdmUsuario(json) {
    let objRet = JSON.parse(json.retorno);
    if (objRet != undefined) {
        alert(objRet.NomeCompleto)
        let btnConfirmarCadastroUsuario = document.getElementById("btnConfirmarCadastroUsuario"); 
        let btnConfirmarEditarUsuario = document.getElementById("btnConfirmarEditarUsuario");
        btnConfirmarEditarUsuario.style.display = "block"
        btnConfirmarCadastroUsuario.style.display = "none"
    }
}