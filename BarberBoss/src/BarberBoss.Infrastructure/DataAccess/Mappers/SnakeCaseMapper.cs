using Dapper;

namespace BarberBoss.Infrastructure.DataAccess.Mappers;
public static class SnakeCaseMapper
{
    public static void Configure()
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}
