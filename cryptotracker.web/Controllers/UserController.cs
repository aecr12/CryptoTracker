using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.dataaccess.Abstract;
using cryptotracker.entity;
using Microsoft.AspNetCore.Mvc;
namespace cryptotracker.web.Controllers
{
    public class UserController:Controller
    {
        
        public UserController()
        {
            
        }

        public IActionResult Index()
        {   

            return View();
        }
    }
}