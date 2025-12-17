var lines = File.ReadLines(@"./example.txt");

int line_num = 0;
int line_len = 0;
var paper = new List<bool>();

foreach (var line in lines)
{
    line_num++;
    if (line_len == 0) line_len = line.Length;
    paper.AddRange(line.ToCharArray().Select(x => x == '@'));
}

var paper_arr = paper.ToArray();
var move_arr = paper_arr.Select(x => false).ToArray();
paper.Clear();
paper = null;

int moveable = CountMoveable();
Console.WriteLine($"can move: {moveable}");

PrintPaper();
;

int CountMoveable()
{
    int moveable = 0;
    int neighbors = 0;

    for (int y = 0; y < line_num; y++)
    {
        for (int x = 0; x < line_len; x++)
        {
            int idx = CordsToIndex(x, y);
            if (paper_arr[idx] == false) continue;

            neighbors = 0;

            if (CheckIndex(CordsToIndex(x - 1, y - 1))) ++neighbors;
            if (CheckIndex(CordsToIndex(x - 1, y)))     ++neighbors;
            if (CheckIndex(CordsToIndex(x - 1, y + 1))) ++neighbors;
            if (CheckIndex(CordsToIndex(x, y - 1)))     ++neighbors;
            if (CheckIndex(CordsToIndex(x, y + 1)))     ++neighbors;
            if (CheckIndex(CordsToIndex(x + 1, y - 1))) ++neighbors;
            if (CheckIndex(CordsToIndex(x + 1, y)))     ++neighbors;
            if (CheckIndex(CordsToIndex(x + 1, y + 1))) ++neighbors;

            if (neighbors < 4)
            {
                move_arr[idx] = true;
                ++moveable;
            }
        }
    }

    return moveable;
}

bool CheckIndex(int idx)
{
    if (idx > paper_arr.Length - 1) return false;
    if (idx < 0) return false;
    return paper_arr[idx];
}

int CordsToIndex(int x, int y)
{
    if (x < 0 || x > line_len - 1) return -1;
    if (y < 0 || y > line_num - 1) return -1;
    return (y * line_len) + x;
}

void PrintPaper()
{
    int row_num = -1;
    for (int i = 0; i < paper_arr.Length; i++)
    {
        Console.ResetColor();

        if (i % line_len == 0)
        {
            ++row_num;
            if (i > 0) Console.Write('\n');
            Console.Write($"{row_num}_");
        }

        if (paper_arr[i] == false)
            Console.Write(".");
        else if (move_arr[i] == false)
            Console.Write("@");
        else
            Console.Write("x");
    }
    Console.Write('\n');
}