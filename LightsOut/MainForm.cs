﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Author: David Anderson
//Assignment: Lights Out!
namespace LightsOut
{
    public partial class MainForm : Form
    {
        private const int GridOffset = 25;
        private const int GridLength = 200;
        private LightsOutGame game;
        private const int CellLength = GridLength / 3;

        
        
 
        public MainForm()
        {
            InitializeComponent();
            game = new LightsOutGame();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGameButton_Click(sender, e);
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            for(int r = 0; r < game.GridSize; r++)
            {
                for(int c = 0; c < game.GridSize; c++)
                {
                    Brush brush;
                    Pen pen;

                    if (game.GetGridValue(r, c))
                    {
                        pen = Pens.Black;
                        brush = Brushes.White;
                    }
                    else
                    {
                        pen = Pens.White;
                        brush = Brushes.Black;
                    }

                    int x = c * CellLength + GridOffset;
                    int y = r * CellLength + GridOffset;

                    g.DrawRectangle(pen, x, y, CellLength, CellLength);
                    g.FillRectangle(brush, x + 1, y + 1, CellLength - 1, CellLength - 1);
                }
            }
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X < GridOffset || e.X > CellLength * game.GridSize + GridOffset || e.Y < GridOffset || e.Y > CellLength * game.GridSize + GridOffset)
                return;

            int r = (e.Y - GridOffset) / CellLength;
            int c = (e.X - GridOffset) / CellLength;

            game.Move(r, c);
            this.Invalidate();

            if (game.IsGameOver())
            {
                MessageBox.Show(this, "Congratulations! You've Won!", "Lights Out!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            game.NewGame();
            this.Invalidate();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm aboutBox = new AboutForm();
            aboutBox.ShowDialog(this);
        }
    }
}
