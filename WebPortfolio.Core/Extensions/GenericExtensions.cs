using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

namespace WebPortfolio.Core.Extensions
{
    public static class GenericExtensions
    {
        public static IList CreateList(this Type myType)
        {
            Type genericListType = typeof(List<>).MakeGenericType(myType);
            return (IList)Activator.CreateInstance(genericListType);
        }

        public static byte[] ToByteArray(this Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        // Convert a byte array to an Object
        public static Object ToObject(this byte[] arrBytes)
        {
            MemoryStream memStream = new MemoryStream();
            BinaryFormatter binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            Object obj = (Object)binForm.Deserialize(memStream);
            return obj;
        }


        public static T Clone<T>(this T item)
        {
            FieldInfo[] fis = item.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            object tempMyClass = Activator.CreateInstance(item.GetType());
            foreach (FieldInfo fi in fis)
            {
                if (fi.FieldType.Namespace != item.GetType().Namespace)
                    fi.SetValue(tempMyClass, fi.GetValue(item));
                else
                {
                    object obj = fi.GetValue(item);
                    if(obj != null)
                        fi.SetValue(tempMyClass, obj.Clone());
                }
            }
            return (T)tempMyClass;
        }

        public static object ValueOrNull(this object x)
        {
            switch (x.GetType().Name.ToLower())
            {
                case "string":
                    return string.IsNullOrEmpty(x.ToString()) ? null : x;
            }

            return x;
        }

    }
}