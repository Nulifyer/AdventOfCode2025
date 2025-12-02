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
    string? last_chunk, chunk;

    int saw_same_chunk = 0;
    int expected_repeat; 

    for (int sub_len = 1; sub_len <= num_str.Length / 2; ++sub_len)
    {
        last_chunk = null;
        saw_same_chunk = 1;
        
        expected_repeat = num_str.Length / sub_len;
        if (sub_len * expected_repeat != num_str.Length) continue; // if not divided equally, skip

        for (int i = 0; i + sub_len <= num_str.Length; i += sub_len)
        {
            chunk = num_str.Substring(i, sub_len);

            if (last_chunk == null)
            {
                last_chunk = chunk;
                continue;
            }
            else if (last_chunk == chunk)
            {
                ++saw_same_chunk;

                if (saw_same_chunk == expected_repeat)
                    return true;
            }
            else
            {
                break; // non matching chunk size
            }
        }
    }

    return false;
}
