namespace console_draw
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            Console.BackgroundColor = ConsoleColor.Red;
            do
            {
                var key = Console.ReadKey().Key;

                if (Console.CursorTop > 0 && Console.CursorTop < Console.WindowHeight && Console.CursorLeft > 0 && Console.CursorLeft < Console.WindowWidth)
                {
                    switch (key)
                    {
                        case ConsoleKey.DownArrow:
                            Console.Write(' ');
                            Console.CursorTop++;
                            Console.CursorLeft--;
                            break;
                        case ConsoleKey.UpArrow:
                            Console.Write(' ');
                            Console.CursorTop--;
                            Console.CursorLeft--;
                            break;
                        case ConsoleKey.RightArrow:
                            Console.Write(' ');
                            break;
                        case ConsoleKey.LeftArrow:
                            Console.Write(' ');
                            Console.CursorLeft = Console.CursorLeft - 2;
                            break;
                    }
                }
                else
                {
                    if (Console.CursorTop == 0 || Console.CursorLeft == 0)
                    {
                        Console.CursorTop++;
                        Console.CursorLeft++;
                    }
                    else
                    {
                        if (Console.CursorLeft == Console.WindowWidth || Console.CursorTop == Console.WindowHeight)
                        {
                            Console.CursorTop--;
                            Console.CursorLeft--;
                        }
                        else
                        {
                            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
                        }
                        
                    }
                }


            }
            while (Console.ReadKey().Key != ConsoleKey.Enter);

        }
    }
}
