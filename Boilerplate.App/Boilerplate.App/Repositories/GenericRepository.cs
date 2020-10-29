using Boilerplate.App.CrossPlatform;
using Boilerplate.App.Repositories.Contracts;
using Boilerplate.App.Services.General.Contracts;
using Boilerplate.Common.Constants;
using Boilerplate.Common.Exceptions;
using Boilerplate.Common.Extensions;
using Newtonsoft.Json;
using Polly;
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.App.Repositories
{
    public class GenericRepository : IGenericRepository
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private const short RetryNum = 3;
        private readonly IAppSettings _appSettingsService;

        public GenericRepository(IAppSettings appSettingsService)
        {
            _appSettingsService = appSettingsService;
        }

        public Task<T> GetAsync<T>(string uri, CancellationToken? token = null)
        {
            return RunWithExceptionHandlingAsync(async () =>
            {
                var httpClient = GetHttpClientWithHeaders();
                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        RetryNum,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.GetAsync(uri, token ?? CancellationToken.None));

                var response = await ParseResponseAsync<T>(responseMessage);
                return response;
            });
        }

        public Task<T> PostAsync<T>(string uri, T data, CancellationToken? token = null)
        {
            return RunWithExceptionHandlingAsync(async () =>
            {
                var httpClient = GetHttpClientWithHeaders();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        RetryNum,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content, token ?? CancellationToken.None));

                var response = await ParseResponseAsync<T>(responseMessage);
                return response;
            });
        }

        public Task<TR> PostAsync<T, TR>(string uri, T data, CancellationToken? token = null)
        {
            return RunWithExceptionHandlingAsync(async () =>
            {
                var httpClient = GetHttpClientWithHeaders();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        RetryNum,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(uri, content, token ?? CancellationToken.None));

                var response = await ParseResponseAsync<TR>(responseMessage);
                return response;
            });
        }

        public Task<T> PutAsync<T>(string uri, T data, CancellationToken? token = null)
        {
            return RunWithExceptionHandlingAsync(async () =>
            {
                var httpClient = GetHttpClientWithHeaders();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        RetryNum,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content, token ?? CancellationToken.None));

                var response = await ParseResponseAsync<T>(responseMessage);
                return response;
            });
        }

        public Task<TR> PutAsync<T, TR>(string uri, T data, CancellationToken? token = null)
        {
            return RunWithExceptionHandlingAsync(async () =>
            {
                var httpClient = GetHttpClientWithHeaders();

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        RetryNum,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(uri, content, token ?? CancellationToken.None));

                var response = await ParseResponseAsync<TR>(responseMessage);
                return response;
            });
        }

        public Task DeleteAsync(string uri, CancellationToken? token = null)
        {
            return RunWithExceptionHandlingAsync(async () =>
            {
                var httpClient = GetHttpClientWithHeaders();
                await httpClient
                    .DeleteAsync(uri, token ?? CancellationToken.None)
                    .ConfigureAwait(false);
            });
        }

        public Task<TR> DeleteAsync<TR>(string uri, CancellationToken? token = null)
        {
            return RunWithExceptionHandlingAsync(async () =>
            {
                var httpClient = GetHttpClientWithHeaders();

                var responseMessage = await httpClient.DeleteAsync(uri, token ?? CancellationToken.None);

                var response = await ParseResponseAsync<TR>(responseMessage);
                return response;
            });
        }

        private Task RunWithExceptionHandlingAsync(Func<Task> action)
        {
            try
            {
                return action();
            }
            catch (TaskCanceledException tcEx)
            {
                Debug.WriteLine($"Operation cancelled: {tcEx.Message}");
                throw;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private Task<TR> RunWithExceptionHandlingAsync<TR>(Func<Task<TR>> action)
        {
            try
            {
                return action();
            }
            catch (TaskCanceledException tcEx)
            {
                Debug.WriteLine($"Operation cancelled: {tcEx.Message}");
                throw;
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{ e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        private HttpClient GetHttpClientWithHeaders()
        {
            var acceptHeader = new MediaTypeWithQualityHeaderValue("application/json");
            if (!HttpClient.DefaultRequestHeaders.Accept.Contains(acceptHeader))
            {
                HttpClient.DefaultRequestHeaders.Accept.Add(acceptHeader);
            }

            if (!string.IsNullOrWhiteSpace(_appSettingsService.AccessToken))
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _appSettingsService.AccessToken);
            }

            // add X-Device-Id header to all requests
            if (!HttpClient.DefaultRequestHeaders.Contains(AppConstants.HEADER_DEVICE_ID))
            {
                HttpClient.DefaultRequestHeaders.Add(AppConstants.HEADER_DEVICE_ID, AppRuntime.DEVICE_ID.Value);
            }
            
            return HttpClient;
        }

        private async Task<T> ParseResponseAsync<T>(HttpResponseMessage responseMessage)
        {
            var jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject<T>(jsonResult);
                return json;
            }

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.Forbidden:
                case HttpStatusCode.Unauthorized:
                    throw new ApiAuthException(responseMessage.StatusCode, jsonResult.ToApiError());
                default:
                    throw new ApiRequestException(responseMessage.StatusCode, jsonResult.ToApiError());
            }

            throw new ApiRequestException(responseMessage.StatusCode, jsonResult);
        }
    }
}
