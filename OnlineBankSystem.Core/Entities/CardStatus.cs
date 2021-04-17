using System.Collections.Generic;

namespace OnlineBankSystem.Core.Entities
{
    public class CardStatus : Entity<int>
    {
        public string Name { get; set; }

        public List<Card> Cards { get; set; }
    }
}
