using System;
using System.Collections.Generic;

#nullable disable

namespace GamingConsole.Models
{
    public partial class SomeWord
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Word { get; set; }
        public int? Score { get; set; }

        public virtual UserDetail UsernameNavigation { get; set; }
    }
}
