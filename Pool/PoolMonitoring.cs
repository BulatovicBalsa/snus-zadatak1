namespace Zadatak1.Pool;

public class PoolMonitoring
{
    public PoolMonitoring()
    {
        Pool = new Pool();
        Pool.PoolLevelChanged += OnPoolLevelChanged;
        Pool.PoolStatusChanged += OnPoolStatusChanged;
        Pool.PoolLevelChanged += OnPoolLevelChangedLogger;
    }

    public PoolMonitoring(Pool pool)
    {
        Pool = pool;
        Pool.PoolLevelChanged += OnPoolLevelChanged;
        Pool.PoolStatusChanged += OnPoolStatusChanged;
        Pool.PoolLevelChanged += OnPoolLevelChangedLogger;
    }

    public Pool Pool { get; set; }


    public void LevelChangeSimulation(double outRate, double inRate)
    {
        outRate = -Math.Abs(outRate);
        inRate = Math.Abs(inRate);
        var currentRate = outRate;
        while (true)
        {
            Pool.Level += currentRate;
            currentRate = Pool.Status switch
            {
                PoolStatus.Empty => inRate,
                PoolStatus.Full => outRate,
                _ => currentRate
            };

            var sleepTime = currentRate < 0 ? 1000 : 1500;
            Thread.Sleep(sleepTime);
        }
    }

    private static void OnPoolLevelChanged(double level)
    {
        Console.WriteLine($"Pool level changed to {Math.Round(level, 2)}, at {DateTime.Now}");
    }

    private static void OnPoolStatusChanged(PoolStatus status)
    {
        Console.WriteLine($"Pool status changed to {status}, at {DateTime.Now}");
    }

    private static void OnPoolLevelChangedLogger(double level)
    {
        using var sw = File.AppendText("bazenLog.txt");
        sw.WriteLine($"Nivo vode: {Math.Round(level, 2)} metara, zabelezeno: {DateTime.Now}");
    }
}