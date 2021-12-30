using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Models
{
    public enum MovieType
    {
        Action, Comedy, Drama, Thriller, Fantasy, Horror, Mystery, Romance
    }

    [Table("Movies")]
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Required]
        public int? Price { get; set; }
        [Required]
        public TimeSpan Length { get; set; }
        [Required]
        public MovieType Type { get; set; }

        [NotMapped]
        [JsonIgnore]
        public virtual Room Room { get; set; }
        
        //fk?
        public int RoomId { get; set; }
    }
}
