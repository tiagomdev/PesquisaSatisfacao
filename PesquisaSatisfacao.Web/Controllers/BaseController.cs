using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PesquisaSatisfacao.Core.Domain.Users;
using PesquisaSatisfacao.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PesquisaSatisfacao.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> _signInManager;
        public BaseController(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> userManager)
        {
            this._signInManager = _signInManager;
            this.userManager = userManager;
        }

        public Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

        public int GetUserId() => int.Parse(userManager.GetUserId(this.User));
    }
}
