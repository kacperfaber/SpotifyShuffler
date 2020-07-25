﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

        public async Task<HttpResponseMessage> SendAsync(string url, object body, HttpMethod method, SpotifyAuthorization spotifyAuthorization)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8),
                RequestUri = new Uri(url)
            };

            request.Headers.Add("Authorization", spotifyAuthorization.GetToken());

            return await Http.SendAsync(request);
        }

        public async Task<TResult> SendAsync<TResult>(string url, object body, HttpMethod method, SpotifyAuthorization spotifyAuthorization)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                Content = new StringContent(JsonConvert.SerializeObject(body)),
                RequestUri = new System.Uri(url)
            };

            request.Headers.Add("Authorization", spotifyAuthorization.GetToken());

            HttpResponseMessage response = await Http.SendAsync(request);

            return JsonConvert.DeserializeObject<TResult>(response.Content.ReadAsStringAsync().Result);
        }

        public async Task<TResult> SendAsync<TResult>(string url, object queryParameters, object body, HttpMethod method, SpotifyAuthorization spotifyAuthorization)
        {
            HttpRequestMessage request = new HttpRequestMessage
            {
                Method = method,
                Content = body != null ? new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json") : new StringContent(string.Empty),
                RequestUri = new System.Uri(QueryGenerator.Generate(url, InstanceToDictionaryConverter.Convert(queryParameters)))
            };
            
            request.Headers.Add("Authorization", spotifyAuthorization.GetToken());
            request.Headers.Add("Accept", "*/*");

            HttpResponseMessage response = await Http.SendAsync(request);
            string result = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<TResult>(result);
        }
    }
}