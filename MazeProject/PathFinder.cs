using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MazeProject
{
    internal class PathFinder
    {
        private int gRID_WIDTH;
        private int gRID_HEIGHT;
        private bool[,] grid;
        private bool[,] wasHere;
        private bool[,] correctPath;
        private List<int[]> correctPathList;

        public List<int[]> getCorrectPathList()
        {
            return correctPathList;
        }

        private bool[,] maze;
        //bool[,] wasHere;
        //bool[,] correctPath; // The solution to the maze
        //int startX, startY; // Starting X and Y values of maze
        //int endX, endY;     // Ending X and Y values of maze

        public PathFinder(int gRID_WIDTH, int gRID_HEIGHT, bool[,] grid)
        {
            this.gRID_WIDTH = gRID_WIDTH;
            this.gRID_HEIGHT = gRID_HEIGHT;
            this.grid = grid;

            maze = new bool[gRID_WIDTH, this.gRID_HEIGHT]; // The maze
        }

        public bool pathExists(int[] startPoint, int[] endPoint)
        {
            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(new int[] { startPoint[0], startPoint[1] });
            bool[,] gridPath = duplicateGrid();
                
                
            int[] currentNode;
            while (q.Count != 0) //  while q has elements
            {
                currentNode = q.Dequeue();
                if (gridPath[currentNode[0], currentNode[1]]) // is it a corridor?
                {
                    gridPath[currentNode[0], currentNode[1]] = false; // make it a wall
                    if (currentNode[0] == endPoint[0] && currentNode[1] == endPoint[1]) // are we at the end?
                        return true; // then we can reach the end!
                    else
                    {
                        // add adjacent nodes to the queue 
                        if (currentNode[0] > 0)
                            q.Enqueue(new int[] { currentNode[0] - 1, currentNode[1] });
                        if (currentNode[1] > 0)
                            q.Enqueue(new int[] { currentNode[0], currentNode[1] - 1 });
                        if (currentNode[0] < gRID_WIDTH - 1)
                            q.Enqueue(new int[] { currentNode[0] + 1, currentNode[1] });
                        if (currentNode[1] < gRID_HEIGHT - 1)
                            q.Enqueue(new int[] { currentNode[0], currentNode[1] + 1 });
                    }
                }
            }
            return false;
        }

        // copy the maze grid for general shenanegans
        private bool[,] duplicateGrid()
        {
            bool[,] gridPath = new bool[gRID_WIDTH, gRID_HEIGHT];
            for (int x = 0; x < gridPath.GetLength(0); x++)
            {
                for (int y = 0; y < gridPath.GetLength(1); y++)
                {
                    gridPath[x, y] = grid[x, y];
                }
            }
            return gridPath;
        }

        public enum MazeDirection
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
            NONE
        }

        public MazeDirection getNextMove(int[] startPoint, int[] endPoint)
        {
            if (!solveMaze(startPoint, endPoint))
                return MazeDirection.NONE;

            //Gets value from correctPath

            // move right?
            if (correctPath[startPoint[0] + 1, startPoint[1]])
                return MazeDirection.RIGHT;

            if (startPoint[0] > 0 && correctPath[startPoint[0] - 1, startPoint[1]])
                return MazeDirection.LEFT;

            if (correctPath[startPoint[0], startPoint[1] - 1])
                return MazeDirection.UP;

            if (correctPath[startPoint[0], startPoint[1] + 1])
                return MazeDirection.DOWN;

            else
                return MazeDirection.NONE;
        }

        /*
         * adapted from Wikipedia: https://en.wikipedia.org/wiki/Maze-solving_algorithm
         */
        public bool solveMaze(int[] startPoint, int[] endPoint)
        {
            maze = duplicateGrid(); // Create Maze (false = path, true = wall)

            wasHere = new bool[gRID_WIDTH, this.gRID_HEIGHT];
            correctPath = new bool[gRID_WIDTH, gRID_HEIGHT];
            correctPathList = new List<int[]>();
            

            for (int x = 0; x < maze.GetLength(0); x++)
                // Sets boolean Arrays to default values
                for (int y = 0; y < maze.GetLength(1); y++)
                {
                    wasHere[x, y] = false;
                    correctPath[x, y] = false;
                }

            bool b = recursiveSolve(startPoint, endPoint);
            // Will leave you with a boolean array (correctPath) 
            // with the path indicated by true values.
            // If b is false, there is no solution to the maze

            return b;
        }

        public bool recursiveSolve(int[] currentPoint, int[] endPoint)
        {
            int x = currentPoint[0];
            int y = currentPoint[1];

            if (x == endPoint[0] && y == endPoint[1])
            {
                correctPath[x, y] = true;
                correctPathList.Add(currentPoint);
                return true; // If you reached the end
            }
                

            // If you are on a wall or already were here
            if (!maze[x,y] || wasHere[x, y]) 
                return false;

            wasHere[x, y] = true;

            if (x != 0) // Checks if not on left edge
                if (recursiveSolve(new int[] {x - 1, y}, endPoint))
                { // Recalls method one to the left
                    correctPath[x, y] = true; // Sets that path value to true;
                    correctPathList.Add(currentPoint);
                    return true;
                }
            if (x != this.gRID_WIDTH - 1) // Checks if not on right edge
                if (recursiveSolve(new int[] {x + 1, y }, endPoint))
                { // Recalls method one to the right
                    correctPath[x, y] = true;
                    correctPathList.Add(currentPoint);
                    return true;
                }
            if (y != 0)  // Checks if not on top edge
                if (recursiveSolve(new int[] {x, y - 1 }, endPoint))
                { // Recalls method one up
                    correctPath[x, y] = true;
                    correctPathList.Add(currentPoint);
                    return true;
                }
            if (y != this.gRID_HEIGHT - 1) // Checks if not on bottom edge
                if (recursiveSolve(new int[] {x, y + 1 }, endPoint))
                { // Recalls method one down
                    correctPath[x, y] = true;
                    correctPathList.Add(currentPoint);
                    return true;
                }
            return false;
        }
        /*public int[] nextStep(int[] startPoint, int[] endPoint)
        {
            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(new int[] { startPoint[0], startPoint[1] });
            bool[,] gridPath = new bool[gRID_WIDTH, gRID_HEIGHT];
            for (int x = 0; x < gridPath.GetLength(0); x++)
            {
                for (int y = 0; y < gridPath.GetLength(1); y++)
                {
                    gridPath[x, y] = grid[x, y];
                }
            }
            int[] currentNode;
            while (q.Count != 0) //  while q has elements
            {
                currentNode = q.Dequeue();
                if (gridPath[currentNode[0], currentNode[1]]) // is it a corridor?
                {
                    gridPath[currentNode[0], currentNode[1]] = false; // make it a wall
                    if (currentNode[0] == endPoint[0] && currentNode[1] == endPoint[1]) // are we at the end?
                        return true; // then we can reach the end!
                    else
                    {
                        // add adjacent nodes to the queue 
                        if (currentNode[0] > 0)
                            q.Enqueue(new int[] { currentNode[0] - 1, currentNode[1] });
                        if (currentNode[1] > 0)
                            q.Enqueue(new int[] { currentNode[0], currentNode[1] - 1 });
                        if (currentNode[0] < gRID_WIDTH - 1)
                            q.Enqueue(new int[] { currentNode[0] + 1, currentNode[1] });
                        if (currentNode[1] < gRID_HEIGHT - 1)
                            q.Enqueue(new int[] { currentNode[0], currentNode[1] + 1 });
                    }
                }
            }
            return false;
        }*/
    }
}
