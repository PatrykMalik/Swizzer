using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swizzer.Shared.Serializers
{
    public static class SwizzerJsonSerializer
    {
        public static string Serialize(object obj)
            => JsonConvert.SerializeObject(obj);
        public static TObject Deserialize<TObject>(string json)
            => JsonConvert.DeserializeObject<TObject>(json);
    }
}
