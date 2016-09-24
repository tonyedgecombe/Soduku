using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;

namespace Sodukuk
{
    public static class Program
    {
        static void Main()
        {
        }

        public static bool AreRowsValid(int[] grid)
        {
            Debug.Assert(grid.Length == 81);
            return Enumerable.Range(0, 9).All(row => IsRowValid(grid, row));
        }

        public static bool AreColumnsValid(int[] grid)
        {
            Debug.Assert(grid.Length == 81);
            return Enumerable.Range(0, 9).All(column => IsColumnValid(grid, column));
        }

        public static bool IsColumnValid(int[] grid, int column)
        {
            return IsSequenceValid(GetColumn(grid, column));
        }

        public static bool IsRowValid(int[] grid, int row)
        {
            return IsSequenceValid(GetRow(grid, row));
        }

        public static int[] GetRow(int[] grid, int row)
        {
            return grid.Skip(row*9).Take(9).ToArray();
        }

        public static int[] GetColumn(int[] grid, int column)
        {
            Debug.Assert(grid.Length == 81);
            return Enumerable.Range(0, 9).Select(r => grid[column + r*9]).ToArray();
        }

        public static bool IsSequenceValid(int[] row)
        {
            Debug.Assert(row.Length == 9);
            return row.Where(c => c != 0).GroupBy(c => c).All(g => g.Count() == 1);
        }
    }
}
