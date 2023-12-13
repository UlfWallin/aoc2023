using System.Text.RegularExpressions;

const string path = "input.txt";
Regex regex = new(@"\w+");

Dictionary<string, (string Left, string Right)> nodes = [];
const string TARGET = "ZZZ";

using var reader = new StreamReader(path);
var lr = reader.ReadLine() ?? "";
var ilr = 0;
reader.ReadLine();
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine() ?? "";
    var m = regex.Matches(line);
    nodes.Add(m[0].Value, (Left: m[1].Value, Right: m[2].Value));
}

var steps = 0L;
var node = "AAA";

while(node != TARGET) {
    var dir = lr[ilr];

    node = dir == 'L' ? nodes[node].Left : nodes[node].Right;

    ilr = ilr == lr.Length - 1 ? 0 : ++ilr;
    steps++;
}
Console.WriteLine($"Part one: {steps}");

// --- PART TWO ---
steps = 0;
var currNodes = nodes.Keys.Where(k => k[2] == 'A').ToArray();
var len = currNodes.Length;

var follow = (0L, currNodes[0]);
var c = 0L;
for(int i = 0; i < 5; i++) {
    follow = FollowPathToZ(follow.Item2);
    c += follow.Item1;
}

// TODO: Didn't think, doesn't work
var found = true;
while(!found) {
    var m = true;
    for(var i = 0; i < len; i++) {
        currNodes[i] = lr[ilr] == 'L' ? nodes[currNodes[i]].Left : nodes[currNodes[i]].Right;
        m &= currNodes[i][2] == 'Z';
    }
    ilr = ilr == lr.Length - 1 ? 0 : ++ilr;
    steps++;
    found = m;
}

Console.WriteLine($"Part two: {steps}");
(long, string) FollowPathToZ(string node)
{
    var steps = 0L;
    var currNode = node;
    var i = 0;
    while(currNode[2] != 'Z')
    {
        currNode = lr[i] == 'L' ? nodes[currNode].Left : nodes[currNode].Right;
        i = i == lr.Length - 1 ? 0 : ++i;
        steps++;
    }
    return (steps, currNode);
}