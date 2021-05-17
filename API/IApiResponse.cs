namespace minesweeper_API
{
    public interface IApiResponse
    {
        public string Status { get; }

        public string Message { get; set; }
    }
}
