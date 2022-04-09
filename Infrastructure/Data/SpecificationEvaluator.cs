using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuerry(IQueryable<TEntity> inputQuerry, ISpecification<TEntity> spec)
        {
            var querry = inputQuerry;
            if (spec.Criteria != null)
            {
                querry = querry.Where(spec.Criteria);
            }

            querry = spec.Includes.Aggregate(querry, (current, include) => current.Include(include));

            return querry;
        }
    }
}