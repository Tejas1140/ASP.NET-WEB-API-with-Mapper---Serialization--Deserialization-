namespace WebApp2.Model.ResponseModel
{
    public class CommonStatusResponse<T>
    {
        public int statusCode { get; set; }
        public string? statusMsg { get; set; }
        public T? Data { get; set; }
    }
}
