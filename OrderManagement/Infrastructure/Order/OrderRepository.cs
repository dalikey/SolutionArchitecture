using Microsoft.EntityFrameworkCore;
using OrderManagement.Domain;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrderManagement.Infrastructure.Order;

public class OrderRepository
{
    private readonly OrderMySQLContext _sqlContext;

    public OrderRepository(OrderMySQLContext context)
    {
        _sqlContext = context;
    }

    public async Task<bool> AddOrderAsync(Domain.Order order)
    {
        try
        {
            await _sqlContext.Orders.AddAsync(order);
            await _sqlContext.SaveChangesAsync();
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }   
    }

    public async Task<Domain.User> UpdateUserAsync(Domain.User user)
    {
        try
        {
            _sqlContext.Users.Update(user);

            await _sqlContext.SaveChangesAsync();

            
            return await _sqlContext.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<Domain.Order> UpdateOrderAsync(Domain.Order order)
    {
        try
        {
            order = _sqlContext.Orders!.FirstOrDefault(x => x.OrderNumber == order.OrderNumber);

            _sqlContext.Orders.Update(order);

            await _sqlContext.SaveChangesAsync();

            return await _sqlContext.FindAsync<Domain.Order>(order.OrderNumber);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Domain.Order?> CancelOrderAsync(string OrderNumber)
    {
        try
        {
            var order = await _sqlContext.Orders.FindAsync(OrderNumber);
            order.Status = "Cancelled";
            _sqlContext.Orders.Update(order);
            await _sqlContext.SaveChangesAsync();

            return await _sqlContext.FindAsync<Domain.Order>(order.OrderNumber);
        }
        catch
        {
            return null;
        }
    }

    public async Task<Domain.Order> GetOrderAsync(string orderNumber)
    {
        return await _sqlContext.Orders.FindAsync(orderNumber);
    }
}
