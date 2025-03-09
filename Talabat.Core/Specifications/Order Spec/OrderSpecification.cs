using Talabat.Core.Entites.Order_Aggregate;

namespace Talabat.Core.Specifications;
public class OrderSpecification : BaseSpecifications<Order>
{

    // Used in Get Orders For Specific User
    public OrderSpecification(string email) : base(O => O.BuyerEmail == email)  // Criteria (Filteration)
    {
        // Includes
        Includes.Add(O => O.DeliveryMethod);
        Includes.Add(O => O.Items);

        // OrderBy
        AddOrderByDescending(O => O.OrderDate);
    }


    // Used in Get Order By Id For Specific User
    public OrderSpecification(string email, int id) : base(O => O.BuyerEmail == email && O.Id == id)  // Criteria (Filteration)
    {
        // Includes
        Includes.Add(O => O.DeliveryMethod);
        Includes.Add(O => O.Items);

        // OrderBy
        AddOrderByDescending(O => O.OrderDate);

    }
}
