using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeProject
{
    public partial class CustomMaze : Form
    {
        int mazeWidth;
        public int getMazeWidth()
        {
            return mazeWidth;
        }
        int mazeHeight;
        public int getMazeHeight()
        {
            return mazeHeight;
        }
        public string getSeed()
        {
            return textBox2.Text;
        }
        public CustomMaze()
        {
            InitializeComponent();
            mazeWidth = Convert.ToInt32(numericUpDown1.Value);
            mazeHeight = Convert.ToInt32(numericUpDown2.Value);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mazeWidth = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            mazeHeight = Convert.ToInt32(numericUpDown2.Value);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            Form1 form1 = new Form1(this);
            form1.ShowDialog();
            form1 = null;
        }
    }
}
