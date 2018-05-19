using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PesquisaSatisfacao.Web.Models.Users;
using PesquisaSatisfacao.Core.Application;
using Microsoft.AspNetCore.Identity;
using PesquisaSatisfacao.Core.Domain.Users;

namespace PesquisaSatisfacao.Web.Controllers
{
    public class UserController : BaseController
    {
        readonly UserAppService service;
        public UserController(UserAppService service, SignInManager<ApplicationUser> _signInManager,
            UserManager<ApplicationUser> userManager) : base(_signInManager, userManager)
        {
            this.service = service;
        }

        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            try
            {
                var user = await service.Authenticate(model.Email, model.Password);
                await _signInManager.SignInAsync(new ApplicationUser() { UserName = user.Email, Id = user.Id.ToString()}, isPersistent: false);
                return Json(new { Success = true, Url = Url.Action("Index", "Home") });
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, ex.Message });
            }
        }

        public IActionResult Create()
        {
            return PartialView(new UserCreateViewModel());
        }

        [HttpPost]
        public async Task<JsonResult> Create(UserCreateViewModel model)
        {
            try
            {
                var user = await service.CreateUser(model.ToDomain());
                await _signInManager.SignInAsync(new ApplicationUser() { UserName = user.Email, Id = user.Id.ToString() }, isPersistent: false);
                return Json(new { Success = true, Url = Url.Action("Index", "Home") });
            }
            catch (Exception ex)
            {
                return Json(new { Success = true, ex.Message});
            }
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
    }
}