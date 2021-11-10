using System;
using System.Net.Http;
using System.Threading.Tasks;
using CoreLib.Extensions;
using CoreLib.HttpClients.Interfaces;
using CoreLib.Models.Response;

namespace CoreLib.HttpClients
{
    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _client;

        public UserServiceClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<VerifyUserResponseModel> VerifyUser(string token)
        {
            var response = await _client.PostAsync("verify", new JsonContent(token));
            var result = await response.DeserializeAsync<VerifyUserResponseModel>();
            return result;
        }
    }
}