namespace Homework3
{
    public class CalculatorOfNumeralSystem
    {
        private string MathematicalExpression {get;}
        private int FirstNumeralSystem {get;}
        private int SecondNumeralSystem {get;}
        private string TestOfCharString = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private List<string> MathematicalExpressionList {get;}

        public CalculatorOfNumeralSystem(string mathematicalExpression, string firstNumeralSystem, string secondNumeralSystem)
        {
            MathematicalExpression = mathematicalExpression;
            int.TryParse(firstNumeralSystem, out int FirstNumeralSystem);
            this.FirstNumeralSystem = FirstNumeralSystem;
            int.TryParse(secondNumeralSystem, out int SecondNumeralSystem);
            this.SecondNumeralSystem = SecondNumeralSystem;
            
            CheckTheCorrectInput(MathematicalExpression, this.FirstNumeralSystem, this.SecondNumeralSystem,TestOfCharString);
            MathematicalExpressionList=ConvertMathematicalExpressionStringToList(MathematicalExpression);
            ConvertOfAnExpressionToAnotherNumberSystem(MathematicalExpressionList, this.FirstNumeralSystem, this.SecondNumeralSystem, TestOfCharString);
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

        private static List<string> ConvertMathematicalExpressionStringToList(string mathematicalExpression)
        {
            int counter = 0;
            List<string> ListMathematicalExpression = new List<string>();
            
            for (int i = 0; i < mathematicalExpression.Length; i++)
            {
                if (mathematicalExpression[i] == '+' || mathematicalExpression[i] == '-' ||
                    mathematicalExpression[i] == '/' || mathematicalExpression[i] == '*')
                {
                    ListMathematicalExpression.Add(mathematicalExpression.Substring(counter,i-counter));
                    ListMathematicalExpression.Add(mathematicalExpression[i].ToString());
                    counter = i + 1;
                }
            }
            ListMathematicalExpression.Add(mathematicalExpression.Substring(counter));
            if (ListMathematicalExpression[0] == "")
            {
                ListMathematicalExpression[0] = "0";
            }

            return ListMathematicalExpression;
        }
        
        private static void ConvertOfAnExpressionToAnotherNumberSystem(List<string> mathematicalExpression,int firstNumeralSystem, int secondNumeralSystem, string testOfCharString)
        {
            for (int i = 0; i < mathematicalExpression.Count; i++)
            {
                if (mathematicalExpression[i] == "*")
                {
                    mathematicalExpression[i] = MultiOfTwoNumbers(mathematicalExpression[i-1],mathematicalExpression[i+1],firstNumeralSystem,testOfCharString);
                    mathematicalExpression.RemoveAt(i-1);//тут ошибки вылетают
                    mathematicalExpression.RemoveAt(i+1);
                }
                
                
                
            }
            for (int i = 0; i < mathematicalExpression.Count; i++)
            {
                if (mathematicalExpression[i] == "+")
                {
                    mathematicalExpression[i] = SumOfTwoNumbers(mathematicalExpression[i-1],mathematicalExpression[i+1],firstNumeralSystem,testOfCharString);
                    mathematicalExpression.RemoveAt(i-1);
                    mathematicalExpression.RemoveAt(i+1);
                }
                
                
            }
            
        }

        private static string SumOfTwoNumbers(string firstNumber, string secondNumber, int firstNumeralSystem, string testOfCharString)//В этом методе надо добавить чтобы у меньшего по длине числа добавлялись спереди нули
        {
           
            string result="",transfer="";
            string substring = string.Create(Math.Abs(firstNumber.Length-secondNumber.Length), '0', (c, b) => { for (int i = 0; i < c.Length; i++) c[i] = '0'; });
            if (firstNumber.Length > secondNumber.Length)
            {
                secondNumber = substring +  secondNumber;
            }
            else
            {
                firstNumber = substring +  firstNumber;
            }
            for (int i = firstNumber.Length - 1; i >= 0; i--)
            {   
                result = testOfCharString[((testOfCharString.IndexOf(firstNumber[i]) + testOfCharString.IndexOf(secondNumber[i])+testOfCharString.IndexOf(transfer))%firstNumeralSystem)] + result;
                transfer = testOfCharString[((testOfCharString.IndexOf(firstNumber[i]) + testOfCharString.IndexOf(secondNumber[i])+testOfCharString.IndexOf(transfer))/firstNumeralSystem)].ToString();
            }
            result=(transfer!="0")?transfer+result:result;
            return result;
        }
        
        private static string MultiOfTwoNumbers(string firstNumber, string secondNumber, int firstNumeralSystem, string testOfCharString)
        {
            string result = "",resultIntermediative = "",transfer="";

            for (int i = firstNumber.Length - 1; i >= 0; i--)
            {   
                for (int j = secondNumber.Length - 1; j >= 0; j--)
                {
                    resultIntermediative = testOfCharString[(testOfCharString.IndexOf(firstNumber[i]) * testOfCharString.IndexOf(secondNumber[j])+testOfCharString.IndexOf(transfer))%firstNumeralSystem] + resultIntermediative;
                    transfer = testOfCharString[(testOfCharString.IndexOf(firstNumber[i]) * testOfCharString.IndexOf(secondNumber[j])+testOfCharString.IndexOf(transfer))/firstNumeralSystem].ToString();
                }
                resultIntermediative=(transfer!="0")?transfer+resultIntermediative:resultIntermediative;
                result=SumOfTwoNumbers(result,resultIntermediative,firstNumeralSystem,testOfCharString);
                resultIntermediative = "";
            }
            return result;
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
