using System;

namespace BlogFsn.Models
{
    public class GetUserDetailsViewModel
    {
        public string UserId { get; set; }
        public string AccessLevel { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string fullName { get; set; }
        public DateTime Data { get; set; }
    }
}