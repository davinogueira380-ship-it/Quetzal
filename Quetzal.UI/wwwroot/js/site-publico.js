
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

// #region Galeria da capa do portfólio (Home)
var galeriaPortfolioFotos = [];
var galeriaPortfolioNome = '';
var galeriaPortfolioIndice = -1;

function abrirGaleriaPortfolio(elemento) {
    var fotos = [];
    try {
        fotos = JSON.parse(elemento.getAttribute('data-fotos') || '[]');
    } catch (erro) {
        fotos = [];
    }
    var nome = elemento.getAttribute('data-nome') || '';

    var overlay = document.getElementById('galeriaPortfolioOverlay');
    var titulo = document.getElementById('galeriaPortfolioTitulo');
    var miniaturas = document.getElementById('galeriaPortfolioMiniaturas');
    if (!overlay || !titulo || !miniaturas) {
        return;
    }

    galeriaPortfolioFotos = fotos;
    galeriaPortfolioNome = nome;

    titulo.textContent = nome;
    miniaturas.innerHTML = '';

    fotos.forEach(function (foto, indice) {
        var miniatura = document.createElement('img');
        miniatura.src = foto;
        miniatura.alt = nome;
        miniatura.loading = 'lazy';
        miniatura.addEventListener('click', function () {
            ampliarImagemPortfolio(indice);
        });
        miniaturas.appendChild(miniatura);
    });

    overlay.classList.add('ativa');
    document.body.classList.add('sem-scroll');
}

function fecharGaleriaPortfolio() {
    var overlay = document.getElementById('galeriaPortfolioOverlay');
    if (overlay) {
        overlay.classList.remove('ativa');
    }
    document.body.classList.remove('sem-scroll');
}

function ampliarImagemPortfolio(indice) {
    var ampliada = document.getElementById('galeriaPortfolioAmpliada');
    var imagem = document.getElementById('galeriaPortfolioImagemAmpliada');
    if (!ampliada || !imagem || !galeriaPortfolioFotos[indice]) {
        return;
    }

    galeriaPortfolioIndice = indice;
    imagem.src = galeriaPortfolioFotos[indice];
    imagem.alt = galeriaPortfolioNome || '';
    ampliada.classList.add('ativa');
    atualizarSetasGaleriaPortfolio();
}

function atualizarSetasGaleriaPortfolio() {
    var setas = document.querySelectorAll('.galeria-portfolio-seta');
    var mostrarSetas = galeriaPortfolioFotos.length > 1;
    setas.forEach(function (seta) {
        seta.style.display = mostrarSetas ? '' : 'none';
    });
}

// delta: -1 = foto anterior, 1 = próxima foto (com efeito circular)
function mudarImagemAmpliada(delta) {
    if (!galeriaPortfolioFotos.length) {
        return;
    }

    var novoIndice = (galeriaPortfolioIndice + delta + galeriaPortfolioFotos.length) % galeriaPortfolioFotos.length;
    ampliarImagemPortfolio(novoIndice);
}

function fecharImagemAmpliada() {
    var ampliada = document.getElementById('galeriaPortfolioAmpliada');
    if (ampliada) {
        ampliada.classList.remove('ativa');
    }
}

// Esc fecha primeiro a imagem ampliada e, se não houver, a galeria de miniaturas.
// Setas do teclado trocam a foto enquanto a imagem ampliada estiver aberta.
document.addEventListener('keydown', function (evento) {
    var ampliada = document.getElementById('galeriaPortfolioAmpliada');
    var ampliadaAtiva = ampliada && ampliada.classList.contains('ativa');

    if (ampliadaAtiva && evento.key === 'ArrowLeft') {
        mudarImagemAmpliada(-1);
        return;
    }

    if (ampliadaAtiva && evento.key === 'ArrowRight') {
        mudarImagemAmpliada(1);
        return;
    }

    if (evento.key !== 'Escape') {
        return;
    }

    if (ampliadaAtiva) {
        fecharImagemAmpliada();
        return;
    }

    var overlay = document.getElementById('galeriaPortfolioOverlay');
    if (overlay && overlay.classList.contains('ativa')) {
        fecharGaleriaPortfolio();
    }
});
// #endregion