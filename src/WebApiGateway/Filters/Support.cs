using System.Reflection;

namespace WebApiGateway.Filters
{
    // TODO: this one should be removed. And Non Extension Static classes should be placed under "Helpers" Folder
    public static class Support
    {
        public static bool IsAnyNullOrEmpty(object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
