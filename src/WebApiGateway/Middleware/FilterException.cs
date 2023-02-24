namespace WebApiGateway.Middleware
{
    public class FilterException : Exception
    {
        public ExceptionObject ExceptionObject { get; set; }
        
        public FilterException(string exceptionString) : base(exceptionString)
        {
            ExceptionObject = new ExceptionObject()
            {
                StatusCode = 400,
                Successful = false,
                Error = exceptionString,
            };
        }
    }

    public class ExceptionObject
    {
        public int StatusCode { get; set; }
        public bool Successful { get; set; } = false;
        public string Error { get; set; } = null!;
    }
}
