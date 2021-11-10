using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoreLib.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> DeserializeAsync<T>(this HttpResponseMessage message)
        {
            var data = await message.Content.ReadAsStringAsync();

            if (data != null)
            {
                return JsonConvert.DeserializeObject<T>(data);
            }

            return default;
        }
    }

    public class JsonContent : StringContent
    {
        public JsonContent(object obj)
            : base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        {
        }
    }
}