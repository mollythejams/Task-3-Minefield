using System;
using System.Collections.Generic;

class Program
{
    static char[,] minefield ={
      {'x','x','x','o','x'},
      {'x','x','x','o','x'},
      {'x','x','o','x','o'},
      {'x','x','o','x','x'},
      {'x','x','o','x','x'}
    };
    // array store safe path
    List<int[]> safe_path = new List<int[]>();

    static int toshka_step = -1;

    // Get size of minefield
    static int ROWS = minefield.GetLength(0);
    static int COLS = minefield.GetLength(1);
    // Adjacent path coordinates 
    int[] adjacent_r = { -1, -1, -1, 0, 0, 1, 1, 1 };
    int[] adjacent_c = { -1, 0, 1, -1, 1, -1, 0, 1 };

    public bool isSafe(int x, int y)
    {
        return minefield[x, y] == 'o';
    }

    public bool isValid(int x, int y)
    {
        return x < ROWS && y < COLS && x >= 0 && y >= 0;
    }

    public void displayField()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLS; j++)
            {
                Console.Write(minefield[i, j] + "\t");
            }
            Console.WriteLine('\n');
        }
        Console.WriteLine("-----------------\n");
    }

    public void findPath(int x, int y)
    {
        if (isSafe(x, y))
        {
            if (x == 0)
            {
                // safe_path.Add(new int[] {x,y});
                safe_path.Clear();
            }
            // Totoshka steps forward
            minefield[x, y] = 'T';
            Console.WriteLine("Totoshka:" + x + ", " + y);
            // Ally steps forward where Totoshka was
            if (toshka_step > 0)
            {
                minefield[safe_path[toshka_step - 1][0], safe_path[toshka_step - 1][1]] = 'A';
                Console.WriteLine("Ally:" + safe_path[toshka_step - 1][0] + ", " + safe_path[toshka_step - 1][1] + "\n");
            }
            // mark visited
            if (toshka_step > 1)
            {
                minefield[safe_path[toshka_step - 2][0], safe_path[toshka_step - 2][1]] = 'v';
            }

            displayField();
            safe_path.Add(new int[] { x, y });
            toshka_step = safe_path.Count;
            for (int i = 0; i < 8; i++)
            {
                if (isValid(x + adjacent_r[i], y + adjacent_c[i]))
                {
                    if (safe_path[toshka_step - 1][0] == ROWS - 1)
                    {
                        break;
                    }
                    findPath(x + adjacent_r[i], y + adjacent_c[i]);
                }
            }

        }//end if
    }//end findPath
    public void exit()
    {
        Console.WriteLine("Totoshka: Safe Zone");
        minefield[safe_path[toshka_step - 1][0], safe_path[toshka_step - 1][1]] = 'A';
        Console.WriteLine("Ally:" + safe_path[toshka_step - 1][0] + ", " + safe_path[toshka_step - 1][1] + "\n");
        minefield[safe_path[toshka_step - 2][0], safe_path[toshka_step - 2][1]] = 'v';
        displayField();

        Console.WriteLine("Totoshka: Safe Zone");
        Console.WriteLine("Ally: Safe Zone\n");
        minefield[safe_path[toshka_step - 1][0], safe_path[toshka_step - 1][1]] = 'v';
        displayField();
    }

    public static void Main(string[] args)
    {
        Program p = new Program();
        // Use a for loop to look for safe entrance in the field
        for (int i = 0; i < COLS; i++)
        {
            p.findPath(0, i);
        }

        //Once Totoshka found the exit, exit function is called to illustrate Ally and Totoshka making it to the safe zone.
        p.exit();


        Console.WriteLine("====SAFE ZONE====\n");
        Console.WriteLine("◝(⁰v⁰)◜   U・;・U");
    }

    // end of main


}
