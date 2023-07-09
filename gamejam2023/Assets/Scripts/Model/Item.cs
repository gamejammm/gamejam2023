using System.Collections.Generic;

namespace DefaultNamespace.Model
{
    public class Item
    {
        private static List<Item> _items;

        private static void initialize()
        {
            _items = new List<Item>();
            _items.Add(new Item("Brühe", "Backwaren", 5));
            _items.Add(new Item("Butter", "Milchprodukte", 3));
            _items.Add(new Item("Erdbeeren", "Obst Gemüse", 5));
            _items.Add(new Item("Essig", "Backwaren", 8));
            _items.Add(new Item("Fleisch", "Fleisch/Wurst", 15));
            _items.Add(new Item("Gurke", "Obst Gemüse", 2));
            _items.Add(new Item("Käse", "Milchprodukte", 3));
            _items.Add(new Item("Knoblauch", "Obst Gemüse", 1));
            _items.Add(new Item("Kondome", "Hygiene", 7));
            _items.Add(new Item("Mehl", "Backwaren", 5));
            _items.Add(new Item("Milch", "Milchprodukte", 2));
            _items.Add(new Item("Nudeln", "Teigwaren", 5));
            _items.Add(new Item("Nüsse", "Süßigkeiten", 4));
            _items.Add(new Item("Öl", "Backwaren", 8));
            _items.Add(new Item("Paprika", "Obst Gemüse", 3));
            _items.Add(new Item("Pfeffer", "Backwaren", 5));
            _items.Add(new Item("Pilze", "Obst Gemüse", 5));
            _items.Add(new Item("Reis", "Teigwaren", 3));
            _items.Add(new Item("Risottoreis", "Teigwaren", 4));
            _items.Add(new Item("Sahne", "Milchprodukte", 2));
            _items.Add(new Item("Salat", "Obst Gemüse", 3));
            _items.Add(new Item("Salz", "Backwaren", 6));
            _items.Add(new Item("Schokolade", "Süßigkeiten", 4));
            _items.Add(new Item("Teures Wasser", "Getränke", 19));
            _items.Add(new Item("Tomate", "Obst Gemüse", 3));
            _items.Add(new Item("Wein", "Getränke", 15));
            _items.Add(new Item("Weißwein", "Getränke", 7));
            _items.Add(new Item("Zitrone", "Obst Gemüse", 2));
            _items.Add(new Item("Zucker", "Backwaren", 5));
            _items.Add(new Item("Zwiebel", "Obst Gemüse", 3));
        }

        public static Item Factory(string name)
        {
            if (_items == null)
            {
                initialize();
            }

            Item result = _items.Find(i => i.Name == name);
            if (result == null)
            {
                result = new Item("Leergut", "Leergut", -3);
            }

            return result;
        }
        
        public readonly string Name;
        public readonly string Category;
        public readonly float Price;

        private Item(string name, string category, float price)
        {
            Name = name;
            Category = category;
            Price = price;
        }
    }
}