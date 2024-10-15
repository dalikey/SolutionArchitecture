using OrderManagement.Infrastructure.Order;
using System.Text.Json;

namespace OrderManagement.Infrastructure.User;

public class UserRepository
{
    private readonly OrderMySQLContext _sqlContext;

    public UserRepository(OrderMySQLContext context)
    {
        _sqlContext = context;
    }

    public async Task<bool> AddUserAsync(Domain.User user)
    {
        try
        {
            await _sqlContext.Users.AddAsync(user);
            await _sqlContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateUserAsync(Domain.User user)
    {
        try
        {
            _sqlContext.Users.Attach(user);
            _sqlContext.Users.Update(user);
            await _sqlContext.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<Domain.User> GetUserAsync(string userId)
    {
        return await _sqlContext.Users.FindAsync(userId);
    }
}
