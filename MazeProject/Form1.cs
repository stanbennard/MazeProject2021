using System.Diagnostics;

namespace MazeProject
{ 
    public partial class Form1 : Form
    {
        const int GRID_WIDTH = 80;
        const int GRID_HEIGHT = 80;
        const int GRID_SIZE = 5;
        private System.Windows.Forms.DataGridView myNewGrid;  // Declare a grid for this form
        //private List<BattleShipRow> battleShipGrid; // Declare this here so that you can use it later to manipulate the cell contents
        private bool[,] grid = new bool[GRID_WIDTH,GRID_HEIGHT];
        private int[] player = { 0, 0 };
        private int[] goal;
        private Random RNG = new Random();
        bool[] keyPressed = {false, false, false, false};

        public Form1()
		{
			InitializeComponent();
            /*myNewGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(myNewGrid)).BeginInit();
            this.SuspendLayout();
            myNewGrid.Parent = this;  // You have to set the parent manually so that the grid is displayed on the form
            myNewGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            myNewGrid.Location = new System.Drawing.Point(10, 10);  // You will need to calculate this postion based on your other controls.  
            myNewGrid.Name = "myNewGrid";
            myNewGrid.Size = new System.Drawing.Size(400, 400);  // You said you need the grid to be 12x12.  You can change the size here.
            myNewGrid.TabIndex = 0;
            myNewGrid.ColumnHeadersVisible = true; // You could turn this back on if you wanted, but this hides the headers that would say, "Cell1, Cell2...."
            myNewGrid.RowHeadersVisible = true;
            myNewGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            myNewGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            //myNewGrid.CellClick += MyNewGrid_CellClick;  // Set up an event handler for CellClick.  You handle this in the MyNewGrid_CellClick method, below
            ((System.ComponentModel.ISupportInitialize)(myNewGrid)).EndInit();
            this.ResumeLayout(false);
            myNewGrid.Visible = true;
            //LoadGridData();*/
            goal = new int[] { RNG.Next(GRID_WIDTH / 2, GRID_WIDTH - 1), RNG.Next(GRID_HEIGHT / 2, GRID_HEIGHT - 1) };
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for(int y = 0; y < grid.GetLength(1); y++)
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
            while (pathFind(player,goal) == false)
            {
                grid[RNG.Next(0, GRID_WIDTH), RNG.Next(0, GRID_HEIGHT)] = true;
            }
            timer1.Enabled = true;            
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
                Debug.Print("w");
                keyPressed[0] = true;
            }
            if (e.KeyCode == Keys.A)
            {
                Debug.Print("s");
                keyPressed[1] = true;
            }
            if (e.KeyCode == Keys.S)
            {
                Debug.Print("a");
                keyPressed[2] = true;
            }
            if (e.KeyCode == Keys.D)
            {
                Debug.Print("d");
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
    }
}