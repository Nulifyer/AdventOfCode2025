// loading ranges and numbers
using System.Text;

var lines = File.ReadAllLines(@"./input.txt");

var ranges = new List<Range>();
var nums = new List<ulong>();

foreach (var line in lines)
{
    if (line.IndexOf('-') > -1)
    {
        var bits = line.Split('-').Select(x => Convert.ToUInt64(x)).ToArray();
        ranges.Add(new Range(bits[0], bits[1]));
    }
    else if (line.Length > 0)
    {
        nums.Add(Convert.ToUInt64(line));
    }
}

Range?[] ranges_arr = ranges.ToArray();
ranges.Clear();
ranges = null;

int num_in_range_before_merge = nums
    .Count(num => ranges_arr
        .Any(range => range!.IncludesNumber(num)));
Console.WriteLine($"Valid Nums (before merge): {num_in_range_before_merge}");

// Console.WriteLine("-------------------");
// var range_str = new StringBuilder();
// foreach (var range in ranges_arr)
// {
//     range_str.AppendLine(range!.ToString());
// }
// Console.WriteLine(range_str.ToString());
// Console.WriteLine("-------------------");

// merge ranges
bool contin = true;
while (contin)
{
    contin = false;
    Range? range_a, range_b;
    for (int a = 0, b = 0; a < ranges_arr.Length; ++a)
    {
        range_a = ranges_arr[a];
        if (range_a == null) continue;

        for (b = a + 1; b < ranges_arr.Length; ++b)
        {
            if (a == b) continue;
            range_b = ranges_arr[b];
            if (range_b == null) continue;

            if (range_a.OverlapsWith(range_b) || range_a.IsAdjacentTo(range_b))
            {
                ranges_arr[a] = Range.Merge(range_a, range_b);
                ranges_arr[b] = null;
                contin = true;
            }
            if (contin == true) break;
        }
        if (contin == true) break;
    }
}
ranges_arr = ranges_arr.Where(x => x != null).ToArray();

// check how many numbers are in the range
int num_in_range = nums
    .Count(num => ranges_arr
        .Any(range => range!.IncludesNumber(num)));

Console.WriteLine($"Valid Nums (after merge) : {num_in_range}");

ulong total_valid_ids = 0;
foreach (var range in ranges_arr)
{
    total_valid_ids += range!.NumsInRange();
}
Console.WriteLine($"Total Valid IDs: {total_valid_ids}");

// Console.WriteLine("-------------------");
// range_str = new StringBuilder();
// foreach (var range in ranges_arr)
// {
//     range_str.AppendLine(range!.ToString());
// }
// Console.WriteLine(range_str.ToString());
// Console.WriteLine("-------------------");


;

class Range
{
    public ulong Min;
    public ulong Max;

    public Range(ulong min, ulong max)
    {
        Min = min;
        Max = max;
    }

    public ulong NumsInRange()
    {
        return (Max - Min) + 1;
    }

    public bool IncludesNumber(ulong num)
    {
        return Min <= num && num <= Max;
    }

    public bool OverlapsWith(Range b)
    {
        if (this.Max < b.Min || this.Min > b.Max)
            return false;
        return true;
    }
    public bool IsAdjacentTo(Range b)
    {
        if (this.Max + 1 == b.Min || this.Min - 1 == b.Max)
            return true;
        return false;
    }

    public static Range Merge(Range a, Range b)
    {
        return new Range(
            Math.Min(a.Min, b.Min),
            Math.Max(a.Max, b.Max)
        );
    }

    public override string ToString()
    {
        return $"({Min} - {Max} | {NumsInRange()})";
    }
}