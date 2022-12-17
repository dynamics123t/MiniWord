using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tan_MiniWord
{
    public partial class Form2 : Form
    {
        RichTextBox richtb = new RichTextBox();
        Form1 form1 = new Form1();
        public Form2(RichTextBox richText, Form1 form1)
        {
            InitializeComponent();
            richtb = richText;
            this.form1 = form1;
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (txtFind.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập từ cần tìm !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Find(richtb, txtFind.Text.ToString());
            }
        }
        private int Find(RichTextBox richText, string text)
        {
            RichTextBoxFinds options = new RichTextBoxFinds();
            int index = new int();
                options |= RichTextBoxFinds.Reverse;
                index = richText.Find(text, richText.SelectionStart + richText.SelectionLength, options);
            if (index >= 0)
            {
                richText.SelectionStart = index;
                richText.SelectionLength = text.Length;
                form1.Focus();
            }
            else
            {
                MessageBox.Show("Không tìm thấy từ cần tìm !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return index;
        }
        private void btnReplace_Click(object sender, EventArgs e)
        {
            int i = Find(richtb, txtFind.Text);
            if (txtFind.Text == "")
                MessageBox.Show("Vui lòng nhập từ cần tìm !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (txtReplace.Text == "")
                MessageBox.Show("Vui lòng nhập từ cần thay thế !", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (i != 0)
                richtb.Text = richtb.Text.Replace(txtFind.Text, txtReplace.Text);
        }

        private void btnCalcel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
