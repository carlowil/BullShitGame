using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VNCreator.VNCreator.Data;
using VNCreator.VNCreator.Misc;

namespace VNCreator.VNCreator.Behaviors
{
    public class VnCreatorEndScreen : MonoBehaviour
    {
        public Button restartButton;
        public Button mainMenuButton;
        [Scene]
        public string mainMenu;

        private void Start()
        {
            restartButton.onClick.AddListener(Restart);
            mainMenuButton.onClick.AddListener(MainMenu);
        }

        private static void Restart()
        {
            GameSaveManager.NewLoad("MainGame");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        private void MainMenu()
        {
            SceneManager.LoadScene(mainMenu, LoadSceneMode.Single);
        }
    }
}
