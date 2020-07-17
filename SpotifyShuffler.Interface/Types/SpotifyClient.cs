using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpotifyShuffler.Interface
{
    public class SpotifyClient
    {
        HttpClient Http = new HttpClient();

        public IQueryGenerator QueryGenerator;
        public IInstanceToDictionaryConverter InstanceToDictionaryConverter;

        public SpotifyClient(IInstanceToDictionaryConverter instanceToDictionaryConverter, IQueryGenerator queryGenerator)
        {
            InstanceToDictionaryConverter = instanceToDictionaryConverter;
            QueryGenerator = queryGenerator;
        }

        public async Task<HttpResponseMessage> SendAsync(string url, object body, HttpMethod method, Authorization authorization)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                Content = new StringContent(JsonConvert.SerializeObject(body)),
                RequestUri = new Uri(url)
            };

            request.Headers.Add("Authorization", authorization.GetToken());

            return await Http.SendAsync(request);
        }

        public async Task<TResult> SendAsync<TResult>(string url, object body, HttpMethod method, Authorization authorization)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                Content = new StringContent(JsonConvert.SerializeObject(body)),
                RequestUri = new Uri(url)
            };

            request.Headers.Add("Authorization", authorization.GetToken());

            HttpResponseMessage response = await Http.SendAsync(request);

            return JsonConvert.DeserializeObject<TResult>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<TResult> SendAsync<TResult>(string url, object queryParameters, object body, HttpMethod method, Authorization authorization)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                Content = new StringContent(JsonConvert.SerializeObject(body)),
                RequestUri = new Uri(QueryGenerator.Generate(url, InstanceToDictionaryConverter.Convert(queryParameters)))
            };
            
            request.Headers.Add("Authorization", authorization.GetToken());

            HttpResponseMessage response = await Http.SendAsync(request);

            return JsonConvert.DeserializeObject<TResult>(response.Content.ReadAsStringAsync().Result);
        }
    }
}