using System.ComponentModel;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.Extenstions
{
    public static class ConvertExtension
    {
        public static T Convert<T>(this string? input)
        {
            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    // Cast ConvertFromString(string text) : object to (T)
                    return (T)converter.ConvertFromString(input);
                }
                return default(T);
            }
            catch (NotSupportedException)
            {
                return default(T);
            }
        }
    }
}