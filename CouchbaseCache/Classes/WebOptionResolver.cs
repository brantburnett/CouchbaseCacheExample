using System;
using Couchbase;
using Couchbase.Core;

namespace CouchbaseCache.Classes
{
    public class WebOptionResolver : IWebOptionResolver
    {
        private static readonly TimeSpan CacheInterval = TimeSpan.FromMinutes(15);
        private const string OptionsKeyFormat = "WebOptions-{0}";

        private readonly ClientInfo _clientInfo;
        private readonly IBucket _bucket;

        private WebOptions _options;

        public WebOptionResolver(ClientInfo clientInfo, IBucket bucket)
        {
            _clientInfo = clientInfo;
            _bucket = bucket;
        }
        
        public WebOptions Resolve()
        {
            if (_options != null)
            {
                // Already cached in this request
                return _options;
            }

            var key = GetKey();

            var documentResult = _bucket.GetDocument<WebOptions>(key);
            if (documentResult.Success && documentResult.Content != null)
            {
                // Loaded from Couchbase cache
                _options = documentResult.Content;
            }
            else
            {
                // Need to load from database, then store in Couchbase cache
                _options = LoadFromDatabase();

                _bucket.Upsert(new Document<WebOptions>()
                {
                    Id = key,
                    Content = _options,
                    Expiry = (uint) CacheInterval.TotalMilliseconds
                });
            }

            return _options;
        }

        public void ClearCache()
        {
            _options = null;
            _bucket.Remove(GetKey());
        }

        private string GetKey()
        {
            return string.Format(OptionsKeyFormat, _clientInfo.ClientName);
        }

        private WebOptions LoadFromDatabase()
        {
            // Load from database here
            return new WebOptions()
            {
                { "key1", "value1" },
                { "key2", "value2" }
            };
        }
    }
}