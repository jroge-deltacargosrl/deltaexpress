using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DeltaXpress.Models.Interfaz
{
    public interface IRepositoryApi<T>
    {
        IEnumerable<T> getAll(Expression<Func<T,bool>> filter =  null);
        T get(int? id);
        T create(T value);
        T update(int id, T value);
        T delete(int id);
        void setResource(string rosource);
    }
}
