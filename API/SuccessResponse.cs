namespace minesweeper_API
{
    public class SuccessResponse<T> : IApiResponse
    {
        public string Status => "success";

        public T Data { get; set; }

        public string Message { get; set; }
    }
}
