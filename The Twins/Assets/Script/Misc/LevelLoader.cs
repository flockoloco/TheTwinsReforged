using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    public void LoadLevel (string sceneName)
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }
    
    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!loadOperation.isDone)
        {
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            slider.value = progress;
            progressText.text = progress * 100f + "%";

            yield return null;
        }
    }

}
