namespace Boilerplate.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null)
                return true;
            
            if (obj is string strObject)
                return string.IsNullOrWhiteSpace(strObject);

            return false;
        }
    }
}
