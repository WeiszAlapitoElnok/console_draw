using System.Diagnostics;
using System.Drawing;

namespace console_draw
{
    internal class Program
    {
        public static int[,] current_page = new int[Console.WindowWidth, Console.WindowHeight];
        public static int[,] page1 = new int[Console.WindowWidth, Console.WindowHeight];
        public static int[,] page2 = new int[Console.WindowWidth, Console.WindowHeight];
        public static int[,] page3 = new int[Console.WindowWidth, Console.WindowHeight];
        public static string[] savedata = File.ReadAllLines("mentes.txt");
        public static Dictionary<(int x, int y), (ConsoleColor color, char op)> save = new Dictionary<(int x, int y), (ConsoleColor color, char op)>();
        public static int color_value = 0;
        public static bool active = true;
        public static bool main_menu_active = false;
        public static int menu_pos = color_value;
        public static int main_menu_pos = 0;
        static void pagegenerating()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            load();
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (global::System.Int32 j = 0; j < Console.WindowWidth; j++)
                {
                    if (i == 0 && j != 0 && j != Console.WindowWidth - 1 || i == Console.WindowHeight - 1 && j != 0 && j != Console.WindowWidth - 1)
                    {
                        Console.Write('─');
                    }
                    else if (j == 1)
                    {
                        Console.Write('│');
                        Console.CursorLeft = Console.WindowWidth - Console.WindowWidth + 20;
                        Console.Write('│');
                        Console.CursorLeft = Console.WindowWidth - 1;
                        Console.Write('│');
                    }
                    else if (i == 0 && j == 0)
                    {
                        Console.Write('┌');
                    }
                    else if (i == 0 && j == Console.WindowWidth - 1)
                    {
                        Console.Write('┐');
                    }
                    else if (i == Console.WindowHeight - 1 && j == 0)
                    {
                        Console.Write('└');
                    }
                    else if (i == Console.WindowHeight - 1 && j == Console.WindowWidth - 1)
                    {
                        Console.Write('┘');
                    }
                }
            }

            for (int i = 0; i < savedata.Length; i++)
            {
                Console.SetCursorPosition(int.Parse(savedata[i].Split(',')[0]), int.Parse(savedata[i].Split(',')[1]));
                Console.BackgroundColor = (ConsoleColor)int.Parse(savedata[i].Split(',')[2]);
                Console.Write(' ');
            }
            Console.BackgroundColor = ConsoleColor.Black;
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (i + 1)));
                Console.Write(((ConsoleColor)i).ToString());
            }
            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (20)));
            Console.Write("Rainbow_Mode");
            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (21)));
            Console.Write("Box_Drawer");
            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (color_value + 1)));
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(((ConsoleColor)color_value).ToString());
            Console.BackgroundColor = (ConsoleColor)color_value;
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
        }
        static void Main_Menu()
        {
            Save();
            Console.BackgroundColor = ConsoleColor.Black;
            main_menu_active = true;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Console.WindowWidth/2-10, Console.WindowHeight / 10);
            for (int row = 0; row <16; row++)
            {
                for (int column = 0; column < 15; column++)
                {
                    if (row == 0 || column == 0 || row == 16 - 1 || column == 15 - 1)
                    {
                        Console.Write("* ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }
                Console.SetCursorPosition(Console.WindowWidth / 2-10, Console.WindowHeight / 10 + (row+1));
            }
            Console.SetCursorPosition(Console.WindowWidth / 2+2, Console.WindowHeight / 10+2);
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Save");
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, Console.WindowHeight / 10 + 3);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Exit");
            Console.SetCursorPosition(Console.WindowWidth / 2 + 2, Console.WindowHeight / 10 + 6);
            Console.Write("Help:");
            Console.SetCursorPosition(Console.WindowWidth / 2 -9, Console.WindowHeight / 10 + 8);
            Console.Write("Delete: reseting the canvas");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 10 + 10);
            Console.Write("Z: undoing the last cube");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 10 + 11);
            Console.Write("painted");
            Console.SetCursorPosition(Console.WindowWidth / 2 -9, Console.WindowHeight / 10 + 13);
            Console.Write("F1,F2,F3: switching between");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 9, Console.WindowHeight / 10 + 14);
            Console.Write("pages");
            Console.SetCursorPosition(Console.WindowWidth / 2 + 6, Console.WindowHeight / 10 + 2);
        }


        static void Save()
        {
            File.AppendAllLines("mentes.txt", save.Select(data => $"{data.Key.x},{data.Key.y},{(int)data.Value.color},{(int)data.Value.op}"));
        }

        static void load()
        {
            savedata = File.ReadAllLines("mentes.txt");
            for (int i = 0; i < savedata.Length; i++)
            {
                current_page[int.Parse(savedata[i].Split(',')[0]), int.Parse(savedata[i].Split(',')[1])] = 1;
            }
        }
        static void Main(string[] args)
        {
            Console.SetWindowSize(120,30);
            Tuple<int, int> last_pos = new Tuple<int, int>(0, 0);
            bool first = true;
            pagegenerating();
            Console.SetCursorPosition(Console.WindowWidth / 2, Console.WindowHeight / 2);
            var cur_color = new ConsoleColor();
            cur_color = ConsoleColor.Red;
            ConsoleKey key;
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
                key = Console.ReadKey(true).Key;

                if (Console.CursorTop > 0 && Console.CursorTop < Console.WindowHeight - 1 && Console.CursorLeft > (Console.WindowWidth - Console.WindowWidth + 22) && Console.CursorLeft < Console.WindowWidth - 1)
                {
                    switch (key)
                    {
                        case ConsoleKey.Spacebar:
                            Console.Write(' ');
                            break;
                        case ConsoleKey.DownArrow:
                            if (main_menu_active)
                            {
                                
                            }
                            else
                            {
                                Console.CursorTop++;
                                Console.CursorLeft--;
                                current_page[Console.CursorLeft, Console.CursorTop] = 1;
                                Console.Write(' ');
                                lastkeys.Add(key);
                                save[(Console.CursorLeft, Console.CursorTop)] = (Console.BackgroundColor, ' ');
                            }
                            break;
                        case ConsoleKey.UpArrow:
                            if (main_menu_active)
                            {
                            }
                            else
                            {
                                Console.CursorTop--;
                                Console.CursorLeft--;
                                current_page[Console.CursorLeft, Console.CursorTop] = 1;
                                Console.Write(' ');
                                lastkeys.Add(key);
                                save[(Console.CursorLeft, Console.CursorTop)] = (Console.BackgroundColor, ' ');
                            }                     
                            break;
                        case ConsoleKey.RightArrow:
                            if (main_menu_active)
                            {
                                
                            }
                            else
                            {
                                current_page[Console.CursorLeft, Console.CursorTop] = 1;
                                Console.Write(' ');
                                lastkeys.Add(key);
                                save[(Console.CursorLeft, Console.CursorTop)] = (Console.BackgroundColor, ' ');
                            }                           
                            break;
                        case ConsoleKey.LeftArrow:
                            if (main_menu_active)
                            {
                                
                            }
                            else
                            {
                                Console.CursorLeft = Console.CursorLeft - 2;
                                current_page[Console.CursorLeft, Console.CursorTop] = 1;
                                Console.Write(' ');
                                lastkeys.Add(key);
                                save[(Console.CursorLeft, Console.CursorTop)] = (Console.BackgroundColor, ' ');
                            }         
                            break;
                        case ConsoleKey.Delete:
                            File.WriteAllLines("mentes.txt", new string[] { });
                            pagegenerating();
                            break;
                        case ConsoleKey.PageDown:
                            if (main_menu_active)
                            {
                                Console.SetCursorPosition(Console.WindowWidth / 2 + 2, Console.WindowHeight / 10 + 2);
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Save");
                                Console.SetCursorPosition(Console.WindowWidth / 2 + 2, Console.WindowHeight / 10 + 3);
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("Exit");
                                main_menu_pos = 1;
                            }
                            else
                            {
                                if (menu_pos < 15)
                                {
                                    last_pos = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
                                    menu_pos++;
                                    color_value++;
                                    Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (color_value + 1)));
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write(((ConsoleColor)color_value).ToString());
                                    Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (color_value)));
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write(((ConsoleColor)(color_value - 1)).ToString());
                                    Console.SetCursorPosition(last_pos.Item1, last_pos.Item2);
                                }
                                else
                                {
                                    switch (menu_pos)
                                    {
                                        case 15:
                                            last_pos = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
                                            menu_pos++;
                                            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - 20));
                                            Console.BackgroundColor = ConsoleColor.White;
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            Console.Write("Rainbow_Mode");
                                            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (color_value + 1)));
                                            Console.BackgroundColor = ConsoleColor.Black;
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write(((ConsoleColor)(color_value)).ToString());
                                            Console.SetCursorPosition(last_pos.Item1, last_pos.Item2);
                                            break;
                                        case 16:
                                            last_pos = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
                                            menu_pos++;
                                            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - 21));
                                            Console.BackgroundColor = ConsoleColor.White;
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            Console.Write("Box_Drawer");
                                            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - 20));
                                            Console.BackgroundColor = ConsoleColor.Black;
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("Rainbow_Mode");
                                            Console.SetCursorPosition(last_pos.Item1, last_pos.Item2);
                                            break;
                                    }
                                }
                            }
                            break;
                        case ConsoleKey.PageUp:
                            if (main_menu_active)
                            {
                                Console.SetCursorPosition(Console.WindowWidth / 2 + 2, Console.WindowHeight / 10 + 3);
                                Console.BackgroundColor = ConsoleColor.Black;
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("Exit");
                                Console.SetCursorPosition(Console.WindowWidth / 2 + 2, Console.WindowHeight / 10 + 2);
                                Console.BackgroundColor = ConsoleColor.White;
                                Console.ForegroundColor = ConsoleColor.Black;
                                Console.Write("Save");
                                main_menu_pos = 0;
                            }
                            else
                            {
                                if (menu_pos > 0 && menu_pos < 16)
                                {
                                    last_pos = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
                                    menu_pos--;
                                    color_value--;
                                    Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (color_value + 1)));
                                    Console.BackgroundColor = ConsoleColor.White;
                                    Console.ForegroundColor = ConsoleColor.Black;
                                    Console.Write(((ConsoleColor)color_value).ToString());
                                    Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (color_value + 2)));
                                    Console.BackgroundColor = ConsoleColor.Black;
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write(((ConsoleColor)(color_value + 1)).ToString());
                                    Console.SetCursorPosition(last_pos.Item1, last_pos.Item2);
                                }
                                else
                                {
                                    switch (menu_pos)
                                    {
                                        case 16:
                                            last_pos = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
                                            menu_pos--;
                                            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - (color_value + 1)));
                                            Console.BackgroundColor = ConsoleColor.White;
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            Console.Write(((ConsoleColor)color_value).ToString());
                                            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - 20));
                                            Console.BackgroundColor = ConsoleColor.Black;
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("Rainbow_Mode");
                                            Console.SetCursorPosition(last_pos.Item1, last_pos.Item2);
                                            break;
                                        case 17:
                                            last_pos = new Tuple<int, int>(Console.CursorLeft, Console.CursorTop);
                                            menu_pos--;
                                            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - 20));
                                            Console.BackgroundColor = ConsoleColor.White;
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            Console.Write("Rainbow_Mode");
                                            Console.SetCursorPosition(Console.WindowWidth - (Console.WindowWidth - 2), Console.WindowHeight - (Console.WindowHeight - 21));
                                            Console.BackgroundColor = ConsoleColor.Black;
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("Box_Drawer");
                                            Console.SetCursorPosition(last_pos.Item1, last_pos.Item2);
                                            break;
                                    }

                                }
                            }
                            break;
                        case ConsoleKey.Z:
                            cur_color = (ConsoleColor)color_value;
                            color_value = 0;
                            Console.BackgroundColor = (ConsoleColor)color_value;
                            if (lastkeys.Count > 0)
                            {
                                switch (lastkeys[lastkeys.Count - 1])
                                {

                                    case ConsoleKey.DownArrow:
                                        if (first)
                                        {
                                            Console.CursorLeft--;
                                            first = false;
                                        }
                                        Console.CursorTop++;
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
                                        if (first)
                                        {
                                            Console.CursorLeft--;
                                            first = false;
                                        }
                                        Console.CursorTop--;
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
                                        first = true;
                                        Console.CursorLeft++;
                                        Console.Write(' ');
                                        Console.CursorLeft -= 4;
                                        Console.Write(' ');
                                        Console.Write(' ');
                                        Console.CursorLeft -= 2;
                                        lastkeys.RemoveAt(lastkeys.Count - 1);
                                        break;
                                    case ConsoleKey.LeftArrow:
                                        first = true;
                                        Console.Write(' ');
                                        lastkeys.RemoveAt(lastkeys.Count - 1);
                                        break;
                                }
                            }
                            color_value = cur_color.GetHashCode();
                            Console.BackgroundColor = (ConsoleColor)color_value;
                            break;
                        case ConsoleKey.Enter:
                            if (main_menu_active)
                            {
                                switch (main_menu_pos)
                                {
                                    case 0:
                                        Save();
                                        main_menu_active = false;
                                        pagegenerating();
                                        break;
                                    case 1:
                                        active = false;
                                        break;
                                    }
                                }
                            else
                            {
                                switch (menu_pos)
                                {
                                    case 16:
                                        if (rainbow_mode)
                                        {
                                            rainbow_mode = false;
                                        }
                                        else
                                        {
                                            rainbow_mode = true;
                                        }
                                        break;
                                    case 17:
                                        for (int i = 0; i < 3; i++)
                                        {
                                            for (int j = 0; j < 6; j++)
                                            {
                                                Console.Write(' ');
                                            }
                                            Console.CursorTop++;
                                            Console.CursorLeft -= 6;
                                        }
                                        Console.CursorLeft--;
                                        Console.CursorTop -= 3;
                                        Console.Write(' ');
                                        Console.CursorLeft--;
                                        break;
                                }
                            }     
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
                        case ConsoleKey.Escape:
                            if (main_menu_active == false)
                            {
                                Main_Menu();
                            }
                            else
                            {
                                main_menu_active = false;
                                pagegenerating();
                            }
                            
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
                    Console.BackgroundColor = (ConsoleColor)cur_color.GetHashCode();
                }
                else
                {
                    Console.BackgroundColor = (ConsoleColor)color_value;
                }
                
            }
            while (active);

        }
    }
}
