using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Tan_MiniWord
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FontFamily[] families = FontFamily.Families;
            foreach(FontFamily family in families)
                cbbFont.Items.Add( family.Name);
        }
        private string fontName = "Times New Roman";
        float fontSize = 15;
        private void btnColor_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            c.ShowDialog();
            richTb.SelectionColor = c.Color;
        }

        private void btnNenChu_Click(object sender, EventArgs e)
        {
            ColorDialog mauNen = new ColorDialog();
            mauNen.ShowDialog();
            richTb.SelectionBackColor = mauNen.Color;
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            richTb.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void btnCenter_Click(object sender, EventArgs e)
        {
            richTb.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            richTb.SelectionAlignment = HorizontalAlignment.Right;
        }

        private void cbbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            fontName = cbbFont.SelectedItem.ToString();
            richTb.SelectionFont = new Font(fontName, fontSize);
        }

        private void cbbFontSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            fontSize = float.Parse(cbbFontSize.SelectedItem.ToString());
            richTb.SelectionFont = new Font(fontName, fontSize);
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            if (richTb.SelectionLength > 0)
                richTb.Cut();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (richTb.SelectionLength > 0)
                richTb.Copy();
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Text))
                richTb.Paste();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            richTb.Undo();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            richTb.Redo();
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog chenAnh = new OpenFileDialog();
            chenAnh.Filter = "Image|*.bmp;*.jpg;*.gif;*.png;*.tif";
            chenAnh.ShowDialog();
            string path = chenAnh.FileName;
            if (path != "")
            {
                Clipboard.SetImage(Image.FromFile(path));
                richTb.Paste();
            }
        }
        public float zoomText = 1;
        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            if (zoomText >= 64)
            {
                return;
            }
            else
            {
                richTb.ZoomFactor = zoomText;
                zoomText += 2;
            }
        }

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            if (zoomText <= 1)
            {
                return;
            }
            else
            {
                richTb.ZoomFactor = zoomText;
                zoomText -= 2;
            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        bool Ktra = false;
        String path = "";
        private bool checkFile = false;
        private string pathS = "";
        public void newFile()
        {
            richTb.Text = String.Empty;
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            checkFile = false;
            pathS = "";
            richTb.Visible = true;
            newFile();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            listView1.Visible = false;
            richTb.Visible = false;
            checkFile = false;
            pathS = "";
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkFile = false;
            pathS = "";
            richTb.Visible = true;
            newFile();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFile();
        }
        public void openFile()
        {
            OpenFileDialog mo = new OpenFileDialog();
            mo.ShowDialog();
            path = mo.FileName;
            if (path != "")
            {
                richTb.LoadFile(path);
                checkFile = true;
                pathS = path;
            }

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (checkFile)
            {
                richTb.SaveFile(pathS);
            }
            else
            {
                saveFile();
            }
        }
        public void saveFile()
        {
            SaveFileDialog luu = new SaveFileDialog();
            luu.Filter = "Save File (*.rtf)|*.rtf| (*.txt)|*.txt";

            if (luu.ShowDialog() == DialogResult.OK)
            {


                path = luu.FileName;
                if (path != "")
                {
                    richTb.SaveFile(path);
                    checkFile = true;
                    pathS = path;
                }
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
                saveFile();
        }
        private int id = 0;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listView1.SelectedIndices.Count <= 0) return;
                if (listView1.FocusedItem == null) return;
                id = listView1.SelectedIndices[0];
                if (id < 0) return;
                Clipboard.SetImage(imageList1.Images[id]);
                richTb.Paste();
                listView1.Visible = false;
            }
            catch
            {

            }

        }


        private void btnIcon_Click_1(object sender, EventArgs e)
        {
            string duongDan = Environment.CurrentDirectory.ToString();
            var url = Directory.GetParent(Directory.GetParent(duongDan).ToString());
            string path = url + @"\Resources";
            string[] files = Directory.GetFiles(path);
            foreach (String f in files)
            {
                Image img = Image.FromFile(f);
                imageList1.Images.Add(img);
            }

            this.listView1.LargeImageList = this.imageList1;

            for (int i = 0; i < this.imageList1.Images.Count; i++)
            {
                this.listView1.Items.Add(" ", i);
            }
        }

        private void listView1_MouseLeave(object sender, EventArgs e)
        {
            listView1.Visible = false;
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(richTb, this);
            form2.Show();
        }
    }
}
