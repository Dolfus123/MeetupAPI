using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("meetup")]
    public class Meetup
    {
        [Column(" MeetupId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title can't be longer than 50 characters")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Date of meet is required")]
        public DateTime DateOfMeet { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters")]
        public string? Desc { get; set; }
        [StringLength(50, ErrorMessage = "Tag cannot be longer than 50 characters")]
        public string? Tag { get; set; }
        public ICollection<Account>? Accounts { get; set; }

    }
}
