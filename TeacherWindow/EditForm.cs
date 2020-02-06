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

    struct EditAnswer
    {
        public String text;
        public Link link;
        public bool needToAdd;
        public Tuple<TextBox, Button> GUIElem;
        public EditAnswer (String _text, Link _link, bool _needToAdd, Tuple<TextBox, Button> _GUIElem)
        {
            text = _text;
            link = _link;
            needToAdd = _needToAdd;
            GUIElem = _GUIElem;
        }
    }
    public partial class EditForm : Form
    {
        Link currentLink;
        Context context;
        List<EditAnswer> answerList;
        TeacherGUI mainGUI;
        
        public EditForm(Context _context, TeacherGUI _mainGUI)
        {
            InitializeComponent();
            context = _context;
            mainGUI = _mainGUI;
            answerList = new List<EditAnswer>();
        }
        public void fillAnswerList()
        {

            answerList = new List<EditAnswer>();
            List<String> answers = (currentLink as Question).getAnswers();

            for (int i = 0; i < answers.Count; i++)
            {
                TextBox tmp = new TextBox();
                tmp.Location = new Point(16, 97 + 26 * i);
                tmp.Size = new Size(256, 20);
                tmp.Text = answers[i];
                Button tmp2 = new Button();
                tmp2.Location = new Point(298, 97 + 26 * i);
                tmp2.Size = new Size(71, 19);
                tmp2.Text = "DELETE";
                tmp2.Click += new EventHandler(delAnswers);
                answerList.Add(new EditAnswer(answers[i], (currentLink as Question).getNext(answers[i]), false, new Tuple<TextBox, Button>(tmp, tmp2)));
            }
            displayAnswersList();
        }

        private void correctAnswerList()
        {
            List<EditAnswer> tmpList = new List<EditAnswer>(answerList);
            answerList = new List<EditAnswer>();
            

            for (int i = 0; i < tmpList.Count; i++)
            {
                TextBox tmp = new TextBox();
                tmp.Location = new Point(16, 97 + 26 * i);
                tmp.Size = new Size(256, 20);
                tmp.Text = tmpList[i].text;
                Button tmp2 = new Button();
                tmp2.Location = new Point(298, 97 + 26 * i);
                tmp2.Size = new Size(71, 19);
                tmp2.Text = "DELETE";
                tmp2.Click += new EventHandler(delAnswers);
                answerList.Add(new EditAnswer(tmpList[i].text, tmpList[i].link, tmpList[i].needToAdd, new Tuple<TextBox, Button>(tmp, tmp2)));
            }
        }

        private void displayAnswersList()
        {
            if (answerList.Count != 0)
            {
                for (int i = 0; i < answerList.Count; i++)
                {
                    Controls.Remove(answerList[i].GUIElem.Item1);
                    Controls.Remove(answerList[i].GUIElem.Item2);
                }
            }
            for (int i = 0; i < answerList.Count; i++)
            {
                this.Controls.Add(answerList[i].GUIElem.Item1);
                this.Controls.Add(answerList[i].GUIElem.Item2);
            }
        }


        private void delAnswers(object sender, EventArgs e)
        {

            if (MessageBox.Show("Вы уверенны? Если вы удалите ответ, вернуть его не получиться. Только создать заново! ",
                "Удаление ответа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            for (int i=0; i<answerList.Count; i++)
            {
                if (answerList[i].GUIElem.Item2 == (sender as Button))
                {
                    Controls.Remove(answerList[i].GUIElem.Item1);
                    Controls.Remove(answerList[i].GUIElem.Item2);
                    answerList.RemoveAt(i);
                }
            }
            correctAnswerList();
            displayAnswersList();
        }

        public void setCurrentLink(Link link)
        {
            currentLink = link;
            richTextBox1.Text = currentLink.getText();
            if (currentLink.isQuestion() == true)
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }
        }
        private void EditForm_Load(object sender, EventArgs e)
        {
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                if (currentLink.isQuestion())
                {
                    if (context.prevLink != null)
                        (context.prevLink as Question).removeAnswer(context.answerText);
                    currentLink = new Answer(richTextBox1.Text);
                    if (context.prevLink != null)
                        (context.prevLink as Question).addAnswer(context.answerText, currentLink);
                    mainGUI.renewCurLink(currentLink);
                }
                if(answerList!=null)
                    for (int i = 0; i < answerList.Count(); i++)
                    {
                        this.Controls.Remove(answerList[i].GUIElem.Item1);
                        this.Controls.Remove(answerList[i].GUIElem.Item2);
                    }
                this.Controls.Remove(button1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                int i = answerList.Count();
                TextBox tmp = new TextBox();
                tmp.Location = new Point(16, 97 + 26 * i);
                tmp.Size = new Size(256, 20);
                tmp.Text = "Ответ " + (i+1);

                Button tmp2 = new Button();
                tmp2.Location = new Point(298, 97 + 26 * i);
                tmp2.Size = new Size(71, 19);
                tmp2.Text = "DELETE";
                tmp2.Click += new EventHandler(delAnswers);

                answerList.Add(new EditAnswer("Ответ "+(i+1), new Answer("Текст\n"), true, new Tuple<TextBox, Button>(tmp, tmp2)));
                this.Controls.Add(tmp);
                this.Controls.Add(tmp2);
            }
            //if (radioButton1.Checked == true)
            //{
            //    (currentLink as Answer).setText(richTextBox1.Text);
            //    Label save = new Label();
            //    save.Text = "Сохранено";
            //    save.Font = new Font("Arial", 18, FontStyle.Regular);
            //    save.Width = 200;
            //    save.Location = new Point(16, 120);
            //    this.Controls.Add(save);
            //}


        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = richTextBox1.Text;
            if (text[text.Length-1] != '\n') text += '\n';
            currentLink.setText(text);
            if (currentLink.isQuestion() == true)
            {
                for (int i = 0; i < answerList.Count; i++)
                {
                    if(answerList[i].needToAdd)
                    {
                        (currentLink as Question).addAnswer(answerList[i].GUIElem.Item1.Text, answerList[i].link);
                    }
                    else
                    {
                        if (answerList[i].text != answerList[i].GUIElem.Item1.Text)
                        {
                            (currentLink as Question).replaceAnswerText(answerList[i].text, answerList[i].GUIElem.Item1.Text);
                        }
                    }
                }

            }
            mainGUI.renewCurLink(currentLink);
            Close();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                if(!currentLink.isQuestion())
                {
                    if (context.prevLink != null)
                        (context.prevLink as Question).removeAnswer(context.answerText);
                    currentLink = new Question(richTextBox1.Text);
                    if (context.prevLink != null)
                        (context.prevLink as Question).addAnswer(context.answerText, currentLink);
                    for (int i = 0; i < answerList.Count; i++)
                    {
                        (currentLink as Question).addAnswer(answerList[i].text, answerList[i].link);
                    }
                }
                fillAnswerList();
                this.Controls.Add(button1);
            }
            
        }
    }
}
