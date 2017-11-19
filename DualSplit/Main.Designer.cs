namespace DualSplit
{
    partial class Main
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lblTimerA = new System.Windows.Forms.Label();
            this.lblTimerB = new System.Windows.Forms.Label();
            this.lblPlayerA = new System.Windows.Forms.Label();
            this.lblPlayerB = new System.Windows.Forms.Label();
            this.lblCommentary = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.cmdConnect = new System.Windows.Forms.Button();
            this.cmdStartServer = new System.Windows.Forms.Button();
            this.splitMTenA = new System.Windows.Forms.Button();
            this.splitPTenA = new System.Windows.Forms.Button();
            this.splitMTenB = new System.Windows.Forms.Button();
            this.splitPTenB = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCommentary = new System.Windows.Forms.TextBox();
            this.txtPlayerA = new System.Windows.Forms.TextBox();
            this.txtPlayerB = new System.Windows.Forms.TextBox();
            this.btnResetClocks = new System.Windows.Forms.Button();
            this.splitMOneA = new System.Windows.Forms.Button();
            this.splitPOneA = new System.Windows.Forms.Button();
            this.splitMOneB = new System.Windows.Forms.Button();
            this.splitPOneB = new System.Windows.Forms.Button();
            this.reverseB = new System.Windows.Forms.Button();
            this.reverseA = new System.Windows.Forms.Button();
            this.splitB = new System.Windows.Forms.Button();
            this.splitA = new System.Windows.Forms.Button();
            this.mOneA = new System.Windows.Forms.Button();
            this.pOneA = new System.Windows.Forms.Button();
            this.mOneB = new System.Windows.Forms.Button();
            this.pOneB = new System.Windows.Forms.Button();
            this.btnStartClocks = new System.Windows.Forms.Button();
            this.lblGameName = new System.Windows.Forms.Label();
            this.btnChooseGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 90;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 90;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // lblTimerA
            // 
            this.lblTimerA.AutoSize = true;
            this.lblTimerA.Font = new System.Drawing.Font("Dragon Quest", 18F);
            this.lblTimerA.ForeColor = System.Drawing.Color.White;
            this.lblTimerA.Location = new System.Drawing.Point(16, 646);
            this.lblTimerA.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblTimerA.Name = "lblTimerA";
            this.lblTimerA.Size = new System.Drawing.Size(229, 30);
            this.lblTimerA.TabIndex = 0;
            this.lblTimerA.Text = "0:00:00.0";
            // 
            // lblTimerB
            // 
            this.lblTimerB.AutoSize = true;
            this.lblTimerB.Font = new System.Drawing.Font("Dragon Quest", 18F);
            this.lblTimerB.ForeColor = System.Drawing.Color.White;
            this.lblTimerB.Location = new System.Drawing.Point(1019, 646);
            this.lblTimerB.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblTimerB.Name = "lblTimerB";
            this.lblTimerB.Size = new System.Drawing.Size(229, 30);
            this.lblTimerB.TabIndex = 1;
            this.lblTimerB.Text = "0:00:00.0";
            this.lblTimerB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPlayerA
            // 
            this.lblPlayerA.AutoSize = true;
            this.lblPlayerA.Font = new System.Drawing.Font("Dragon Quest", 18F);
            this.lblPlayerA.ForeColor = System.Drawing.Color.White;
            this.lblPlayerA.Location = new System.Drawing.Point(16, 408);
            this.lblPlayerA.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblPlayerA.Name = "lblPlayerA";
            this.lblPlayerA.Size = new System.Drawing.Size(181, 30);
            this.lblPlayerA.TabIndex = 2;
            this.lblPlayerA.Text = "PlayerA";
            // 
            // lblPlayerB
            // 
            this.lblPlayerB.Font = new System.Drawing.Font("Dragon Quest", 18F);
            this.lblPlayerB.ForeColor = System.Drawing.Color.White;
            this.lblPlayerB.Location = new System.Drawing.Point(819, 408);
            this.lblPlayerB.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblPlayerB.Name = "lblPlayerB";
            this.lblPlayerB.Size = new System.Drawing.Size(429, 30);
            this.lblPlayerB.TabIndex = 3;
            this.lblPlayerB.Text = "PlayerB";
            this.lblPlayerB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCommentary
            // 
            this.lblCommentary.Font = new System.Drawing.Font("Dragon Quest", 12F);
            this.lblCommentary.ForeColor = System.Drawing.Color.White;
            this.lblCommentary.Location = new System.Drawing.Point(16, 465);
            this.lblCommentary.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblCommentary.Name = "lblCommentary";
            this.lblCommentary.Size = new System.Drawing.Size(277, 78);
            this.lblCommentary.TabIndex = 4;
            this.lblCommentary.Text = "Commentary:";
            this.lblCommentary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(604, 9);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(300, 56);
            this.listBox1.TabIndex = 88;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(386, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 86;
            this.label3.Text = "Port:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(386, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 85;
            this.label2.Text = "IP Address:";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPort.Location = new System.Drawing.Point(453, 34);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(67, 20);
            this.txtPort.TabIndex = 84;
            // 
            // txtIP
            // 
            this.txtIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIP.Location = new System.Drawing.Point(453, 9);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(135, 20);
            this.txtIP.TabIndex = 83;
            // 
            // cmdConnect
            // 
            this.cmdConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdConnect.Location = new System.Drawing.Point(786, 117);
            this.cmdConnect.Name = "cmdConnect";
            this.cmdConnect.Size = new System.Drawing.Size(87, 23);
            this.cmdConnect.TabIndex = 82;
            this.cmdConnect.Text = "Connect";
            this.cmdConnect.UseVisualStyleBackColor = true;
            this.cmdConnect.Click += new System.EventHandler(this.cmdConnect_Click);
            // 
            // cmdStartServer
            // 
            this.cmdStartServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdStartServer.Location = new System.Drawing.Point(693, 117);
            this.cmdStartServer.Name = "cmdStartServer";
            this.cmdStartServer.Size = new System.Drawing.Size(87, 23);
            this.cmdStartServer.TabIndex = 81;
            this.cmdStartServer.Text = "Start Server";
            this.cmdStartServer.UseVisualStyleBackColor = true;
            this.cmdStartServer.Click += new System.EventHandler(this.cmdStartServer_Click);
            // 
            // splitMTenA
            // 
            this.splitMTenA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitMTenA.Location = new System.Drawing.Point(72, 136);
            this.splitMTenA.Name = "splitMTenA";
            this.splitMTenA.Size = new System.Drawing.Size(47, 23);
            this.splitMTenA.TabIndex = 80;
            this.splitMTenA.Text = "-10 s";
            this.splitMTenA.UseVisualStyleBackColor = true;
            this.splitMTenA.Click += new System.EventHandler(this.splitMTenA_Click);
            // 
            // splitPTenA
            // 
            this.splitPTenA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitPTenA.Location = new System.Drawing.Point(20, 136);
            this.splitPTenA.Name = "splitPTenA";
            this.splitPTenA.Size = new System.Drawing.Size(47, 23);
            this.splitPTenA.TabIndex = 79;
            this.splitPTenA.Text = "+10 s";
            this.splitPTenA.UseVisualStyleBackColor = true;
            this.splitPTenA.Click += new System.EventHandler(this.splitPTenA_Click);
            // 
            // splitMTenB
            // 
            this.splitMTenB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitMTenB.Location = new System.Drawing.Point(1200, 136);
            this.splitMTenB.Name = "splitMTenB";
            this.splitMTenB.Size = new System.Drawing.Size(47, 23);
            this.splitMTenB.TabIndex = 78;
            this.splitMTenB.Text = "-10 s";
            this.splitMTenB.UseVisualStyleBackColor = true;
            this.splitMTenB.Click += new System.EventHandler(this.splitMTenB_Click);
            // 
            // splitPTenB
            // 
            this.splitPTenB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitPTenB.Location = new System.Drawing.Point(1147, 136);
            this.splitPTenB.Name = "splitPTenB";
            this.splitPTenB.Size = new System.Drawing.Size(47, 23);
            this.splitPTenB.TabIndex = 77;
            this.splitPTenB.Text = "+10 s";
            this.splitPTenB.UseVisualStyleBackColor = true;
            this.splitPTenB.Click += new System.EventHandler(this.splitPTenB_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(402, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 76;
            this.label1.Text = "Commentary:";
            // 
            // txtCommentary
            // 
            this.txtCommentary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCommentary.Location = new System.Drawing.Point(476, 79);
            this.txtCommentary.Name = "txtCommentary";
            this.txtCommentary.Size = new System.Drawing.Size(381, 20);
            this.txtCommentary.TabIndex = 75;
            // 
            // txtPlayerA
            // 
            this.txtPlayerA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlayerA.Location = new System.Drawing.Point(20, 12);
            this.txtPlayerA.Name = "txtPlayerA";
            this.txtPlayerA.Size = new System.Drawing.Size(160, 20);
            this.txtPlayerA.TabIndex = 74;
            this.txtPlayerA.Text = "PlayerA";
            // 
            // txtPlayerB
            // 
            this.txtPlayerB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPlayerB.Location = new System.Drawing.Point(1087, 12);
            this.txtPlayerB.Name = "txtPlayerB";
            this.txtPlayerB.Size = new System.Drawing.Size(160, 20);
            this.txtPlayerB.TabIndex = 73;
            this.txtPlayerB.Text = "PlayerB";
            // 
            // btnResetClocks
            // 
            this.btnResetClocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetClocks.Location = new System.Drawing.Point(405, 117);
            this.btnResetClocks.Name = "btnResetClocks";
            this.btnResetClocks.Size = new System.Drawing.Size(87, 23);
            this.btnResetClocks.TabIndex = 72;
            this.btnResetClocks.Text = "Reset Clocks";
            this.btnResetClocks.UseVisualStyleBackColor = true;
            this.btnResetClocks.Click += new System.EventHandler(this.btnResetClocks_Click);
            // 
            // splitMOneA
            // 
            this.splitMOneA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitMOneA.Location = new System.Drawing.Point(72, 165);
            this.splitMOneA.Name = "splitMOneA";
            this.splitMOneA.Size = new System.Drawing.Size(47, 23);
            this.splitMOneA.TabIndex = 71;
            this.splitMOneA.Text = "-1 sec";
            this.splitMOneA.UseVisualStyleBackColor = true;
            this.splitMOneA.Click += new System.EventHandler(this.splitMOneA_Click);
            // 
            // splitPOneA
            // 
            this.splitPOneA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitPOneA.Location = new System.Drawing.Point(19, 165);
            this.splitPOneA.Name = "splitPOneA";
            this.splitPOneA.Size = new System.Drawing.Size(47, 23);
            this.splitPOneA.TabIndex = 70;
            this.splitPOneA.Text = "+1 sec";
            this.splitPOneA.UseVisualStyleBackColor = true;
            this.splitPOneA.Click += new System.EventHandler(this.splitPOneA_Click);
            // 
            // splitMOneB
            // 
            this.splitMOneB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitMOneB.Location = new System.Drawing.Point(1200, 165);
            this.splitMOneB.Name = "splitMOneB";
            this.splitMOneB.Size = new System.Drawing.Size(47, 23);
            this.splitMOneB.TabIndex = 69;
            this.splitMOneB.Text = "-1 sec";
            this.splitMOneB.UseVisualStyleBackColor = true;
            this.splitMOneB.Click += new System.EventHandler(this.splitMOneB_Click);
            // 
            // splitPOneB
            // 
            this.splitPOneB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitPOneB.Location = new System.Drawing.Point(1147, 165);
            this.splitPOneB.Name = "splitPOneB";
            this.splitPOneB.Size = new System.Drawing.Size(47, 23);
            this.splitPOneB.TabIndex = 68;
            this.splitPOneB.Text = "+1 sec";
            this.splitPOneB.UseVisualStyleBackColor = true;
            this.splitPOneB.Click += new System.EventHandler(this.splitPOneB_Click);
            // 
            // reverseB
            // 
            this.reverseB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reverseB.Location = new System.Drawing.Point(1059, 165);
            this.reverseB.Name = "reverseB";
            this.reverseB.Size = new System.Drawing.Size(71, 23);
            this.reverseB.TabIndex = 67;
            this.reverseB.Text = "Reverse";
            this.reverseB.UseVisualStyleBackColor = true;
            this.reverseB.Click += new System.EventHandler(this.reverseB_Click);
            // 
            // reverseA
            // 
            this.reverseA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reverseA.Location = new System.Drawing.Point(139, 165);
            this.reverseA.Name = "reverseA";
            this.reverseA.Size = new System.Drawing.Size(71, 23);
            this.reverseA.TabIndex = 66;
            this.reverseA.Text = "Reverse";
            this.reverseA.UseVisualStyleBackColor = true;
            this.reverseA.Click += new System.EventHandler(this.reverseA_Click);
            // 
            // splitB
            // 
            this.splitB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitB.Location = new System.Drawing.Point(1059, 136);
            this.splitB.Name = "splitB";
            this.splitB.Size = new System.Drawing.Size(71, 23);
            this.splitB.TabIndex = 65;
            this.splitB.Text = "Split";
            this.splitB.UseVisualStyleBackColor = true;
            this.splitB.Click += new System.EventHandler(this.splitB_Click);
            // 
            // splitA
            // 
            this.splitA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitA.Location = new System.Drawing.Point(139, 136);
            this.splitA.Name = "splitA";
            this.splitA.Size = new System.Drawing.Size(71, 23);
            this.splitA.TabIndex = 64;
            this.splitA.Text = "Split";
            this.splitA.UseVisualStyleBackColor = true;
            this.splitA.Click += new System.EventHandler(this.splitA_Click);
            // 
            // mOneA
            // 
            this.mOneA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mOneA.Location = new System.Drawing.Point(19, 72);
            this.mOneA.Name = "mOneA";
            this.mOneA.Size = new System.Drawing.Size(90, 23);
            this.mOneA.TabIndex = 61;
            this.mOneA.Text = "Clock -1 sec";
            this.mOneA.UseVisualStyleBackColor = true;
            this.mOneA.Click += new System.EventHandler(this.adjustClocks);
            // 
            // pOneA
            // 
            this.pOneA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pOneA.Location = new System.Drawing.Point(19, 43);
            this.pOneA.Name = "pOneA";
            this.pOneA.Size = new System.Drawing.Size(90, 23);
            this.pOneA.TabIndex = 60;
            this.pOneA.Text = "Clock +1 sec";
            this.pOneA.UseVisualStyleBackColor = true;
            this.pOneA.Click += new System.EventHandler(this.adjustClocks);
            // 
            // mOneB
            // 
            this.mOneB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mOneB.Location = new System.Drawing.Point(1157, 72);
            this.mOneB.Name = "mOneB";
            this.mOneB.Size = new System.Drawing.Size(90, 23);
            this.mOneB.TabIndex = 57;
            this.mOneB.Text = "Clock -1 sec";
            this.mOneB.UseVisualStyleBackColor = true;
            this.mOneB.Click += new System.EventHandler(this.adjustClocks);
            // 
            // pOneB
            // 
            this.pOneB.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pOneB.Location = new System.Drawing.Point(1157, 43);
            this.pOneB.Name = "pOneB";
            this.pOneB.Size = new System.Drawing.Size(90, 23);
            this.pOneB.TabIndex = 56;
            this.pOneB.Text = "Clock +1 sec";
            this.pOneB.UseVisualStyleBackColor = true;
            this.pOneB.Click += new System.EventHandler(this.adjustClocks);
            // 
            // btnStartClocks
            // 
            this.btnStartClocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartClocks.Location = new System.Drawing.Point(498, 117);
            this.btnStartClocks.Name = "btnStartClocks";
            this.btnStartClocks.Size = new System.Drawing.Size(87, 23);
            this.btnStartClocks.TabIndex = 89;
            this.btnStartClocks.Text = "Start Clocks";
            this.btnStartClocks.UseVisualStyleBackColor = true;
            this.btnStartClocks.Click += new System.EventHandler(this.startClocks_Click);
            // 
            // lblGameName
            // 
            this.lblGameName.AutoSize = true;
            this.lblGameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameName.ForeColor = System.Drawing.Color.White;
            this.lblGameName.Location = new System.Drawing.Point(511, 170);
            this.lblGameName.Name = "lblGameName";
            this.lblGameName.Size = new System.Drawing.Size(142, 13);
            this.lblGameName.TabIndex = 90;
            this.lblGameName.Text = "Game:  Dragon Quest 1 SFC";
            // 
            // btnChooseGame
            // 
            this.btnChooseGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChooseGame.Location = new System.Drawing.Point(405, 165);
            this.btnChooseGame.Name = "btnChooseGame";
            this.btnChooseGame.Size = new System.Drawing.Size(87, 23);
            this.btnChooseGame.TabIndex = 91;
            this.btnChooseGame.Text = "Choose Game";
            this.btnChooseGame.UseVisualStyleBackColor = true;
            this.btnChooseGame.Click += new System.EventHandler(this.btnChooseGame_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.ClientSize = new System.Drawing.Size(1264, 682);
            this.Controls.Add(this.btnChooseGame);
            this.Controls.Add(this.lblGameName);
            this.Controls.Add(this.btnStartClocks);
            this.Controls.Add(this.listBox1);
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
            this.Controls.Add(this.mOneA);
            this.Controls.Add(this.pOneA);
            this.Controls.Add(this.mOneB);
            this.Controls.Add(this.pOneB);
            this.Controls.Add(this.lblCommentary);
            this.Controls.Add(this.lblPlayerB);
            this.Controls.Add(this.lblPlayerA);
            this.Controls.Add(this.lblTimerB);
            this.Controls.Add(this.lblTimerA);
            this.Font = new System.Drawing.Font("Dragon Quest", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.Name = "Main";
            this.Text = "DualSplit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lblTimerA;
        private System.Windows.Forms.Label lblTimerB;
        public System.Windows.Forms.Label lblPlayerA;
        public System.Windows.Forms.Label lblPlayerB;
        public System.Windows.Forms.Label lblCommentary;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Button cmdConnect;
        private System.Windows.Forms.Button cmdStartServer;
        private System.Windows.Forms.Button splitMTenA;
        private System.Windows.Forms.Button splitPTenA;
        private System.Windows.Forms.Button splitMTenB;
        private System.Windows.Forms.Button splitPTenB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCommentary;
        private System.Windows.Forms.TextBox txtPlayerA;
        private System.Windows.Forms.TextBox txtPlayerB;
        private System.Windows.Forms.Button btnResetClocks;
        private System.Windows.Forms.Button splitMOneA;
        private System.Windows.Forms.Button splitPOneA;
        private System.Windows.Forms.Button splitMOneB;
        private System.Windows.Forms.Button splitPOneB;
        private System.Windows.Forms.Button reverseB;
        private System.Windows.Forms.Button reverseA;
        private System.Windows.Forms.Button splitB;
        private System.Windows.Forms.Button splitA;
        private System.Windows.Forms.Button mOneA;
        private System.Windows.Forms.Button pOneA;
        private System.Windows.Forms.Button mOneB;
        private System.Windows.Forms.Button pOneB;
        private System.Windows.Forms.Button btnStartClocks;
        private System.Windows.Forms.Label lblGameName;
        private System.Windows.Forms.Button btnChooseGame;
    }
}

