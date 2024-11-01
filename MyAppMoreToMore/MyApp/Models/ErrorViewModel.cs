//reprezentuje dane aplikacji  tutaj mamy error model do ob³ugi errorów
namespace MyAppMoreToMore.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
