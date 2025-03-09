using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Webgame26
{
    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var json = session.GetString(key);
            return json == null ? default : JsonSerializer.Deserialize<T>(json);
        }

        public static void SetBool(this ISession session, string key, bool value)
        {
            session.SetString(key, value.ToString());
        }

        public static bool GetBool(this ISession session, string key)
        {
            var value = session.GetString(key);
            return bool.TryParse(value, out bool result) && result;
        }
    }
}
