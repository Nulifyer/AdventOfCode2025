var lines = File.ReadLines(@"./example.txt");

int line_len = 0;
var paper = new List<bool>();

foreach (var line in lines)
{
    if (line_len == 0) line_len = line.Length;
    paper.AddRange(line.ToCharArray().Select(x => x == '@'));
}

var paper_arr = paper.ToArray();
paper.Clear();
paper = null;

PrintPaper();

;








void PrintPaper()
{
    for (int i = 0; i < paper_arr.Length; i++)
    {
        if (i > 0 && i % line_len == 0)
            Console.Write('\n');
        
        Console.Write(paper_arr[i] ? '@' : '.');
    }
}