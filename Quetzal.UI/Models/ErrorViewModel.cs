namespace Quetzal.UI.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        // A view só mostra o bloco do ID se ele existir
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}