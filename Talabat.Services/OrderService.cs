using Talabat.Core;
using Talabat.Core.Entites;
using Talabat.Core.Entites.Order_Aggregate;
using Talabat.Core.Repositories;
using Talabat.Core.Services;

namespace Talabat.Service;
public class OrderService : IOrderService
{
    private readonly IBasketRepository _basketRepository;
    private readonly IUnitOfWork _unitOfWork;

    // Without UnitOfWork
    ///private readonly IGenericRepository<Product> _productRepo;
    ///private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepo;
    ///private readonly IGenericRepository<Order> _orderRepo;

    public OrderService(IBasketRepository basketRepository,

        // Without UnitOfWork
        ///IGenericRepository<Product> ProductRepo,
        ///IGenericRepository<DeliveryMethod> DeliveryMethodRepo,
        ///IGenericRepository<Order> OrderRepo

        IUnitOfWork unitOfWork)
    {
        _basketRepository = basketRepository;
        this._unitOfWork = unitOfWork;

        // Without UnitOfWork
        ///_productRepo = ProductRepo;
        ///_deliveryMethodRepo = DeliveryMethodRepo;
        ///_orderRepo = OrderRepo;
    }

    public async Task<Order?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
    {
        // 1.Get Basket From Basket Repo
        var Basket = await _basketRepository.GetBasketAsync(BasketId);

        // 2.Get Selected Items at Basket From Product Repo
        var OrderIteams = new List<OrderItem>();
        if (Basket?.Items.Count > 0)
        {
            foreach (var item in Basket.Items)
            {
                var Product = await _unitOfWork.Repository<Product>().GetByIdAsync(item.Id);

                var ProductItemOrder = new ProductItemOrder(Product.Id, Product.Name, Product.PictureUrl);

                var OrderItem = new OrderItem(ProductItemOrder, item.Quantity, Product.Price);

                OrderIteams.Add(OrderItem);
            }

        }

        // 3.Calculate SubTotal
        var SubTotal = OrderIteams.Sum(item => item.Price * item.Quantity);

        // 4.Get Delivery Method From DeliveryMethod Repo
        var DeliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);

        // 5.Create Order
        var Order = new Order(BuyerEmail, ShippingAddress, DeliveryMethod, OrderIteams, SubTotal);

        // 6.Add Order Locally
        await _unitOfWork.Repository<Order>().AddAsync(Order);

        // 7.Save Order To Database => After Adding Unit Of Work :
        var Result = await _unitOfWork.CompleteAsync();
        if (Result < 0) return null;
        return Order;
    }


    public Task<Order> GetOrderByIdForSpecificUserAsync(string BuyerEmail, int OrderId)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Order>> GetOrdersForSpecificUserAsync(string BuyerEmail)
    {
        throw new NotImplementedException();
    }
}
