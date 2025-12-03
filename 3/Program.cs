var lines = File.ReadLines(@"./input.txt");

int biggest_num_sum = 0;

foreach (var line in lines)
{
    var nums = line.ToCharArray().Select(x => x - '0').ToArray();
    if (!nums.Any()) continue;
    PrintArray(nums);

    int biggest_num = nums.First();
    for (int i = 0; i < nums.Length - 1; i++)
    {
        int current = nums[i];
        for (int j = i + 1; j < nums.Length; j++)
        {
            int next = nums[j];
            int computed = (current * 10) + next;
            if (computed > biggest_num)
                biggest_num = computed;
        }
    }

    Console.Write($" - {biggest_num}\n");

    biggest_num_sum += biggest_num;
}

Console.WriteLine($"Biggest Num Sum: {biggest_num_sum}");

;

void PrintArray(IEnumerable<int> nums)
{
    Console.Write($"[{string.Join(",", nums)}]");
}