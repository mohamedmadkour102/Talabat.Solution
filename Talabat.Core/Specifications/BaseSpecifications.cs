﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get ; set ; }
        public List<Expression<Func<T, object>>> Includes {  get; set;  } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderBy { get ; set ; }
        public Expression<Func<T, object>> OrderByDescending { get ; set ; }
        public int Take { get ; set ; }
        public int Skip { get ; set ; }
        public bool IsPaginationEnabled { get ; set ; }

        public BaseSpecifications()
        {
            // Criteria = null ====> allowed 
           // Includes = new List<Expression<Func<T, object>>>();
        }

        public BaseSpecifications(Expression<Func<T, bool>> criteriaExp)
        {
            Criteria = criteriaExp;
            
        }

        // Setting Orders experssions values 

        public void SetOrderBy (Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void SetOrderByDescending(Expression<Func<T,object>> orderByDesc )
        {
            OrderByDescending = orderByDesc;
        }

        public void ApplyPagination(int skip , int take)
        {
            IsPaginationEnabled = true;
            Take = take ;
            Skip = skip ;
        }

    }
    
   
}
