using System;

namespace OnlineBankSystem.Core.Entities
{
    public class Departament : Entity<Guid>
    {
        public string Address { get; set; }
    }
}
