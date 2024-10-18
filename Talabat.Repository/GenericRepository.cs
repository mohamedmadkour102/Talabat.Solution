using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    
        public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
        {
            private readonly StoreContext _storeContext;

            public GenericRepository(StoreContext storeContext)
            {
                _storeContext = storeContext;
            }
            public async Task<IReadOnlyList<T>> GetAllAsync()
            {
                if (typeof(T) == typeof(Product))
                {
                    return (IReadOnlyList<T>)await _storeContext.Set<Product>().Include(P => P.Brand).Include(p => p.Category).ToListAsync();

                }
                return await _storeContext.Set<T>().ToListAsync();
                // why tolist?

            }


            public async Task<T> GetAsync(int id)
            {
                if (typeof(T) == typeof(Product))
                {
                    return await _storeContext.Set<Product>().Where(p => p.Id == id).Include(p => p.Brand).Include(p => p.Category).FirstOrDefaultAsync() as T;

                }
                return await _storeContext.Set<T>().FindAsync(id);
                // find better than firstor default bec it search locally first 
            }
            public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification)
            {
                return await SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>(), specification).ToListAsync();
            }

            public async Task<T> GetWithSpecAsync(ISpecification<T> specification)
            {
                return await SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>(), specification).FirstOrDefaultAsync();
            }

        public async Task<int> GetCountWithSpecAsync(ISpecification<T> specification)
        {
            return await SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>(), specification).CountAsync();
        }


    }
}
