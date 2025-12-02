var lines = File.ReadAllLines(@"./input.txt");

var dial = new Dial(99, 50);

Console.WriteLine($"The dial starts by pointing at {dial.Position}.");

foreach (var line in lines)
{
    char dir = line[0];
    int num = Convert.ToInt32(line.Substring(1));

    switch (dir)
    {
        case 'L':
            dial.RotateLeft(num);
            break;
        case 'R':
            dial.RotateRight(num);
            break;
    }
}

Console.WriteLine($"The dial pointed at zero {dial.NumZeros} times.");

public class Dial
{
    public int NumZeros { get; private set; }
    public readonly int TotalTicks;
    public int Position { get; private set; }

    public Dial(int total_ticks = 99, int position = 0)
    {
        NumZeros = 0;
        TotalTicks = total_ticks + 1;
        Position = position;
    }

    public void RotateLeft(int ticks)
    {
        int num_full_rotaions = ticks / TotalTicks;
        if (num_full_rotaions > 0)
        {
            NumZeros += num_full_rotaions;
            Console.WriteLine($"\tThe dial is rotated left fully {num_full_rotaions} times.");
        }

        ticks = ticks % TotalTicks; // get real ticks

        if (Position > 0 && Position - ticks < 0)
        {
            ++NumZeros;
            Console.WriteLine($"\tThe dial is rotated left past zero 1 times.");
        }

        Position = Position - ticks;
        if (Position < 0)
            Position = TotalTicks + Position;

        if (Position == 0)
        {
            ++NumZeros;
            Console.WriteLine($"\tThe dial is rotated left to zero 1 times.");
        }

        Console.WriteLine($"The dial is now at {Position}. -{ticks}");
    }

    public void RotateRight(int ticks)
    {
        int num_full_rotaions = ticks / TotalTicks;
        if (num_full_rotaions > 0)
        {
            NumZeros += num_full_rotaions;
            Console.WriteLine($"\tThe dial is rotated right fully {num_full_rotaions} times.");
        }

        ticks = ticks % TotalTicks; // get real ticks

        if (Position != 0 && Position + ticks > TotalTicks)
        {
            ++NumZeros;
            Console.WriteLine($"\tThe dial is rotated right past zero 1 times.");
        }

        Position = Position + ticks;
        if (Position >= TotalTicks)
            Position = Position - TotalTicks;

        if (Position == 0)
        {
            ++NumZeros;
            Console.WriteLine($"\tThe dial is rotated right to zero 1 times.");
        }

        Console.WriteLine($"The dial is now at {Position}. +{ticks}");
    }
}
