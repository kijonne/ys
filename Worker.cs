using UserService2.Seed;
using UserService2.Services;

namespace UserService2;

public class Worker : BackgroundService
{
    private readonly UserManagementService _userService;

    public Worker(UserManagementService userService)
    {
        _userService = userService;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        Console.WriteLine("User Service 2 запущен");

        var users = UserSeeder.GetUsers();

        foreach (var user in users)
        {
            await _userService.RegisterUser(user);

            Console.WriteLine(
                $"Зарегистрирован: {user.Name}, {user.Email}"
            );
        }

        Console.WriteLine("Все тестовые пользователи загружены");

        await Task.Delay(
            Timeout.Infinite,
            stoppingToken
        );
    }
}