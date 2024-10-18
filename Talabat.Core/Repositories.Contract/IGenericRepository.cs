﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Core.Repositories.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetAsync(int id);   
<<<<<<< Updated upstream
        Task<IEnumerable<T>> GetAllAsync();
=======
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> specification);
        Task<T> GetWithSpecAsync(ISpecification<T> specification);
        Task<int> GetCountWithSpecAsync(ISpecification<T> specification);
>>>>>>> Stashed changes
    }
    
}
