using System;
using System.Reflection;

namespace EAU.Utilities
{
    public static class ObjectHelpers
    {
        public static object CloneObject(this object objSource)
        {
            Type type = objSource.GetType();
            object obj = Activator.CreateInstance(type);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            PropertyInfo[] array = properties;
            foreach (PropertyInfo propertyInfo in array)
            {
                if (!propertyInfo.CanWrite)
                {
                    continue;
                }

                if (propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsEnum || propertyInfo.PropertyType.Equals(typeof(string)))
                {
                    propertyInfo.SetValue(obj, propertyInfo.GetValue(objSource, null), null);
                    continue;
                }

                object value = propertyInfo.GetValue(objSource, null);
                if (value == null)
                {
                    propertyInfo.SetValue(obj, null, null);
                }
                else
                {
                    propertyInfo.SetValue(obj, value.CloneObject(), null);
                }
            }

            return obj;
        }
    }
}
