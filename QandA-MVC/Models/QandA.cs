using System.ComponentModel.DataAnnotations;

namespace QandA_MVC.Models
{
    public class QandA
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Questions { get; set; }

        public string Answer { get; set; }
    }
}
