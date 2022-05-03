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
using System.Security.Cryptography;

namespace GoodStuf
{
    public partial class OwnEncryption : Form
    {
        public OwnEncryption()
        {
            InitializeComponent();
            EnableBlur();
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#190624");
        }

        private void OwnEncryption_Load(object sender, EventArgs e)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void siticoneGradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                //encrypt
                string plainText = richTextBox1.Text;
                string passPhrase = bunifuTextBox2.Text;
                string cipherText = Encrypt(plainText, passPhrase);
                richTextBox1.Text = cipherText;
            }
            catch
            {
                MessageBox.Show("Please enter a valid password");
            }
            
        }

        //create encrypt class
        public static string Encrypt(string text, string key)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            //rfc2898
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] keyByte = md5.ComputeHash(utf8.GetBytes(key));
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyByte;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            byte[] textByte = utf8.GetBytes(text);
            ICryptoTransform transform = tdes.CreateEncryptor();
            results = transform.TransformFinalBlock(textByte, 0, textByte.Length);
            return Convert.ToBase64String(results, 0, results.Length);
        }

        //create decryption class
        public static string Decrypt(string text, string key)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            //rfc2898
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] keyByte = md5.ComputeHash(utf8.GetBytes(key));
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyByte;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            byte[] textByte = Convert.FromBase64String(text);
            ICryptoTransform transform = tdes.CreateDecryptor();
            results = transform.TransformFinalBlock(textByte, 0, textByte.Length);
            return utf8.GetString(results);
        }

        private void siticoneGradientButton2_Click(object sender, EventArgs e)
        {
            try
            {
                //Decrypt
                string cipherText = richTextBox1.Text;
                string passPhrase = bunifuTextBox2.Text;
                string plainText = Decrypt(cipherText, passPhrase);
                richTextBox1.Text = plainText;
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void siticoneGradientButton3_Click(object sender, EventArgs e)
        {
            //save richtextbox to settings
            //MessageBox with yes and no
            if (MessageBox.Show("Do you want to save the changes?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //save
                Properties.Settings.Default.richTextBox1 = richTextBox1.Text;
                Properties.Settings.Default.bunifuTextBox1 = bunifuTextBox1.Text;
                Properties.Settings.Default.bunifuTextBox2 = bunifuTextBox2.Text;
                Properties.Settings.Default.Save();
            }
            else
            {
                //dont save
            }

        }

        private void siticoneGradientButton4_Click(object sender, EventArgs e)
        {
            //load richtextbox from settings
            if (MessageBox.Show("Do you want load changes?", "Load", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //save
                richTextBox1.Text = Properties.Settings.Default.richTextBox1;
                bunifuTextBox1.Text = Properties.Settings.Default.bunifuTextBox1;
                bunifuTextBox2.Text = Properties.Settings.Default.bunifuTextBox2;
            }
            else
            {
                //dont save
            }

        }
    }


}
