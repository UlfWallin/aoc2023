﻿using System.Text.RegularExpressions;

List<long> seeds = [];
Dictionary<string, List<string>> stupidMap = []; 
var nre = new Regex(@"\d+");
var currentMap = "";
var inMap = false;

const string path = "sample.txt";
using var reader = new StreamReader(path);
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine() ?? "";

    if (inMap && line == "") {
        inMap = false;
    }
    
    if (line.StartsWith("seeds: ")) {
        var nums = nre.Matches(line).Select(n => long.Parse(n.Value));
        seeds.AddRange(nums);
    }
    
    if (line.EndsWith("map:")) {
        currentMap = line.Split(' ')[0];
        stupidMap.Add(currentMap, []);
        inMap = true;
        line = reader.ReadLine() ?? "";
    }

    if (inMap) {
        stupidMap[currentMap].Add(line);
    }
}
var low = long.MaxValue;
foreach(var seed in seeds) {
    var location = FindLocation(seed);
    low = Math.Min(location, low);
    Console.WriteLine($"Seed {seed}: {location}");
}
Console.WriteLine($"Part One = {low}");

for(var seed = 0; seed < seeds.Count - 1; seed += 2) {
    (long seed, long length) range = (seeds[seed], seeds[seed + 1]);
    long newSeed = 0;
    foreach(var map in stupidMap) {
        newSeed = GetDestinationInRange(range, map.Value);
    }
}

long GetDestination(long seed, IList<string> lines) {
    var destination = seed;
    foreach(var line in lines) {
        var map  = nre.Matches(line).Select(n => long.Parse(n.Value)).ToArray();
        var destStart = map[0];
        var srcStart = map[1];
        var range = map[2];

        if (seed >= srcStart && seed <= srcStart + range) {
            var offset = seed - srcStart;
            destination = destStart + offset;
            break;
        }
    }
    return destination;
}

long GetDestinationInRange((long seed,long length) range, IList<string> lines) {
    var destination = range.seed;
    foreach(var line in lines) {
        var map  = nre.Matches(line).Select(n => long.Parse(n.Value)).ToArray();
        var destStart = map[0];
        var srcStart = map[1];
        var length = map[2];
        
    }
    return destination;
}

long FindLocation(long startSeed) {
    var seed = startSeed;
    foreach(var map in stupidMap) {
        seed = GetDestination(seed, map.Value);
    }
    return seed;
}