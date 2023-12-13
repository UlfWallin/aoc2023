long sum = 0;
long sum2 = 0;
using var reader = new StreamReader("input.txt");
while (reader.Peek() >= 0)
{
    Stack<long> stack = [];
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
Console.WriteLine(sum);
Console.WriteLine(sum2);