using SQLite;

namespace DatabaseExample
{
    public class Note
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [MaxLength(64)]
        public string Title { get; set; }
        [MaxLength(256)]
        public string Content { get; set; }

    }
}