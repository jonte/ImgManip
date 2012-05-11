using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgResize
{
    public partial class PercentageSlider : Form
    {
        public int getSelection() {
            InitializeComponent();
            this.ShowDialog();
            return percSlider.Value;
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            Dispose(true);
        }

        private void percSlider_Scroll(object sender, EventArgs e)
        {
            percLabel.Text = ""+percSlider.Value;
        }

        private void PercentageSlider_Load(object sender, EventArgs e)
        {
            percSlider_Scroll(sender, e);
        }
    }
}
