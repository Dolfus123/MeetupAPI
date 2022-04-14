using System;

namespace Entities.DataTransferObjects
{
    public class MeetupDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public DateTime DateOfMeet { get; set; }
        public string? Desc { get; set; }
        public string? Tag { get; set; }
        
    }
}
