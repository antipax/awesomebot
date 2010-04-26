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
        public awesomebotform()
        {
            InitializeComponent();
            string[] row0 = { "0", "0", "0", "0", "0", "0", "0", "0" };
            mapGrid.Rows.Add(row0);
            mapGrid.Rows[0].DefaultCellStyle.BackColor = Color.Aquamarine;


        }

        private void mapGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
