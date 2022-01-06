﻿using System;
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
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            PathFinderTest.mine();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            //Form1 form1 = new Form1();
            //form1.ShowDialog();
            //form1 = null;
            CustomMaze customMaze = new CustomMaze();
            customMaze.ShowDialog();
            customMaze = null;
            Show();
        }
    }
}
