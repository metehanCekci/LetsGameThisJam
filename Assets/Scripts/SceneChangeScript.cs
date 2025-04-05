using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneChangeScript : MonoBehaviour
{
    public PauseScript pauseScript;
    public SplashScreenFade SplashScreenFade;
    public Image FadeInAnim;
    public Image FadeOutAnim;
    public float fadeDuration = 1f;

    public void Start()
    {
        pauseScript = FindFirstObjectByType<PauseScript>();
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex>=1)
        {
            StartCoroutine(FadeOut());
        }
        if (FadeInAnim==null)
        {
            FadeInAnim = GameObject.Find("UI").transform.Find("FadeIn").GetComponent<Image>();
        }
        if (FadeOutAnim == null)
        {
            FadeOutAnim = GameObject.Find("UI").transform.Find("FadeOut").GetComponent<Image>();
        }
    }
    public void StartGame()
    {
        
        StartCoroutine(FadeIn());
        StartCoroutine(FadeInAndLoadScene(8));
    }
    public IEnumerator FadeIn()
    {
        if (FadeInAnim == null)
        {
            yield return null;
        }
        else
        {
            FadeInAnim.gameObject.SetActive(true);
            Color color = FadeInAnim.color;
            color.a = 0f;
            FadeInAnim.color = color;

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / fadeDuration);
            FadeInAnim.color = color;
            yield return null;
            }
            color.a = 1f;
            FadeInAnim.color = color;
            FadeInAnim.gameObject.SetActive(false); // ekranda kalmasýn
        }
        
        
    }
    IEnumerator FadeOut()
    {
        if (FadeOutAnim == null)
        {
            yield return null;
        }
        else
        {
            FadeOutAnim.gameObject.SetActive(true);
            Color color = FadeOutAnim.color;
            color.a = 1f;
            FadeOutAnim.color = color;

            float elapsed = 0f;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                color.a = 1f - Mathf.Clamp01(elapsed / fadeDuration);
                FadeOutAnim.color = color;
                yield return null;
            }
            color.a = 0f;
            FadeOutAnim.color = color;
            FadeOutAnim.gameObject.SetActive(false);
        }
    }
    public void NextScene()
    {
        GameManagerScript.instance.level += 1;
        StartCoroutine(FadeInAndLoadScene(GameManagerScript.instance.level + 1));
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
    public void mainMenu()
    {
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex <= 1)
        {
            Destroy(AudioManager.instance.gameObject);
        }
        pauseScript.active = false;
        pauseScript.pauseMenu.SetActive(false);
        StartCoroutine(FadeInAndLoadScene(1));
    }
    public void loadLevel1()
    {
        //Destroy(AudioManager.instance.gameObject);
        StartCoroutine(FadeInAndLoadScene(2));
    }
    public void loadLevel2()
    {
        //Destroy(AudioManager.instance.gameObject);
        StartCoroutine(FadeInAndLoadScene(3));
    }
    public void loadLevel3()
    {
        //Destroy(AudioManager.instance.gameObject);
        StartCoroutine(FadeInAndLoadScene(4));
    }
    public void loadLevel4()
    {
        //Destroy(AudioManager.instance.gameObject);
        StartCoroutine(FadeInAndLoadScene(5));
    }
    public void loadLevel5()
    {
        //Destroy(AudioManager.instance.gameObject);
        StartCoroutine(FadeInAndLoadScene(6));
    }
    public void loadLevel6()
    {
        //Destroy(AudioManager.instance.gameObject);
        StartCoroutine(FadeInAndLoadScene(7));
    }
    
    public IEnumerator FadeInAndLoadScene(int sceneIndex)
    {
        Time.timeScale = 1f;
        yield return FadeIn(); // önce fade in bitsin
        SceneManager.LoadScene(sceneIndex); // sonra sahne geçsin
        yield return new WaitForSeconds(1f);
        FadeInAnim.gameObject.SetActive(false);
    }
}
