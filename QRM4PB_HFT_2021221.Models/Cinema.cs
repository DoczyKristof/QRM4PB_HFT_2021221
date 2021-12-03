using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Models
{
    [Table("Cinemas")]
    public class Cinema
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [NotMapped]
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual Room Room { get; set; }

        public Cinema()
        {
            Rooms = new HashSet<Room>();
        }

    }
}
