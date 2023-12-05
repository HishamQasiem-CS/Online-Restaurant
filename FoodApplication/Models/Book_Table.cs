namespace FoodApplication.Models
{
    public class Book_Table
    {
        public int Book_TableId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int OfPepole { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }

        public bool IsDone { get; set; }
    }
}
