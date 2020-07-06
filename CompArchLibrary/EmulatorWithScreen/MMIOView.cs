using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmulatorWithScreen
{
    public partial class MMIOView : Form
    {
        public MMIOView()
        {
            InitializeComponent();
            panel1.AutoScroll = true;
        }
    }
}
