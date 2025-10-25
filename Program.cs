using ChessExample;
public abstract class ChessFigures
{
    public string Name{get;}
    protected ChessFigures(string name)=>Name=name;
    public abstract bool ValidStep(CheckerBoardPosition start,CheckerBoardPosition end,string colorFigure);
}
public class PawnFigure:ChessFigures
{
    public PawnFigure():base("Пешка"){}
    public override bool ValidStep(CheckerBoardPosition start,CheckerBoardPosition end, string colorFigure) 
        => ((Math.Abs(start.Y-end.Y)<=1||Math.Abs(start.Y-end.Y)<=3&&start.Y==2)&&colorFigure=="белый"
        ||(Math.Abs(start.Y-end.Y)<=1||Math.Abs(start.Y-end.Y)<=3&&start.Y==7)&& colorFigure=="черный")&&start!=end;
}
public class RookFigure:ChessFigures
{
    public RookFigure():base("Ладья"){}
    public override bool ValidStep(CheckerBoardPosition start,CheckerBoardPosition end,string colorFigure)=>start.Y==end.Y&&start.X==end.X&&start!=end;
}
public class BishopFigure:ChessFigures
{
    public BishopFigure():base("Слон"){}
    public override bool ValidStep(CheckerBoardPosition start,CheckerBoardPosition end,string colorFigure)=>Math.Abs(start.Y-end.Y)==Math.Abs(start.X-end.X)&&start!=end;
}
public class KnightFigure:ChessFigures
{
    public KnightFigure():base("Конь"){}
    public override bool ValidStep(CheckerBoardPosition start,CheckerBoardPosition end,string colorFigure)
        =>(Math.Abs(start.Y-end.Y)==2&&Math.Abs(start.X-end.X)==1||Math.Abs(start.Y-end.Y)==1&&Math.Abs(start.X-end.X)==2)&&start!=end;
}
public class KingFigure:ChessFigures
{
    public KingFigure():base("Король"){}
    public override bool ValidStep(CheckerBoardPosition start,CheckerBoardPosition end,string colorFigure)=>Math.Abs(start.Y-end.Y)<=1&&Math.Abs(start.X-end.X)<=1&&start!=end;
}
public class QueenFigure:ChessFigures
{
    public QueenFigure():base("Ферзь"){}
    public override bool ValidStep(CheckerBoardPosition start,CheckerBoardPosition end,string colorFigure)
        =>(Math.Abs(start.Y-end.Y)<=1&&Math.Abs(start.X-end.X)<=1||Math.Abs(start.Y-end.Y)==Math.Abs(start.X-end.X)||start.Y==end.Y&&start.X==end.X)&&start!=end;
}
public class ProgramChess
{
    public static void Main()
    {
        bool repeat=true;
        while (repeat)
        {
            Console.Clear();
            Console.WriteLine("Доступные фигуры: Пешка, Ладья, Слон, Конь, Ферзь, Король ");
            Console.Write("Введите фигуру: ");
            string? figureName=Console.ReadLine()?.Trim().ToLower();
            ChessFigures? figure=figureName switch
            {
                "пешка"=>new PawnFigure(),
                "ладья"=>new RookFigure(),
                "слон"=>new BishopFigure(),
                "конь"=>new KnightFigure(),
                "ферзь"=>new QueenFigure(),
                "король"=>new KingFigure(),
                _=>null
            };
            if(figure is null)
            {
                Console.WriteLine("Неизвестная фигура!");
                return;
            }
            Console.Write("Введите цвет фигуры (например, черный): ");
            string? colorFigure = Console.ReadLine()?.Trim().ToLower();
            colorFigure = (colorFigure == "белый" || colorFigure == "черный") ? colorFigure : "";
            Console.Write("Введите начальную позицию (A1): ");
            if (!CheckerBoardPosition.TryParse(Console.ReadLine(), null, out var start))
            {
                Console.WriteLine("Такой позиции не существует.");
                return;
            }
            Console.Write("Введите конечную позицию (F8): ");
            if (!CheckerBoardPosition.TryParse(Console.ReadLine(), null, out var end))
            {
                Console.WriteLine("Такой позиции не существует.");
                return;
            }
            bool valid = figure.ValidStep(start,end,colorFigure);
            Console.WriteLine($"Ход фигурой {figure.Name} из положения {start} в {end} — {(valid?"возможен":"невозможен")}");
            Console.WriteLine("Для выхода из программы нажмите TAB, для продолжения любую другую клавишу.");
            repeat=Console.ReadKey().Key!=ConsoleKey.Tab;
        }
    }
}

