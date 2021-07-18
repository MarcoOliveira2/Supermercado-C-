using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Supermercado.Data
{
    class ToolsTable
    {
        static private int tableWidth;
        static private List<string> table;

        #region Print 
        static public void Print(int tableWidth_, List<string> table_)
        {
            try
            {
                tableWidth = tableWidth_ * 2;
                table = table_;

                PrintLine();
                string head = table.FirstOrDefault();
                PrintRow(head.Split("|"));

                PrintLine();
                foreach (var item in table.Where(x => x != head))
                {
                    PrintRow(item.Split("|"));
                }

                PrintLine();
            }
            catch (Exception a)
            {
                Console.WriteLine("Impossível printar lista. Razão: " + a.Message);
            }
        }

        static private void PrintLine()
        {
            try
            {
                Console.WriteLine(new string('-', tableWidth));
            }
            catch (Exception a)
            {
                Console.WriteLine("Impossivel printar lista. Razão: " + a.Message);
            }
        }

        static private void PrintRow(params string[] columns)
        {
            try
            {
                int width = (tableWidth - columns.Length) / columns.Length;
                string row = "|";

                foreach (string column in columns)
                {
                    row += AlignCentre(column, width) + "|";
                }

                Console.WriteLine(row);
            }
            catch (Exception a)
            {
                Console.WriteLine("Impossivel printar lista. Razão: " + a.Message);
            }
        }
        #endregion Print

        #region Align
        static private string AlignCentre(string text, int width)
        {
            try
            {
                text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

                if (string.IsNullOrEmpty(text))
                {
                    return new string(' ', width);
                }
                else
                {
                    return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
                }
            }
            catch (Exception a)
            {
                return a.Message;
            }
        }
        #endregion

    }
}
