using domain_layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.Contracts
{
    public interface IJwtGenerator
    {
        string CreateToken(SystemUser user, List<string> roles);
    }
}
