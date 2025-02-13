using Platformer.BlurredScreenshot;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer.UI
{
    public class PauseMenu : MonoBehaviour
    {

        [SerializeField] private BlurredBackground blurredBackground;

        public void Show()
        {
            blurredBackground.Show();
            gameObject.SetActive(true);
            TimeScaleManager(0); //Temp pause...
        }

        //We don't really need this :|
        //public void Hide()
        //{
        //    blurredBackground.Hide();
        //    gameObject.SetActive(false);
        //}

        #region Event Handlers

        public void BtnResumeClicked()
        {
            blurredBackground.Hide();
            gameObject.SetActive(false);
            TimeScaleManager(); //Aaannd... we're back! C:
        }

        public void BtnMainMenuClicked()
        {
            SceneManager.LoadScene("Assets/Scenes/MainScene.unity", LoadSceneMode.Single);
        }

        public void BtnRestartClicked()
        {
            SceneManager.LoadScene("Assets/Scenes/LevelScene.unity", LoadSceneMode.Single);
        }

        public float TimeScaleManager(float timeSpeed = 1)
        {
            Time.timeScale = timeSpeed;
            return Time.timeScale;
        }

        #endregion Event Handlers
    }
}