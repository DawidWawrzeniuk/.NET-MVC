//reprezentuje dane aplikacji  tutaj mamy error model do ob�ugi error�w
namespace MyApp.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
