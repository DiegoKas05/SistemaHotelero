using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using SistemaHotelero.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaHotelero.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Empleado")]
    [Area("Admin")]
    public class UsuariosController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IContenedorTrabajo _contenedorTrabajo;



        public UsuariosController(UserManager<ApplicationUser> userManager, IContenedorTrabajo contenedorTrabajo)
        {
            _userManager = userManager;
            _contenedorTrabajo = contenedorTrabajo;
        }



        public IActionResult IndexUsuarios() => View();
        public IActionResult IndexAdministradores() => View();

        public IActionResult IndexEmpleados() => View();


        public IActionResult CreateAdministradores() => View();

        public IActionResult CreateEmpleados() => View();
        public IActionResult CreateUsuarios() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEmpleados(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya existe en la base de datos
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está en uso.");
                    return View(model);
                }

                // Crear un nuevo objeto ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Direccion = model.Direccion,
                    TipoDocumento = model.TipoDocumento,
                    NumeroDocumento = model.NumeroDocumento
                };

                // Crear el usuario en la base de datos con la contraseña proporcionada
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Asignar el rol de "Empleado" al usuario recién creado
                    await _userManager.AddToRoleAsync(user, "Empleado");

                    TempData["Success"] = "Empleado creado correctamente.";
                    return RedirectToAction(nameof(IndexEmpleados)); // Redirigir al listado de empleados
                }

                // Si hubo algún error al crear el usuario, lo mostramos
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model); // Si hay algún error de validación, volvemos a mostrar el formulario
        }



        [HttpGet]
        public async Task<IActionResult> EditUsuarios(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var model = new EditarUsuarioViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellido = usuario.Apellido,
                Email = usuario.Email,
                Direccion = usuario.Direccion,
                TipoDocumento = usuario.TipoDocumento,
                NumeroDocumento = usuario.NumeroDocumento
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdministradores(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el correo ya existe en la base de datos
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "El correo electrónico ya está en uso.");
                    return View(model);
                }

                // Crear un nuevo objeto ApplicationUser
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Direccion = model.Direccion,
                    TipoDocumento = model.TipoDocumento,
                    NumeroDocumento = model.NumeroDocumento
                };

                // Crear el usuario en la base de datos con la contraseña proporcionada
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Asignar el rol de "Administrador" al usuario recién creado
                    await _userManager.AddToRoleAsync(user, "Admin");

                    TempData["Success"] = "Administrador creado correctamente.";
                    return RedirectToAction(nameof(IndexAdministradores)); // Redirigir al listado de administradores
                }

                // Si hubo algún error al crear el usuario, lo mostramos
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model); // Si hay algún error de validación, volvemos a mostrar el formulario
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUsuarios(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "El correo ya está en uso.");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Nombre = model.Nombre,
                    Apellido = model.Apellido,
                    Direccion = model.Direccion,
                    TipoDocumento = model.TipoDocumento,
                    NumeroDocumento = model.NumeroDocumento
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Usuario");
                    TempData["Success"] = "Usuario creado correctamente.";
                    return RedirectToAction(nameof(IndexUsuarios));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditEmpleados(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var empleado = await _userManager.FindByIdAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }

            var model = new EditarUsuarioViewModel
            {
                Id = empleado.Id,
                Nombre = empleado.Nombre,
                Apellido = empleado.Apellido,
                Email = empleado.Email,
                Direccion = empleado.Direccion,
                TipoDocumento = empleado.TipoDocumento,
                NumeroDocumento = empleado.NumeroDocumento
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmpleados(EditarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var empleado = await _userManager.FindByIdAsync(model.Id);
                if (empleado == null)
                {
                    return NotFound();
                }

                if (empleado.Email != model.Email)
                {
                    var existingUser = await _userManager.FindByEmailAsync(model.Email);
                    if (existingUser != null && existingUser.Id != model.Id)
                    {
                        ModelState.AddModelError("Email", "El correo ya está en uso.");
                        return View(model);
                    }

                    empleado.Email = model.Email;
                    empleado.UserName = model.Email;
                }

                empleado.Nombre = model.Nombre;
                empleado.Apellido = model.Apellido;
                empleado.Direccion = model.Direccion;
                empleado.TipoDocumento = model.TipoDocumento;
                empleado.NumeroDocumento = model.NumeroDocumento;

                var result = await _userManager.UpdateAsync(empleado);

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(empleado);
                    var passwordResult = await _userManager.ResetPasswordAsync(empleado, token, model.Password);

                    if (!passwordResult.Succeeded)
                    {
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }

                if (result.Succeeded)
                {
                    TempData["Success"] = "Empleado actualizado correctamente.";
                    return RedirectToAction(nameof(IndexEmpleados));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditAdministradores(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var administrador = await _userManager.FindByIdAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }

            var model = new EditarUsuarioViewModel
            {
                Id = administrador.Id,
                Nombre = administrador.Nombre,
                Apellido = administrador.Apellido,
                Email = administrador.Email,
                Direccion = administrador.Direccion,
                TipoDocumento = administrador.TipoDocumento,
                NumeroDocumento = administrador.NumeroDocumento
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdministradores(EditarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var administrador = await _userManager.FindByIdAsync(model.Id);
                if (administrador == null)
                {
                    return NotFound();
                }

                if (administrador.Email != model.Email)
                {
                    var existingUser = await _userManager.FindByEmailAsync(model.Email);
                    if (existingUser != null && existingUser.Id != model.Id)
                    {
                        ModelState.AddModelError("Email", "El correo ya está en uso.");
                        return View(model);
                    }

                    administrador.Email = model.Email;
                    administrador.UserName = model.Email;
                }

                administrador.Nombre = model.Nombre;
                administrador.Apellido = model.Apellido;
                administrador.Direccion = model.Direccion;
                administrador.TipoDocumento = model.TipoDocumento;
                administrador.NumeroDocumento = model.NumeroDocumento;

                var result = await _userManager.UpdateAsync(administrador);

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(administrador);
                    var passwordResult = await _userManager.ResetPasswordAsync(administrador, token, model.Password);

                    if (!passwordResult.Succeeded)
                    {
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }

                if (result.Succeeded)
                {
                    TempData["Success"] = "Administrador actualizado correctamente.";
                    return RedirectToAction(nameof(IndexAdministradores));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUsuarios(EditarUsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _userManager.FindByIdAsync(model.Id);
                if (usuario == null)
                {
                    return NotFound();
                }

                if (usuario.Email != model.Email)
                {
                    var existingUser = await _userManager.FindByEmailAsync(model.Email);
                    if (existingUser != null && existingUser.Id != model.Id)
                    {
                        ModelState.AddModelError("Email", "El correo ya está en uso.");
                        return View(model);
                    }

                    usuario.Email = model.Email;
                    usuario.UserName = model.Email;
                }

                usuario.Nombre = model.Nombre;
                usuario.Apellido = model.Apellido;
                usuario.Direccion = model.Direccion;
                usuario.TipoDocumento = model.TipoDocumento;
                usuario.NumeroDocumento = model.NumeroDocumento;

                var result = await _userManager.UpdateAsync(usuario);

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(usuario);
                    var passwordResult = await _userManager.ResetPasswordAsync(usuario, token, model.Password);

                    if (!passwordResult.Succeeded)
                    {
                        foreach (var error in passwordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }

                if (result.Succeeded)
                {
                    TempData["Success"] = "Usuario actualizado correctamente.";
                    return RedirectToAction(nameof(IndexUsuarios));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        #region API

        [HttpGet]
        public async Task<IActionResult> GetAllEmpleados()
        {
            var empleados = (await _userManager.GetUsersInRoleAsync("Empleado"))
                .Select(e => new
                {
                    e.Id,
                    e.Nombre,
                    e.Apellido,
                    e.Email,
                    e.Direccion,
                    e.TipoDocumento,
                    e.NumeroDocumento
                });

            return Json(new { data = empleados });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var usuarios = (await _userManager.GetUsersInRoleAsync("Usuario"))
                .Select(u => new
                {
                    u.Id,
                    u.Nombre,
                    u.Apellido,
                    u.Email,
                    u.Direccion,
                    u.TipoDocumento,
                    u.NumeroDocumento
                });

            return Json(new { data = usuarios });
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAdministradores()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Admin"))
                .Select(u => new
                {
                    u.Id,
                    u.Nombre,
                    u.Apellido,
                    u.Email,
                    u.Direccion,
                    u.TipoDocumento,
                    u.NumeroDocumento
                });

            return Json(new { data = admins });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "Usuario no encontrado." });
            }

            var esCliente = await _userManager.IsInRoleAsync(usuario, "Usuario");
            if (esCliente)
            {
                var tieneReservaActiva = _contenedorTrabajo.Recepcion
            .GetAll()
            .Any(r => r.IdApplicationUser == id && r.Estado == true);
                if (tieneReservaActiva)
                {
                    return Json(new { success = false, message = "No se puede eliminar este cliente porque tiene una habitación ocupada." });
                }
            }

            var result = await _userManager.DeleteAsync(usuario);
            if (result.Succeeded)
            {
                return Json(new { success = true, message = "Usuario eliminado correctamente." });
            }

            return Json(new { success = false, message = "Error al eliminar el usuario." });
        }


        #endregion
    }
}