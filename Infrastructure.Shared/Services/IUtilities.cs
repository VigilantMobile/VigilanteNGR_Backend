using Application.Services.Interfaces;
using RestSharp;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;


namespace Infrastructure.Shared.Services
{
    public interface IUtilities : IAutoDependencyService
    {
        Task<RestResponse> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, Dictionary<string, string> headers);
    }


}
