namespace minesweeper_API
{
    public class ErrorResponse : IApiResponse
    {
        public string Status => "error";

        public string Message { get; set; }
    }
}
