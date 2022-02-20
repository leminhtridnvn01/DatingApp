using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asp_net_core.DatingApp.DatingApp.API.Database.Entities;

namespace asp_net_core.DatingApp.DatingApp.API.Service
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}