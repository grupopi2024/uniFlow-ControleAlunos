$(document).ready(function () {
    let path = $(location).attr("pathname");

    if (path.toLowerCase() === '/aluno/cadastroaluno') {
        verificaUsuarioAdm();
        
    }
});

function formataMascaraCadastroTelefoneAluno() {
    $("#txtTelefoneCadastroAluno").mask('(00) 0 0000-0000', { reverse: false });
}
function cadastrarAluno() {
     
    let slcSerie = document.getElementById('slcSerieAluno')

    let Aluno = {
        NomeCompleto: $("#txtNomeCompletoCadastroAluno").val(),
        Email: $("#txtEmailCadastroAluno").val(),
        Telefone:$('#txtTelefoneCadastroAluno').val(),
        RA: $("#txtRegistroAcademico").val(),
        Serie: slcSerie.value,
    };

    requisicaoAssincrona("POST", "../Aluno/CadastrarAluno", Aluno, sucessoEditarAluno, erroObterAluno);
};

function sucessoEditarAluno(json) {
    alert(json.retorno)
    if (json.retorno == "Cadastro realizado com sucesso!") {
        limparCamposTelaCadastroAluno();
        let txtNomeCompletoCadastroAluno = document.getElementById("txtNomeCompletoCadastroAluno");
        txtNomeCompletoCadastroAluno.focus();
    };
    
}
function erroObterAluno() {
    alert("erro")
}
function limparCamposTelaCadastroAluno() {
    let slcSerieAluno = document.getElementById("slcSerieAluno");
    let txtNomeCompletoCadastroAluno = document.getElementById("txtNomeCompletoCadastroAluno");
    let txtEmailCadastroAluno = document.getElementById("txtEmailCadastroAluno");
    let txtTelefoneCadastroAluno = document.getElementById("txtTelefoneCadastroAluno");
    let txtRegistroAcademico = document.getElementById("txtRegistroAcademico");
    
    slcSerieAluno.value = "0";
    txtNomeCompletoCadastroAluno.value = "";
    txtEmailCadastroAluno.value = "";
    txtTelefoneCadastroAluno.value = "";
    txtRegistroAcademico.value = "";   
}

function cancelarCadastrarAluno() {
    window.location.assign("../Principal/Index");
}