namespace Game.Character
{
    public class NPCData
    {
        private string _name;
        private int _level;
        private float _maxHealth;
        private float _movementSpeed;

        public NPCData(string name, int level, float maxHealth, float movementSpeed)
        {
            this._name = name;
            this._level = level;
            this._maxHealth = maxHealth;
            this._movementSpeed = movementSpeed;
        }
        
        public string Name => _name;

        public int Level => _level;

        public float MaxHealth => _maxHealth;

        public float MovementSpeed => _movementSpeed;
    }
}