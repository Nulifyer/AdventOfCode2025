var lines = File.ReadLines(@"./input.txt");

uint digits = 12;
ulong biggest_num_sum = 0;

foreach (var line in lines)
{
    var nums = line.ToCharArray().Select(x => Convert.ToUInt32(x - '0')).ToArray();
    if (!nums.Any()) continue;
    PrintArray(nums);

    ulong biggest_num = FindBiggestNum(nums, digits);

    Console.Write($" - {biggest_num}\n");

    biggest_num_sum += biggest_num;
}

Console.WriteLine($"Biggest Num Sum: {biggest_num_sum}");

;

TrackedNum? FindNextPossible(TrackedNum[] nums, TrackedNum cursor, int skip)
{
    for (int i = 0; i < nums.Length; i++)
    {
        if (nums[i].Index <= cursor.Index) continue;
        if (skip > 0)
        {
            skip--;
            continue;
        }
        return nums[i];
    }
    return null;
}

ulong FindBiggestNum(uint[] nums, uint digits)
{
    if (nums.Length <= digits)
        return ComputeFromArray(nums);

    TrackedNum[] nums_with_idx = nums
        .Select((v, i) => new TrackedNum(v, i))
        .ToArray();
    TrackedNum[] nums_with_idx_ordered = nums_with_idx
        .OrderByDescending(x => x.Value)
        .ThenBy(x => x.Index)
        .ToArray();

    // Console.Write(" - ");
    // PrintArray(nums_with_idx_ordered.Select(x => x.Value).ToArray());

    for (int i = 0; i < nums_with_idx_ordered.Length; i++)
    {
        var num_list = new List<TrackedNum>();
        var cursor = nums_with_idx_ordered[i];
        do
        {
            num_list.Add(cursor);

            if (num_list.Count == digits) break;

            cursor = FindNextPossible(nums_with_idx_ordered, cursor, cursor.Skip);

            if (cursor == null)
            {
                if (num_list.Count == 1)
                {
                    break; // nothing here.
                }
                else
                {
                    num_list.RemoveAt(num_list.Count - 1); // pop
                    cursor = num_list.ElementAt(num_list.Count - 1); // prev cursor
                    num_list.RemoveAt(num_list.Count - 1); // pop cursor, it will be added back
                    cursor.Skip++;

                    for (int j = cursor.Index + 1; j < nums_with_idx.Length; j++)
                    {
                        nums_with_idx[j].Skip = 0;
                    }
                }
            }
        }
        while (num_list.Count < digits);

        if (num_list.Count == digits)
        {
            return ComputeFromArray(num_list.Select(x => x.Value).ToArray());
        }
    }

    return 0;
}

ulong ComputeFromArray(uint[] nums)
{
    ulong big_num = 0;
    for (int i = 0; i < nums.Length; i++)
    {
        uint current = nums[i];
        int power = nums.Length - i;
        ulong tens = Convert.ToUInt64(Math.Pow(10, power - 1));
        big_num += current * tens;
    }
    return big_num;
}

void PrintArray(uint[] nums)
{
    Console.Write($"[{string.Join(",", nums)}]");
}

class TrackedNum
{
    public uint Value;
    public int Index;
    public int Skip;

    public TrackedNum(uint value, int index)
    {
        Value = value;
        Index = index;
        Skip = 0;
    }
}