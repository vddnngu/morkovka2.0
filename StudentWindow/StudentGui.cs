using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Morkovka
{
    class StudentGUI
    {
        Form1 form;
        TestProcessing game;
        public StudentGUI(Form1 f, TestProcessing _game)
        {
            form = f;
            game = _game;
        }
        public void start()
        {
            displayLink();
        }
        public void goNext(string answer)
        {
            bool nextType = game.goNext(answer);
            displayLink();
        }
        void displayLink()
        {
            form.setMainText(game.getCurLinkText());
            createButtons();

        }
        Button createButton(string name, string text, Link link)
        {
            Button newbtn = new Button();
            newbtn.Name = name;
            newbtn.Text = text;
            newbtn.UseVisualStyleBackColor = true;
            newbtn.Click += new EventHandler(newbtn_Click);
            return newbtn;
        }
        void newbtn_Click(object sender, EventArgs e)
        {
            this.goNext((sender as Button).Text);
        }
        void createButtons()
        {
            if (game.curLinkIsQuestion())
            {
                var links = game.getLinks();
                var answers = game.getAnswers();
                List<Button> buts = new List<Button>();
                for (int i = 0; i < answers.Count; i++)
                {
                    buts.Add(createButton("but_" + i, answers[i], links[i]));
                }
                form.addButtons(buts);
            }
            else
            {
                form.removeButtons();
            }
        }
    }
}
