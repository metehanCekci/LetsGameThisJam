using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChangeScript : MonoBehaviour
{
    public SplashScreenFade SplashScreenFade;
    public Image image;
    public float fadeDuration = 1f;

    public void Start()
    {

    }
    public void StartGame()
    {
        
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        Color color = image.color;
        color.a = 0f;
        image.color = color;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            image.color = color;
            
            yield return null;
        }
        SceneManager.LoadScene(8);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(GameManagerScript.instance.level + 1);
        GameManagerScript.instance.level += 1;
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBGL
            Application.ExternalEval("window.close();");
#elif UNITY_STANDALONE
            Application.Quit();
#else
            Application.Quit();
#endif
    }
    public void loadLevel1()
    {
        Destroy(MusicDontDestroyOnLoad.instance.gameObject);
        SceneManager.LoadScene(2);
    }
    public void loadLevel2()
    {
        Destroy(MusicDontDestroyOnLoad.instance.gameObject);
        SceneManager.LoadScene(3);
    }
    public void loadLevel3()
    {
        Destroy(MusicDontDestroyOnLoad.instance.gameObject);
        SceneManager.LoadScene(4);
    }
    public void loadLevel4()
    {
        Destroy(MusicDontDestroyOnLoad.instance.gameObject);
        SceneManager.LoadScene(5);
    }
    public void loadLevel5()
    {
        Destroy(MusicDontDestroyOnLoad.instance.gameObject);
        SceneManager.LoadScene(6);
    }
    public void loadLevel6()
    {
        Destroy(MusicDontDestroyOnLoad.instance.gameObject);
        SceneManager.LoadScene(7);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
