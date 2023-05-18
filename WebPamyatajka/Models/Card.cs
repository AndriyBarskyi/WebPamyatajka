using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebPamyatajka.Models;
public class Card
    {
        public int Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Front { get; set; }
        [Required]
        [StringLength(120)]
        public string Back { get; set; }
        [ForeignKey("CategoryId")]
        public int CategoryId{ get; set; }
    }