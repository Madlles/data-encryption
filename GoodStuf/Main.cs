using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using static GoodStuf.EffectBlur;

namespace GoodStuf
{
    public partial class Main : Form
    {
        private Form currentChildForm;
        public Main()
        {

            InitializeComponent();
            EnableBlur();
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#000740");
            siticoneShadowForm1.SetShadowForm(this);

        }

        async private void Mains_Load(object sender, EventArgs e)
        {
            siticoneCustomGradientPanel1.Visible = false;
            siticoneCustomGradientPanel2.Visible = false;
            bunifuButton1.Visible = false;
            bunifuButton2.Visible = false;
            bunifuButton3.Visible = false;
            bunifuButton4.Visible = false;
            for (Opacity = 0; Opacity < 1; Opacity += 0.03)
                await Task.Delay(1);
            
            await Task.Delay(1000);
            siticoneTransition1.Show(siticoneCustomGradientPanel1);
            siticoneTransition1.Show(siticoneCustomGradientPanel2);

            await Task.Delay(1000);
            siticoneTransition2.Show(bunifuButton1);
            siticoneTransition2.Show(bunifuButton2);
            siticoneTransition2.Show(bunifuButton3);
            siticoneTransition2.Show(bunifuButton4);
            


        }

        private void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel1.Controls.Add(childForm);
            panel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

        }

        private void siticonePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuElipse1_TargetControlResized(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        #region Code to blur form

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        private uint _blurOpacity;

        public double BlurOpacity
        {
            get { return _blurOpacity; }
            set { _blurOpacity = (uint)value; EnableBlur(); }
        }

        private uint _blurBackgroundColor = 0x990000;

        internal void EnableBlur()
        {

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            accent.GradientColor = (_blurOpacity << 24) | (_blurBackgroundColor & 0xFFFFFF);
            var accentStructSize = Marshal.SizeOf(accent);
            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);
            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;
            SetWindowCompositionAttribute(this.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }
        #endregion

        private void siticonePictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void siticonePictureBox1_Click_2(object sender, EventArgs e)
        {
          
        }

        private void siticonePictureBox2_Click(object sender, EventArgs e)
        {
           
        }

        private void siticoneRoundedGradientButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void siticoneRoundedGradientButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        async private void bunifuButton1_Click(object sender, EventArgs e)
        {
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            
            label1.Visible = false;
            label1.Text = bunifuButton1.Text;
            await Task.Delay(1);
            siticoneTransition3.Show(label1);
            
        }

        async private void bunifuButton2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Encryption());
            label1.Visible = false;
            label1.Text = bunifuButton2.Text;
            await Task.Delay(1);
            siticoneTransition3.Show(label1);
            
        }

        async private void bunifuButton3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new OwnEncryption());
            label1.Visible = false;
            label1.Text = bunifuButton3.Text;
            await Task.Delay(1);
            siticoneTransition3.Show(label1);
        }

        async private void bunifuButton4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new Settings());
            label1.Visible = false;
            label1.Text = bunifuButton4.Text;
            await Task.Delay(1);
            siticoneTransition3.Show(label1);
        }
    }

}
