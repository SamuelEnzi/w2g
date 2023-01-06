using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace w2g.core.standart
{
    public static class Extensions
    {
        public static T Deserialize<T>(this string json) =>
            JsonConvert.DeserializeObject<T>(json);

        public static string Serialize(this object Object) =>
            JsonConvert.SerializeObject(Object);

    }
}
