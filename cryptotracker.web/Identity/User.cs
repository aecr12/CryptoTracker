using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.entity;
using Microsoft.AspNetCore.Identity;

namespace cryptotracker.web.Identity
{
    public class User:IdentityUser
    {   
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}