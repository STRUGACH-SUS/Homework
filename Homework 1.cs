// Перевод числа пользователя из одной системы счисления в другую
InputNumberInAnotherSystem();

static string InputNumberInAnotherSystem()// Получаем данные от пользователя
{
    Console.Write("Please enter number: ");
    string result = Console.ReadLine();
    Console.Write("Please enter first system of your number: ");
    int.TryParse(Console.ReadLine(),out int firstSystem);
    Console.Write("Please enter end system of your number: ");
    int.TryParse(Console.ReadLine(),out int endSystem);
    CheckValidInputNumber(result, firstSystem, endSystem);
    return result;
}

static string CheckValidInputNumber(string number,int firstSystem,int endSystem)// Проверка полученных данных
{
    string testOfCharString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-";
    testOfCharString = testOfCharString.Substring(0,firstSystem)+'-';
    bool invalidDataNumber = false;
    for (int i = 0; i < number.Length; i++)
    {
        if (!(testOfCharString.Contains(number[i])))
        {
            invalidDataNumber=true;
        }
        if (number.Substring(1).Contains('-'))
        {
            invalidDataNumber=true;
        }
    }
    if (invalidDataNumber^!(firstSystem<=62 && firstSystem>=2)^!(endSystem<=62 && endSystem>=2))
    {
        Console.WriteLine("Invalid input, try again");
        InputNumberInAnotherSystem();
    }
    ChangeSystemOfNumber(number, firstSystem, endSystem);
    return number;
}

static string ChangeSystemOfNumber(string number,int firstSystem,int endSystem)// Перевод из одной системы счисления в другую числа пользователя
{
    bool negative = false;
    string testOfCharString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    string result = "";
    
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
    }
    else
    {
        Console.WriteLine("Your number in 10 system: " + numberInTenSystem);
    }

    while (true)
    {
        result = testOfCharString[numberInTenSystem % endSystem] + result;
        
        if (numberInTenSystem / endSystem < endSystem)
        {
            numberInTenSystem = numberInTenSystem / endSystem;
            result = testOfCharString[numberInTenSystem] + result;
            break;
        }
        numberInTenSystem = numberInTenSystem / endSystem;
    }

    if (negative)
    {
        result = "-" + result;
    }
    
    Console.WriteLine("Your number in " + endSystem +" system: " + result);
    
    return result;

}
