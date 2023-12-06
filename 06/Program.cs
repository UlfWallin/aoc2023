long[] time = [42, 68, 69, 85];
long[] distance = [284, 1005, 1122, 1341];

const bool PART_TWO = true;
if (PART_TWO)
{
    time = [(long.Parse(time.Aggregate("", (acc, curr) => acc += curr.ToString())))];
    distance = [(long.Parse(distance.Aggregate("", (acc, curr) => acc += curr.ToString())))];
}
List<long> numberOfWays = [];
for (var i = 0; i < time.Length; i++)
{
    numberOfWays.Add(CalcDuration(time[i], distance[i]));
}
var p = numberOfWays.Aggregate((acc, curr) => acc * curr);
Console.WriteLine(p);

static long CalcDuration(long duration, long recordDistance)
{
    var options = 0;
    for (var hold = 1; hold <= duration - 1; hold++)
    {
        if (hold * (duration - hold) > recordDistance)
        {
            options++;
        }
    }
    return options;
}