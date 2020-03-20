using System;
using System.Collections.Generic;

namespace LECHO.Infrastructure
{
    public partial class Choices
    {
        public int UserId { get; set; }
        public int SubjectId { get; set; }

        public virtual Subjects Subject { get; set; }
        public virtual Students User { get; set; }
    }
}
