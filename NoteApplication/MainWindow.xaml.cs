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

        private class Item
        {
            public int Id { get; set; }
            public string Title { get; set; }
        }


        public MainWindow()
        {

            InitializeComponent();
            
            using (ApplicationContext db = new ApplicationContext())
            {
                //исправить на правильное заполнение, так как тратит много
                var allNotes = db.Note.ToList();
                foreach(var i in allNotes)
                {
                    DataFromBase.Items.Add(new Item {Id = i.Id, Title = i.Title});
                }
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

                    //исправить на правильное заполнение, так как тратит много
                    var allNotes = db.Note.ToList();
                    foreach (var i in allNotes)
                    {
                        DataFromBase.Items.Add(new Item { Id = i.Id, Title = i.Title });
                    }
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

        private async void DeleteNoteButton_Click(object sender, RoutedEventArgs e)
        {
            AcceptWindow acceptWindow = new AcceptWindow();

            if (acceptWindow.ShowDialog() == true)
            {
                await using (ApplicationContext db = new ApplicationContext())
                {
                    var infoAboutSelectedCell = (Item)DataFromBase.SelectedItem;

                    var forRemove = db.Note.FirstOrDefault(p => p.Id == infoAboutSelectedCell.Id);
                    if (forRemove != null)
                    {
                        db.Remove(forRemove);
                        db.SaveChanges();
                    }


                    //исправить на правильное заполнение, так как тратит много
                    var allNotes = db.Note.ToList();
                    foreach (var i in allNotes)
                    {
                        DataFromBase.Items.Add(new Item { Id = i.Id, Title = i.Title });
                    }
                }
            }
        }

        private async void DataFromBase_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            await using (ApplicationContext db = new ApplicationContext())
            {
                var infoAboutSelectedCell = (Item)DataFromBase.SelectedItem;

                var showNote = db.Note.FirstOrDefault(p => p.Id == infoAboutSelectedCell.Id);
                if (showNote != null)
                {
                    TitleTextBox.Text = showNote.Title;
                    TextTextBox.Text = showNote.Text;
                }
               
            }
        }
    }
}
