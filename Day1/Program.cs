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

Console.WriteLine(Part1(list1, list2));
Console.WriteLine(Part2(list1, list2));


int Part1(List<int> list1, List<int> list2)
{
    var distanceList = new List<int>(list1.Count);
    for (var i = 0; i < list1.Count; i++)
    {
        var distance = Math.Abs(list2[i] - list1[i]);
        distanceList.Add(distance);
    }

    return distanceList.Sum();
}

int Part2(List<int> list1, List<int> list2)
{
    var duplicatesList = list1.Intersect(list2).ToList();

    var similarityList = new List<int>(duplicatesList.Count);
    for (int i = 0; i < duplicatesList.Count; i++)
    {
        var duplicateNumber = duplicatesList[i];
        var duplicateNumberCount = list2.FindAll(x => x == duplicateNumber).Count;
        similarityList.Add(duplicateNumber * duplicateNumberCount);
    }
    
    return similarityList.Sum();
}
