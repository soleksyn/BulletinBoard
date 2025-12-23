using System.Collections.Generic;

namespace BulletinBoard.Core.Constants
{
    public static class CategoryConstants
    {
        public static readonly Dictionary<string, List<string>> Categories = new Dictionary<string, List<string>>
        {
            { "Побутова техніка", new List<string> { "Холодильники", "Пральні машини", "Бойлери", "Печі", "Витяжки", "Мікрохвильові печі" } },
            { "Комп'ютерна техніка", new List<string> { "ПК", "Ноутбуки", "Монітори", "Принтери", "Сканери" } },
            { "Смартфони", new List<string> { "Android смартфони", "iOS/Apple смартфони" } },
            { "Інше", new List<string> { "Одяг", "Взуття", "Аксесуари", "Спортивне обладнання", "Іграшки" } }
        };

        public static readonly List<string> Statuses = new List<string> { "Active", "Inactive" };
    }
}
