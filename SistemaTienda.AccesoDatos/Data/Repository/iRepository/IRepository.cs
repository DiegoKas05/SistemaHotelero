using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SistemaHotelero.DataAccess.Data.Repository.iRepository
{
    public interface iRepository<T> where T : class
    {
        T Get(int id);


        IEnumerable<T> GetAll(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedEnumerable<T>>? orderBy = null,
            string? includeProperties = null
            );

        T GetFirstOrderDefault(
            Expression<Func<T, bool>>? filter = null,
            string? includeProperties = null
            );

        void Add(T entity);

        void Remove(int id);

        void Remove(T entity);

    }
}
