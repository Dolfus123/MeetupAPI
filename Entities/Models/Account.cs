using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("account")]
    public class Account
    {
        [Column ("AccountId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
        [Required(ErrorMessage = "Account type is required")]
        public string? AccountType { get; set; }
        [ForeignKey(nameof(Meetup))]
        public Guid MeetupId { get; set; }
        public Meetup? Meetup { get; set; }
    }
}
