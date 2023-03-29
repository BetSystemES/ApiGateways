using Google.Protobuf.WellKnownTypes;

namespace WebApiGateway.Mapper.Extensions
{
    public  static class Converters
    {
        public static Timestamp ToTimestamp(this DateTimeOffset dateTimeOffset)
        {
            return Timestamp.FromDateTimeOffset(dateTimeOffset);
        }
    }
}