using System.Text.RegularExpressions;
using System.Linq;

int sum = 0;
int sumRatio = 0;
List<string> lines = [];
Regex regex = new Regex("\\*");
Regex numRegex = new Regex("\\d+");

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
    var matches = regex.Matches(lines[il + 1]);
    var numsTop = numRegex.Matches(lines[il]);
    var numsBottom = numRegex.Matches(lines[il + 2]);
    var numsCenter = numRegex.Matches(lines[il + 1]);
    foreach(Match match in matches)
    {
        List<int> adjacent = [];
        AddAdjacents(numsTop, match, adjacent);
        AddAdjacents(numsBottom, match, adjacent);
        AddAdjacents(numsCenter, match, adjacent);

        if (adjacent.Count == 2)
        {
            var ratio = adjacent[0] * adjacent[1];
            sumRatio += ratio;
        }
    }

    sum += GetEngineParts(lines[il], lines[il + 1], lines[il + 2]).Sum();
}

Console.WriteLine(sum);
Console.WriteLine(sumRatio);

static int[] GetEngineParts(string top, string middle, string bottom)
{
    List<string> parts = [];
    var matches = Regex.Matches(middle, @"\d+");

    foreach (Match match in matches)
    {
        var left = Math.Max(match.Index - 1, 0);
        var right = Math.Min(match.Index + match.Length + 1, middle.Length - 1);

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

static void AddAdjacents(MatchCollection numMatches, Match match, List<int> adjacent)
{
    adjacent.AddRange(from Match num in numMatches
                      where (num.Index <= match.Index && match.Index <= num.Index + num.Length) || match.Index + 1 == num.Index
                      select int.Parse(num.Value));
}