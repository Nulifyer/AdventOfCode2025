
var lines = File.ReadAllLines(@"./input.txt");

int num_zeros = 0;
var dial = new Dial(99,50);

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

    Console.WriteLine($"The dial is rotated {line} to point at {dial.Position}.");

    if (dial.Position == 0) num_zeros++;
}

;

public class Dial
{
    public readonly int TotalTicks;
    public int Position { get; private set; }

    public Dial(int total_ticks = 99, int position = 0)
    {
        TotalTicks = total_ticks + 1;
        Position = position;
    }

    public void RotateLeft(int ticks)
    {
        ticks = ticks % TotalTicks;
        Position = Position - ticks;
        if (Position < 0)
            Position = TotalTicks + Position;
    }

    public void RotateRight(int ticks)
    {
        ticks = ticks % TotalTicks;
        Position = Position + ticks;
        if (Position >= TotalTicks)
            Position = Position - TotalTicks;
    }
}