namespace WebApiGateway.Middleware
{
    public class FilterException : Exception
    {
        public ExceptionObject ExceptionObject;
        public FilterException(ExceptionObject exceptionObject)
            : base(exceptionObject.Error)
        {
            ExceptionObject = exceptionObject;
        }
    }

    public class ExceptionObject
    {
        public int StatusCode { get; set; }
        public bool Successful { get; set; } = false;
        public string Error { get; set; } = null!;
    }
}
