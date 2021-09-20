using System;
using System.Collections.Generic;

#nullable disable

namespace GamingConsole.Models
{
    public partial class UserDetail
    {
        public UserDetail()
        {
            FriendUsers = new HashSet<FriendUser>();
            SomeWords = new HashSet<SomeWord>();
        }

        public string Username { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<FriendUser> FriendUsers { get; set; }
        public virtual ICollection<SomeWord> SomeWords { get; set; }
    }
}
