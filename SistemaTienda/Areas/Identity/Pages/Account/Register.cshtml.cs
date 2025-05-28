using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using SistemaHotelero.Models;
using SistemaHotelero.Utilidades;

namespace SistemaHotelero.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El correo electrónico es requerido")]
            [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido")]
            [Display(Name = "Correo Electrónico")]
            public string Email { get; set; }

            [Required(ErrorMessage = "La contraseña es requerida")]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y máximo {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirmar Contraseña")]
            [Compare("Password", ErrorMessage = "La contraseña y la confirmación no coinciden.")]
            public string ConfirmPassword { get; set; }

            [Required(ErrorMessage = "El nombre es requerido")]
            [Display(Name = "Nombre")]
            [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo debe contener letras")]
            [StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres")]
            public string Nombre { get; set; }

            [Required(ErrorMessage = "El apellido es requerido")]
            [Display(Name = "Apellido")]
            [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo debe contener letras")]
            [StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres")]
            public string Apellido { get; set; }

            [Required(ErrorMessage = "La dirección es requerida")]
            [Display(Name = "Dirección")]
            [StringLength(100, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 100 caracteres")]
            public string Direccion { get; set; }

            [Required(ErrorMessage = "El tipo de documento es requerido")]
            [Display(Name = "Tipo de Documento")]
            public string TipoDocumento { get; set; }

            [Required(ErrorMessage = "El número de documento es requerido")]
            [Display(Name = "Número de Documento")]
            [CustomDocumentValidation(ErrorMessage = "El número de documento no es válido")]
            public string NumeroDocumento { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                // Validación adicional para DUI y Pasaporte según el tipo seleccionado
                if (Input.TipoDocumento == "DUI")
                {
                    if (!EsDuiValido(Input.NumeroDocumento))
                    {
                        ModelState.AddModelError("Input.NumeroDocumento", "El formato del DUI debe ser 12345678-9");
                        return Page();
                    }
                }
                else if (Input.TipoDocumento == "Pasaporte")
                {
                    if (!EsPasaporteValido(Input.NumeroDocumento))
                    {
                        ModelState.AddModelError("Input.NumeroDocumento", "El formato del pasaporte no es válido");
                        return Page();
                    }
                }

                var user = CreateUser();

                user.Nombre = Input.Nombre;
                user.Apellido = Input.Apellido;
                user.Direccion = Input.Direccion;
                user.TipoDocumento = Input.TipoDocumento;
                user.NumeroDocumento = Input.NumeroDocumento;

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(CNT.Admin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(CNT.Admin));
                        await _roleManager.CreateAsync(new IdentityRole(CNT.Usuario));
                        await _roleManager.CreateAsync(new IdentityRole(CNT.Empleado));
                    }

                    string rol = Request.Form["radUsuarioRole"].ToString();

                    if (rol == CNT.Admin)
                    {
                        await _userManager.AddToRoleAsync(user, CNT.Admin);
                    }
                    else if (rol == CNT.Empleado)
                    {
                        await _userManager.AddToRoleAsync(user, CNT.Empleado);
                    }
                    else if (rol == CNT.Usuario)
                    {
                        await _userManager.AddToRoleAsync(user, CNT.Usuario);
                    }

                    _logger.LogInformation("User created a new account with password.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private bool EsDuiValido(string dui)
        {
            // Formato: 12345678-9 (8 dígitos, guión, 1 dígito)
            if (string.IsNullOrEmpty(dui))
                return false;

            var partes = dui.Split('-');
            if (partes.Length != 2)
                return false;

            if (partes[0].Length != 8 || partes[1].Length != 1)
                return false;

            return partes[0].All(char.IsDigit) && partes[1].All(char.IsDigit);
        }

        private bool EsPasaporteValido(string pasaporte)
        {
            // Formato básico para pasaporte: letras y números, sin caracteres especiales
            if (string.IsNullOrEmpty(pasaporte))
                return false;

            // Longitud típica entre 6 y 12 caracteres
            if (pasaporte.Length < 6 || pasaporte.Length > 12)
                return false;

            // Debe contener al menos una letra y un número
            return pasaporte.Any(char.IsLetter) && pasaporte.Any(char.IsDigit);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }

    public class CustomDocumentValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (RegisterModel.InputModel)validationContext.ObjectInstance;
            var numeroDocumento = value?.ToString();

            if (model.TipoDocumento == "DUI")
            {
                if (string.IsNullOrEmpty(numeroDocumento))
                    return new ValidationResult("El DUI es requerido");

                var partes = numeroDocumento.Split('-');
                if (partes.Length != 2)
                    return new ValidationResult("El formato del DUI debe ser 12345678-9");

                if (partes[0].Length != 8 || partes[1].Length != 1)
                    return new ValidationResult("El DUI debe tener 8 dígitos, un guión y 1 dígito");

                if (!partes[0].All(char.IsDigit) || !partes[1].All(char.IsDigit))
                    return new ValidationResult("El DUI solo debe contener números y un guión");
            }
            else if (model.TipoDocumento == "Pasaporte")
            {
                if (string.IsNullOrEmpty(numeroDocumento))
                    return new ValidationResult("El pasaporte es requerido");

                if (numeroDocumento.Length < 6 || numeroDocumento.Length > 12)
                    return new ValidationResult("El pasaporte debe tener entre 6 y 12 caracteres");

                if (!numeroDocumento.Any(char.IsLetter) || !numeroDocumento.Any(char.IsDigit))
                    return new ValidationResult("El pasaporte debe contener letras y números");
            }

            return ValidationResult.Success;
        }
    }
}