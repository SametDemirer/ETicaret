using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace ETicaret.UI
{
    public static class SessionExtensions
    {
        public static T GeT<T>(this ISession session, string key)
        {
            var response = session.GetString(key);
            return response == null ? default : JsonSerializer.Deserialize<T>(response);
        }
        public static void SeT<T>(this ISession session, string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            session.SetString(key, json);
        }


    }
}
