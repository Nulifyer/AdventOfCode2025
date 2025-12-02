using System.Collections.Generic;
using System.Transactions;

var raw = File.ReadAllText(@"./input.txt");

ulong sum_invalid_ids = 0;

var ranges = raw
    .Split(',')
    .Select(x => x.Trim())
    .Where(x => x.Length > 0 && x.All(x => "0123456789-".Contains(x)))
    .Select(x => x.Split('-'))
    .Where(x => x.Length == 2)
    .Select(x => new
    {
        Low = Convert.ToUInt64(x[0]),
        High = Convert.ToUInt64(x[1]),
    })
    .ToArray();

foreach (var range in ranges)
{
    for (ulong i = range.Low; i <= range.High; ++i)
    {
        string i_str = i.ToString();
        if (HasRepeatChunk(i_str))
        {
            Console.WriteLine($"{range.Low}-{range.High} has invalid ID, {i_str}");
            sum_invalid_ids = sum_invalid_ids + i;
            continue;
        }
    }
}

Console.WriteLine($"Sum: {sum_invalid_ids}");

;

bool HasRepeatChunk(string num_str)
{
    string last_chunk = null;
    foreach (var chunk in SplitIntoChunks(num_str))
    {
        if (last_chunk != null && last_chunk == chunk) return true;
        last_chunk = chunk;
    }
    return false;
}

IEnumerable<string> SplitIntoChunks(string num_str)
{
    // for (int sub_len = 1; sub_len <= num_str.Length / 2; ++sub_len)
    // {
    //     for (int i = 0; i + sub_len <= num_str.Length; i += sub_len)
    //     {
    //         yield return num_str.Substring(i, sub_len);
    //     }
    // }

    int half_len = num_str.Length / 2;
    return [
        num_str.Substring(0, half_len),
        num_str.Substring(half_len),
    ];
}