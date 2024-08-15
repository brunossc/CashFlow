using System.Text.Json;

namespace CashFlow.Sidecar
{
    public class ErrorResponse
    {
        public static bool Success => false;
        public int StatusCode { get; set; }
        //public Exception Exception { get; set; }
        public string Message { get; set; }
        public int Reason { get; set; }

        public override string ToString() =>
            JsonSerializer.Serialize(this);
    }
}
