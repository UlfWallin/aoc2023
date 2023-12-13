long sum = 0;
using var reader = new StreamReader("input.txt");
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine().Split(' ').Select(m => long.Parse(m)).ToArray();
    sum += line[^1];
    while(line.Sum() != 0) {
        line = line[1..].Zip(line, (x, y) => x - y).ToArray();
        sum += line[^1];
    }
}
Console.WriteLine(sum);

