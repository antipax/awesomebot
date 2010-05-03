namespace awesomebot
{
    partial class awesomebotform
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mapGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.leftTicks = new System.Windows.Forms.TextBox();
            this.rightTicks = new System.Windows.Forms.TextBox();
            this.forward = new System.Windows.Forms.Button();
            this.left = new System.Windows.Forms.Button();
            this.right = new System.Windows.Forms.Button();
            this.backwards = new System.Windows.Forms.Button();
            this.openSerialPort = new System.Windows.Forms.Button();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.wave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // mapGrid
            // 
            this.mapGrid.AllowUserToAddRows = false;
            this.mapGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.mapGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mapGrid.ColumnHeadersVisible = false;
            this.mapGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8});
            this.mapGrid.Location = new System.Drawing.Point(12, 12);
            this.mapGrid.Name = "mapGrid";
            this.mapGrid.RowHeadersVisible = false;
            this.mapGrid.Size = new System.Drawing.Size(312, 204);
            this.mapGrid.TabIndex = 0;
            this.mapGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.mapGrid_CellEndEdit);
            this.mapGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mapGrid_KeyPress);
            this.mapGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.mapGrid_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Column3";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Column4";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Column5";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Column6";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Column7";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Column8";
            this.Column8.Name = "Column8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(73, 225);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Left ticks";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(73, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Right ticks";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // leftTicks
            // 
            this.leftTicks.Location = new System.Drawing.Point(23, 225);
            this.leftTicks.Name = "leftTicks";
            this.leftTicks.Size = new System.Drawing.Size(44, 20);
            this.leftTicks.TabIndex = 5;
            // 
            // rightTicks
            // 
            this.rightTicks.Location = new System.Drawing.Point(23, 248);
            this.rightTicks.Name = "rightTicks";
            this.rightTicks.Size = new System.Drawing.Size(44, 20);
            this.rightTicks.TabIndex = 6;
            // 
            // forward
            // 
            this.forward.Location = new System.Drawing.Point(206, 222);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(75, 23);
            this.forward.TabIndex = 7;
            this.forward.Text = "Forward";
            this.forward.UseVisualStyleBackColor = true;
            this.forward.Click += new System.EventHandler(this.forward_Click);
            // 
            // left
            // 
            this.left.Location = new System.Drawing.Point(149, 248);
            this.left.Name = "left";
            this.left.Size = new System.Drawing.Size(75, 23);
            this.left.TabIndex = 8;
            this.left.Text = "Left";
            this.left.UseVisualStyleBackColor = true;
            this.left.Click += new System.EventHandler(this.left_Click);
            // 
            // right
            // 
            this.right.Location = new System.Drawing.Point(249, 248);
            this.right.Name = "right";
            this.right.Size = new System.Drawing.Size(75, 23);
            this.right.TabIndex = 9;
            this.right.Text = "Right";
            this.right.UseVisualStyleBackColor = true;
            this.right.Click += new System.EventHandler(this.right_Click);
            // 
            // backwards
            // 
            this.backwards.Location = new System.Drawing.Point(206, 277);
            this.backwards.Name = "backwards";
            this.backwards.Size = new System.Drawing.Size(75, 23);
            this.backwards.TabIndex = 10;
            this.backwards.Text = "Backwards";
            this.backwards.UseVisualStyleBackColor = true;
            this.backwards.Click += new System.EventHandler(this.backwards_Click);
            // 
            // openSerialPort
            // 
            this.openSerialPort.Location = new System.Drawing.Point(12, 287);
            this.openSerialPort.Name = "openSerialPort";
            this.openSerialPort.Size = new System.Drawing.Size(75, 23);
            this.openSerialPort.TabIndex = 11;
            this.openSerialPort.Text = "Open Serial Port";
            this.openSerialPort.UseVisualStyleBackColor = true;
            this.openSerialPort.Click += new System.EventHandler(this.openSerialPort_Click);
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 115200;
            this.serialPort1.PortName = "COM6";
            this.serialPort1.ReadTimeout = 250;
            // 
            // wave
            // 
            this.wave.Location = new System.Drawing.Point(111, 300);
            this.wave.Name = "wave";
            this.wave.Size = new System.Drawing.Size(75, 23);
            this.wave.TabIndex = 12;
            this.wave.Text = "Wavefront";
            this.wave.UseVisualStyleBackColor = true;
            this.wave.Click += new System.EventHandler(this.wave_Click);
            // 
            // awesomebotform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 335);
            this.Controls.Add(this.wave);
            this.Controls.Add(this.openSerialPort);
            this.Controls.Add(this.backwards);
            this.Controls.Add(this.right);
            this.Controls.Add(this.left);
            this.Controls.Add(this.forward);
            this.Controls.Add(this.rightTicks);
            this.Controls.Add(this.leftTicks);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapGrid);
            this.Name = "awesomebotform";
            this.Text = "Awesomebot";
            ((System.ComponentModel.ISupportInitialize)(this.mapGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView mapGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox leftTicks;
        private System.Windows.Forms.TextBox rightTicks;
        private System.Windows.Forms.Button forward;
        private System.Windows.Forms.Button left;
        private System.Windows.Forms.Button right;
        private System.Windows.Forms.Button backwards;
        private System.Windows.Forms.Button openSerialPort;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button wave;

    }
}

