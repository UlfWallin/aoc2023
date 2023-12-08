const bool PART_TWO = true;
const string path = "input.txt";
string strengths = "AKQJT98765432";
if (PART_TWO)
{
    strengths = "AKQT98765432J";
}

var sum = 0;

Dictionary<string, (int Type, string NewHand, int Bid)> hands = [];

using var reader = new StreamReader(path);
while (reader.Peek() >= 0)
{
    var line = reader.ReadLine().Split(' ');
    var hand = line[0];
    var bid = int.Parse(line[1]);

    var newHand = TradeJokers(hand);
    var t = GetHandType(PART_TWO ? newHand : hand);
    hands.Add(hand, (t, newHand, bid));
}

var comparer = new HandComparer(strengths);
var ranked = hands.OrderBy(kv => kv.Value.Type)
    .ThenByDescending(kv => kv.Key, comparer);

var rank = 1;
foreach (var kv in ranked)
{
    Console.WriteLine($"{kv.Key} {kv.Value.NewHand}");
    sum += kv.Value.Bid * rank;
    rank++;
}
Console.WriteLine(sum);

int GetHandType(string hand)
{
    var tmp = hand.GroupBy(c => c)
        .Select(g => new { Card = g.Key, Count = g.Count() });

    // Five of a kind
    if (tmp.Count() == 1)
    {
        return 6;
    }

    if (tmp.Count() == 2)
    {
        if (tmp.Any(t => t.Count == 4))
        {
            // Four of a kind
            return 5;
        }
        else
        {
            // Full house
            return 4;
        }
    }

    if (tmp.Count() == 3)
    {
        if (tmp.Any(t => t.Count == 3))
        {
            // Three of a kind
            return 3;
        }
        else
        {
            // Two pair
            return 2;
        }
    }

    if (tmp.Count() == 4)
    {
        // One pair 
        return 1;
    }

    return 0;
}

string TradeJokers(string hand)
{
    var grp = hand.GroupBy(c => c)
        .Select(g => new { Card = g.Key, Count = g.Count() });

    var currentHandType = GetHandType(hand);
    var newHand = hand;
    var jokers = hand.Count(c => c == 'J');
    if (jokers == 5)
    {
        newHand = "AAAAA";
    }
    else if (jokers > 0 && currentHandType > 0)
    {
        newHand = hand.Replace('J', grp.Where(g => g.Card != 'J')
            .Aggregate((acc, curr) => curr.Count > acc.Count || (curr.Count == acc.Count && strengths.IndexOf(curr.Card) < strengths.IndexOf(acc.Card)) ? curr : acc).Card);
    }
    else if (jokers > 0)
    {
        // Make best pair
        var best = hand.Aggregate((acc, curr) => strengths.IndexOf(curr) < strengths.IndexOf(acc) ? curr : acc);
        newHand = hand.Replace('J', best);
    }
    return newHand;
}

public class HandComparer(string order) : Comparer<string>
{
    private readonly string strengths = order;
    public override int Compare(string? hand1, string? hand2)
    {
        for (var i = 0; i < hand1.Length && i < hand2.Length; i++)
        {
            if (strengths.IndexOf(hand1[i]) < strengths.IndexOf(hand2[i]))
            {
                return -1;
            }
            else if (strengths.IndexOf(hand1[i]) > strengths.IndexOf(hand2[i]))
            {
                return 1;
            }
        }
        return 0;
    }
}