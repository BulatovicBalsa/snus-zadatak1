namespace Zadatak1;

internal delegate void PoolLevelHandler(double level);

internal delegate void PoolStatusHandler(PoolStatus status);

internal class PoolMonitoring
{
    public PoolMonitoring()
    {
        Pool = new Pool();
        PoolLevelChanged += OnPoolLevelChanged;
        PoolStatusChanged += OnPoolStatusChanged;
        PoolLevelChanged += OnPoolLevelChangedLogger;
    }

    public Pool Pool { get; set; }

    public event PoolLevelHandler PoolLevelChanged;
    public event PoolStatusHandler PoolStatusChanged;

    public void LevelChangeSimulation(double inRate, double outRate)
    {
        inRate = -Math.Abs(inRate);
        outRate = Math.Abs(outRate);
        var currentRate = outRate;
        const double tolerance = 0.001;
        while (true)
        {
            Pool.Level += currentRate;
            if (Pool.Level <= Pool.MinLevel)
            {
                Pool.Level = Pool.MinLevel;
                currentRate = outRate;
            }
            else if (Pool.Level >= Pool.MaxLevel)
            {
                Pool.Level = Pool.MaxLevel;
                currentRate = inRate;
            }

            PoolLevelChanged?.Invoke(Pool.Level);

            if (Math.Abs(Pool.Level - Pool.MinLevel) < tolerance)
            {
                Pool.Status = PoolStatus.Empty;
                PoolStatusChanged?.Invoke(Pool.Status);
            }
            else if (Math.Abs(Pool.Level - Pool.MaxLevel) < tolerance && Pool.Status != PoolStatus.Full)
            {
                Pool.Status = PoolStatus.Full;
                PoolStatusChanged?.Invoke(Pool.Status);
            }

            Thread.Sleep(1000);
        }
    }

    private static void OnPoolLevelChanged(double level)
    {
        Console.WriteLine($"Pool level changed to {level}, at {DateTime.Now}");
    }

    private static void OnPoolStatusChanged(PoolStatus status)
    {
        Console.WriteLine($"Pool status changed to {status}, at {DateTime.Now}");
    }

    private static void OnPoolLevelChangedLogger(double level)
    {
        using var sw = File.AppendText("pool.log");
        sw.WriteLine($"Nivo vode: {level}, zabelezeno: {DateTime.Now}");
    }
}