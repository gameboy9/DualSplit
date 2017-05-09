namespace DualSplit
{
    partial class ControlCenter
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
            this.startClocks = new System.Windows.Forms.Button();
            this.mTenthA = new System.Windows.Forms.Button();
            this.pTenthA = new System.Windows.Forms.Button();
            this.mOneA = new System.Windows.Forms.Button();
            this.pOneA = new System.Windows.Forms.Button();
            this.mTenthB = new System.Windows.Forms.Button();
            this.pTenthB = new System.Windows.Forms.Button();
            this.mOneB = new System.Windows.Forms.Button();
            this.pOneB = new System.Windows.Forms.Button();
            this.splitA = new System.Windows.Forms.Button();
            this.splitB = new System.Windows.Forms.Button();
            this.reverseB = new System.Windows.Forms.Button();
            this.reverseA = new System.Windows.Forms.Button();
            this.splitMOneB = new System.Windows.Forms.Button();
            this.splitPOneB = new System.Windows.Forms.Button();
            this.splitMOneA = new System.Windows.Forms.Button();
            this.splitPOneA = new System.Windows.Forms.Button();
            this.btnResetClocks = new System.Windows.Forms.Button();
            this.txtPlayerB = new System.Windows.Forms.TextBox();
            this.txtPlayerA = new System.Windows.Forms.TextBox();
            this.txtCommentary = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.splitMTenB = new System.Windows.Forms.Button();
            this.splitPTenB = new System.Windows.Forms.Button();
            this.splitMTenA = new System.Windows.Forms.Button();
            this.splitPTenA = new System.Windows.Forms.Button();
            this.cmdStartServer = new System.Windows.Forms.Button();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmdTest = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // startClocks
            // 
            this.startClocks.Location = new System.Drawing.Point(229, 43);
            this.startClocks.Name = "startClocks";
            this.startClocks.Size = new System.Drawing.Size(87, 23);
            this.startClocks.TabIndex = 30;
            this.startClocks.Text = "Start Clocks";
            this.startClocks.UseVisualStyleBackColor = true;
            this.startClocks.Click += new System.EventHandler(this.startClocks_Click);
            // 
            // mTenthA
            // 
            this.mTenthA.Location = new System.Drawing.Point(67, 72);
            this.mTenthA.Name = "mTenthA";
            this.mTenthA.Size = new System.Drawing.Size(71, 23);
            this.mTenthA.TabIndex = 29;
            this.mTenthA.Text = "-1/10 sec";
            this.mTenthA.UseVisualStyleBackColor = true;
            this.mTenthA.Click += new System.EventHandler(this.adjustClocks);
            // 
            // pTenthA
            // 
            this.pTenthA.Location = new System.Drawing.Point(67, 43);
            this.pTenthA.Name = "pTenthA";
            this.pTenthA.Size = new System.Drawing.Size(71, 23);
            this.pTenthA.TabIndex = 28;
            this.pTenthA.Text = "+1/10 sec";
            this.pTenthA.UseVisualStyleBackColor = true;
            this.pTenthA.Click += new System.EventHandler(this.adjustClocks);
            // 
            // mOneA
            // 
            this.mOneA.Location = new System.Drawing.Point(158, 72);
            this.mOneA.Name = "mOneA";
            this.mOneA.Size = new System.Drawing.Size(47, 23);
            this.mOneA.TabIndex = 27;
            this.mOneA.Text = "-1 sec";
            this.mOneA.UseVisualStyleBackColor = true;
            this.mOneA.Click += new System.EventHandler(this.adjustClocks);
            // 
            // pOneA
            // 
            this.pOneA.Location = new System.Drawing.Point(158, 43);
            this.pOneA.Name = "pOneA";
            this.pOneA.Size = new System.Drawing.Size(47, 23);
            this.pOneA.TabIndex = 26;
            this.pOneA.Text = "+1 sec";
            this.pOneA.UseVisualStyleBackColor = true;
            this.pOneA.Click += new System.EventHandler(this.adjustClocks);
            // 
            // mTenthB
            // 
            this.mTenthB.Location = new System.Drawing.Point(396, 72);
            this.mTenthB.Name = "mTenthB";
            this.mTenthB.Size = new System.Drawing.Size(71, 23);
            this.mTenthB.TabIndex = 25;
            this.mTenthB.Text = "-1/10 sec";
            this.mTenthB.UseVisualStyleBackColor = true;
            this.mTenthB.Click += new System.EventHandler(this.adjustClocks);
            // 
            // pTenthB
            // 
            this.pTenthB.Location = new System.Drawing.Point(396, 43);
            this.pTenthB.Name = "pTenthB";
            this.pTenthB.Size = new System.Drawing.Size(71, 23);
            this.pTenthB.TabIndex = 24;
            this.pTenthB.Text = "+1/10 sec";
            this.pTenthB.UseVisualStyleBackColor = true;
            this.pTenthB.Click += new System.EventHandler(this.adjustClocks);
            // 
            // mOneB
            // 
            this.mOneB.Location = new System.Drawing.Point(332, 72);
            this.mOneB.Name = "mOneB";
            this.mOneB.Size = new System.Drawing.Size(47, 23);
            this.mOneB.TabIndex = 23;
            this.mOneB.Text = "-1 sec";
            this.mOneB.UseVisualStyleBackColor = true;
            this.mOneB.Click += new System.EventHandler(this.adjustClocks);
            // 
            // pOneB
            // 
            this.pOneB.Location = new System.Drawing.Point(332, 43);
            this.pOneB.Name = "pOneB";
            this.pOneB.Size = new System.Drawing.Size(47, 23);
            this.pOneB.TabIndex = 22;
            this.pOneB.Text = "+1 sec";
            this.pOneB.UseVisualStyleBackColor = true;
            this.pOneB.Click += new System.EventHandler(this.adjustClocks);
            // 
            // splitA
            // 
            this.splitA.Location = new System.Drawing.Point(134, 165);
            this.splitA.Name = "splitA";
            this.splitA.Size = new System.Drawing.Size(71, 23);
            this.splitA.TabIndex = 31;
            this.splitA.Text = "Split";
            this.splitA.UseVisualStyleBackColor = true;
            this.splitA.Click += new System.EventHandler(this.splitA_Click);
            // 
            // splitB
            // 
            this.splitB.Location = new System.Drawing.Point(332, 165);
            this.splitB.Name = "splitB";
            this.splitB.Size = new System.Drawing.Size(71, 23);
            this.splitB.TabIndex = 32;
            this.splitB.Text = "Split";
            this.splitB.UseVisualStyleBackColor = true;
            this.splitB.Click += new System.EventHandler(this.splitB_Click);
            // 
            // reverseB
            // 
            this.reverseB.Location = new System.Drawing.Point(332, 194);
            this.reverseB.Name = "reverseB";
            this.reverseB.Size = new System.Drawing.Size(71, 23);
            this.reverseB.TabIndex = 34;
            this.reverseB.Text = "Reverse";
            this.reverseB.UseVisualStyleBackColor = true;
            this.reverseB.Click += new System.EventHandler(this.reverseB_Click);
            // 
            // reverseA
            // 
            this.reverseA.Location = new System.Drawing.Point(134, 194);
            this.reverseA.Name = "reverseA";
            this.reverseA.Size = new System.Drawing.Size(71, 23);
            this.reverseA.TabIndex = 33;
            this.reverseA.Text = "Reverse";
            this.reverseA.UseVisualStyleBackColor = true;
            this.reverseA.Click += new System.EventHandler(this.reverseA_Click);
            // 
            // splitMOneB
            // 
            this.splitMOneB.Location = new System.Drawing.Point(473, 194);
            this.splitMOneB.Name = "splitMOneB";
            this.splitMOneB.Size = new System.Drawing.Size(47, 23);
            this.splitMOneB.TabIndex = 36;
            this.splitMOneB.Text = "-1 sec";
            this.splitMOneB.UseVisualStyleBackColor = true;
            this.splitMOneB.Click += new System.EventHandler(this.splitMOneB_Click);
            // 
            // splitPOneB
            // 
            this.splitPOneB.Location = new System.Drawing.Point(420, 194);
            this.splitPOneB.Name = "splitPOneB";
            this.splitPOneB.Size = new System.Drawing.Size(47, 23);
            this.splitPOneB.TabIndex = 35;
            this.splitPOneB.Text = "+1 sec";
            this.splitPOneB.UseVisualStyleBackColor = true;
            this.splitPOneB.Click += new System.EventHandler(this.splitPOneB_Click);
            // 
            // splitMOneA
            // 
            this.splitMOneA.Location = new System.Drawing.Point(67, 194);
            this.splitMOneA.Name = "splitMOneA";
            this.splitMOneA.Size = new System.Drawing.Size(47, 23);
            this.splitMOneA.TabIndex = 38;
            this.splitMOneA.Text = "-1 sec";
            this.splitMOneA.UseVisualStyleBackColor = true;
            this.splitMOneA.Click += new System.EventHandler(this.splitMOneA_Click);
            // 
            // splitPOneA
            // 
            this.splitPOneA.Location = new System.Drawing.Point(14, 194);
            this.splitPOneA.Name = "splitPOneA";
            this.splitPOneA.Size = new System.Drawing.Size(47, 23);
            this.splitPOneA.TabIndex = 37;
            this.splitPOneA.Text = "+1 sec";
            this.splitPOneA.UseVisualStyleBackColor = true;
            this.splitPOneA.Click += new System.EventHandler(this.splitPOneA_Click);
            // 
            // btnResetClocks
            // 
            this.btnResetClocks.Location = new System.Drawing.Point(229, 194);
            this.btnResetClocks.Name = "btnResetClocks";
            this.btnResetClocks.Size = new System.Drawing.Size(87, 23);
            this.btnResetClocks.TabIndex = 39;
            this.btnResetClocks.Text = "Reset Clocks";
            this.btnResetClocks.UseVisualStyleBackColor = true;
            this.btnResetClocks.Click += new System.EventHandler(this.btnResetClocks_Click);
            // 
            // txtPlayerB
            // 
            this.txtPlayerB.Location = new System.Drawing.Point(360, 12);
            this.txtPlayerB.Name = "txtPlayerB";
            this.txtPlayerB.Size = new System.Drawing.Size(160, 20);
            this.txtPlayerB.TabIndex = 40;
            this.txtPlayerB.Text = "PlayerB";
            this.txtPlayerB.Leave += new System.EventHandler(this.txtPlayerB_Leave);
            // 
            // txtPlayerA
            // 
            this.txtPlayerA.Location = new System.Drawing.Point(12, 12);
            this.txtPlayerA.Name = "txtPlayerA";
            this.txtPlayerA.Size = new System.Drawing.Size(160, 20);
            this.txtPlayerA.TabIndex = 41;
            this.txtPlayerA.Text = "PlayerA";
            this.txtPlayerA.TextChanged += new System.EventHandler(this.txtPlayerA_TextChanged);
            // 
            // txtCommentary
            // 
            this.txtCommentary.Location = new System.Drawing.Point(86, 137);
            this.txtCommentary.Name = "txtCommentary";
            this.txtCommentary.Size = new System.Drawing.Size(381, 20);
            this.txtCommentary.TabIndex = 42;
            this.txtCommentary.Leave += new System.EventHandler(this.txtCommentary_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 43;
            this.label1.Text = "Commentary:";
            // 
            // splitMTenB
            // 
            this.splitMTenB.Location = new System.Drawing.Point(473, 165);
            this.splitMTenB.Name = "splitMTenB";
            this.splitMTenB.Size = new System.Drawing.Size(47, 23);
            this.splitMTenB.TabIndex = 45;
            this.splitMTenB.Text = "-10 s";
            this.splitMTenB.UseVisualStyleBackColor = true;
            this.splitMTenB.Click += new System.EventHandler(this.splitMTenB_Click);
            // 
            // splitPTenB
            // 
            this.splitPTenB.Location = new System.Drawing.Point(420, 163);
            this.splitPTenB.Name = "splitPTenB";
            this.splitPTenB.Size = new System.Drawing.Size(47, 23);
            this.splitPTenB.TabIndex = 44;
            this.splitPTenB.Text = "+10 s";
            this.splitPTenB.UseVisualStyleBackColor = true;
            this.splitPTenB.Click += new System.EventHandler(this.splitPTenB_Click);
            // 
            // splitMTenA
            // 
            this.splitMTenA.Location = new System.Drawing.Point(67, 165);
            this.splitMTenA.Name = "splitMTenA";
            this.splitMTenA.Size = new System.Drawing.Size(47, 23);
            this.splitMTenA.TabIndex = 47;
            this.splitMTenA.Text = "-10 s";
            this.splitMTenA.UseVisualStyleBackColor = true;
            this.splitMTenA.Click += new System.EventHandler(this.splitMTenA_Click);
            // 
            // splitPTenA
            // 
            this.splitPTenA.Location = new System.Drawing.Point(15, 165);
            this.splitPTenA.Name = "splitPTenA";
            this.splitPTenA.Size = new System.Drawing.Size(47, 23);
            this.splitPTenA.TabIndex = 46;
            this.splitPTenA.Text = "+10 s";
            this.splitPTenA.UseVisualStyleBackColor = true;
            this.splitPTenA.Click += new System.EventHandler(this.splitPTenA_Click);
            // 
            // cmdStartServer
            // 
            this.cmdStartServer.Location = new System.Drawing.Point(584, 206);
            this.cmdStartServer.Name = "cmdStartServer";
            this.cmdStartServer.Size = new System.Drawing.Size(87, 23);
            this.cmdStartServer.TabIndex = 48;
            this.cmdStartServer.Text = "Start Server";
            this.cmdStartServer.UseVisualStyleBackColor = true;
            this.cmdStartServer.Click += new System.EventHandler(this.cmdStartServer_Click);
            // 
            // cmdConnect
            // 
            this.cmdConnect.Location = new System.Drawing.Point(696, 206);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(87, 23);
            this.cmdConnect.TabIndex = 49;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(648, 16);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(135, 20);
            this.txtIP.TabIndex = 50;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(648, 41);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(67, 20);
            this.txtPort.TabIndex = 51;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(581, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 52;
            this.label2.Text = "IP Address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(581, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 53;
            this.label3.Text = "Port:";
            // 
            // cmdTest
            // 
            this.cmdTest.Location = new System.Drawing.Point(799, 206);
            this.cmdTest.Name = "cmdTest";
            this.cmdTest.Size = new System.Drawing.Size(87, 23);
            this.cmdTest.TabIndex = 54;
            this.cmdTest.Text = "Test";
            this.cmdTest.UseVisualStyleBackColor = true;
            this.cmdTest.Click += new System.EventHandler(this.cmdTest_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(841, 16);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 56);
            this.listBox1.TabIndex = 55;
            // 
            // ControlCenter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 248);
            this.ControlBox = false;
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.cmdTest);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIP);
            this.Controls.Add(this.cmdConnect);
            this.Controls.Add(this.cmdStartServer);
            this.Controls.Add(this.splitMTenA);
            this.Controls.Add(this.splitPTenA);
            this.Controls.Add(this.splitMTenB);
            this.Controls.Add(this.splitPTenB);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCommentary);
            this.Controls.Add(this.txtPlayerA);
            this.Controls.Add(this.txtPlayerB);
            this.Controls.Add(this.btnResetClocks);
            this.Controls.Add(this.splitMOneA);
            this.Controls.Add(this.splitPOneA);
            this.Controls.Add(this.splitMOneB);
            this.Controls.Add(this.splitPOneB);
            this.Controls.Add(this.reverseB);
            this.Controls.Add(this.reverseA);
            this.Controls.Add(this.splitB);
            this.Controls.Add(this.splitA);
            this.Controls.Add(this.startClocks);
            this.Controls.Add(this.mTenthA);
            this.Controls.Add(this.pTenthA);
            this.Controls.Add(this.mOneA);
            this.Controls.Add(this.pOneA);
            this.Controls.Add(this.mTenthB);
            this.Controls.Add(this.pTenthB);
            this.Controls.Add(this.mOneB);
            this.Controls.Add(this.pOneB);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ControlCenter";
            this.Text = "ControlCenter";
            this.Load += new System.EventHandler(this.ControlCenter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startClocks;
        private System.Windows.Forms.Button mTenthA;
        private System.Windows.Forms.Button pTenthA;
        private System.Windows.Forms.Button mOneA;
        private System.Windows.Forms.Button pOneA;
        private System.Windows.Forms.Button mTenthB;
        private System.Windows.Forms.Button pTenthB;
        private System.Windows.Forms.Button mOneB;
        private System.Windows.Forms.Button pOneB;
        private System.Windows.Forms.Button splitA;
        private System.Windows.Forms.Button splitB;
        private System.Windows.Forms.Button reverseB;
        private System.Windows.Forms.Button reverseA;
        private System.Windows.Forms.Button splitMOneB;
        private System.Windows.Forms.Button splitPOneB;
        private System.Windows.Forms.Button splitMOneA;
        private System.Windows.Forms.Button splitPOneA;
        private System.Windows.Forms.Button btnResetClocks;
        private System.Windows.Forms.TextBox txtPlayerB;
        private System.Windows.Forms.TextBox txtPlayerA;
        private System.Windows.Forms.TextBox txtCommentary;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button splitMTenB;
        private System.Windows.Forms.Button splitPTenB;
        private System.Windows.Forms.Button splitMTenA;
        private System.Windows.Forms.Button splitPTenA;
        private System.Windows.Forms.Button cmdStartServer;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button cmdTest;
        private System.Windows.Forms.ListBox listBox1;
    }
}