using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client_2.Services
{
    public interface IApiResourceHttpClient
    {
        Task<HttpClient> GetHttpClient();
    }
}
