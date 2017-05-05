using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualSplit
{
    public partial class ControlCenter : Form
    {
        Main main;

        public ControlCenter()
        {
            InitializeComponent();
        }

        private void ControlCenter_Load(object sender, EventArgs e)
        {

        }

        public void setupForm(Main firstForm)
        {
            main = firstForm;
        }

        private void startClocks_Click(object sender, EventArgs e)
        {
            main.startClocks();
        }

        private void adjustClocks(object sender, EventArgs e)
        {
            Button actualSender = (Button)sender;
            main.adjustTime(actualSender.Name == "pOneA" || actualSender.Name == "mOneA" || actualSender.Name == "pTenthA" || actualSender.Name == "mTenthA",
                actualSender.Name == "pTenthA" || actualSender.Name == "mTenthA" || actualSender.Name == "pTenthB" || actualSender.Name == "mTenthB",
                actualSender.Name == "mOneB" || actualSender.Name == "mOneA" || actualSender.Name == "mTenthB" || actualSender.Name == "mTenthA");
        }

        private void splitA_Click(object sender, EventArgs e)
        {
            main.split(true);
        }

        private void splitB_Click(object sender, EventArgs e)
        {
            main.split(false);
        }

        private void reverseA_Click(object sender, EventArgs e)
        {
            main.reverseSplit(true);
        }

        private void reverseB_Click(object sender, EventArgs e)
        {
            main.reverseSplit(false);
        }

        private void splitPOneA_Click(object sender, EventArgs e)
        {
            main.adjustSplit(true, true, false);
        }

        private void splitMOneA_Click(object sender, EventArgs e)
        {
            main.adjustSplit(true, false, false);
        }

        private void splitPOneB_Click(object sender, EventArgs e)
        {
            main.adjustSplit(false, true, false);
        }

        private void splitMOneB_Click(object sender, EventArgs e)
        {
            main.adjustSplit(false, false, false);
        }

        private void btnResetClocks_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "DualSplit", MessageBoxButtons.YesNo) == DialogResult.Yes)
                main.resetClocks();
        }

        private void txtPlayerB_Leave(object sender, EventArgs e)
        {
            main.lblPlayerB.Text = txtPlayerB.Text;
        }

        private void txtPlayerA_TextChanged(object sender, EventArgs e)
        {
            main.lblPlayerA.Text = txtPlayerA.Text;
        }

        private void txtCommentary_Leave(object sender, EventArgs e)
        {
            main.lblCommentary.Text = "Commentary:  " + txtCommentary.Text;
        }

        private void splitPTenA_Click(object sender, EventArgs e)
        {
            main.adjustSplit(true, true, true);
        }

        private void splitMTenA_Click(object sender, EventArgs e)
        {
            main.adjustSplit(true, false, true);
        }

        private void splitPTenB_Click(object sender, EventArgs e)
        {
            main.adjustSplit(false, true, true);
        }

        private void splitMTenB_Click(object sender, EventArgs e)
        {
            main.adjustSplit(false, false, true);
        }
    }
}
