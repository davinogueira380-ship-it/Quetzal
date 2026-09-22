//#region Validação
//Não mexer (caso queira mexer chamar por Davi)
//Validação de formúlario

// function validarFormulario(){
//     alert("Função fucionando")

//     if (document.getElementById('Nome').value.trim() == ""){
//     alert("Digite o seu nome")
//     return false
//     }

//      if (document.getElementById('Email').value.trim() == ""){
//         alert("Campo E-mail é obrigatório")
//         return false
//     }else if (validarEmail(document.getElementById('Email').value.trim()) == false){
//         alert("Email está incorreto")
//         return false
//     }
//         if (document.getElementById('Telefone').value.trim() == ""){
//         alert("Campo telefone é obrigatório")
//         return false
//     }else if(document.getElementById('Telefone').value.trim().length !=11){
//         alert("Telefone invalido")
//         return false
//     }

//     var opcoes = document.getElementsByName('FormaContato')
//     return false
//     var qtdSelecionado = 0

//     for (let i = 0; i < opcoes.length; i++){
//         if (opcoes[i].checked == true){
//             qtdSelecionado++
//             break
//         }
//     }

//      if (qtdSelecionado == 0){
//         alert("Selecione uma forma de contato")
//         return false
//     }
// }

// Validando Email
// function validarEmail(email){
//     return /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
// }

// #endregion

// #region Instagram parte Sthefany
function rolarEsquerdaIndex() {
    const galeria = document.getElementById("galeria");

    galeria.scrollBy({
        left: -300,
        behavior: "smooth"
    });
}

function rolarDireitaIndex() {
    const galeria = document.getElementById("galeria");

    galeria.scrollBy({
        left: 300,
        behavior: "smooth"
    });
} 

//#endregion

// #region carrossel Portifolio

// #region Portfolio
function rolarDireitaPortfolio(botao) {
    const galeria = botao.parentElement.querySelector('.instagram-galeria');
    galeria.scrollBy({
        left: 300,
        behavior: 'smooth'
    });
}

function rolarEsquerdaPortfolio(botao) {
    const galeria = botao.parentElement.querySelector('.instagram-galeria');
    galeria.scrollBy({
        left: -300,
        behavior: 'smooth'
    });
}
//#endregion
