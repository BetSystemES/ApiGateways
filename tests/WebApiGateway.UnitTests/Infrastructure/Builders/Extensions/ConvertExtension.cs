using System.ComponentModel;

namespace WebApiGateway.UnitTests.Infrastructure.Builders.Extensions
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