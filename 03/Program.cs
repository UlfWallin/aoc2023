using System.Text.RegularExpressions;

int sum = 0;
List<string> lines = [];
using var reader = new StreamReader("input.txt");
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine() ?? "";
    lines.Add(line);
}

lines.Insert(0, new string('.', lines[0].Length));
lines.Add(new string('.', lines[0].Length));

for (int il = 0; il < lines.Count - 2; il++)
{
    sum += GetEngineParts(lines[il], lines[il + 1], lines[il + 2]).Sum();
}
Console.WriteLine(sum);

static int[] GetEngineParts(string top, string middle, string bottom)
{
    List<string> parts = [];
    var matches = Regex.Matches(middle, @"\d+");

    foreach (Match match in matches)
    {
        var left = Math.Max(match.Index - 1, 0);
        var right = Math.Min(match.Index + match.Length + 1, middle.Length - 1);

        Console.WriteLine(top.Substring(left, right - left));
        Console.WriteLine(middle.Substring(left, right - left));
        Console.WriteLine(bottom.Substring(left, right - left));

        bool adj = false;

        for (int i = left; i < right; i++)
        {
            if (IsSymbol(top[i]) || IsSymbol(bottom[i]))
            {
                adj = true;
                break;
            }
        }
        if (IsSymbol(middle[left]) || IsSymbol(middle[right - 1]))
        {
            adj = true;
        }

        var adjT = IsSymbol(top[left]) || IsSymbol(top[left + 1]) || IsSymbol(top[right - 2]) || IsSymbol(top[right -1]);
        var adjB = IsSymbol(bottom[left]) || IsSymbol(bottom[left + 1]) || IsSymbol(bottom[right - 2]) || IsSymbol(bottom[right -1]);
        var adjM = IsSymbol(middle[left]) || IsSymbol(middle[right - 1]);

        if (adj)
        {
            parts.Add(match.Value);
        }
    }
    return parts.Select(p => int.Parse(p)).ToArray();
}

static bool IsSymbol(char chr)
{
    return !(Char.IsDigit(chr) || chr == '.');
}