using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications;
public class BaseSpecifications<T> : ISpecefications<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(); // تجنب التكرار


    // GET ALL
    public BaseSpecifications()
    {
        //Includes = new List<Expression<Func<T, object>>>();
    }

    // GET By Id 
    public BaseSpecifications(Expression<Func<T, bool>> criteriaExpression)
    {
        Criteria = criteriaExpression;
        // Includes = new List<Expression<Func<T, object>>>();
    }

}
