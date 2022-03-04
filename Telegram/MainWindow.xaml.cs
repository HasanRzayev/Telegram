
using AIMLbot;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Telegram
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        class Human
        {
            public Human(string name, DateTime time, List<string> data)
            {
                this.name = name;
                this.time = time;
                this.data = data;
            }

            public string name { get; set; }
            public DateTime time { get; set; }
            public List<string> data { get; set; }

        }
        public MainWindow()
        {
            InitializeComponent();
        }
        List<Human> contact = new List<Human>();
        string gelenmesaj;
        string qarsiinsan;
        private void gonder_Click(object sender, RoutedEventArgs e)
        {
            mesaj.Text = null;
            Label gonderiren=new Label();
            Color c = Colors.Cyan;
            

            gonderiren.Background=new SolidColorBrush(c);
            gonderiren.Content = $"You:{mesaj.Text}";
          
            Chat.HorizontalAlignment = HorizontalAlignment.Stretch;
            gonderiren.HorizontalContentAlignment= HorizontalAlignment.Right;
            
            Chat.Items.Add(gonderiren);
            //////////////////////////////////////////////////////////////////
            Bot AI = new Bot();

            AI.loadSettings(); 

            AI.loadAIMLFromFiles(); 

            AI.isAcceptingUserInput = false; 

            User myUser = new User("Username", AI);

            AI.isAcceptingUserInput = true; 

           

                Request r = new Request(mesaj.Text, myUser, AI);

                Result res = AI.Chat(r);

                 gelenmesaj= res.Output;

            
            Label GELEN = new Label();
            GELEN.Content =gelenmesaj ;
            Chat.HorizontalAlignment = HorizontalAlignment.Stretch;
            GELEN.HorizontalContentAlignment = HorizontalAlignment.Left;



            GELEN.Background = new SolidColorBrush(c);
            Chat.Items.Add(GELEN);
            ///////////////////////////////////////////////////////////////////////////////////
            //foreach (Human human in contact)
            //{
            //    if ((human as Human).name == qarsiinsan && Chat.Items!=null)
            //    {
            //        foreach (var item in Chat.Items)
            //        {
            //            (human as Human).data.Add((item as Label).Content.ToString());
            //        }
            //    }
            //}
            ///////////////////////////////////////////////////////////////////////////////////////////
            //var contactjson = JsonConvert.SerializeObject(contact, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("contact.json", contactjson);

        }

        private void Chat_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
       
          
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            qarsiinsan = (sender as Button).Content.ToString();
            foreach (Human human in contact)
            {
                if ((human as Human).name == qarsiinsan  && human.data!=null)
                {
                    foreach (var item in human.data)
                    {
                        Label gonderiren = new Label();
                        Color c = Colors.Cyan;


                        gonderiren.Background = new SolidColorBrush(c);
                        gonderiren.Content= item;
                        Chat.Items.Add(gonderiren);

                    }
                }
            }
           
           

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Chat.Items.Clear();
            if (File.Exists("contact.json") == true)
            {
                var dialog = File.ReadAllText($"contact.json");
               
                contact = JsonConvert.DeserializeObject<List<Human>>(dialog);
            }
            else
            {
                contact.Add(new Human("Admistration bot", DateTime.Today, null));
                contact.Add(new Human("Cristiano Ronaldo", DateTime.Today, null));
                contact.Add(new Human("angelina jolie", DateTime.Today, null));
            }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
           
         
        }
    }
}
