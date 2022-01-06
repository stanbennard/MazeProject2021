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
        private int[] ai = { 0, 0 };
        private int[] goal;
        static int seednum;
        //private Random RNG = new Random(seednum);
        string seed;
        bool[] keyPressed = { false, false, false, false };
        PathFinder pathFinder;

        public Form1(CustomMaze customMaze)
        {
            string inputSeed = customMaze.getSeed();
            if (inputSeed == "")
            {
                Debug.Print("big oof");
                Random RSEED = new Random();
                int rseednum = RSEED.Next();
                Random RNG = new Random(rseednum);
                GRID_WIDTH = customMaze.getMazeWidth();
                GRID_HEIGHT = customMaze.getMazeHeight();
                grid = new bool[GRID_WIDTH, GRID_HEIGHT];
                pathFinder = new PathFinder(GRID_WIDTH, GRID_HEIGHT, grid);
                Application.EnableVisualStyles();
                InitializeComponent();
                goal = new int[] { RNG.Next(GRID_WIDTH / 2, GRID_WIDTH - 1), RNG.Next(GRID_HEIGHT / 2, GRID_HEIGHT - 1) }; //This is the coordinates of the red goal
                Debug.Print(Convert.ToString(goal[0]));
                Debug.Print(Convert.ToString(goal[1]));
                seed = (goal[0] + "," + goal[1] + ",");
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
                        if (x == ai[0] && y == ai[1])
                            grid[x, y] = true;
                    }
                }
                //grid[10, 10] = true; this removes the wall
                while (!pathFinder.pathExists(player, goal))
                {
                    int firstRdm = RNG.Next(0, GRID_WIDTH);
                    int secondRdm = RNG.Next(0, GRID_WIDTH);
                    //grid[RNG.Next(0, GRID_WIDTH), RNG.Next(0, GRID_HEIGHT)] = true;
                    grid[firstRdm, secondRdm] = true;
                }
                textBox1.Text = rseednum.ToString();
                timer1.Enabled = true;
                this.customMaze = customMaze;
            }
            else
            {
                seednum = Convert.ToInt32(inputSeed);
                Random RNG = new Random(seednum);
                GRID_WIDTH = customMaze.getMazeWidth();
                GRID_HEIGHT = customMaze.getMazeHeight();
                grid = new bool[GRID_WIDTH, GRID_HEIGHT];
                Application.EnableVisualStyles();
                InitializeComponent();
                goal = new int[] { RNG.Next(GRID_WIDTH / 2, GRID_WIDTH - 1), RNG.Next(GRID_HEIGHT / 2, GRID_HEIGHT - 1) }; //This is the coordinates of the red goal
                Debug.Print(Convert.ToString(goal[0]));
                Debug.Print(Convert.ToString(goal[1]));
                seed = (goal[0] + "," + goal[1] + ",");
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
                        if (x == ai[0] && y == ai[1])
                            grid[x, y] = true;
                    }
                }
                //grid[10, 10] = true; this removes the wall
                while (pathFinder.pathExists(player, goal) == false)
                {
                    int firstRdm = RNG.Next(0, GRID_WIDTH);
                    int secondRdm = RNG.Next(0, GRID_WIDTH);
                    //grid[RNG.Next(0, GRID_WIDTH), RNG.Next(0, GRID_HEIGHT)] = true;
                    grid[firstRdm, secondRdm] = true;
                    //Debug.Print(Convert.ToString(firstRdm));
                    //Debug.Print(Convert.ToString(secondRdm));
                }
                textBox1.Text = inputSeed;
                timer1.Enabled = true;
                this.customMaze = customMaze;
                /*
                GRID_WIDTH = customMaze.getMazeWidth();
                GRID_HEIGHT = customMaze.getMazeHeight();
                Application.EnableVisualStyles();
                InitializeComponent();
                string[] coords = inputSeed.Split(",");
                int coord1 = Convert.ToInt32(coords[0]);
                int coord2 = Convert.ToInt32(coords[1]);
                Int32 length = coords.Count();
                List<int> seedList = new List<int>();
                List<int> seedList2 = new List<int>();

                for (var i = 0; i < length; i++)
                {
                    int intcoord = Convert.ToInt32(coords[i]);
                    if (intcoord == coord1)
                    {
                        goal = new int[] { coord1, coord2 }; //This is the coordinates of the red goal
                    }
                    else if (intcoord == coord2)
                    {
                    }
                    else
                    {
                        int current = Convert.ToInt32(coords[i]);
                        if (i % 2 == 1)
                        {
                            seedList2.Add(current);
                        }
                        else
                        {
                            seedList.Add(current);
                        }
                    }

                }
                Int32 amount = seedList.Count();
                grid = new bool[GRID_WIDTH, GRID_HEIGHT];
                for (var i = 0; i < amount; i++)
                {
                    grid[seedList[i], seedList2[i]] = true;
                }
            */
            }
                //timer1.Enabled = true;
                //Debug.Print(inputSeed);

            }


        private List<int[]> aiPath;
        bool aiWonYet = false;
        public void aiMove()
        {
            //randomMove();
            
            if(aiPath == null)
            {
                pathFinder.getNextMove(ai, goal);
                aiPath = pathFinder.getCorrectPathList();
                aiPath.RemoveAt(aiPath.Count - 1);
            }
            if (aiPath.Count != 0)
            { 
              ai = aiPath.ElementAt(aiPath.Count - 1);
              aiPath.RemoveAt(aiPath.Count - 1);
            }
            if (aiPath.Count == 0)
            {
                if (aiWonYet == false)
                {
                    aiWonYet=true;
                    MessageBox.Show("The AI has won! YOU LOSE"); 
                    this.Close();
                }
                 
            }
            /*
            switch (pathFinder.getNextMove(ai, goal))
            { 
                case PathFinder.MazeDirection.UP:
                    ai[1] -= 1;
                    break;
                case PathFinder.MazeDirection.DOWN:
                    ai[1] += 1;
                    break;
                case PathFinder.MazeDirection.RIGHT:
                    ai[0] += 1;
                    break;
                case PathFinder.MazeDirection.LEFT:
                    ai[0] -= 1;
                    break;

            }
            */
        }

            public void randomMove()
            {
            Random RMOVE = new Random();
            int rmovenum = RMOVE.Next(1,5);
            //Up
            if (rmovenum == 1 && ai[1] > 0)
                if (grid[ai[0], ai[1] - 1] == true) //stops player from hitting wall
                    ai[1] -= 1;
            //Left
            if (rmovenum == 2 && ai[0] > 0)
                if (grid[ai[0] - 1, ai[1]] == true) //stops player from hitting wall
                    ai[0] -= 1;
            //Down
            if (rmovenum == 3 && ai[1] < GRID_HEIGHT - 1)
                if (grid[ai[0], ai[1] + 1] == true) //stops player from hitting wall
                    ai[1] += 1;
            //Right
            if (rmovenum == 4 && ai[0] < GRID_WIDTH - 1)
                if (grid[ai[0] + 1, ai[1]] == true) //stops player from hitting wall
                    ai[0] += 1;
            }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool playerWonYet = false;
            //W
            if (keyPressed[0] && player[1] > 0)
                if (grid[player[0], player[1] - 1] == true) //stops player from hitting wall
                    {
                    player[1] -= 1;
                    aiMove(); 
                    }
            //A
            if (keyPressed[1] && player[0] > 0)
                if (grid[player[0] - 1, player[1]] == true) //stops player from hitting wall
                {
                    player[0] -= 1;
                    aiMove();
                }
            //S
            if (keyPressed[2] && player[1] < GRID_HEIGHT - 1)
                if (grid[player[0], player[1] + 1] == true) //stops player from hitting wall
                {
                    player[1] += 1;
                    aiMove();
                }
            //D
            if (keyPressed[3] && player[0] < GRID_WIDTH - 1)
                if (grid[player[0] + 1, player[1]] == true) //stops player from hitting wall
                {
                    player[0] += 1;
                    aiMove();
                }
            if (playerWonYet == false && player[0] == goal[0] && player[1] == goal[1])
            {
                playerWonYet=true;
                MessageBox.Show("You found the exit! YOU WON"); 
                this.Close();
            }
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(b);
            int scale = 2;
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if (grid[x, y]) // If true
                        g.FillRectangle(Brushes.White, x * GRID_SIZE * scale, y * GRID_SIZE * scale, GRID_SIZE * scale, GRID_SIZE * scale);
                    else //If false
                        g.FillRectangle(Brushes.Black, x * GRID_SIZE * scale, y * GRID_SIZE * scale, GRID_SIZE * scale, GRID_SIZE * scale);
                    if (x == goal[0] && y == goal[1])
                        g.FillRectangle(Brushes.Red, x * GRID_SIZE * scale, y * GRID_SIZE * scale, GRID_SIZE * scale, GRID_SIZE * scale);
                    if (x == player[0] && y == player[1])
                        g.FillRectangle(Brushes.Green, x * GRID_SIZE * scale, y * GRID_SIZE * scale, GRID_SIZE * scale, GRID_SIZE * scale);
                    if (x == ai[0] && y == ai[1])
                        g.FillRectangle(Brushes.Purple, x * GRID_SIZE * scale, y * GRID_SIZE * scale, GRID_SIZE * scale, GRID_SIZE * scale);

                }
            }
            pictureBox1.Image = b;
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
                Clipboard.SetText(textBox1.Text);
            }
        }
    }