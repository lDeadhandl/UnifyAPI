using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnifyAPI.Service
{
    public interface ISpotifyService
    {
        Task SetAuthorization(string code);
    }
}
