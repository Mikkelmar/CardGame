using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame.Client
{
    using CardGame.Objects.Cards;
    using System.Text.Json;

    public class CardSerializer
    {
        public static string Serialize(Card card)
        {
            return JsonSerializer.Serialize(card);
        }

        public static Card Deserialize(string json)
        {
            return JsonSerializer.Deserialize<Card>(json);
        }
    }
}
