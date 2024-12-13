using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTH
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeComboBox();
            SetDefaultValues();
            // Thêm các sự kiện xử lý khi thay đổi lựa chọn của ComboBox
            cmbFonts.SelectedIndexChanged += cmbFonts_SelectedIndexChanged;
            cmbSizes.SelectedIndexChanged += cmbSizes_SelectedIndexChanged;
        }

        // Tạo dữ liệu cho ComboBox Font và Size
        private void InitializeComboBox()
        {
            foreach (FontFamily font in new System.Drawing.Text.InstalledFontCollection().Families)
            {
                cmbFonts.Items.Add(font.Name);
            }
            cmbSizes.Items.AddRange(new object[] { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 });
        }

        // Thiết lập giá trị mặc định
        private void SetDefaultValues()
        {
            cmbFonts.SelectedItem = "Tahoma";
            cmbSizes.SelectedItem = 14;
            richText.Font = new Font("Tahoma", 14);
        }

        // Tạo Văn Bản Mới
        private void NewDocument()
        {
            richText.Clear();
            SetDefaultValues();
        }

        // Liên kết với menu Tạo văn bản mới và nút New:
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void tạoVănBảnMớiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        // Mở Tập Tin
        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Text Files|*.txt;*.rtf",
                Title = "Open File"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                richText.LoadFile(openFileDialog.FileName, RichTextBoxStreamType.RichText);
            }
        }

        // Liên kết với menu Mở tập tin và nút Open:
        private void mởTệpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        // Lưu Tập Tin
        private string currentFilePath = null;

        private void SaveFile()
        {
            if (currentFilePath == null) // Nếu chưa lưu lần nào
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Filter = "Rich Text Format|*.rtf",
                    Title = "Save File"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentFilePath = saveFileDialog.FileName;
                    richText.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                    MessageBox.Show("File saved successfully!");
                }
            }
            else // Lưu lại file đã mở trước đó
            {
                richText.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                MessageBox.Show("File saved successfully!");
            }
        }

        // Liên kết với menu Lưu tập tin và nút Save:
        private void lưuNộiDungVănBảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        // Định Dạng Văn Bản
        private void FormatText()
        {
            FontDialog fontDlg = new FontDialog
            {
                ShowColor = true,
                ShowApply = true,
                ShowEffects = true,
                ShowHelp = true
            };

            if (fontDlg.ShowDialog() != DialogResult.Cancel)
            {
                richText.ForeColor = fontDlg.Color;
                richText.Font = fontDlg.Font;
            }
        }

        // Liên kết với menu Định dạng văn bản:
        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormatText();
        }

        // In Đậm, Nghiêng, Gạch Dưới
        private void ToggleBold()
        {
            var currentFont = richText.SelectionFont;
            if (currentFont != null)
            {
                FontStyle newStyle = currentFont.Style ^ FontStyle.Bold;
                richText.SelectionFont = new Font(currentFont, newStyle);
            }
        }

        private void ToggleItalic()
        {
            var currentFont = richText.SelectionFont;
            if (currentFont != null)
            {
                FontStyle newStyle = currentFont.Style ^ FontStyle.Italic;
                richText.SelectionFont = new Font(currentFont, newStyle);
            }
        }

        private void ToggleUnderline()
        {
            var currentFont = richText.SelectionFont;
            if (currentFont != null)
            {
                FontStyle newStyle = currentFont.Style ^ FontStyle.Underline;
                richText.SelectionFont = new Font(currentFont, newStyle);
            }
        }

        // Liên kết với các nút Bold, Italic, Underline:
        private void btnBold_Click(object sender, EventArgs e)
        {
            ToggleBold();
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            ToggleItalic();
        }

        private void btnUnderline_Click(object sender, EventArgs e)
        {
            ToggleUnderline();
        }

        // Sự kiện xử lý thay đổi font chữ
        private void cmbFonts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbFonts.SelectedItem != null)
            {
                string selectedFont = cmbFonts.SelectedItem.ToString();
                float currentSize = richText.SelectionFont.Size;
                richText.SelectionFont = new Font(selectedFont, currentSize);
            }
        }

        // Sự kiện xử lý thay đổi kích thước chữ
        private void cmbSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSizes.SelectedItem != null)
            {
                string selectedFont = richText.SelectionFont.Name;
                float selectedSize = Convert.ToSingle(cmbSizes.SelectedItem);
                richText.SelectionFont = new Font(selectedFont, selectedSize);
            }
        }

        
    }
}
