using ColoritSummer.Interfaces.Entities;
using ColoritSummer.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ColoritSummery.WebAPIClient.Repository
{
    public class WebRepository<T> : IRepository<T> where T : IEntity
    {
        private HttpClient _client;
        public WebRepository(HttpClient client)
        {
            _client = client;
        }

        public async Task<T> Add(T item, CancellationToken cancel = default)
        {
            var response = await _client.PostAsJsonAsync("", item, cancel).ConfigureAwait(false);
            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);
            return result;
        }

        public async Task<T> DeleteById(int id, CancellationToken cancel = default)
        {
            var response = await _client.DeleteAsync($"{id}", cancel).ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.NotFound)
                return default;

            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadFromJsonAsync<T>(cancellationToken: cancel)
                .ConfigureAwait(false);

            return result;
        }

        public async Task<IEnumerable<T>> Get(int skip, int count, CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<IEnumerable<T>>($"items[{skip}:{count}]", cancel).ConfigureAwait(false);
        }

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<IEnumerable<T>>("all", cancel).ConfigureAwait(false);
        }

        public async Task<T> GetById(int id, CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<T>($"{id}", cancel).ConfigureAwait(false);
        }

        public async Task<int> GetCount(CancellationToken cancel = default)
        {
            return await _client.GetFromJsonAsync<int>("count", cancel).ConfigureAwait(false);
        }

        public async Task<T> Update(T item, CancellationToken cancel = default)
        {
            var response = await _client.PutAsJsonAsync("", item, cancel).ConfigureAwait(false);
            var result = await response
              .EnsureSuccessStatusCode()
              .Content
              .ReadFromJsonAsync<T>(cancellationToken: cancel)
              .ConfigureAwait(false);
            return result;
        }
    }
}
