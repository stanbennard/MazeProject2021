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
            timer1.Enabled = true;            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bitmap b = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(b);
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    if(grid[x, y])
                        g.FillRectangle(Brushes.White, x*GRID_SIZE, y*GRID_SIZE, GRID_SIZE, GRID_SIZE);
                    else
                        g.FillRectangle(Brushes.Black, x*GRID_SIZE, y*GRID_SIZE, GRID_SIZE, GRID_SIZE);
                    if(x == goal[0] && y == goal[1])
                        g.FillRectangle(Brushes.Red, x * GRID_SIZE, y * GRID_SIZE, GRID_SIZE, GRID_SIZE);
                    if (x == player[0] && y == player[1])
                        g.FillRectangle(Brushes.Green, x * GRID_SIZE, y * GRID_SIZE, GRID_SIZE, GRID_SIZE);
                }
            }
            pictureBox1.Image = b;
        }
    }
}