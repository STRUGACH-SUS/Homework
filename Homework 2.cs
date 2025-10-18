// Перевод числа пользователя из одной системы счисления в другую
InputNumberInAnotherSystem();
static void InputNumberInAnotherSystem() // Получаем данные от пользователя
{
    Console.Write("Please enter first number for sum: ");
    string result1 = Console.ReadLine();
    Console.Write("Please enter first system of your first number: ");
    int.TryParse(Console.ReadLine(),out int firstSystem1);
    Console.Write("Please enter second number for sum: ");
    string result2 = Console.ReadLine();
    Console.Write("Please enter first system of your second number: ");
    int.TryParse(Console.ReadLine(),out int firstSystem2);
    Console.Write("Please enter end system of your sum numbers: ");
    int.TryParse(Console.ReadLine(),out int endSystem);
    CheckValidInputNumber(result1, firstSystem1, endSystem);
    CheckValidInputNumber(result2, firstSystem2, endSystem);
    int number1 = ChangeSystemOfNumberInTenSystem(result1, firstSystem1);
    int number2 = ChangeSystemOfNumberInTenSystem(result2, firstSystem2);
    int resultInTenSystem = number1 + number2;
    Console.WriteLine("Your sum numbers in 10 system: " + resultInTenSystem);
    ChangeSystemOfSumNumbers(resultInTenSystem, endSystem);
}
static void CheckValidInputNumber(string number,int firstSystem,int endSystem) // Проверка полученных данных
{
    string testOfCharString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-";
    testOfCharString = testOfCharString.Substring(0,firstSystem)+'-';
    bool invalidDataNumber = false;
    for (int i = 0; i < number.Length; i++)
    {
        if (!(testOfCharString.Contains(number[i])) ^ (number.Substring(1).Contains('-')))
        {
            invalidDataNumber=true;
            break;
        }
    }
    if (invalidDataNumber^!(firstSystem<=62 && firstSystem>=2)^!(endSystem<=62 && endSystem>=2))
    {
        Console.WriteLine("Invalid input, try again");
        InputNumberInAnotherSystem();
    }
}
static int ChangeSystemOfNumberInTenSystem(string number,int firstSystem) // Перевод из одной системы счисления в другую числа пользователя
{
    bool negative = false;
    string testOfCharString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    if (number[0] == '-')
    {
        number = number.Substring(1);
        negative = true;
    }
    int counter = 0;
    int numberInTenSystem = 0;
    for (int i = number.Length-1; i >= 0; i--)
    {
        numberInTenSystem = numberInTenSystem+(testOfCharString.IndexOf(number[i]))*Convert.ToInt32(Math.Pow(firstSystem,counter));
        counter++;
    }
    if (negative)
    {
        Console.WriteLine("Your number in 10 system: -" + numberInTenSystem);
        numberInTenSystem=-numberInTenSystem;
    }
    else
    {
        Console.WriteLine("Your number in 10 system: " + numberInTenSystem);
    }
    return numberInTenSystem;
}
static string ChangeSystemOfSumNumbers(int sumNumbersInTenSystem, int endSystem)// Перевод из одной системы счисления в другую числа пользователя
{
    string result = "";
    bool negative = false;
    string testOfCharString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    if (sumNumbersInTenSystem < 0)
    {
        sumNumbersInTenSystem =  -sumNumbersInTenSystem;
        negative = true;
    }
    while (true)
    {
        result = testOfCharString[sumNumbersInTenSystem % endSystem] + result;
        if (sumNumbersInTenSystem / endSystem < endSystem)
        {
            sumNumbersInTenSystem = sumNumbersInTenSystem / endSystem;
            result = testOfCharString[sumNumbersInTenSystem] + result;
            break;
        }
        sumNumbersInTenSystem = sumNumbersInTenSystem / endSystem;
    }
    if (negative)
    {
        result = "-" + result;
    }
    Console.WriteLine("Your sum numbers in " + endSystem +" system: " + result);
    return result;
} 
