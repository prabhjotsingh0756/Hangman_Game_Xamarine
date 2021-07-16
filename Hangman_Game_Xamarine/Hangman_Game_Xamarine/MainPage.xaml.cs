using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hangman_Game_Xamarine
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        List<Button> buttons;
        List<ImageSource> images;
        List<Label> fieldChar;
        string word;
        int counterMiss = 0;
        public MainPage()
        {
            InitializeComponent();
            images = new List<ImageSource>();
            LoadImage();
        }

        private void BT_Click_NewGAME(object sender, EventArgs e)
        {
            DoWordArea();
        }

        private void LoadImage()
        {
            for (int i = 0; i < 10; i++)
            {
                ImageSource image = ImageSource.FromFile("hangman" + i.ToString() + ".png");
                images.Add(image);
            }
        }

        private void CreateKeyBoard()
        {
            buttons = new List<Button>();
            firstRow.Children.Clear();
            secondRow.Children.Clear();
            thirdRow.Children.Clear();
            for (int i = 65; i < 91; i++)//Ascii [A-Z]
            {
                Button button = new Button()
                {
                    Text=((char)i).ToString(),
                    FontSize=22,
                    WidthRequest=40,
                    HeightRequest=50
                };
                button.Clicked += BT_Click_Key;
                if (i % 65 < 8) firstRow.Children.Add(button);
                else if (i % 65 >= 8 && i % 65 < 17) secondRow.Children.Add(button);
                else thirdRow.Children.Add(button);
                buttons.Add(button);
            }
        }

        private void BT_Click_Key(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string character = button.Text.ToString();
            bool hit = false;
            for (int i = 0; i < this.word.Length-1; i++)
            {
                if (this.word[i].ToString().ToLower() == character.ToLower())
                {
                    hit = true;
                    fieldChar[i].Text = character.ToLower();
                }
            }
            if (hit == false)
            {
                counterMiss += 1;
                ImageMiss.Source = images[counterMiss];
            }
            if (counterMiss == 9)
            {
                MessageToUser("You Lose");
            }
            int count = 0;
            for (int i = 0; i < this.word.Length; i++)
            {
                if (fieldChar[i].Text != "-")
                {
                    count++;
                }
            }
            if (count==this.word.Length)
            {
                MessageToUser("You Win");
            }
            button.IsEnabled = false;
        }

        private void MessageToUser(string v)
        {
            DisplayAlert(v,v, "Play Again");
            DoWordArea();
        }

        private string RandomWord()
        {
            string[] words = {"dog","cat","apple","banana","house","elephant" };
            Random random = new Random();
            return words[random.Next(words.Length)];
        }

        private void DoWordArea()
        {
            counterMiss = 0;
            CreateKeyBoard();
            this.word = RandomWord();
            ImageMiss.Source = images[0];
            fieldChar = new List<Label>();
            WordArea.Children.Clear();
            for (int i = 0; i < this.word.Length; i++)
            {
                Label label = new Label()
                {
                    Text="-",
                    Margin=new Thickness(10),
                    FontSize=50,
                    TextColor=Color.White
                };
                WordArea.Children.Add(label);
                fieldChar.Add(label);
            }
            fieldChar[0].Text = this.word[0].ToString();
            fieldChar[this.word.Length - 1].Text = this.word[this.word.Length - 1].ToString();

        }
    }
}
