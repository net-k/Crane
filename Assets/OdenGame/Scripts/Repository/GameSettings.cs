namespace OdenGame.Repository
{
    public class GameSettings 
    {
        GameSettings()
        {
            _gameMode = GameMode.Item;
        }
        
        public enum GameMode
        {
            Normal,
            Item,
        }

        private GameMode _gameMode;

        public GameMode _GameMode
        {
            get => _gameMode;
            set => _gameMode = value;
        }
    }
}
