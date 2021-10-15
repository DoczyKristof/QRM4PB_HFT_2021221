using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public int RoomNumber { get; set; }

        [NotMapped]
        public virtual Cinema Cinema { get; set; }
        public int CinemaId { get; set; }

        [NotMapped]
        public virtual Movie Movie { get; set; }
    }
}
