using System;
using System.Collections.Generic;
using System.Text;

namespace Work.Entities.group
{
    public class GroupContext
    {
        public Guid TargetOfSubscriber { get; set; }

        public Guid Subcriber { get; set; }
    }
}
