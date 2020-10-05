using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SJCSceneManager : MonoBehaviour
{

    #region Singleton

    public static SJCSceneManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public TextMeshProUGUI percentageText;
    public GameObject loadingScreen;
    public Animator uiAnimator;
    int loadSceneHash;

    private void Start()
    {
        loadSceneHash = Animator.StringToHash("LoadScene");
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        uiAnimator.SetTrigger(loadSceneHash);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            percentageText.SetText(Mathf.RoundToInt((progress * 100)).ToString() + "%");
            yield return null;
        }
        loadingScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
  
