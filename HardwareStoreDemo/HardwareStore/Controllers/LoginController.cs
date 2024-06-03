using HardwareStore.Models;
using HardwareStore.Repositories.Logins;
using HardwareStore.Repositories.Roles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using FluentValidation;

namespace HardwareStore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginsRepository _loginRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IValidator<Logins_Model> _validator;


        private SelectList _rolesList;

        public LoginController(ILoginsRepository loginRepository, IRolesRepository rolesRepository, IValidator<Logins_Model> validator)
        {
            _loginRepository = loginRepository;
            _rolesRepository = rolesRepository;
            _validator = validator;
            InitializeAsync().GetAwaiter().GetResult();
        }

        private async Task InitializeAsync()
        {
            var roles = await _loginRepository.GetAllRolesAsync();
            _rolesList = new SelectList(
                roles,
                nameof(RolesModel.Id_Rol),
                nameof(RolesModel.Name_Rol)
            );
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {

            var logins = await _loginRepository.GetAllLoginAsync();

            return View(logins);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Rol = _rolesList;

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Logins_Model loginModel)
        {
            try
            {

                await _loginRepository.AddLoginAsync(loginModel);

                TempData["message"] = "Datos guardados correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Rol = _rolesList;

                TempData["message"] = ex.Message;

                return View(loginModel);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var login = await _loginRepository.GetByIdLoginAsync(id);

            if (login == null)
            {
                return NotFound();
            }

            var roles = await _loginRepository.GetAllRolesAsync();
            _rolesList = new SelectList(
                roles,
                nameof(RolesModel.Id_Rol),
                nameof(RolesModel.Name_Rol)
            );

            ViewBag.Rol = _rolesList;

            return View(login);
        }


        public ActionResult Login()
        {
            ClaimsPrincipal claimsUsuario = HttpContext.User;
            if (claimsUsuario.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Logins_Model loginModel)
        {

            var credentialsList = await _loginRepository.GetAllAsyncLogin();
            var credential = credentialsList.FirstOrDefault(c => c.LoginUser == loginModel.LoginUser && c.LoginPassword == loginModel.LoginPassword);
            var roles = await _rolesRepository.GetAllAsync();

            if (credential != null)
            {
                credential.Roles = roles.FirstOrDefault(r => r.Id_Rol == credential.Id_Rol);
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Role, credential.Roles.Name_Rol)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["messageLogin"] = "Usuario o Contraseña Incorrectos, Vuelva a Intentarlo";
            }

            return View(loginModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Login");
        }

        private async Task ViewBagLists()
        {

            var roles = await _rolesRepository.GetAllAsync();
            ViewBag.Rol = new SelectList(roles, nameof(RolesModel.Id_Rol), nameof(RolesModel.Name_Rol));
        }
    }
}
