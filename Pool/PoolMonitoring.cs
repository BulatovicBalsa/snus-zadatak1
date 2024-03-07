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

    public Pool Pool { get; set; }


    public void LevelChangeSimulation(double inRate, double outRate)
    {
        inRate = -Math.Abs(inRate);
        outRate = Math.Abs(outRate);
        var currentRate = outRate;
        while (true)
        {
            Pool.Level += currentRate;
            currentRate = Pool.Status switch
            {
                PoolStatus.Empty => outRate,
                PoolStatus.Full => inRate,
                _ => currentRate
            };

            Thread.Sleep(1000);
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
        using var sw = File.AppendText("pool.log");
        sw.WriteLine($"Nivo vode: {Math.Round(level, 2)}, zabelezeno: {DateTime.Now}");
    }
}