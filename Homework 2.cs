// Перевод числа пользователя из одной системы счисления в другую
InputNumberInAnotherSystem();
static void InputNumberInAnotherSystem() // Получаем данные от пользователя
{
    Console.Write("Please enter the first number: ");
    string result1 = Console.ReadLine();
    Console.Write("Please enter the first system of your first number: ");
    int.TryParse(Console.ReadLine(),out int firstSystem1);
    Console.Write("Please enter the operation with numbers: ");
    string operation = Console.ReadLine().Trim();
    Console.Write("Please enter the second number: ");
    string result2 = Console.ReadLine();
    Console.Write("Please enter the first system of your second number: ");
    int.TryParse(Console.ReadLine(),out int firstSystem2);
    Console.Write("Please enter the end system of your numbers: ");
    int.TryParse(Console.ReadLine(),out int endSystem);
    CheckValidInputNumber(result1, firstSystem1, endSystem);
    CheckValidInputNumber(result2, firstSystem2, endSystem);
    int number1 = ChangeSystemOfNumberInTenSystem(result1, firstSystem1);
    int number2 = ChangeSystemOfNumberInTenSystem(result2, firstSystem2);
    switch (operation)
    {
        case "*":
            ChangeSystemOfNumbers(number1 * number2, endSystem);
            break;
        case "/":
            ChangeSystemOfNumbers(number1 / number2, endSystem);
            break;
        case "+":
            ChangeSystemOfNumbers(number1 + number2, endSystem);
            break;
        case "-":
            ChangeSystemOfNumbers(number1 - number2, endSystem);
            break;
        default:
            Console.WriteLine("Invalid operation!");
            break;
    }
}
static void CheckValidInputNumber(string number,int firstSystem,int endSystem) // Проверка полученных данных
{
    string testOfCharString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-";
    testOfCharString = testOfCharString.Substring(0,firstSystem)+'-';
    bool invalidDataNumber = false;
    for (int i = 0; i < number.Length; i++)
    {
        if (!(testOfCharString.Contains(number[i]))||(number.Substring(1).Contains('-')))
        {
            invalidDataNumber=true;
            break;
        }
    }
    if (invalidDataNumber||!(firstSystem<=62 && firstSystem>=2)||!(endSystem<=62 && endSystem>=2))
    {
        Console.WriteLine("Invalid input, try again");
        InputNumberInAnotherSystem();
    }
}
static int ChangeSystemOfNumberInTenSystem(string number,int firstSystem) // Перевод из одной системы счисления в другую числа пользователя
{
    
    string testOfCharString="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    bool negative=number[0]=='-';
    number=negative?number.Substring(1):number;
    int counter=0;
    int numberInTenSystem=0;
    for (int i=number.Length-1;i>=0;i--)
    {
        numberInTenSystem=numberInTenSystem+(testOfCharString.IndexOf(number[i]))*Convert.ToInt32(Math.Pow(firstSystem,counter));
        counter++;
    }
    numberInTenSystem=negative?-1*numberInTenSystem:numberInTenSystem;
    return numberInTenSystem;
}
static string ChangeSystemOfNumbers(int sumNumbersInTenSystem,int endSystem)// Перевод из одной системы счисления в другую числа пользователя
{
    string result="";
    string testOfCharString="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
    bool negative=(sumNumbersInTenSystem<0);
    sumNumbersInTenSystem=negative?-1*sumNumbersInTenSystem:sumNumbersInTenSystem;
    while (true)
    {
        result=testOfCharString[sumNumbersInTenSystem%endSystem]+result;
        if (sumNumbersInTenSystem/endSystem<endSystem)
        {
            sumNumbersInTenSystem=sumNumbersInTenSystem/endSystem;
            result=testOfCharString[sumNumbersInTenSystem]+result;
            break;
        }
        sumNumbersInTenSystem=sumNumbersInTenSystem/endSystem;
    }
    Console.WriteLine($"Your result number in {endSystem} system: {(negative?'-'+result:result)}");
    return result;
} 
