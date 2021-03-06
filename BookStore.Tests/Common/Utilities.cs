using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xunit.Sdk;

namespace BookStore.Tests.Common;

public class Utilities
{
    public static StringContent GetRequestContent(object obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
    }

    public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
    {
        var stringResponse = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<T>(stringResponse);

        return result ?? throw new NullException("Response is null");
    }
}