using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataTransferObjects
{
     public class MeetupForCreationDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Title can't be longer than 50 characters")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "Date of Meet is required")]
        public DateTime DateOfMeet { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, ErrorMessage = "Description cannot be longer than 100 characters")]
        public string? Desc { get; set; }
        [StringLength(50, ErrorMessage = "Tag cannot be longer than 50 characters")]
        public string? Tag { get; set; }
    }
}
