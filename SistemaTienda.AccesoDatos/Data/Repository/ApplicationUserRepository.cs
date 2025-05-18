using Microsoft.AspNetCore.Identity;
using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using SistemaHotelero.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ApplicationUserRepository : IApplicationUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ApplicationDbContext _db;

    public ApplicationUserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> RegistrarUsuarioAsync(ApplicationUser usuario, string password, string rolUsuario)
    {
        var resultado = await _userManager.CreateAsync(usuario, password);

        if (resultado.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync(rolUsuario))
            {
                await _roleManager.CreateAsync(new IdentityRole(rolUsuario));
            }

            await _userManager.AddToRoleAsync(usuario, rolUsuario);
        }

        return resultado;
    }

    // esto es nuevo lo coloque yo para lo de agregar un cliente con una habitacion
    public IEnumerable<ApplicationUser> GetAll()
    {
        return _userManager.Users.ToList();

    }
}

