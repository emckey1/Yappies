using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YappiesTesting.Models
{
    public class Conversation
    {
        public Conversation ()
        {
            this.Messages = new HashSet<Message>();
        }

        public int ParentID { get; set; }
        public virtual Parent Parent { get; set; }

        public int ProgramSupervisorID { get; set; }
        public virtual ProgramSupervisor ProgramSupervisor { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
    }
}
