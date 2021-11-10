using Platformer.Gameplay;
using Platformer.Model;
using TMPro;
using UnityEngine;
using System.Collections;
//using Platformer.Core;

namespace Platformer.UI
{
    public class LevelCanvas : MonoBehaviour
    {
        #region Fields and Properties

        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private LevelEndedPopup levelEndedPopup;
        [SerializeField] private TMP_Text lblTokens;
        [SerializeField] private TMP_Text lblEnemiesKilled;
        [SerializeField] private TMP_Text lblUsername;
        #endregion Fields and Properties
        
        
        private static LevelCanvas _instance;
        public static LevelCanvas Instance => _instance;

        void Awake()
        {
            if (_instance == null) _instance = this;

            PlayerDeath.OnExecute += PlayerDiedCallback;
            PlayerEnteredVictoryZone.OnExecute += PlayerWonCallback;
            
            GameDatabase.instance.ResetScore();

            lblUsername.text = GameDatabase.instance.CurrentUser.Username;
            pauseMenu.TimeScaleManager();
        }

        private void OnDestroy()
        {
            PlayerDeath.OnExecute -= PlayerDiedCallback;
            PlayerEnteredVictoryZone.OnExecute -= PlayerWonCallback;
        }

        private void Update()
        {
            lblTokens.text = GameDatabase.instance.CurrentUser.Tokens.ToString();
            lblEnemiesKilled.text = GameDatabase.instance.CurrentUser.EnemiesKilled.ToString();
        }

        #region Event Handlers
        
        private void PlayerDiedCallback(PlayerDeath playerDeath)
        {
            //levelEndedPopup.Show(false);
            StartCoroutine(_LevelEnded(false));
        }

        private void PlayerWonCallback(PlayerEnteredVictoryZone playerEnteredVictoryZone)
        {
            //levelEndedPopup.Show(true);
            StartCoroutine(_LevelEnded());
        }


        #region Win Lose Delay:
        //float wfsDelay;
        //WaitForSeconds wfsValue { get { return wfs; } set { value = new WaitForSeconds(wfsDelay); } }
        WaitForSeconds wfs = new WaitForSeconds(2f);
        IEnumerator _LevelEnded(bool won = true)
        {
            //var player = Simulation.GetModel<PlatformerModel>().player;
            //wfsDelay = 1;
            yield return wfs;
            levelEndedPopup.Show(won);
        }

        #endregion Win Lose Delay

        public void BtnPauseClicked()
        {
            pauseMenu.Show();
        }

        #endregion Event Handlers
    }
}