using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MazeProject
{ 
    public partial class Form1 : Form
    {
        int GRID_WIDTH = -1;
        int GRID_HEIGHT = -1;
        const int GRID_SIZE = 5;
        private readonly CustomMaze customMaze;
        private bool[,] grid;
        private int[] player = { 0, 0 };
        private int[] goal;
        private Random RNG = new Random();
        string seed;
        bool[] keyPressed = {false, false, false, false};

        public Form1(CustomMaze customMaze)
		{
            string inputSeed = customMaze.getSeed();
            if (inputSeed == "") {
                //Debug.Print("big oof");
                GRID_WIDTH = customMaze.getMazeWidth();
                GRID_HEIGHT = customMaze.getMazeHeight();
                grid = new bool[GRID_WIDTH, GRID_HEIGHT];
                Application.EnableVisualStyles();
                InitializeComponent();
                goal = new int[] { RNG.Next(GRID_WIDTH / 2, GRID_WIDTH - 1), RNG.Next(GRID_HEIGHT / 2, GRID_HEIGHT - 1) }; //This is the coordinates of the red goal
                Debug.Print(Convert.ToString(goal[0]));
                Debug.Print(Convert.ToString(goal[1]));
                seed = (goal[0] + "," + goal[1] + ";");
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    for (int y = 0; y < grid.GetLength(1); y++)
                    {
                        if (x == goal[0] && y == goal[1])
                            grid[x, y] = true;
                        else if (x == player[0] && y == player[1])
                            grid[x, y] = true;
                        else
                            grid[x, y] = false;
                    }
                }
                //grid[10, 10] = true; this removes the wall
                List<int> seedList = new List<int>();
                List<int> seedList2 = new List<int>();
                while (pathFind(player, goal) == false)
                {
                    int firstRdm = RNG.Next(0, GRID_WIDTH);
                    int secondRdm = RNG.Next(0, GRID_WIDTH);
                    //grid[RNG.Next(0, GRID_WIDTH), RNG.Next(0, GRID_HEIGHT)] = true;
                    grid[firstRdm, secondRdm] = true;
                    //Debug.Print(Convert.ToString(firstRdm));
                    seedList.Add(firstRdm);
                    //Debug.Print(Convert.ToString(secondRdm));
                    seedList2.Add(secondRdm);
                }

                Int32 length = seedList.Count;
                for (var i = 0; i < length; i++)
                {
                    seed = seed + Convert.ToString(seedList[i]);
                    seed = seed + ",";
                    seed = seed + Convert.ToString(seedList2[i]);
                    seed = seed + ";";
                }
                Debug.Print(seed);
                textBox1.Text = seed;
                timer1.Enabled = true;
                this.customMaze = customMaze;
            }
            else
            {

            }
            //Debug.Print(inputSeed);

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            //W
            if (keyPressed[0] && player[1] > 0)
                if (grid[player[0], player[1]-1] == true) //stops player from hitting wall
                    player[1] -= 1;
            //A
            if (keyPressed[1] && player[0] > 0)
                if (grid[player[0] - 1, player[1]] == true) //stops player from hitting wall
                    player[0] -= 1;
            //S
            if (keyPressed[2] && player[1] < GRID_HEIGHT - 1)
                if (grid[player[0], player[1]+1] == true) //stops player from hitting wall
                    player[1] += 1;
            //D
            if (keyPressed[3] && player[0] < GRID_WIDTH - 1)
                if(grid[player[0]+1,player[1]] == true) //stops player from hitting wall
                    player[0] += 1;

            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(b);
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if(grid[x, y]) // If true
                        g.FillRectangle(Brushes.White, x*GRID_SIZE, y*GRID_SIZE, GRID_SIZE, GRID_SIZE);
                    else //If false
                        g.FillRectangle(Brushes.Black, x*GRID_SIZE, y*GRID_SIZE, GRID_SIZE, GRID_SIZE);
                    if(x == goal[0] && y == goal[1])
                        g.FillRectangle(Brushes.Red, x * GRID_SIZE, y * GRID_SIZE, GRID_SIZE, GRID_SIZE);
                    if (x == player[0] && y == player[1])
                        g.FillRectangle(Brushes.Green, x * GRID_SIZE, y * GRID_SIZE, GRID_SIZE, GRID_SIZE);
                }
            }
            pictureBox1.Image = b;
        }

        private bool pathFind(int[] startPoint, int[] endPoint)
        {
            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(new int[] { startPoint[0], startPoint[1] });
            bool[,] gridPath = new bool[GRID_WIDTH, GRID_HEIGHT];
            for (int x = 0; x < gridPath.GetLength(0); x++)
            {
                for (int y = 0; y < gridPath.GetLength(1); y++)
                {
                    gridPath[x, y] = grid[x, y];
                }
            }
            int[] currentNode;
            while(q.Count != 0)
            {
                currentNode = q.Dequeue();
                if (gridPath[currentNode[0], currentNode[1]])
                {
                    gridPath[currentNode[0], currentNode[1]] = false;
                    if (currentNode[0] == endPoint[0] && currentNode[1] == endPoint[1])
                        return true;
                    else
                    {
                        if (currentNode[0] > 0)
                            q.Enqueue(new int[] {currentNode[0]-1, currentNode[1]});
                        if (currentNode[1] > 0)
                            q.Enqueue(new int[] {currentNode[0], currentNode[1]-1});
                        if (currentNode[0] < GRID_WIDTH - 1)
                            q.Enqueue(new int[] { currentNode[0] + 1, currentNode[1] });
                        if (currentNode[1] < GRID_HEIGHT - 1)
                            q.Enqueue(new int[] { currentNode[0], currentNode[1] + 1 });
                    }
                }
            }
            return false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                //Debug.Print("w");
                keyPressed[0] = true;
            }
            if (e.KeyCode == Keys.A)
            {
                //Debug.Print("s");
                keyPressed[1] = true;
            }
            if (e.KeyCode == Keys.S)
            {
                //Debug.Print("a");
                keyPressed[2] = true;
            }
            if (e.KeyCode == Keys.D)
            {
                //Debug.Print("d");
                keyPressed[3] = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                keyPressed[0] = false;
            }
            if (e.KeyCode == Keys.A)
            {
                keyPressed[1] = false;
            }
            if (e.KeyCode == Keys.S)
            {
                keyPressed[2] = false;
            }
            if (e.KeyCode == Keys.D)
            {
                keyPressed[3] = false;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(seed);
        }
    }
}