using System.Collections.Generic;

namespace ProductsCRUD.Infra.Session
{
    public class SessionCache
    {
        private readonly Dictionary<string, object> Cache = new Dictionary<string, object>();

        public void Set(string key, object value)
        {
            Cache[key] = value;
        }

        public T Get<T>(string key)
        {
            if (Cache.TryGetValue(key, out var value) && value is T typedValue)
            {
                return typedValue;
            }
            return default(T);
        }

        public bool Contains(string key)
        {
            return Cache.ContainsKey(key);
        }

        public void Remove(string key)
        {
            if (Cache.ContainsKey(key))
            {
                Cache.Remove(key);
            }
        }
    }
}
