using System.Linq.Expressions;
using Talabat.Core.Entites;

namespace Talabat.Core.Specifications;
public class BaseSpecifications<T> : ISpecefications<T> where T : BaseEntity
{
    public Expression<Func<T, bool>> Criteria { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(); // تجنب التكرار
    public Expression<Func<T, object>> OrderBy { get; set; }
    public Expression<Func<T, object>> OrderByDescending { get; set; }


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


    public void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
    {
        OrderBy = OrderByExpression;
    }

    public void AddOrderByDescending(Expression<Func<T, object>> OrderByDescExpression)
    {
        OrderByDescending = OrderByDescExpression;
    }

}
