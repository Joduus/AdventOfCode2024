var inputFile = File.Open("input.txt", FileMode.Open);
var reader = new StreamReader(inputFile);

var list1 = new List<int>();
var list2 = new List<int>();
for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
{
    var list1Number = line.AsSpan(0, 5);
    var list2Number = line.AsSpan(8, 5);
    
    list1.Add(int.Parse(list1Number));
    list2.Add(int.Parse(list2Number));
}

list1.Sort();
list2.Sort();

var distanceList = new List<int>(list1.Count);
for (var i = 0; i < list1.Count; i++)
{
    var distance = Math.Abs(list2[i] - list1[i]);
    distanceList.Add(distance);
}

Console.WriteLine(distanceList.Sum());
