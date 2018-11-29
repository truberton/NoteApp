using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace DatabaseExample
{
    [Activity(Label = "Note")]
    public class NoteActivity : AppCompatActivity
    {
        public static int noteIndex { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.layout1);
            //var weather = Core.Weathers.weathers[Core.Weathers.chosenDay];
            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            var notes = databaseService.GetAllNotes().ToList();
            noteIndex = ChosenNote.Index;

            FindViewById<TextView>(Resource.Id.textView1).Text = notes[ChosenNote.Index].Title;
            FindViewById<TextView>(Resource.Id.textView2).Text = notes[ChosenNote.Index].Content;

            var edit = FindViewById<Button>(Resource.Id.button1);
            var delete = FindViewById<Button>(Resource.Id.button2);

            edit.Click += delegate
            {
                string content = FindViewById<EditText>(Resource.Id.editText1).Text;
                FindViewById<TextView>(Resource.Id.textView2).Text = content;
                databaseService.EditContent(notes[noteIndex].Id, content, notes[noteIndex].Title);
            };
            delete.Click += Delete_Click;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            databaseService.DeleteNote(noteIndex);
        }
    }
}