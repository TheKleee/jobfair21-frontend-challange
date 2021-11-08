using Platformer.Gameplay;

namespace Platformer.Model
{
    public class GameDatabase
    {
        public static GameDatabase instance = new GameDatabase();
        public UserData CurrentUser { get; private set; }

        private GameDatabase()
        {
            CurrentUser = new UserData();
            PlayerTokenCollision.OnExecute += PlayerCollectedToken;
            EnemyDeath.OnExecute += PlayerKilledEnemy;
        }

        private void PlayerKilledEnemy(EnemyDeath enemyDeath)
        {
            CurrentUser.EnemiesKilled++;
        }

        private void PlayerCollectedToken(PlayerTokenCollision playerTokenCollision)
        {
            CurrentUser.Tokens++;
        }

        public void SetUsername(string newName)
        {
            CurrentUser.Username = newName;
        }

        public void ResetScore()
        {
            CurrentUser.Tokens = 0;
            CurrentUser.EnemiesKilled = 0;
        }

        /// <summary>
        /// Just wanted to say I'm a huge fan of destructors so I'll leave this comment here >:D
        /// </summary>
        ~GameDatabase()
        {
            PlayerTokenCollision.OnExecute -= PlayerCollectedToken;
            EnemyDeath.OnExecute -= PlayerKilledEnemy;
        }
    }

    public class UserData
    {
        public string Username = "";
        public int Tokens { get; internal set; }
        public int EnemiesKilled { get; internal set; }
        public int Score => Tokens * 10 + EnemiesKilled * 100;

    }
}
   
