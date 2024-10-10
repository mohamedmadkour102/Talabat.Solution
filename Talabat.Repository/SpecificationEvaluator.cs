using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Specifications;

namespace Talabat.Repository
{
    public static class SpecificationEvaluator <TEntity> where TEntity : BaseEntity
    {
        // IQueryable 3shan yro7 ynfz el query hnak fe el DB w y3ml gilter 3ndy hna msh aktar
        // // 3ks el IEnumerable , hygbly el data kolha w yfltr hna 
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery , ISpecification<TEntity> spec)
        {
            // hna ana 2olt en el query deh = elsequence elly sh3'al 3leeh ==> _storeContext.Set<Product>()
            var query = InputQuery;
            if (spec.Criteria is not null)
            {
                // hna ana deft elgoz2 bta3 elcriteria 3la el seq lw elcriteria deh msh b null
                // w b3t fe el where deh el criteria el goz2 elly heya shaylah ya3ny 
                query = query.Where(spec.Criteria); 


            }
            // Includes Addition 

            query = spec.Includes.Aggregate(query , (currentquery , includeexp) => currentquery.Include(includeexp));
            return query;

        }
    }
}
