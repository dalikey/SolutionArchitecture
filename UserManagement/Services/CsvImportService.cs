using CsvHelper;
using System.Globalization;
using UserManagement.Domain;
using UserManagement.Infrastructure;

namespace UserManagement.Services;

public class CsvImportService
{
    private readonly UserRepository _userRepository;

    public CsvImportService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task ImportUsersFromCsvAsync(string filePath)
    {
        try
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var users = csv.GetRecords<User>().ToList();

            foreach (var user in users)
            {
                await _userRepository.AddUserAsync(user);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error importing users from CSV: {ex.Message}");
            throw new Exception("Error importing users from CSV", ex);
        }
    }
}
