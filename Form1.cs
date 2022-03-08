using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Praktika_WinForms_4
{
    public partial class Form1 : Form
    {
        ToolStripMenuItem fileItem = new ToolStripMenuItem("Файл");
        ToolStripMenuItem transform = new ToolStripMenuItem("Преобразовать");
        ToolStripMenuItem openItem = new ToolStripMenuItem("Открыть");
        ToolStripMenuItem saveItem = new ToolStripMenuItem("Сохранить");
        ToolStripMenuItem saveAsItem = new ToolStripMenuItem("Сохранить как");
        ToolStripMenuItem exit = new ToolStripMenuItem("Выход");
        public Form1()
        {
            InitializeComponent();


            fileItem.DropDownItems.Add(openItem);
            fileItem.DropDownItems.Add(saveItem);
            fileItem.DropDownItems.Add(saveAsItem);
            fileItem.DropDownItems.Add(exit);

            menuStrip1.Items.Add(fileItem);
            menuStrip1.Items.Add(transform);
            saveItem.Enabled = false;
            saveAsItem.Enabled = false;

            transform.Click += Transform_Click;
            openItem.Click += openItem_Click;
            saveItem.Click += saveItem_Click;
            saveAsItem.Click += saveAsItem_Click;
            exit.Click += exit_Click;
        }

        private void Transform_Click(object sender, EventArgs e)
        {
            textBox1.Clear();

            string[] sogl = "б в г д ж з й к л м н п р с т ф х ц ч ш щ".Split(' ');
            for (int i = 0; i < fileText.Count; i++)
            {
                string str = fileText[i];
                bool IsUpperCase = false;
                for (int j = 0; j < str.Length; j++)
                {
                    for (int k = 0; k < sogl.Length; k++)
                    {
                        if (str[j].ToString().Contains(sogl[k]))
                        {
                            textBox1.Text += str[j].ToString().ToUpper();
                            IsUpperCase = true;
                            break;
                        }
                        else IsUpperCase = false;
                    }
                    if (!IsUpperCase)
                        textBox1.Text += str[j].ToString();
                }
            }

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        List<string> fileText = new List<string>();
        List<string> fileTextTransformed = new List<string>();
        string filePath = "";
        private void openItem_Click(object sender, EventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();

            ofd.FileName = "Document";
            ofd.DefaultExt = ".txt";
            ofd.Filter = "Текстовый файл. Файл формата .txt|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
            }
            string line;

            using (StreamReader reader = new StreamReader(filePath))
            {
                textBox1.Clear();
                fileText.Clear();
                while ((line = reader.ReadLine()) != null)
                {
                    fileText.Add(line);
                    fileTextTransformed.Add(line);
                    textBox1.Text += line;
                }
            }
            saveItem.Enabled = true;
            saveAsItem.Enabled = true;
        }
        private void saveItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false))//false=перезапись
                {
                    foreach (string item in fileTextTransformed)
                        writer.Write(item);
                }
                MessageBox.Show("Файл сохранен");
            }
            catch
            {
                MessageBox.Show("Ошибка записи");
            }
        }
        private void saveAsItem_Click(object sender, EventArgs e)
        {
            StreamWriter SW;
            SaveFileDialog sfd = new SaveFileDialog();

            if (sfd.ShowDialog() == DialogResult.Cancel)
            {
                MessageBox.Show("Отмена");
                return;
            }
            try
            {
                // получаем выбранный файл
                sfd.DefaultExt = ".txt";
                sfd.Filter = "Текстовый файл.Файл формата .txt | *.txt";
                string filename = sfd.FileName;

                // сохраняем текст в файл
                SW = new StreamWriter(sfd.FileName + ".txt");
                SW.Write(textBox1.Text);
                SW.Close();
                MessageBox.Show("Файл сохранен");
            }
            catch
            {
                MessageBox.Show("Ошибка записи");
            }
        }
        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
