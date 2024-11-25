using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using IDatabase = StackExchange.Redis.IDatabase;

namespace E_Commerce1DB_V01;

public class RedisContext : IDisposable
{
    private readonly ConnectionMultiplexer _connection;
    public IDatabase Database { get; }

    public RedisContext(IConfiguration configuration)
    {
        var redisConnectionString = configuration["RedisConfig:ConnectionString"];
        var databaseIndex = int.TryParse(configuration["RedisConfig:Database"], out var db) ? db : 0;

        if (redisConnectionString != null) _connection = ConnectionMultiplexer.Connect(redisConnectionString);
        if (_connection != null) Database = _connection.GetDatabase(databaseIndex);
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}