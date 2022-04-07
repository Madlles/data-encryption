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
    public partial class Encryption : Form
    {
        public Encryption()
        {
            InitializeComponent();
            EnableBlur();
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#000080");

            richTextBox1.BackColor = Color.FromArgb(255, 255, 255, 255);

        }

        async private void Encryption_Load(object sender, EventArgs e)
        {
            bunifuTextBox1.Visible = false;
            siticoneGradientButton1.Visible = false;
            siticoneGradientButton2.Visible = false;
            siticoneGradientButton3.Visible = false;
            richTextBox1.Visible = false;
            label1.Visible = false;
            siticoneRoundedComboBox1.Visible = false;
            
            await Task.Delay(150);
            bunifuTransition1.ShowSync(siticoneGradientButton1);
            await Task.Delay(150);
            bunifuTransition1.ShowSync(siticoneGradientButton2);
            await Task.Delay(150);
            bunifuTransition1.ShowSync(siticoneGradientButton3);
            await Task.Delay(150);
            bunifuTransition1.ShowSync(richTextBox1);
            await Task.Delay(150);
            bunifuTransition1.ShowSync(label1);
            await Task.Delay(150);
            bunifuTransition1.ShowSync(siticoneRoundedComboBox1);




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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void siticoneGradientButton2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile1 = new SaveFileDialog();
            saveFile1.DefaultExt = "*.rtf";
            saveFile1.Filter = "RTF Files|*.rtf";

            if (saveFile1.ShowDialog() == DialogResult.OK && saveFile1.FileName.Length > 0)
            {
                richTextBox1.SaveFile(saveFile1.FileName, RichTextBoxStreamType.PlainText);
            }
        }

        private void siticoneGradientButton3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile1 = new OpenFileDialog();
            openFile1.DefaultExt = "*.rtf";
            openFile1.Filter = "RTF Files|*.rtf";

            if (openFile1.ShowDialog() == DialogResult.OK && openFile1.FileName.Length > 0)
            {
                richTextBox1.LoadFile(openFile1.FileName, RichTextBoxStreamType.PlainText);
            }
            
            
        }

        private void siticoneGradientButton1_Click(object sender, EventArgs e)
        {
            if(siticoneRoundedComboBox1.Text == "Цезарь")
            {
                string text = richTextBox1.Text;
                string result = "";
                for (int i = 0; i < text.Length; i++)
                {
                    result += (char)(text[i] + Convert.ToInt32(bunifuTextBox1.Text));
                }
                richTextBox1.Text = result;
            }    
            
            

        }

        async private void siticoneRoundedComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (siticoneRoundedComboBox1.Text == "Цезарь")
            {
                bunifuTextBox1.Visible = false;
                await Task.Delay(150);
                bunifuTransition1.ShowSync(bunifuTextBox1);
            }
            else
            {
                bunifuTextBox1.Visible = false;
            }
        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
