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
    public partial class decryption_of_ciphers : Form
    {
        public decryption_of_ciphers()
        {
            InitializeComponent();
            EnableBlur();
            this.BackColor = System.Drawing.ColorTranslator.FromHtml("#310062");


            richTextBox1.BackColor = Color.FromArgb(255, 254, 254, 254);
            richTextBox1.ForeColor = Color.FromArgb(255, 1, 1, 1);
            bunifuTextBox1.ForeColor = Color.FromArgb(255, 1, 1, 1);
            bunifuTextBox1.BackColor = Color.FromArgb(255, 254, 254, 254);
        }

        async private void OwnEncryption_Load(object sender, EventArgs e)
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
            await Task.Delay(50);
            bunifuTransition1.ShowSync(siticoneGradientButton2);
            await Task.Delay(50);
            bunifuTransition1.ShowSync(siticoneGradientButton3);
            await Task.Delay(50);
            bunifuTransition1.ShowSync(richTextBox1);
            await Task.Delay(50);
            bunifuTransition1.ShowSync(label1);
            await Task.Delay(50);
            bunifuTransition1.ShowSync(siticoneRoundedComboBox1);
        }
        
        char[] characters = new char[] { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };
        
        

        
        
        
        
        private string Decode(string input, string keyword)
        {
            input = input.ToUpper();
            keyword = keyword.ToUpper();
            int N = characters.Length;
            string result = "";

            int keyword_index = 0;

            foreach (char symbol in input)
            {
                int p = (Array.IndexOf(characters, symbol) + N -
                    Array.IndexOf(characters, keyword[keyword_index])) % N;

                result += characters[p];

                keyword_index++;

                if ((keyword_index + 1) == keyword.Length)
                    keyword_index = 0;
            }

            return result;
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

        private void siticoneGradientButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (siticoneRoundedComboBox1.Text == "Цезарь")
                {
                    string text = richTextBox1.Text;
                    string result = "";
                    for (int i = 0; i < text.Length; i++)
                    {
                        result += (char)(text[i] - Convert.ToInt32(bunifuTextBox1.Text));
                    }
                    richTextBox1.Text = result;
                }
                else if (siticoneRoundedComboBox1.Text == "Гронсфельд(англ)")
                {
                    string key = bunifuTextBox1.Text, text = richTextBox1.Text;
                    string abc = "abcdefghijklmnopqrstuvwxyz", newKey = key, result = "";
                    bool encode = false;
                    int op = encode ? +1 : -1, offset, indexOf = 0;
                    while (newKey.Length < text.Length)
                    {
                        newKey += key;
                    }
                    if (newKey.Length > text.Length)
                    {
                        newKey = newKey.Substring(0, newKey.Length - (newKey.Length - text.Length));
                    }

                    for (int i = 0; i < text.Length; i++)
                    {
                        indexOf = abc.IndexOf(text[i]);
                        if (indexOf != -1)
                        {
                            offset = abc.IndexOf(text[i]) + (Convert.ToInt32(newKey[i]) - 48) * op;
                            if (offset > abc.Length)
                                offset = offset - abc.Length;
                            else if (offset < 0)
                                offset = abc.Length + offset;
                            result += abc[offset];
                        }
                        else
                            result += text[i];
                    }
                    richTextBox1.Text = result;


                }

                else if (siticoneRoundedComboBox1.Text == "Гронсфельд(русс)")
                {
                    //дешифровка шифр гронсфельда
                    string key = bunifuTextBox1.Text, text = richTextBox1.Text;
                    string abc = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя", newKey = key, result = "";
                    bool encode = false;
                    int op = encode ? +1 : -1, offset, indexOf = 0;
                    while (newKey.Length < text.Length)
                    {
                        newKey += key;
                    }
                    if (newKey.Length > text.Length)
                    {
                        newKey = newKey.Substring(0, newKey.Length - (newKey.Length - text.Length));
                    }

                    for (int i = 0; i < text.Length; i++)
                    {
                        indexOf = abc.IndexOf(text[i]);
                        if (indexOf != -1)
                        {
                            offset = abc.IndexOf(text[i]) + (Convert.ToInt32(newKey[i]) - 48) * op;
                            if (offset > abc.Length)
                                offset = offset - abc.Length;
                            else if (offset < 0)
                                offset = abc.Length + offset;
                            result += abc[offset];
                        }
                        else
                            result += text[i];
                    }
                    richTextBox1.Text = result;
                    
                    
                }
                else if(siticoneRoundedComboBox1.Text == "Вижинер")
                {
                    richTextBox1.Text = Decode(richTextBox1.Text, bunifuTextBox1.Text);
                   
                }    

            }
            catch
            {
                MessageBox.Show("Ошибка!");
            }
        }

        async private void siticoneRoundedComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (siticoneRoundedComboBox1.Text == "Цезарь")
            {
                bunifuTextBox1.Visible = false;
                await Task.Delay(150);
                bunifuTransition1.ShowSync(bunifuTextBox1);
                bunifuTextBox1.PlaceholderText = "Величина сдвига";
            }
            else if (siticoneRoundedComboBox1.Text == "Гронсфельд(англ)")
            {
                bunifuTextBox1.Visible = false;
                await Task.Delay(150);
                bunifuTransition1.ShowSync(bunifuTextBox1);
                bunifuTextBox1.PlaceholderText = "Ключ";
            }
            else if (siticoneRoundedComboBox1.Text == "Вижинер")
            {
                bunifuTextBox1.Visible = false;
                await Task.Delay(150);
                bunifuTransition1.ShowSync(bunifuTextBox1);
                bunifuTextBox1.PlaceholderText = "Ключ";
            }
            else if (siticoneRoundedComboBox1.Text == "Гронсфельд(русс)")
            {
                bunifuTextBox1.Visible = false;
                await Task.Delay(150);
                bunifuTransition1.ShowSync(bunifuTextBox1);
                bunifuTextBox1.PlaceholderText = "Ключ";
            }
            else if (siticoneRoundedComboBox1.Text == "Вижинер(англ)")
            {
                bunifuTextBox1.Visible = false;
                await Task.Delay(150);
                bunifuTransition1.ShowSync(bunifuTextBox1);
                bunifuTextBox1.PlaceholderText = "Ключ";
            }
            else if (siticoneRoundedComboBox1.Text == "Вижинер(русс)")
            {
                bunifuTextBox1.Visible = false;
                await Task.Delay(150);
                bunifuTransition1.ShowSync(bunifuTextBox1);
                bunifuTextBox1.PlaceholderText = "Ключ";

            }
           
        }
    }
}
