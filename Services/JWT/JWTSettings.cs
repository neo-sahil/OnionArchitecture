using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.JWT
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public int TokenExpirationInHours { get; set; }
    }
}
