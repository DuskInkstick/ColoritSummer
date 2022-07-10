using ColoritSummer.Interfaces.Auth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ColoritSummery.WebAPIClient.Auth
{ 
    public class AuthClient<TLog, TReg> : IAuthClient<TLog, TReg>
        where TLog : IAuthInfo
        where TReg : IAuthInfo
    {
        private readonly HttpClient _client;
        public AuthClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> Login(TLog info)
        {
            var response = await _client.PostAsJsonAsync("login", info).ConfigureAwait(false);
            var result = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync();
            return result;
        }

        public async Task<bool> Registration(TReg info)
        {
            var response = await _client.PostAsJsonAsync("registration", info).ConfigureAwait(false);
            // Я не уверен, нужна ли эта строка
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
                return true;

            return false;
            
        }
    }
}
