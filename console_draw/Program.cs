namespace console_draw
{
    internal class Program
    {
        public static int[,] current_page = new int[Console.WindowWidth, Console.WindowHeight];
        public static int[,] page1 = new int[Console.WindowWidth, Console.WindowHeight];
        public static int[,] page2 = new int[Console.WindowWidth, Console.WindowHeight];
        public static int[,] page3 = new int[Console.WindowWidth, Console.WindowHeight];
        public static int color_value = 12;
        static void pagegenerating()
        {
            Console.Clear();
            for (int i = 0; i < current_page.GetLength(1); i++)
            {
                for (int j = 0; j < current_page.GetLength(0); j++)
                {
                    if (current_page[j, i] == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.Write(' ');
                    }
                    else if (current_page[j, i] == 1)
                    {
                        Console.BackgroundColor = (ConsoleColor)color_value;
                        Console.Write(' ');
                    }
                }

            }
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
        }


        static void Main(string[] args)
        {
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            var cur_color = new ConsoleColor();
            cur_color = ConsoleColor.Red;
            Console.BackgroundColor = cur_color;
            var key = Console.ReadKey().Key;
            List<ConsoleKey> lastkeys = new List<ConsoleKey>();
            current_page = page1;
            for (int i = 0; i < page1.GetLength(0); i++)
            {
                for (int j = 0; j < page1.GetLength(1); j++)
                {
                    page1[i, j] = 0;
                }
            }

            for (int i = 0; i < page2.GetLength(0); i++)
            {
                for (int j = 0; j < page2.GetLength(1); j++)
                {
                    page2[i, j] = 0;
                }
            }

            for (int i = 0; i < page3.GetLength(0); i++)
            {
                for (int j = 0; j < page3.GetLength(1); j++)
                {
                    page3[i, j] = 0;
                }
            }
            bool rainbow_mode = false;
            do
            {
                key = Console.ReadKey().Key;

                if (Console.CursorTop > 0 && Console.CursorTop < Console.WindowHeight && Console.CursorLeft > 0 && Console.CursorLeft < Console.WindowWidth)
                {
                    switch (key)
                    {
                        case ConsoleKey.DownArrow:
                            current_page[Console.CursorLeft, Console.CursorTop] = 1;
                            Console.Write(' ');       
                            lastkeys.Add(key);
                            Console.CursorTop++;
                            Console.CursorLeft--;
                            break;
                        case ConsoleKey.UpArrow:
                            current_page[Console.CursorLeft, Console.CursorTop] = 1;
                            Console.Write(' ');                            
                            lastkeys.Add(key);
                            Console.CursorTop--;
                            Console.CursorLeft--;
                            break;
                        case ConsoleKey.RightArrow:
                            current_page[Console.CursorLeft, Console.CursorTop] = 1;
                            Console.Write(' ');
                            lastkeys.Add(key);
                            break;
                        case ConsoleKey.LeftArrow:
                            current_page[Console.CursorLeft, Console.CursorTop] = 1;
                            Console.Write(' ');                        
                            lastkeys.Add(key);
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
                            color_value = 0;
                            Console.BackgroundColor = (ConsoleColor)color_value;
                            if (lastkeys.Count > 0)
                            {
                                switch (lastkeys[lastkeys.Count - 1])
                                {

                                    case ConsoleKey.DownArrow:
                                        Console.CursorTop++;
                                        Console.CursorLeft--;
                                        Console.Write(' ');
                                        Console.Write(' ');
                                        Console.CursorTop--;
                                        Console.CursorLeft -= 2;
                                        Console.Write(' ');
                                        Console.CursorLeft--;
                                        Console.CursorTop--;
                                        Console.Write(' ');
                                        Console.CursorLeft--;
                                        lastkeys.RemoveAt(lastkeys.Count - 1);
                                        break;
                                    case ConsoleKey.UpArrow:
                                        Console.CursorTop--;
                                        Console.CursorLeft--;
                                        Console.Write(' ');
                                        Console.Write(' ');
                                        Console.CursorTop++;
                                        Console.CursorLeft -= 2;
                                        Console.Write(' ');
                                        Console.CursorLeft--;
                                        Console.CursorTop++;
                                        Console.Write(' ');
                                        Console.CursorLeft--;
                                        lastkeys.RemoveAt(lastkeys.Count - 1);
                                        break;
                                    case ConsoleKey.RightArrow:
                                        Console.CursorLeft++;
                                        Console.Write(' ');
                                        Console.CursorLeft -= 4;
                                        Console.Write(' ');
                                        Console.Write(' ');
                                        Console.CursorLeft -= 2;
                                        lastkeys.RemoveAt(lastkeys.Count - 1);
                                        break;
                                    case ConsoleKey.LeftArrow:
                                        Console.CursorLeft--;
                                        Console.Write(' ');
                                        lastkeys.RemoveAt(lastkeys.Count - 1);
                                        break;
                                }
                            }
                            
                            color_value = cur_color.GetHashCode();
                            Console.BackgroundColor = (ConsoleColor)color_value;
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
                            Console.Write(' ');
                            Console.CursorLeft--;
                            break;
                        case ConsoleKey.F1:
                            current_page = page1;
                            pagegenerating();
                            break;
                        case ConsoleKey.F2:
                            current_page = page2;
                            pagegenerating();
                            break;
                        case ConsoleKey.F3:
                            current_page = page3;
                            pagegenerating();
                            break;
                    }
                }
                else
                {
                    if (Console.CursorTop == 0 || Console.CursorLeft == 0)
                    {
                        Console.CursorTop++;
                        Console.CursorLeft++;
                        lastkeys.Add(ConsoleKey.RightArrow);
                    }
                    else
                    {
                        if (Console.CursorLeft == Console.WindowWidth || Console.CursorTop == Console.WindowHeight)
                        {
                            Console.CursorTop--;
                            Console.CursorLeft--;
                            lastkeys.Add(ConsoleKey.LeftArrow);
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
                Console.BackgroundColor = (ConsoleColor)color_value;
            }
            while (key != ConsoleKey.Escape);

        }
    }
}
