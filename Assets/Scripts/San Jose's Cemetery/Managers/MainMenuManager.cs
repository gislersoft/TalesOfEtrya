using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
    public class MainMenuManager : MonoBehaviour
    {

        public GameObject loadingScreen;
        public GameObject mainMenuScreen;

        public void PlayButton(string sceneName)
        {
            loadingScreen.SetActive(true);
            mainMenuScreen.SetActive(false);
            //SJCSceneManager.instance.LoadSceneAsync(sceneName);
        }

        public void HelpButton()
        {
            //Run tutorial
        }

        public void ExitButton()
        {
            //This method should return to the main game.
            //For now, it will close the application

            Application.Quit();
        }
    }
}
