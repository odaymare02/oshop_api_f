using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.DAL.DTO.Requests
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Pasword { get; set; }
    }
}
