namespace Homework3
{
    public class CalculatorOfNumeralSystem
    {
        private string MathematicalExpression {get; set;}
        private int FirstNumeralSystem {get; set;}
        private int SecondNumeralSystem {get; set;}
        private string TestOfCharString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        public CalculatorOfNumeralSystem(string mathematicalExpression, string firstNumeralSystem, string secondNumeralSystem)
        {
            MathematicalExpression = mathematicalExpression;
            int.TryParse(firstNumeralSystem, out int FirstNumeralSystem);
            this.FirstNumeralSystem = FirstNumeralSystem;
            int.TryParse(secondNumeralSystem, out int SecondNumeralSystem);
            this.SecondNumeralSystem = SecondNumeralSystem;
            
            CheckTheCorrectInput(MathematicalExpression, this.FirstNumeralSystem, this.SecondNumeralSystem,TestOfCharString);
            ConvertOfAnExpressionToAnotherNumberSystem(MathematicalExpression, this.FirstNumeralSystem,
                this.SecondNumeralSystem, TestOfCharString);
        }

        private static void CheckTheCorrectInput(string mathematicalExpression,int firstNumeralSystem,int secondNumeralSystem,string testOfCharString)
        {
            try//Дописать исключения!!!
            {
                if (!(firstNumeralSystem >= 2 && firstNumeralSystem <= 62 || secondNumeralSystem >= 2 && secondNumeralSystem <= 62))
                {
                    throw new ArgumentException("The numeral systems must be between 2 and 62");
                }

                if (mathematicalExpression == null)
                {
                    throw new ArgumentException("An empty expression cannot be evaluated");
                }

                if (!mathematicalExpression.Where(i=>testOfCharString.Contains(i)).Any())
                {
                    throw new ArgumentException("The expression contains invalid characters");
                }
                
                Console.WriteLine("Correct input, we continue to work.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static void ConvertOfAnExpressionToAnotherNumberSystem(string mathematicalExpression,
            int firstNumeralSystem, int secondNumeralSystem, string testOfCharString)
        {
            
            
            
            
        }
        
        
        
    }
    
    
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a calculator for converting numbers from one number system to another: ");
            Console.Write("Please enter a number or a mathematical expression (for example: AF16+B7*E): ");
            string str = Console.ReadLine().Trim();//Переименовать
            Console.Write("Please enter the number system in which your number or expression is written (for example: 10): ");
            string fns = Console.ReadLine().Trim();//Переименовать
            Console.Write("Enter the number system in which you want to get the result (for example: 8): ");
            string sns = Console.ReadLine().Trim();//Переименовать
            CalculatorOfNumeralSystem yourAttempt =  new CalculatorOfNumeralSystem(str, fns, sns);//Rename
        }
    }
}