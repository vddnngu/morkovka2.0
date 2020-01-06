using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MorkovkaAPI
{
    public partial class TeacherForm : Form
    {
        Label mainTextLable;
        TeacherGUI myGUI;
        List<Button> buttonsForRemove = new List<Button>();
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
                this.Controls.Add(buts[i]);
                buttonsForRemove.Add(buts[i]);
            }
        }


        private void TeacherForm_Load(object sender, EventArgs e)
        {

        }

        private void изФайлаToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Test files(*.test)|*.test";
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                path = OPF.FileName;
                TestParser parser = new TestParser(path);
                parser.Parse();
                TestProcessing game = new TestProcessing(parser.getRootLink());
                myGUI = new TeacherGUI(this, game);
                myGUI.start();
            }
        }
    }
}