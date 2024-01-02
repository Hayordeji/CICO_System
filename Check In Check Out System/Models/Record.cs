using System.ComponentModel.DataAnnotations;

namespace Check_In_Check_Out_System.Models
{
    public class Record
    {
        [Key]
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public string CheckIn { get; set; }
        public string? CheckOut { get; set; }
    }
}
