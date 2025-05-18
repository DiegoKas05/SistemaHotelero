using SistemaHotelero.AccesoDatos.Data.Repository.iRepository;
using SistemaHotelero.AccesoDatos.Repositorio;
using SistemaHotelero.AccesoDatos.Repositorio.IRepositorio;
using SistemaHotelero.Data;
using SistemaHotelero.DataAccess.Data.Repository.iRepository;
using Microsoft.AspNetCore.Identity;
using SistemaHotelero.Models;

namespace SistemaHotelero.DataAccess.Data.Repository
{
    public class ContenedorTrabajo : IContenedorTrabajo
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ContenedorTrabajo(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            _userManager = userManager;

            // Inicializar repositorios
            Productos = new ProductoRepository(_db);
            Habitacion = new HabitacionRepository(_db);
            Categoria = new CategoriaRepository(_db);
            Piso = new PisoRepository(_db);
            EstadoHabitacion = new EstadoHabitacionRepository(_db);
            Recepcion = new RecepcionRepository(_db);
            //recientemente agregado
            ApplicationUser = new ApplicationUserRepository(_db, _userManager, _roleManager);
        }

        // Implementación de las propiedades de la interfaz IContenedorTrabajo
        public IProductoRepository Productos { get; private set; }
        public IHabitacionRepository Habitacion { get; private set; }
        public ICategoriaRepository Categoria { get; private set; }
        public IPisoRepository Piso { get; private set; }
        public IEstadoHabitacionRepository EstadoHabitacion { get; private set; }
        public IRecepcionRepository Recepcion { get; private set; }
        //recien agregado
        public IApplicationUserRepository ApplicationUser { get; private set; }


        public void Save()
        {
            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
