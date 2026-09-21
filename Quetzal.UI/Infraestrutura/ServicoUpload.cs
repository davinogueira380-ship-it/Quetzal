namespace Quetzal.UI.Infraestrutura
{
    public class ResultadoUpload
    {
        public bool Sucesso { get; init; }
        public string? CaminhoRelativo { get; init; }
        public string? Erro { get; init; }

        public static ResultadoUpload Ok(string caminho)
            => new() { Sucesso = true, CaminhoRelativo = caminho };

        public static ResultadoUpload Falha(string erro)
            => new() { Sucesso = false, Erro = erro };
    }

    public class ServicoUpload
    {
        private readonly IWebHostEnvironment _ambiente;

        private static readonly string[] ExtensoesPermitidas =
            { ".jpg", ".jpeg", ".png", ".webp" };

        private const long TamanhoMaximoBytes = 5 * 1024 * 1024; // 5 MB

        public ServicoUpload(IWebHostEnvironment ambiente)
        {
            _ambiente = ambiente;
        }

        // ↓ era Task<ServicoUpload> — o método precisa devolver o RESULTADO, não o serviço
        public async Task<ResultadoUpload> SalvarImagemAsync(IFormFile? arquivo, string subpasta)
        {
            if (arquivo is null || arquivo.Length == 0)
            {
                // ↓ era ServicoUpload.Falha
                return ResultadoUpload.Falha("Nenhum arquivo foi enviado.");
            }

            if (arquivo.Length > TamanhoMaximoBytes)
            {
                // ↓ era ServicoUpload.Falha
                return ResultadoUpload.Falha("A imagem deve ter no máximo 5 MB.");
            }

            // ToLowerInvariant porque ".JPG" e ".jpg" são o mesmo tipo
            var extensao = Path.GetExtension(arquivo.FileName).ToLowerInvariant();

            if (!ExtensoesPermitidas.Contains(extensao))
            {
                // ↓ era ServicoUpload.Falha
                return ResultadoUpload.Falha("Formato inválido. Use JPG, PNG ou WEBP.");
            }

            // Nome novo e aleatório: descarta o nome original por completo
            var nomeArquivo = $"{Guid.NewGuid():N}{extensao}";
            var pastaFisica = Path.Combine(_ambiente.WebRootPath, "uploads", subpasta);

            Directory.CreateDirectory(pastaFisica);

            var caminhoFisico = Path.Combine(pastaFisica, nomeArquivo);
            await using (var stream = new FileStream(caminhoFisico, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            // ↓ era ServicoUpload.Ok
            return ResultadoUpload.Ok($"/uploads/{subpasta}/{nomeArquivo}");
        }
    }
}