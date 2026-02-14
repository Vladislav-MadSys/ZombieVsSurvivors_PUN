using UnityEngine.SceneManagement;
using Zenject;

namespace _Project.Scripts.Low.ScenesController
{
    public class ScenesController : IScenesController, IInitializable
    {
        private const string MENU_SCENE_KEY = "MainMenu";
        private const string GAMEPLAY_SCENE_KEY = "Gameplay";

        public void Initialize()
        {
            LoadMainMenu();
        }
        
        public void LoadGame()
        {   
            SceneManager.LoadScene(GAMEPLAY_SCENE_KEY);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene(MENU_SCENE_KEY);
        }
    }
}
