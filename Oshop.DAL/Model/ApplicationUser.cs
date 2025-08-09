using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oshop.DAL.Model
{
    public class ApplicationUser: IdentityUser//this model given by microcoft i do this if i need to add another atrribute
    {
        public string FullName { get; set; }
        public string? City { get; set; }
        public string ? CodeResetPassword { get; set; }
        public DateTime? CodeResetPasswordExpire { get; set; }
        
    }
}
