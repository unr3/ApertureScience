using System;
using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace ApertureScience.Library.Extension
{
    public static class ObjectExtensions
    {
        public static string GetMessageType(this object obj)
        {
            return obj.GetType().AssemblyQualifiedName;
        }

        public static string ToJsonString(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static Stream ToJsonStream(this object obj)
        {
            var json = obj.ToJsonString();
            return new MemoryStream(Encoding.Default.GetBytes(json));
        }
    }
}
