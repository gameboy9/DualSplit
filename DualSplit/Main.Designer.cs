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
            this.lblTimerA.Location = new System.Drawing.Point(16, 324);
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
            this.lblTimerB.Location = new System.Drawing.Point(1656, 324);
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
            this.lblPlayerA.Location = new System.Drawing.Point(16, 25);
            this.lblPlayerA.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblPlayerA.Name = "lblPlayerA";
            this.lblPlayerA.Size = new System.Drawing.Size(181, 30);
            this.lblPlayerA.TabIndex = 2;
            this.lblPlayerA.Text = "PlayerA";
            // 
            // lblPlayerB
            // 
            this.lblPlayerB.AutoSize = true;
            this.lblPlayerB.Font = new System.Drawing.Font("Dragon Quest", 18F);
            this.lblPlayerB.ForeColor = System.Drawing.Color.White;
            this.lblPlayerB.Location = new System.Drawing.Point(1704, 25);
            this.lblPlayerB.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblPlayerB.Name = "lblPlayerB";
            this.lblPlayerB.Size = new System.Drawing.Size(181, 30);
            this.lblPlayerB.TabIndex = 3;
            this.lblPlayerB.Text = "PlayerB";
            this.lblPlayerB.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblCommentary
            // 
            this.lblCommentary.Font = new System.Drawing.Font("Dragon Quest", 12F);
            this.lblCommentary.ForeColor = System.Drawing.Color.White;
            this.lblCommentary.Location = new System.Drawing.Point(495, 324);
            this.lblCommentary.Margin = new System.Windows.Forms.Padding(7, 0, 7, 0);
            this.lblCommentary.Name = "lblCommentary";
            this.lblCommentary.Size = new System.Drawing.Size(915, 30);
            this.lblCommentary.TabIndex = 4;
            this.lblCommentary.Text = "Commentary:";
            this.lblCommentary.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1904, 382);
            this.Controls.Add(this.lblCommentary);
            this.Controls.Add(this.lblPlayerB);
            this.Controls.Add(this.lblPlayerA);
            this.Controls.Add(this.lblTimerB);
            this.Controls.Add(this.lblTimerA);
            this.Font = new System.Drawing.Font("Dragon Quest", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(7, 5, 7, 5);
            this.Name = "Main";
            this.Text = "DualSplit";
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
    }
}

