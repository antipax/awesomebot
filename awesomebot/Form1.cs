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
            public bool Equals1(object other)
            {
                return ((Point)other).x == this.x && ((Point)other).y == this.y;
            }
            
            override public string ToString()
            {
                return "x: " + x + " y: " + y;
            }
            public int x, y;
        }
        int robotX = 0;
        int robotY = 0;
        int maxTries = 5;
        int orientation = 2;
        const int forwardLeft = 188;
        const int forwardRight = 188;
        const int right45 = -28;
        const int left45 = 28;
        const int right90 = -55;
        const int left90 = 55;
        
        Stack<Point> path = null;

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
            while (!current.Equals1(start))
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
                    if (adjVals[i] >= 0 && adjVals[i] < val)
                    {
                        current = adjPoints[i];
                       
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
            if (start.Equals1(goal))
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
        }

        public void cellBackwards()
        {
            pidMove(-forwardLeft, -forwardRight);
        }

        public void turnRight45()
        {
            pidMove(left45, right45);
            Console.Write("Orientation: " + orientation);
            orientation = (orientation + 1) % 8;
            Console.WriteLine(" to " + orientation);

        }

        public void turnLeft45()
        {
            pidMove(-left45, -right45);
            orientation = (orientation - 1) % 8;
        }

        public void turnRight90()
        {
            pidMove(left90, right90);
            orientation = (orientation + 2) % 8;
        }

        public void turnLeft90()
        {
            pidMove(-left90, -right90);
            orientation = (orientation - 2) % 8;
        }

        public void diagonalForward()
        {
            pidMove(250, 250);
            pidMove(14, 14);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Determine move
            // Wait for move to complete
            // Read tag
            // update gui
            // loop
            timer1.Enabled = false;
            if (path != null && path.Count > 0)
            {
                Point p = path.Pop();
                move(p);
                String tag = sendCommand("T\r");
                if (!tag.StartsWith("tn"))
                {
                    textBox.Text += "Tag read: " + tag + "\n";
                }
            }
            timer1.Enabled = true;
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

        private int toOrientation(int dx, int dy)
        {
            if (dx == 0 && dy == 1)
            {
                return 4;
            }
            if (dx == 1 && dy == 1)
            {
                return 3;
            }
            if (dx == 1 && dy == 0)
            {
                return 2;
            }
            if (dx == 1 && dy == -1)
            {
                return 1;
            }
            if (dx == 0 && dy == -1)
            {
                return 0;
            }
            if (dx == -1 && dy == -1)
            {
                return 7;
            }
            if (dx == -1 && dy == 0)
            {
                return 6;
            }
            if (dx == -1 && dy == 1)
            {
                return 5;
            }
            return -1;
  
        }

        // only moves one cell at a time
        private void move(Point newLoc)
        {
            Console.WriteLine("newLoc: " + newLoc.ToString());
            int dx = newLoc.x - robotX;
            int dy = newLoc.y - robotY;
            
            int newOrientation = toOrientation(dx, dy);
            Console.WriteLine("newOrientation: " + newOrientation);
            int dt = (newOrientation - orientation) % 8;
            if (dt < 0) { dt += 8; }

            if (dt > 4)
            {
                //turning left
                if (dt % 2 != 0)
                {
                    Console.WriteLine("Turning left 45 deg. Current orientation: " + orientation);
                    turnLeft45();
                }
                while (orientation != newOrientation)
                {
                    Console.WriteLine("Turning left 90 deg. Current orientation: " + orientation);
                    turnLeft90();
                }
            }
            else
            {
                //turning right
                if (dt % 2 != 0)
                {
                    Console.WriteLine("Turning right 45 deg. Current orientation: " + orientation);
                    turnRight45();
                }
                while (orientation != newOrientation)
                {
                    Console.WriteLine("Turning right 90 deg. Current orientation: " + orientation);
                    turnRight90();

                }
            }

            if (Math.Abs(dx) + Math.Abs(dy) == 2)
            {
                diagonalForward();
            }
            else
            {
                cellForward();
            }
            updateRobotGui(newLoc.x, newLoc.y);
        }

        private Point findCell(int val)
        {
            for (int i = 0; i < mapGrid.Rows.Count; i++)
            {
                for (int j = 0; j < mapGrid.Rows[i].Cells.Count; j++)
                {
                    if (getCell(i, j) == val)
                    {
                        return new Point(j, i);
                    }
                }
            }
            return null;
        }

        private void wave_Click(object sender, EventArgs e)
        {
            Point goal = findCell(100);
            mapGrid.Rows[goal.y].Cells[goal.x].Style.BackColor = Color.Green;
            path = getPath(new Point(robotX, robotY), goal);
            
            Console.Out.WriteLine("Path length: " + path.Count);
           
        }

    }
}
