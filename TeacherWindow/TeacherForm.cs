using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeacherWindow;

namespace MorkovkaAPI
{
    public partial class TeacherForm : Form
    {
        Label mainTextLable;
        string fileName;
        TeacherGUI myGUI;
        Button editBut;
        List<Button> buttonsForRemove = new List<Button>();
        Link mainLink;

        int j = 0;
        public TeacherForm()
        {
            InitializeComponent();
            mainTextLable = new Label();
            mainTextLable.Location = new Point((int)(Width * 0.07), (int)(Height / 3));
            mainTextLable.AutoSize = true;
            mainTextLable.MaximumSize = new Size(700, 0);
            mainTextLable.Font = new Font(mainTextLable.Font.Name, 20, mainTextLable.Font.Style);
            Controls.Add(mainTextLable);
            editBut = new Button();
            editBut.Location = new Point(Width - 160, Height - 100);
            editBut.Click += new EventHandler(editBut_Click);
            editBut.Text = "EDIT";
            editBut.Visible = false;
            this.Controls.Add(editBut);

        }

        internal void setMainText(string text)
        {
            //TODO добавить поддержку длинных строк (динамическое изменение размера шрифта)
            var tmp = text.Split('\n');
            var cn = tmp.Length;
            mainTextLable.Location = new Point((int)(Width * 0.07), (int)(Height / 3 - 15 * (cn - 1)));
            mainTextLable.Text = text;
        }

        internal void removeButtons()
        {
            foreach (var it in buttonsForRemove)
            {
                Controls.Remove(it);
            }
        }
        internal void addButtons(List<Button> buts)
        {
            removeButtons();
            
            var startPoint = new Point((int)(Width * 0.07), (int)(Height / 2));
            int buttonWidth = (int)(Width * 0.2);
            int buttonHeigth = (int)(Height * 0.1);
            for (int i = 0; i < buts.Count; i++)
            {
                buts[i].Location = new Point((int)(startPoint.X + i * buttonWidth), startPoint.Y + (i / 4 + 1) * buttonHeigth);
                buts[i].Width = buttonWidth;
                buts[i].Height = buttonHeigth;
                buts[i].Font = new Font("Arial", 14, FontStyle.Regular);
                this.Controls.Add(buts[i]);
                buttonsForRemove.Add(buts[i]);
            }
            editBut.Visible = true;


        }
        void editBut_Click(object sender, EventArgs e)
        {
            EditForm form = new EditForm();
            form.setCurrentLink(myGUI.getCurrentLink());
            form.Show();

        }
       

        private void TeacherForm_Load(object sender, EventArgs e)
        {
            
        }

        private void изФайлаToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void изФайлаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Test files(*.test)|*.test";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                path = OPF.FileName;
                var strs = path.Split('\\');
                fileName = strs[strs.Length - 1];
                TestParser parser = new TestParser(path);
                parser.Parse();
                TestProcessing game = new TestProcessing(parser.getRootLink());
                mainLink = parser.getRootLink();
                myGUI = new TeacherGUI(this, game);
                myGUI.start();
            }
            pictureBox1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            button1.Location = new Point(10,390);
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path;
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = fileName;
            SFD.Filter = "test (*.test)|*.test";

            if (SFD.ShowDialog() == DialogResult.OK)
            {
                Property prop = new Property();
                prop.addAuthor("NoBody");
                TestWriter testWriter = new TestWriter(mainLink, prop);
                testWriter.Save(SFD.FileName);
            }
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void TeacherForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}