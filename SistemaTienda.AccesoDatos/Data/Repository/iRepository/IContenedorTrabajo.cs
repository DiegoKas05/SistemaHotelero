using SistemaHotelero.AccesoDatos.Data.Repository.iRepository;
using SistemaHotelero.AccesoDatos.Repositorio.IRepositorio;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface IContenedorTrabajo : IDisposable
    {
        // Repositorios para los distintos modelos
        IProductoRepository Productos { get; }
        IHabitacionRepository Habitacion { get; }
        ICategoriaRepository Categoria { get; }
        IPisoRepository Piso { get; }
        IEstadoHabitacionRepository EstadoHabitacion { get; }
        IRecepcionRepository Recepcion { get; }
        //acabo de agregar esto aqui
        IApplicationUserRepository ApplicationUser { get; }


        // Guarda los cambios en la base de datos
        void Save();
    }
}
