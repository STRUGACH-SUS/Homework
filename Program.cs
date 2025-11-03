using System.Text;

namespace ConsoleApp5
{
    interface IForGettingAPath //Неправильный нейминг
    {
        string SetPath();
        string GetPathNew(string filePath);
    }

    interface IForConvertingTextToCode//Неправильный нейминг
    {
        string[] ConvertTextToCode(string filePath,string nameForNewClass);
    }

    interface IClassBeingCreated//Неправильный нейминг
    {
        void CreateClass(string classPath, string[] contents);
    }
    
    /// <summary>
    /// Accepts parameters from the user's file and creates the file. The parameters are passed as "Name;Type;Access;".
    /// </summary>
    public class CreatureClassBeingCreated: IForGettingAPath, IForConvertingTextToCode, IClassBeingCreated
    {
        /// <summary>
        /// The path to the file with the parameters
        /// </summary>
        private string filePath { get;}
        /// <summary>
        /// The new generated path to the class being created
        /// </summary>
        private string pathForNewClass { get;}
        /// <summary>
        /// The сontents of the new class
        /// </summary>
        private string[] contents { get;}
        
        public CreatureClassBeingCreated()
        {
            filePath = SetPath();
            pathForNewClass = GetPathNew(filePath);
            contents = ConvertTextToCode(filePath, pathForNewClass);
            CreateClass(pathForNewClass, contents);
        }
        
        /// <summary>
        /// The SetPath method gets the path from the keyboard and checks for correct input.
        /// </summary>
        /// <returns>Returns the path to the specified file</returns>
        /// <exception cref="Exception"></exception>
        public string SetPath()
        {
            try
            {
                Console.Write("Enter the file path: ");
                string? filePath = Console.ReadLine();//проработать исключения
                if (!File.Exists(filePath))
                {
                    throw new Exception("File does not exist or wrong path.");
                }
                return filePath;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
            return null;
        }
        
        /// <summary>
        /// Generates the path to the class being created.
        /// </summary>
        /// <returns>Returns the path of the class being created</returns>
        public string GetPathNew(string filePath)
        {
            string nameForNewClass = Path.GetFileNameWithoutExtension(filePath);//По идее тут не нужны исключения
            string filePathWithNewNameOfFile = filePath.Substring(0,filePath.LastIndexOf('\\')+1)+$"{nameForNewClass}.cs";
            return filePathWithNewNameOfFile;
        }

        /// <summary>
        /// Creates class variables and its constructor based on the parameters from the specified file.
        /// </summary>
        /// <returns>Returns an array of strings for the class description</returns>
        public string[] ConvertTextToCode(string filePath, string pathForNewClass)//проработать исключения
        {
            string nameForNewClass = Path.GetFileNameWithoutExtension(pathForNewClass);
            
            try
            {
                string [] allLinesInFile = File.ReadAllLines(filePath);
                if (allLinesInFile.Length == 0)
                {
                    throw new Exception("File empty.");
                }
                
                string [] nameInLines = new string[allLinesInFile.Length-1];
                string [] typeInLines = new string[allLinesInFile.Length-1];
                string [] accessInLines = new string[allLinesInFile.Length-1];
            
                for (int i = 0; i < nameInLines.Length; i++)
                {
                    nameInLines[i] = allLinesInFile[i+1].Split(';',4)[0].ToLower().Trim();
                    typeInLines[i] =  allLinesInFile[i+1].Split(';',4)[1].ToLower().Trim() switch
                    {
                        "int"=>"int",
                        "real"=>"double",
                        "string"=>"string",
                        "bool"=>"bool",
                        _=>throw new Exception("Unknown type in file.")
                    };
                    accessInLines[i] = allLinesInFile[i+1].Split(';',4)[2].Trim();
                }
                if (nameInLines.Contains(null) || typeInLines.Contains(null) || accessInLines.Contains(null))
                {
                    throw new Exception("Wrong format.");
                }
                
                string[] textCodeForNewFile = new string[2*nameInLines.Length+6];
                textCodeForNewFile[0] = $"public class {nameForNewClass}";
                textCodeForNewFile[1] =  "{";
                for (int i =0; i < nameInLines.Length; i++)
                { 
                    textCodeForNewFile[i+2]=$"   public {typeInLines[i]} _{nameInLines[i]}" + "{"+ $"{(accessInLines[i]=(accessInLines[i]=="RW")? "get;set;":"get;")}"+"}";//нужен StringBulder
                }
                textCodeForNewFile[2 + nameInLines.Length] = $"   public {nameForNewClass}(";
                for (int i = 0; i < nameInLines.Length; i++)
                {
                    textCodeForNewFile[2 + nameInLines.Length] = textCodeForNewFile[2 + nameInLines.Length]+$"{typeInLines[i]} {nameInLines[i]},";//нужен StringBulder//ебать работает
                }
                textCodeForNewFile[2+nameInLines.Length] = textCodeForNewFile[2+nameInLines.Length].Substring(0,textCodeForNewFile[2+nameInLines.Length].Length-1)+')'; //нужен StringBulder
                textCodeForNewFile[3+nameInLines.Length] =  "   {";
                for (int i = 0; i < nameInLines.Length; i++)
                {
                    textCodeForNewFile[i+4+nameInLines.Length]= $"      _{nameInLines[i]}={nameInLines[i]};";
                }
                textCodeForNewFile[^2] = "   }";
                textCodeForNewFile[^1] = "}";
                return textCodeForNewFile;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }
            
            return null;
        }

        /// <summary>
        /// A file is created and filled with an array of strings. If a file with that name exists, it is replaced with a new one.
        /// </summary>
        /// <param name="classPath">The path of the new class</param>
        /// <param name="contents">The content of the new class</param>
        public void CreateClass(string classPath, string[] contents)
        {
            if (File.Exists(classPath))
            {
                File.Delete(classPath);
            }
            File.AppendAllLines(classPath, contents, Encoding.UTF8);
            Console.WriteLine("File created successfully!");
        }
    }
    
    public class Practic4
    {
        static void Main()
        {
            CreatureClassBeingCreated theNewClassBeingCreated = new CreatureClassBeingCreated();
        }
    }
}