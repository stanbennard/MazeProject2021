using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject
{
    internal class PathFinderTest
    {

        public static void mine()
        {
            test1();
            test2();
            test3();
            test4();
        }

        private static void test1()
        {
            int[] startPoint = {0,0};
            int[] endPoint = {2,0};

            // bool[,] grid = new bool[3,3];

            bool[,] grid = 
            {
                {true, false, false },
                {true, false, false },
                {true, false, false }
            };

            PathFinder pf  = new PathFinder(3, 3, grid);
            pf.solveMaze(startPoint,endPoint);

            Debug.Print("Finished");
        }
        private static void test2()
        {
            int[] startPoint = {0,0};
            int[] endPoint = {2,1};

            // bool[,] grid = new bool[3,3];

            bool[,] grid = convertToInternalGrid(new bool[,] 
                {
                    {true, true, true },
                    {true, true, true },
                    {false, false, false }
                }
            );

            PathFinder pf  = new PathFinder(3, 3, grid);
            bool solved = pf.solveMaze(startPoint,endPoint);

            Debug.Print("Finished: " + solved);
        }

        private static void test3()
        {
            int[] startPoint = {1,0};
            int[] endPoint = {2,1};

            bool[,] grid = convertToInternalGrid(new bool[,] 
                {
                    {true, true, false },
                    {true, true, true },
                    {false, false, false }
                }
            );

            PathFinder pf  = new PathFinder(3, 3, grid);
            bool solved = pf.solveMaze(startPoint,endPoint);

            Debug.Print("Finished: " + solved);
        }

        private static void test4()
        {
            int[] startPoint = {0,0};
            int[] endPoint = {2,1};

            bool[,] grid = convertToInternalGrid(new bool[,] 
                {
                    {true, true, false },
                    {true, true, true },
                    {false, false, false }
                }
            );

            PathFinder pf  = new PathFinder(3, 3, grid);
            bool solved = pf.solveMaze(startPoint,endPoint);

            Debug.Print("Finished: " + solved);
        }

        /*
         * utility method to swap x and y so we define the test cases in an easy-to-read way
         */
        private static bool[,] convertToInternalGrid(bool[,] humanGrid)
        {
            bool[,] grid = new bool[humanGrid.GetLength(1), humanGrid.GetLength(0)];

            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    grid[x, y] = humanGrid[y,x];
                }
            }

            return grid;
        }
    }



}
