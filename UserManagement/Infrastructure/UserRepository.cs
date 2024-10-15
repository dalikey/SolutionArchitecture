using UserManagement.Domain;

namespace UserManagement.Infrastructure;

public class UserRepository
{
    private readonly UserMySQLContext _SQLcontext;

    public UserRepository(UserMySQLContext context)
    {
        _SQLcontext = context;
    }

    public async Task<bool> UpdateUserAsync(User user)
    {
        try
        {
            _SQLcontext.Users.Update(user);
            await _SQLcontext.SaveChangesAsync();

            return true;
        } catch
        {
            return false;
        }
    }

    public async Task<bool> AddUserAsync(User user)
    {
        try
        {
            await _SQLcontext.Users.AddAsync(user);
            await _SQLcontext.SaveChangesAsync();

            return true;
        } catch 
        {
            return false;
        }
        
    }

    public async Task<User?> GetUserAsync(string userId)
    {
        return await _SQLcontext.Users.FindAsync(userId!);
    }

    public async Task<Support> AddTicketOfUserAsync(Support ticket, int userId)
    {
        var userInSQL = await _SQLcontext.Users.FindAsync(userId);

        userInSQL.Supports.Add(ticket);
        await _SQLcontext.SaveChangesAsync();
        return userInSQL.Supports.Find(p => p.SupportTicketNumber == ticket.SupportTicketNumber);
    }
}
