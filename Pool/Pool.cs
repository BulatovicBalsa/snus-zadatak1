namespace Zadatak1.Pool;

public delegate void PoolLevelHandler(double level);

public delegate void PoolStatusHandler(PoolStatus status);

public class Pool
{
    private const double LevelDiffTolerance = 0.001;
    private double _level = 2.5;

    private PoolStatus _status = PoolStatus.Half;

    public double Level
    {
        get => _level;
        set
        {
            if (value < MinLevel)
                value = MinLevel;
            else if (value > MaxLevel)
                value = MaxLevel;

            if (Math.Abs(_level - value) < LevelDiffTolerance) return;

            _level = value;
            PoolLevelChanged?.Invoke(_level);
            ChangePoolStatus();
        }
    }

    public PoolStatus Status
    {
        get => _status;
        private set
        {
            if (_status == value) return;
            _status = value;
            if (_status is PoolStatus.Empty or PoolStatus.Full)
                PoolStatusChanged?.Invoke(_status);
        }
    }

    public double MinLevel { get; set; } = 0.0;
    public double MaxLevel { get; set; } = 5.0;

    public event PoolLevelHandler? PoolLevelChanged;
    public event PoolStatusHandler? PoolStatusChanged;

    private void ChangePoolStatus()
    {
        if (Math.Abs(_level - MinLevel) < LevelDiffTolerance)
        {
            Status = PoolStatus.Empty;
        }
        else if (Math.Abs(_level - MaxLevel) < LevelDiffTolerance)
        {
            Status = PoolStatus.Full;
        }
        else
        {
            Status = PoolStatus.Half;
        }
    }
}