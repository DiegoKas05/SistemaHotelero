using Microsoft.AspNetCore.Identity;
using SistemaHotelero.Models;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface IApplicationUserRepository
    {
        Task<IdentityResult> RegistrarUsuarioAsync(ApplicationUser usuario, string password, string rolUsuario);
    }

}
