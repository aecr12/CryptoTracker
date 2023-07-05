using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cryptotracker.web.Identity;
using cryptotracker.web.Models;
using cryptotracker.web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace cryptotracker.web.Controllers
{
    public class AccountController:Controller
    {
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private IEmailSender _emailSender;
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender )
        {
            this._signInManager = signInManager;
            this._userManager = userManager;
            this._emailSender = emailSender;
        }
       public IActionResult Login(string ReturnUrl=null)
       {
            return View(new LoginModel()
            {
                ReturnUrl=ReturnUrl
            });
       }


       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Login(LoginModel model)
       {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user==null)
            {
                ModelState.AddModelError("","Kullanıcı Bulunamadı");
                return View(model);
            }
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                ModelState.AddModelError("","Hesap onaylanmamış.");
                CreateMessage("Hesap onayı başarısız","danger");
                return View(model);
            }
            var res = await _signInManager.PasswordSignInAsync(user,model.Password,false,false);
            if(res.Succeeded)
            {
                if(model.ReturnUrl==null)
                {
                    return RedirectToAction("Index","Home");
                }
                return Redirect(model.ReturnUrl);
            }
            return View();
       }


       public IActionResult Register()
       {    
            return View();
       }


       [HttpPost]
       [ValidateAntiForgeryToken]
       public async Task<IActionResult> Register(RegisterModel model)
       {    
            if(!ModelState.IsValid)
            {
                return View(model); 
            }
            var user = new User()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Username,
                Email = model.Email
            };
            var res = await _userManager.CreateAsync(user,model.Password);
            if(res.Succeeded)
            {   
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var url = Url.Action("ConfirmEmail", "Account", new{
                    userId = user.Id,
                    token = code
                });
                await _emailSender.SendEmailAsync(model.Email, "Hesabınızı Onaylayın", $"Hesabınızı onaylamak için linke <a href='https://localhost:5170{url}'>tıklayınız</a>");
                // model.Email,"Hesabınızı Onaylayın",$"Hesabınızı onaylamak için linke <a href='https://localhost:5170{url}'>tıklayınız</a>"
                return RedirectToAction("Login");
            }
            ModelState.AddModelError("","Bir hata oluştu");
            return View(model);
       }


       public async Task<IActionResult> Logout()
       {
        await _signInManager.SignOutAsync();
        return Redirect("~/");
       }


       public async Task<IActionResult> ConfirmEmail(string userId, string token)
       {
        if(userId == null || token == null)
        {
            CreateMessage("Geçersiz Token","success");
            return View();
        }
        var user = await _userManager.FindByIdAsync(userId);
        if(user==null)
        {
            CreateMessage("Hesap onayı başarısız","danger");
            return View();
        }
        var res = await _userManager.ConfirmEmailAsync(user,token);
        if(res.Succeeded)
        {
            CreateMessage("Hesap onaylandı", "success");
            return View();
        }
        return View();
       }

       private void CreateMessage(string message, string alerttype)
       {
            var msg = new AlertMessage()
            {
                Message = message,
                AlertType = alerttype
            };
            TempData["message"] = JsonConvert.SerializeObject(msg);
       }
    }
}