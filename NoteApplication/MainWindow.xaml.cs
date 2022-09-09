using NoteApplication.Model;
using NoteApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace NoteApplication
{
    public partial class MainWindow : Window
    {
        private async void ShowErrorMessage()
        {
            AllNotesLabel.Foreground = Brushes.Red;
            AllNotesLabel.Text = "Ошибка";
            await Task.Delay(3000);
            AllNotesLabel.Foreground = Brushes.Wheat;
            AllNotesLabel.Text = "Все прошлые заметки";
        }


        public MainWindow()
        {

            InitializeComponent();

            using (ApplicationContext db = new ApplicationContext())
            {
                var allNotes = db.Note.ToList();
                DataFromBase.ItemsSource = allNotes;
            }


        }

        private async void SaveNoteButton_Click(object sender, RoutedEventArgs e)
        {
            await using (ApplicationContext db = new ApplicationContext())
            {
                if (TitleTextBox.Text != "")
                {
                    Note note = new Note { Text = TextTextBox.Text, Title = TitleTextBox.Text };

                    db.Note.Add(note);
                    db.SaveChanges();

                    var AllNotes = db.Note.ToList();
                    DataFromBase.ItemsSource = AllNotes;
                }
                else
                {
                    ShowErrorMessage();
                }
            }
        }

        private void DeleteTextFromNoteButton_Click(object sender, RoutedEventArgs e)
        {
            TextTextBox.Text = "";
        }

        private void NewNoteButton_Click(object sender, RoutedEventArgs e)
        {
            TitleTextBox.Text = "";
            TextTextBox.Text = "";
        }
    }
}
