namespace BookStore.Order.Model
{
    public class ResponseModel
    {
        public object? Data { get; set; }
        public bool IsSucess { get; set; }
        public string Message { get; set; }
    }
}
