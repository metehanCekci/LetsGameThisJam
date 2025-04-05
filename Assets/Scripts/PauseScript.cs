using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public Canvas pauseMenu;
    public bool active = false;
    public bool settingsActive = false;
    void Start()
    {
        pauseMenu.enabled = false;
        pauseMenu.transform.Find("MenuBackground").gameObject.SetActive(true);
        pauseMenu.transform.Find("SettingsBackground").gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (active)
            {
                if (settingsActive)
                {
                    pauseMenu.transform.Find("MenuBackground").gameObject.SetActive(true);
                    pauseMenu.transform.Find("SettingsBackground").gameObject.SetActive(false);
                    
                }
                else
                {
                    pauseMenu.enabled = false;
                    Time.timeScale = 1.0f;
                }
            }
            else
            {
                active = true;
                Time.timeScale = 0f;
                pauseMenu.enabled = true;
            }
            
        }
    }
    public void SettingsButton()
    {
        pauseMenu.transform.Find("MenuBackground").gameObject.SetActive(false);
        pauseMenu.transform.Find("SettingsBackground").gameObject.SetActive(true);
        settingsActive = true;
    }
    public void QuitButton()
    {
        SceneManager.LoadScene(2);
    }
    public void SettingsBackButton()
    {
        pauseMenu.transform.Find("MenuBackground").gameObject.SetActive(true);
        pauseMenu.transform.Find("SettingsBackground").gameObject.SetActive(false);
    }
}
