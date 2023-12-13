long sum = 0, sum2 = 0;
Stack<long> stack = [];
using var reader = new StreamReader("input.txt");
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine().Split(' ').Select(m => long.Parse(m)).ToArray();
    sum += line[^1];
    stack.Push(line[0]);
    while(line.Sum() != 0) {
        line = line[1..].Zip(line, (x, y) => x - y).ToArray();
        stack.Push(line[0]);
        sum += line[^1];
    }
    var p2 = stack.Pop();
    while(stack.Count > 0){
        p2 = stack.Pop() - p2;
    }
    sum2 += p2;
}
Console.WriteLine($"Part 1: {sum}\nPart 2: {sum2}");