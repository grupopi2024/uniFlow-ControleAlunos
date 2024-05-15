$(document).ready(function () {
    let path = $(location).attr("pathname");

    if (path.toLowerCase() === '/aluno/editaraluno') {
        verificaUsuarioAdm();

    }
});

function formataMascaraEditarTelefoneAluno() {
    $("#txtTelefoneEditarAluno").mask('(00) 0 0000-0000', { reverse: false });
}
function edicaoAluno() {

    let slcSerie = document.getElementById('slcSerieAlunoEditar')

    let Aluno = {
        NomeCompleto: $("#txtNomeCompletoEditarAluno").val(),
        Email: $("#txtEmailEditarAluno").val(),
        Telefone: $('#txtTelefoneEditarAluno').val(),
        RA: $("#txtRegistroAcademicoEditar").val(),
        Serie: slcSerie.value,
    };

    requisicaoAssincrona("POST", "../Aluno/EdicaoAluno", Aluno, sucessoEdicaoAluno, erroObterAluno);
};

function sucessoEdicaoAluno(json) {
    alert("Aluno editado com sucesso"); 
        limparCamposTelaEditarAluno();
        window.location.assign("../Aluno/ListarAluno");

}
function erroObterAluno() {
    alert("erro")
}
function limparCamposTelaEditarAluno() {
    let slcSerieAluno = document.getElementById("slcSerieAlunoEditar");
    let txtNomeCompletoEditarAluno = document.getElementById("txtNomeCompletoEditarAluno");
    let txtEmailEditarAluno = document.getElementById("txtEmailEditarAluno");
    let txtTelefoneEditarAluno = document.getElementById("txtTelefoneEditarAluno");
    let txtRegistroAcademico = document.getElementById("txtRegistroAcademicoEditar");

    slcSerieAluno.value = "0";
    txtNomeCompletoEditarAluno.value = "";
    txtEmailEditarAluno.value = "";
    txtTelefoneEditarAluno.value = "";
    txtRegistroAcademico.value = "";

}

