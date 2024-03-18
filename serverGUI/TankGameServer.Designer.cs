namespace serverGUI
{
    partial class TankGameServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.consoleControl1 = new ConsoleControl.ConsoleControl();
            this.Stop = new System.Windows.Forms.Button();
            this.SetPort = new System.Windows.Forms.Button();
            this.Log = new System.Windows.Forms.Label();
            this.clientList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // consoleControl1
            // 
            this.consoleControl1.IsInputEnabled = true;
            this.consoleControl1.Location = new System.Drawing.Point(312, 27);
            this.consoleControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.consoleControl1.Name = "consoleControl1";
            this.consoleControl1.SendKeyboardCommandsToProcess = false;
            this.consoleControl1.ShowDiagnostics = false;
            this.consoleControl1.Size = new System.Drawing.Size(475, 411);
            this.consoleControl1.TabIndex = 0;
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(133, 27);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(46, 23);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // SetPort
            // 
            this.SetPort.Location = new System.Drawing.Point(87, 27);
            this.SetPort.Name = "SetPort";
            this.SetPort.Size = new System.Drawing.Size(40, 23);
            this.SetPort.TabIndex = 2;
            this.SetPort.Text = "Start";
            this.SetPort.UseVisualStyleBackColor = true;
            this.SetPort.Click += new System.EventHandler(this.SetPort_Click);
            // 
            // Log
            // 
            this.Log.AutoSize = true;
            this.Log.Location = new System.Drawing.Point(312, 9);
            this.Log.Name = "Log";
            this.Log.Size = new System.Drawing.Size(27, 15);
            this.Log.TabIndex = 3;
            this.Log.Text = "Log";
            // 
            // clientList
            // 
            this.clientList.FormattingEnabled = true;
            this.clientList.ItemHeight = 15;
            this.clientList.Location = new System.Drawing.Point(185, 27);
            this.clientList.Name = "clientList";
            this.clientList.Size = new System.Drawing.Size(120, 409);
            this.clientList.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(185, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Player List";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 27);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(69, 23);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "8000";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Port";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(689, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "Eric Tatchell, 2024";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "IP: Unknown, start the server";
            // 
            // TankGameServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.clientList);
            this.Controls.Add(this.Log);
            this.Controls.Add(this.SetPort);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.consoleControl1);
            this.Name = "TankGameServer";
            this.Text = "Tank Game Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ConsoleControl.ConsoleControl consoleControl1;
        private Button Stop;
        private Button SetPort;
        private Label Log;
        private ListBox clientList;
        private Label label1;
        private TextBox textBox1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}