using Newtonsoft.Json;
using System.Text;

namespace DevOpsWebApp.Utils
{
    public static class Extensions
    {
        public static T Get<T>(this ISession session, string key)
        {
            var str = session.GetString(key);
            if (!string.IsNullOrEmpty(str))
            {
                var obj = JsonConvert.DeserializeObject<T>(str);
                return obj!;
            }
            return default!;
        }
    }
}
