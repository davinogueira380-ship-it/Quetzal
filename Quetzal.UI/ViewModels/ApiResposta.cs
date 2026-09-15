namespace Quetzal.UI.ViewModels
{
    public class ApiResposta<T>
    {
        // Indica se a operacao na API foi concluida com sucesso
        // false significa que houve erro de validacao, negocio ou HTTP
        public bool Sucesso { get; set; }

        // Dados retornados pela API quando a operacao e bem-sucedida
        // Pode ser nulo em operacoes que nao retornam dados (ex: deletar)
        public T? Dados { get; set; }

        // Mensagem de texto retornada pela API
        // Em caso de sucesso: mensagem informativa (ex: "Filme criado com sucesso")
        // Em caso de erro: descricao do problema (ex: "Email ja cadastrado")
        public string? Mensagem { get; set; }

        // Lista de erros de validacao retornados pela API
        // Util para exibir multiplos erros de formulario ao usuario
        public List<string> Erros { get; set; } = new List<string>();

        // Metodo estatico auxiliar para criar uma resposta de sucesso sem dados
        // Util para operacoes como deletar que nao retornam objeto
        public static ApiResposta<T> Ok(string mensagem = "Operacao realizada com sucesso.")
        {
            return new ApiResposta<T>
            {
                Sucesso = true,
                Mensagem = mensagem
            };
        }

        // Metodo estatico auxiliar para criar uma resposta de sucesso com dados
        public static ApiResposta<T> Ok(T dados, string mensagem = "Operacao realizada com sucesso.")
        {
            return new ApiResposta<T>
            {
                Sucesso = true,
                Dados = dados,
                Mensagem = mensagem
            };
        }

        // Metodo estatico auxiliar para criar uma resposta de erro
        public static ApiResposta<T> Falha(string mensagem)
        {
            return new ApiResposta<T>
            {
                Sucesso = false,
                Mensagem = mensagem
            };
        }
    }
}

