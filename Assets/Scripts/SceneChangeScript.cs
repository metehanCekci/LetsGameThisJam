using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeScript : MonoBehaviour
{
    public SplashScreenFade SplashScreenFade;

    public void Start()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene(2);
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
        SceneManager.LoadScene(9);
    }
    public void loadLevel2()
    {
        SceneManager.LoadScene(9);
    }
    public void loadLevel3()
    {
        SceneManager.LoadScene(9);
    }
    public void loadLevel4()
    {
        SceneManager.LoadScene(9);
    }
    public void loadLevel5()
    {
        SceneManager.LoadScene(9);
    }
    public void loadLevel6()
    {
        SceneManager.LoadScene(9);
    }
    public void mainMenu()
    {
        SceneManager.LoadScene(1);
    }
}
