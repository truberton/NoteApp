using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace DatabaseExample
{
    public class DatabaseService
    {
        SQLiteConnection db;

        public void CreateDatabase()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "mydatabase.db3");
            db = new SQLiteConnection(dbPath);            
        }

        public void CreateTableWithData()
        {
            db.CreateTable<Note>();
            if (db.Table<Note>().Count() == 0)
            {
                var newNote = new Note();
                newNote.Title = "Title";
                newNote.Content = "This is very good and original content";
                db.Insert(newNote);
            }
        }

        public void AddNote(string title, string content)
        {
            var NewNote = new Note();
            NewNote.Title = title;
            NewNote.Content = content;
            db.Insert(NewNote);
        }

        public void DeleteNote(int key)
        {
            //Don't judge me
            db.Delete(db.Table<Note>().ToList()[key]);
        }

        public void EditContent(int key, string content)
        {
            //TODO
        }

        public TableQuery<Note> GetAllNotes()
        {
            var table = db.Table<Note>();
            return table;
        }
    }
}