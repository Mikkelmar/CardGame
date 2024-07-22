using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Engine
{
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    public static class ObjectExtensions
    {
        public static T DeepCopy<T>(this T obj)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, obj);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(memoryStream);
            }
        }

        public static List<T> DeepCopyList<T>(this List<T> list)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, list);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (List<T>)formatter.Deserialize(memoryStream);
            }
        }
    }
}
