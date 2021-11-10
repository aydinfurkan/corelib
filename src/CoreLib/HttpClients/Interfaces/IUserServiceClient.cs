using System;
using System.Threading.Tasks;
using CoreLib.Models.Response;

namespace CoreLib.HttpClients.Interfaces
{
    public interface IUserServiceClient
    {
        Task<VerifyUserResponseModel> VerifyUser(string token);
    }
}