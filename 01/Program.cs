var sum = 0;
var partTwo = true;
const string path = "input.txt";
using var reader = new StreamReader(path);
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine() ?? "";
    if (partTwo)
        line = TextToDigits(line);

    var numbers = ExtractNumbers(line);

    var first = numbers[0];
    var last = numbers[^1];
    sum += first * 10 + last;
}

Console.WriteLine(sum);

static int[] ExtractNumbers(string inputString) => (from c in inputString.ToCharArray()
                                                    where Char.IsNumber(c)
                                                    select int.Parse(c.ToString())).ToArray();

static string TextToDigits(string input) => input
        .Replace("one", "o1e")
        .Replace("two", "t2o")
        .Replace("three", "t3e")
        .Replace("four", "4")
        .Replace("five", "5e")
        .Replace("six", "6")
        .Replace("seven", "7n")
        .Replace("eight", "e8t")
        .Replace("nine", "n9e")
        .Replace("zero", "0o");