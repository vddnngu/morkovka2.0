using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MorkovkaAPI;

namespace TeacherWindow
{
    public partial class EditForm : Form
    {
        Link currentLink;
        List<Tuple<TextBox, Button>> answerList;
        
        public EditForm()
        {
            InitializeComponent();
            
        }
        public void fillAnswerList()
        {

            List<String> answers = (currentLink as Question).getAnswers();
           answerList = new List<Tuple<TextBox, Button>>();
            for(int i=0; i<answers.Count; i++)
            {
                TextBox tmp = new TextBox();
                tmp.Location = new Point(16, 97 + 26 * i);
                tmp.Size = new Size(256, 20);
                tmp.Text = answers[i];
                Button tmp2 = new Button();
                tmp2.Location = new Point(298, 97 + 26 * i);
                tmp2.Size = new Size(71,19);
                tmp2.Text = "DELETE";
                tmp2.Click += new EventHandler(delAnswers);
                answerList.Add(new Tuple<TextBox, Button>(tmp, tmp2));
                this.Controls.Add(tmp);
                this.Controls.Add(tmp2);
            }
        }
        private void delAnswers(object sender, EventArgs e)
        {
            
        }
        public void setCurrentLink(Link link)
        {
            currentLink = link;
        }
        private void EditForm_Load(object sender, EventArgs e)
        {
            fillAnswerList();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Link link;

            int i = answerList.Count();
            TextBox tmp = new TextBox();
            tmp.Location = new Point(16, 97 + 26 * i);
            tmp.Size = new Size(256, 20);
            tmp.Text = richTextBox1.Text;

            Button tmp2 = new Button();
            tmp2.Location = new Point(298, 97 + 26 * i);
            tmp2.Size = new Size(71, 19);
            tmp2.Text = "DELETE";
            tmp2.Click += new EventHandler(delAnswers);

            answerList.Add(new Tuple<TextBox, Button>(tmp, tmp2));
            (currentLink as Question).addAnswer(richTextBox1.Text,);
            this.Controls.Add(tmp);
            this.Controls.Add(tmp2);
            
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
