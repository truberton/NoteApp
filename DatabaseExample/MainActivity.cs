using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using System;
using System.IO;

namespace DatabaseExample
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var noteListView = FindViewById<ListView>(Resource.Id.listView1);
            var addNoteEditText = FindViewById<EditText>(Resource.Id.editText1);
            var addButton = FindViewById<Button>(Resource.Id.button1);

            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            databaseService.CreateTableWithData();
            var notes = databaseService.GetAllNotes();

            noteListView.Adapter = new CustomAdapter(this, notes.ToList());

            var list = FindViewById<ListView>(Resource.Id.listView1);

            addButton.Click += delegate
            {
                var noteName = addNoteEditText.Text;
                databaseService.AddNote(noteName, noteName);

                notes = databaseService.GetAllNotes();
                noteListView.Adapter = new CustomAdapter(this, notes.ToList());
            };

            list.ItemClick += list_Click;
        }

        private void list_Click(object sender, AdapterView.ItemClickEventArgs e)
        {
            ChosenNote.Index = e.Position;
            var certainNote = new Intent(this, typeof(NoteActivity));
            StartActivity(certainNote);
            //Resets the notes list to current after exiting activity, but i didn't know how to do it so i just threw it here
            var noteListView = FindViewById<ListView>(Resource.Id.listView1);
            var databaseService = new DatabaseService();
            databaseService.CreateDatabase();
            databaseService.CreateTableWithData();
            var notes = databaseService.GetAllNotes();

            noteListView.Adapter = new CustomAdapter(this, notes.ToList());
        }
    }
}