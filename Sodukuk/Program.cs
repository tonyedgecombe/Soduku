﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Soduku
{
    public static class Program
    {
        static void Main()
        {
            // Solve for an empty grid
            var arr = new[]
            {
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0, 0,
            };

            var solution = Solve(arr);

            for (int row = 0; row < 9; row++)
            {
                Console.WriteLine(string.Join(",", solution.Skip(9 * row).Take(9)));
            }
        }

        public static int[] Solve(int[] grid)
        {
            var nextCell = GetNextTestCell(grid);
            return nextCell == 81 ? grid : SolveForCell(grid, nextCell);
        }

        private static int[] SolveForCell(int[] grid, int cell)
        {
            return Enumerable.Range(1, 9)
                .Select(i => UpdateElement(grid, cell, i))
                .Where(IsValid)
                .Select(Solve)
                .FirstOrDefault(solution => solution != null);
        }

        private static int[] UpdateElement(int[] grid, int cell, int value)
        {
            return Enumerable.Range(0, 81).Select(i => i == cell ? value : grid[i]).ToArray();
        }

        public static int GetNextTestCell(int[] grid)
        {
            return Enumerable.Range(0, 82).TakeWhile(i => i < 81 && grid[i] != 0).DefaultIfEmpty(-1).Last() + 1;
        }

        public static bool IsValid(int[] grid)
        {
            return AreColumnsValid(grid) && AreRowsValid(grid) && AreBlocksValid(grid);
        }

        public static bool AreBlocksValid(int[] grid)
        {
            return Enumerable.Range(0, 3).All(rb => Enumerable.Range(0, 3).All(cb => IsBlockValid(grid, rb, cb)));
        }

        public static bool IsBlockValid(int[] grid, int rb, int cb)
        {
            return IsSequenceValid(GetBlock(grid, rb, cb).ToArray());
        }

        public static int[] GetBlock(int[] grid, int rb, int cb)
        {
            return Enumerable.Range(0, 3).Select(column =>
                    Enumerable.Range(0, 3).Select(row => grid[(rb*3*9) + row + (cb*3) + column*9]))
                        .SelectMany(item => item)
                        .ToArray();
        }


        public static bool AreRowsValid(int[] grid)
        {
            return Enumerable.Range(0, 9).All(row => IsRowValid(grid, row));
        }

        public static bool AreColumnsValid(int[] grid)
        {
            return Enumerable.Range(0, 9).All(column => IsColumnValid(grid, column));
        }

        public static bool IsColumnValid(int[] grid, int column)
        {
            return IsSequenceValid(GetColumn(grid, column));
        }

        public static bool IsRowValid(IEnumerable<int> grid, int row)
        {
            return IsSequenceValid(GetRow(grid, row));
        }

        public static int[] GetRow(IEnumerable<int> grid, int row)
        {
            return grid.Skip(row*9).Take(9).ToArray();
        }

        public static int[] GetColumn(int[] grid, int column)
        {
            return Enumerable.Range(0, 9).Select(r => grid[column + r*9]).ToArray();
        }

        public static bool IsSequenceValid(IEnumerable<int> row)
        {
            return row.Where(c => c != 0).GroupBy(c => c).All(g => g.Count() == 1);
        }
    }
}
