namespace console_draw
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            var cur_color = new ConsoleColor();
            cur_color = ConsoleColor.Red;
            int color_value = 12;
            var last_pos = (Console.CursorLeft, Console.CursorTop);
            Console.BackgroundColor = cur_color;
            var key = Console.ReadKey().Key;
            bool rainbow_mode = false;
            do
            {
                key = Console.ReadKey().Key;

                if (Console.CursorTop > 0 && Console.CursorTop < Console.WindowHeight && Console.CursorLeft > 0 && Console.CursorLeft < Console.WindowWidth)
                {
                    switch (key)
                    {
                        case ConsoleKey.DownArrow:
                            Console.Write(' ');
                            last_pos = (Console.CursorLeft, Console.CursorTop);
                            Console.CursorTop++;
                            Console.CursorLeft--;
                            break;
                        case ConsoleKey.UpArrow:
                            Console.Write(' ');
                            last_pos = (Console.CursorLeft, Console.CursorTop);
                            Console.CursorTop--;
                            Console.CursorLeft--;
                            break;
                        case ConsoleKey.RightArrow:
                            Console.Write(' ');
                            last_pos = (Console.CursorLeft-1, Console.CursorTop);
                            break;
                        case ConsoleKey.LeftArrow:
                            Console.Write(' ');
                            last_pos = (Console.CursorLeft-1, Console.CursorTop);
                            Console.CursorLeft = Console.CursorLeft - 2;
                            break;
                        case ConsoleKey.Delete:
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Clear();
                            break;
                        case ConsoleKey.Q:
                            if (color_value > 0)
                            {
                                cur_color--;
                                color_value--;
                            }
                            Console.CursorLeft--;
                            Console.Write(' ');
                            Console.CursorLeft--;


                            break;
                        case ConsoleKey.E:
                            if (color_value < 15)
                            {
                                cur_color++;
                                color_value++;
                            }
                            Console.CursorLeft--;
                            Console.Write(' ');
                            Console.CursorLeft--;

                            break;
                        case ConsoleKey.Z:
                            cur_color = 0;
                            Console.BackgroundColor = cur_color;
                            Console.CursorLeft--;
                            Console.Write(' ');
                            Console.SetCursorPosition(last_pos.Item1, last_pos.Item2);
                            Console.Write(' ');
                            cur_color = (ConsoleColor)color_value;
                            break;
                        case ConsoleKey.R:
                            if (rainbow_mode)
                            {
                                rainbow_mode = false;
                            }
                            else
                            {
                                rainbow_mode = true;
                            }
                            break;
                        case ConsoleKey.N:
                            for (int i = 0; i < 3; i++)
                            {
                                for (int j = 0; j < 6; j++)
                                {
                                    Console.Write(' ');
                                }
                                Console.CursorTop++;
                                Console.CursorLeft -= 6;
                            }
                            Console.CursorLeft --;
                            Console.CursorTop -= 3;
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
                if (rainbow_mode)
                {
                    cur_color = (ConsoleColor)Random.Shared.Next(0, 16);
                    color_value = cur_color.GetHashCode();
                }
                Console.BackgroundColor = cur_color;
            }
            while (key != ConsoleKey.Enter);

        }
    }
}
