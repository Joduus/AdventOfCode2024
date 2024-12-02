var inputFile = File.Open("input.txt", FileMode.Open);
var reader = new StreamReader(inputFile);

var numberLines = new int[1000][];
var lineNumber = 0;
for (var line = reader.ReadLine(); line != null; line = reader.ReadLine())
{
    var numbers = line.Split(" ").Select(x => int.Parse(x)).ToArray();
    numberLines[lineNumber] = numbers;
    
    lineNumber++;
}

Console.WriteLine(Part1(numberLines));
// Console.WriteLine(Part2(numberLines));

int Part1(int[][] numberLines)
{
    var safeCounter = 0;
    foreach (var numbers in numberLines)
    {
        if (IsSafe(numbers))
        {
            safeCounter++;
        }
    }

    return safeCounter;
}

bool IsSafe(int[] numbers)
{
    var isAscending = true;
    var isDescending = true;
    
    for (var i = 0; i < numbers.Length - 1; i++)
    {
        var diff = numbers[i + 1] - numbers[i];
        
        if (diff is < 1 or > 3) isAscending = false;
        if (diff is > -1 or < -3) isDescending = false;
    }

    return isAscending || isDescending;
}
