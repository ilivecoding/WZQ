using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WZQ
{
    public partial class Form1 : Form
    {
        private const int BOARD_SIZE = 15;
        private const int CELL_SIZE = 30;
        private const int MARGIN = 30;
        private int[,] chessBoard = new int[BOARD_SIZE, BOARD_SIZE];
        private bool isBlack = true;
        public Form1()
        {
            InitializeComponent();
            this.Text = "WZQ";
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = MARGIN * 2 + BOARD_SIZE * CELL_SIZE;
            this.Height = MARGIN * 2 + BOARD_SIZE * CELL_SIZE;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.MouseClick += new MouseEventHandler(Form1_MouseClick);
            InitChessBoard();
        }

        private void InitChessBoard()
        {
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    chessBoard[i, j] = 0;
                }
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X - MARGIN;
            int y = e.Y - MARGIN;
            int i = x / CELL_SIZE;
            int j = y / CELL_SIZE;
            if (i < 0 || i >= BOARD_SIZE || j < 0 || j >= BOARD_SIZE)
            {
                return;
            }
            if (chessBoard[i, j] != 0)
            {
                return;
            }
            if (isBlack)
            {
                chessBoard[i, j] = 1;//黑棋
            }
            else
            {
                chessBoard[i, j] = 2;//白棋
            }
            isBlack = !isBlack;
            this.Invalidate();
            // TODO: 判断是否有人胜出
            var winner = CheckWin();
            if(winner == 1)
            {
                MessageBox.Show("black win");
            }
            if (winner == 2)
            {
                MessageBox.Show("white win");
            }
            if(winner > 0)
            {
                InitChessBoard();
                Refresh();
            }
        }

        private int CheckWin()
        {
            int color;
            // 检查横向
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE - 4; j++)
                {
                    color = chessBoard[i, j];
                    if (color > 0 && color == chessBoard[i, j + 1] && color == chessBoard[i, j + 2]
                        && color == chessBoard[i, j + 3] && color == chessBoard[i, j + 4])
                    {
                        return color;
                    }
                }
            }
            // 检查纵向
            for (int i = 0; i < BOARD_SIZE - 4; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    color = chessBoard[i, j];
                    if (color > 0 && color == chessBoard[i + 1, j] && color == chessBoard[i + 2, j]
                        && color == chessBoard[i + 3, j] && color == chessBoard[i + 4, j])
                    {
                        return color;
                    }
                }
            }
            // 检查右上斜向
            for (int i = 4; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE - 4; j++)
                {
                    color = chessBoard[i, j];
                    if (color > 0 && color == chessBoard[i - 1, j + 1] && color == chessBoard[i - 2, j + 2]
                        && color == chessBoard[i - 3, j + 3] && color == chessBoard[i - 4, j + 4])
                    {
                        return color;
                    }
                }
            }
            // 检查右下斜向
            for (int i = 0; i < BOARD_SIZE - 4; i++)
            {
                for (int j = 0; j < BOARD_SIZE - 4; j++)
                {
                    color = chessBoard[i, j];
                    if (color > 0 && color == chessBoard[i + 1, j + 1] && color == chessBoard[i + 2, j + 2]
                        && color == chessBoard[i + 3, j + 3] && color == chessBoard[i + 4, j + 4])
                    {
                        return color;
                    }
                }
            }
            return 0;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                g.DrawLine(Pens.Silver, MARGIN, MARGIN + i * CELL_SIZE, MARGIN + (BOARD_SIZE - 1) * CELL_SIZE, MARGIN + i * CELL_SIZE);
                g.DrawLine(Pens.Silver, MARGIN + i * CELL_SIZE, MARGIN, MARGIN + i * CELL_SIZE, MARGIN + (BOARD_SIZE - 1) * CELL_SIZE);
            }
            for (int i = 0; i < BOARD_SIZE; i++)
            {
                for (int j = 0; j < BOARD_SIZE; j++)
                {
                    if (chessBoard[i, j] == 1)
                    {
                        g.FillEllipse(Brushes.Black, MARGIN + i * CELL_SIZE - CELL_SIZE / 2, MARGIN + j * CELL_SIZE - CELL_SIZE / 2, CELL_SIZE, CELL_SIZE);
                    }
                    else if (chessBoard[i, j] == 2)
                    {
                        g.FillEllipse(Brushes.White, MARGIN + i * CELL_SIZE - CELL_SIZE / 2, MARGIN + j * CELL_SIZE - CELL_SIZE / 2, CELL_SIZE, CELL_SIZE);
                    }
                }
            }
        }
    }
}
