namespace DefaultNamespace
{
    public class Inventory
    {
        private static Inventory _instance;
        
        public static Inventory Instance()
        {
            if (_instance == null)
            {
                _instance = new Inventory(); 
            }

            return _instance;
        }

        private Inventory()
        {
            
        }
    }
}