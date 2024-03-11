using Zadatak1.Pool;

namespace Zadatak1;

public class Program
{
    public static void Main()
    {
        var pool = new Pool.Pool();
        var poolMonitoring = new PoolMonitoring(pool);
        poolMonitoring.LevelChangeSimulation(0.7, 0.8);
    }
}