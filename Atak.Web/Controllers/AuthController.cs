using Atak.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Atak.Core.Entities;

namespace Atak.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<UsuarioAplicacao> _userManager;
        private readonly SignInManager<UsuarioAplicacao> _signInManager;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserManager<UsuarioAplicacao> userManager, SignInManager<UsuarioAplicacao> signInManager, ILogger<AuthController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = new UsuarioAplicacao
                {
                    UserName = model.Email,
                    Email = model.Email,
                    NomeCompleto = model.NomeCompleto
                };

                var resultado = await _userManager.CreateAsync(usuario, model.Senha);

                if (resultado.Succeeded)
                {
                    await _signInManager.SignInAsync(usuario, isPersistent: false);
                    _logger.LogInformation("Usuário registrado e autenticado com sucesso.");
                    return RedirectToAction("Index", "Restricted");
                }

                foreach (var erro in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, erro.Description);
                }
            }

            return View("Login");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var resultado = await _signInManager.PasswordSignInAsync(model.Email, model.Senha, model.LembrarMe, lockoutOnFailure: false);

                if (resultado.Succeeded)
                {
                    _logger.LogInformation("Usuário autenticado com sucesso.");
                    return RedirectToAction("Index", "Restricted");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Falha ao fazer login. Verifique seu email e senha e tente novamente.");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Usuário desautenticado.");
            return RedirectToAction("Index", "Home");
        }
    }
}
