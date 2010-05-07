using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace awesomebot

{

    public partial class awesomebotform : Form
    {
        public class Point
        {
            public Point(int x, int y)
            { this.x = x; this.y = y; }
            public bool Equals(object other)
            {
                return ((Point)other).x == this.x && ((Point)other).y == this.y;
            }
            public string ToString()
            {
                return "x: " + x + " y: " + y;
            }
            public int x, y;
        }
        int robotX = 0;
        int robotY = 0;
        int maxTries = 5;
        double theta = 0;
        const int forwardLeft = 188;
        const int forwardRight = 188;
        const int right45 = -28;
        const int left45 = 28;
        const int right90 = -55;
        const int left90 = 55;
        bool goForward = false;

        public int getCell(int i, int j)
        {
            if (i >= mapGrid.Rows.Count || i < 0)
                return 9999;
            else if (j >= mapGrid.Rows[i].Cells.Count || j < 0)
                return 9999;
            return Int32.Parse(mapGrid.Rows[i].Cells[j].Value.ToString());
        }
        public int getCell(Point p)
        {
            return getCell(p.y, p.x);
        }
        public void setCell(Point p, int value)
        {
            mapGrid.Rows[p.y].Cells[p.x].Value = ("" + value);
        }
        public void updateRobotGui(int newX, int newY)
        {
            mapGrid.Rows[robotY].Cells[robotX].Style.BackColor = Color.White;
            mapGrid.Rows[newY].Cells[newX].Style.BackColor = Color.Red;
            robotY = newY;
            robotX = newX;
        }

        public void moveRobot(Point p)
        {
            updateRobotGui(p.x, p.y);
        }

        private void resetGrid()
        {
            string[] init = new string[] { "0", "0", "0", "0", "0", "0", "0", "0" };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //init[j] = "" + (i * 8 + j + 1);
                    int val = getCell(i, j);
                    if (val >= 0)
                    {
                        init[j] = "0";
                    }
                    else
                    {
                        init[j] = "" + val;
                    }
                }
                mapGrid.Rows.Add(init);
            }
        }

        public awesomebotform()
        {
            InitializeComponent();

            resetGrid();
            mapGrid.Rows[robotY].Cells[robotX].Style.BackColor = Color.Red;


        }

        private Stack<Point> getPath(Point start, Point goal)
        {
            Stack<Point> path = new Stack<Point>();
            wavefront(start, goal, 1);
            Point current = new Point(goal.x, goal.y);
            while (!current.Equals(start))
            {
                path.Push(current);
                int val = getCell(current);
                
                Point left = new Point(current.x - 1, current.y);
                Point right = new Point(current.x + 1, current.y);
                Point up = new Point(current.x, current.y + 1);
                Point down = new Point(current.x, current.y - 1);
                Point leftup = new Point(current.x-1, current.y + 1);
                Point rightup = new Point(current.x+1, current.y + 1);
                Point leftdown = new Point(current.x-1, current.y - 1);
                Point rightdown = new Point(current.x+1, current.y - 1);

                int leftVal = getCell(left);
                int rightVal = getCell(right);
                int upVal = getCell(up);
                int downVal = getCell(down);
                int leftUpVal = getCell(leftup);
                int rightUpVal = getCell(rightup);
                int leftDownVal = getCell(leftdown);
                int rightDownVal = getCell(rightdown);
                int[] adjVals = new int[] { leftVal, leftDownVal, downVal, leftUpVal, rightUpVal, rightVal, rightDownVal, upVal };
                Point[] adjPoints = new Point[] { left, leftdown, down, leftup, rightup, right, rightdown, up };

                for (int i = 0; i < 8; i++)
                {
                    if (adjVals[i] < val)
                    {
                        current = adjPoints[i];
                        Console.WriteLine(adjPoints[i].ToString());
                        break;
                    }
                }
            }
            return path;
        }
           

        private void wavefront(Point start, Point goal, int val)
        {
            if (start.x < 0 || start.x > 7 || start.y < 0 || start.y > 3)
            {
                return;
            }
            int cell = getCell(start);
            
            if (cell < 0 || (cell > 0 && cell < val))
            {
                return;
            }
            
            setCell(start, val);
            //Console.Out.WriteLine("x: " + start.x + " y: " + start.y + " val: " + getCell(start));
            if (start.Equals(goal))
            {
                return;
            }
            wavefront(new Point(start.x - 1, start.y), goal, val + 1);
            wavefront(new Point(start.x + 1, start.y), goal, val + 1);
            wavefront(new Point(start.x, start.y - 1), goal, val + 1);
            wavefront(new Point(start.x, start.y + 1), goal, val + 1);
            wavefront(new Point(start.x - 1, start.y-1), goal, val + 1);
            wavefront(new Point(start.x + 1, start.y-1), goal, val + 1);
            wavefront(new Point(start.x-1, start.y - 1), goal, val + 1);
            wavefront(new Point(start.x+1, start.y + 1), goal, val + 1);
        }


        private void mapGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void mapGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //rows[e.RowIndex][e.ColumnIndex] = sender.ToString();
            Console.Out.WriteLine(getCell(e.RowIndex,e.ColumnIndex));
        }

        private void mapGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            Random r = new Random();
            updateRobotGui(r.Next(8), r.Next(4));
        }

        private string sendCommand(string cmd)
        {
            string robotData = String.Empty;
            bool success = false;
            int tries = 0;
            while (!success && (tries < maxTries))
            {
                try
                {
                    serialPort1.Write(cmd);
                    robotData = serialPort1.ReadTo("\r");
                    if (robotData.Length > 0)
                        success = true;
                }
                catch (Exception ex)
                {

                }
                tries++;
            }
            return robotData;
        }

        public void pidMove(int leftTicks, int rightTicks)
        {
            sendCommand("MP" + leftTicks + "," + rightTicks + "\r");
            System.Threading.Thread.Sleep(1000);
            while (true)
            {
                System.Threading.Thread.Sleep(250);
                string ret = sendCommand("D\r");
                if (ret.Equals("da"))
                    break;
                
            }
        }

        public void cellForward()
        {
            pidMove(forwardLeft, forwardRight);
            int dx = (int)(Math.Cos(theta));
            int dy = (int)Math.Sin(theta);
            updateRobotGui(robotX + dx, robotY + dy);
        }

        public void cellBackwards()
        {
            pidMove(-forwardLeft, -forwardRight);
            int dx = (int)Math.Cos(theta + Math.PI);
            int dy = (int)Math.Sin(theta + Math.PI);
            updateRobotGui(robotX + dx, robotY + dy);
        }

        public void turnRight45()
        {
            pidMove(left45, right45);
            theta += Math.PI / 4;
            theta %= Math.PI * 2;
        }

        public void turnLeft45()
        {
            pidMove(-left45, -right45);
            theta -= Math.PI / 4;
            theta %= Math.PI * 2;
        }

        public void turnRight90()
        {
            pidMove(left90, right90);
            theta += Math.PI / 2;
            theta %= Math.PI * 2;
        }

        public void turnLeft90()
        {
            pidMove(-left90, -right90);
            theta -= Math.PI / 2;
            theta %= Math.PI * 2;
        }

        public void diagonalForwards()
        {
            pidMove(250, 250);
            pidMove(14, 14);
            int dx = (int)Math.Cos(theta);
            int dy = (int)Math.Sin(theta);
            updateRobotGui(robotX + dx, robotY + dy);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Determine move
            // Wait for move to complete
            // Read tag
            // update gui
            // loop
            if (goForward)
            {
                cellForward();
                goForward = false;
            }
        }

        private void forward_Click(object sender, EventArgs e)
        {
            cellForward();
        }

        private void backwards_Click(object sender, EventArgs e)
        {
            cellBackwards();
        }

        private void left_Click(object sender, EventArgs e)
        {
            turnLeft90();
        }

        private void right_Click(object sender, EventArgs e)
        {
            turnRight90();
        }

        private void openSerialPort_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
                serialPort1.Open();
        }

        // only moves one cell at a time
        private void move(Point newLoc)
        {
            int dx = newLoc.x - robotX;
            int dy = newLoc.y - robotY;
            assert(Math.Abs(dx) <= 1);
            assert(Math.Abs(dy) <= 1);
            double dt = Math.Atan2(dy, dx);

            // TODO: finish
            if (dt 

        }

        private void wave_Click(object sender, EventArgs e)
        {
            Stack<Point> path = getPath(new Point(0, 0), new Point(7, 3));
            Console.Out.WriteLine("Path length: " + path.Count);
            while(path.Count > 0)
            {
                Point p = path.Pop();
                
                Console.Out.WriteLine(p.ToString());
            }
        }

    }
}
