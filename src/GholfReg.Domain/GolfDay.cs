using System;

namespace GholfReg.Domain
{
    public class GolfDay: Entity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}
