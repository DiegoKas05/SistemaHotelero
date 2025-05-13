using Microsoft.AspNetCore.Identity;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationUserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> RegistrarUsuarioAsync(ApplicationUser usuario, string password, string rolUsuario)
    {
        var resultado = await _userManager.CreateAsync(usuario, password);

        if (resultado.Succeeded)
        {
            // Asegura que el rol exista
            if (!await _roleManager.RoleExistsAsync(rolUsuario))
            {
                await _roleManager.CreateAsync(new IdentityRole(rolUsuario));
            }

            // Asignar rol al usuario
            await _userManager.AddToRoleAsync(usuario, rolUsuario);
        }

        return resultado;
    }
}
