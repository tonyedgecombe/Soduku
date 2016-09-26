using System;
using System.Collections.Generic;
using System.Linq;

namespace Soduku
{
    public class Program
    {
        static void Main()
        {
            // Solve for an empty grid
            var arr = new[]
            {
                0, 4, 0, 0, 1, 0, 0, 0, 0,
                2, 0, 5, 0, 6, 8, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 3, 0, 0,
                9, 0, 0, 0, 0, 6, 0, 0, 0,
                8, 0, 1, 3, 0, 2, 6, 0, 5,
                0, 0, 0, 7, 0, 0, 0, 0, 4,
                0, 0, 8, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 6, 3, 0, 8, 0, 7,
                0, 0, 0, 0, 8, 0, 0, 4, 0,
            };

            var solution = new Program().Solve(arr);

            Console.WriteLine(string.Join("\n", Enumerable.Range(0, 9).Select(
                row => string.Join(",", solution.Skip(9*row).Take(9))
            )));
        }

        public int[] Solve(int[] grid)
        {
            var nextCell = GetNextTestCell(grid);
            return nextCell == 81 ? grid : SolveForCell(grid, nextCell);
        }

        private int[] SolveForCell(int[] grid, int cell)
        {
            return Enumerable.Range(1, 9)
                .Select(i => UpdateElement(grid, cell, i))
                .Where(IsValid)
                .Select(Solve)
                .FirstOrDefault(solution => solution != null);
        }

        private int[] UpdateElement(int[] grid, int cell, int value)
        {
            return Enumerable.Range(0, 81).Select(i => i == cell ? value : grid[i]).ToArray();
        }

        public int GetNextTestCell(int[] grid)
        {
            return Enumerable.Range(0, 82).TakeWhile(i => i < 81 && grid[i] != 0).DefaultIfEmpty(-1).Last() + 1;
        }

        public bool IsValid(int[] grid)
        {
            return AreColumnsValid(grid) && AreRowsValid(grid) && AreBlocksValid(grid);
        }

        public bool AreBlocksValid(int[] grid)
        {
            return Enumerable.Range(0, 3).All(rb => Enumerable.Range(0, 3).All(cb => IsBlockValid(grid, rb, cb)));
        }

        public bool IsBlockValid(int[] grid, int rb, int cb)
        {
            return IsSequenceValid(GetBlock(grid, rb, cb).ToArray());
        }

        public int[] GetBlock(int[] grid, int rb, int cb)
        {
            return Enumerable.Range(0, 3).Select(column =>
                    Enumerable.Range(0, 3).Select(row => grid[(rb*3*9) + row + (cb*3) + column*9]))
                        .SelectMany(item => item)
                        .ToArray();
        }


        public bool AreRowsValid(int[] grid)
        {
            return Enumerable.Range(0, 9).All(row => IsRowValid(grid, row));
        }

        public bool AreColumnsValid(int[] grid)
        {
            return Enumerable.Range(0, 9).All(column => IsColumnValid(grid, column));
        }

        public bool IsColumnValid(int[] grid, int column)
        {
            return IsSequenceValid(GetColumn(grid, column));
        }

        public bool IsRowValid(IEnumerable<int> grid, int row)
        {
            return IsSequenceValid(GetRow(grid, row));
        }

        public int[] GetRow(IEnumerable<int> grid, int row)
        {
            return grid.Skip(row*9).Take(9).ToArray();
        }

        public int[] GetColumn(int[] grid, int column)
        {
            return Enumerable.Range(0, 9).Select(r => grid[column + r*9]).ToArray();
        }

        public bool IsSequenceValid(IEnumerable<int> row)
        {
            return row.Where(c => c != 0).GroupBy(c => c).All(g => g.Count() == 1);
        }
    }
}
