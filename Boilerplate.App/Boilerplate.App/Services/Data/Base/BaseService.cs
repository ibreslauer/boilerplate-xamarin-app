using Akavache;
using Boilerplate.App.Bootstrap;
using Boilerplate.App.Repositories.Contracts;
using Boilerplate.App.Services.General.Contracts;
using Boilerplate.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Boilerplate.App.Services.Data.Base
{
    public class BaseService
    {
        private Lazy<IBlobCache> _cache = new Lazy<IBlobCache>(() => AppContainer.Resolve<IBlobCache>() ?? BlobCache.LocalMachine);
        private Lazy<IAppSettings> _settingsService = new Lazy<IAppSettings>(() => AppContainer.Resolve<IAppSettings>());
        private Lazy<IGenericRepository> _genericRepository = new Lazy<IGenericRepository>(() => AppContainer.Resolve<IGenericRepository>());

        protected IBlobCache Cache => _cache.Value;
        protected IAppSettings SettingsService => _settingsService.Value;
        protected IGenericRepository GenericRepository => _genericRepository.Value;

        public async Task<T> GetFromCache<T>(string cacheName)
        {
            try
            {
                T t = await Cache.GetObject<T>(cacheName);
                return t;
            }
            catch (KeyNotFoundException)
            {
                return default(T);
            }
        }

        protected virtual string GetUri(string endpoint, string action = null, object parameter = null)
        {
            var actionPath = string.IsNullOrWhiteSpace(action) ? endpoint : $"{endpoint}/{action}";
            var fullPath = parameter.IsNullOrEmpty() ? actionPath : $"{actionPath}/{parameter}";

            UriBuilder builder = new UriBuilder(SettingsService.ServerUrlSetting)
            {
                Path = fullPath
            };

            return builder.ToString();
        }

        protected virtual string GetUri(string endpoint, string action = null, params object[] parameters)
        {
            var actionPath = string.IsNullOrWhiteSpace(action) ? endpoint : $"{endpoint}/{action}";
            var fullPath = parameters.Length == 0 ? actionPath : $"{actionPath}/{string.Join("/", parameters)}";

            UriBuilder builder = new UriBuilder(SettingsService.ServerUrlSetting)
            {
                Path = fullPath
            };

            return builder.ToString();
        }

        protected virtual string GetUri(string endpoint, IEnumerable<KeyValuePair<string, object>> queryParams, string action = null)
        {
            var path = string.IsNullOrWhiteSpace(action) ? 
                endpoint : 
                $"{endpoint}/{action}";

            UriBuilder builder = new UriBuilder(SettingsService.ServerUrlSetting) { Path = path };
            builder.Query = queryParams.Count() > 0 ?
                string.Join("&", queryParams.Select(p => ParamToQueryString(p))) :
                null;

            return builder.ToString();
        }

        private string ParamToQueryString(KeyValuePair<string, object> p)
        {
            if (p.Value is IEnumerable<object> list)
            {
                return ListParamToQueryString(p.Key, list);
            }
            return $"{p.Key}={p.Value}";
        }

        private string ListParamToQueryString(string key, IEnumerable<object> list)
        {
            return string.Join("&", list.Select(x => $"{key}={x}"));
        }

        public void InvalidateCache()
        {
            // Cache.InvalidateAllObjects<[SomeCachedModel]>();
        }
    }
}
