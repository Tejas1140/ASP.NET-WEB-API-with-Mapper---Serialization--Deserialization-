using System.Diagnostics;

namespace WebApp2.Model.ResponseFormat
{
    public class Result<T>
    {
        public string? traceId { get; private set; }
        public int statusCode { get; private set; }
        public string? message { get; private set; }
        public T? data { get; private set; }

        public Result(int StatusCode, string Message, T? Data)
        {
            this.traceId = Activity.Current?.TraceId.ToString();
            this.statusCode = StatusCode;
            this.message = Message;
            this.data = Data;
        }
    }
}