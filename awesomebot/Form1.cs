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

        public string getCell(int i, int j)
        {
            return mapGrid.Rows[i].Cells[j].Value.ToString();
        }
        public void moveRobot(int newX, int newY)
        {
            mapGrid.Rows[robotY].Cells[robotX].Style.BackColor = Color.White;
            mapGrid.Rows[newY].Cells[newX].Style.BackColor = Color.Red;
            robotY = newY;
            robotX = newX;
        }


        public awesomebotform()
        {
            InitializeComponent();
            string[] init = new string[] { "0", "0", "0", "0", "0", "0", "0", "0" };
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    init[j] = "" + (i * 8 + j + 1);
                }
                mapGrid.Rows.Add(init);
            }
            
            mapGrid.Rows[robotY].Cells[robotX].Style.BackColor = Color.Red;


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
            moveRobot(r.Next(8), r.Next(4));
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
            moveRobot(robotX + dx, robotY + dy);
        }

        public void cellBackwards()
        {
            pidMove(-forwardLeft, -forwardRight);
            int dx = (int)Math.Cos(theta + Math.PI);
            int dy = (int)Math.Sin(theta + Math.PI);
            moveRobot(robotX + dx, robotY + dy);
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
            moveRobot(robotX + dx, robotY + dy);
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

    }
}
