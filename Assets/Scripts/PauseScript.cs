using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject pauseMenu;
    public bool active = false;
    public bool settingsActive = false;
    void Start()
    {
        
        pauseMenu.SetActive(false);
        pauseMenu.transform.Find("MenuBackground").gameObject.SetActive(true);
        pauseMenu.transform.Find("SettingsBackground").gameObject.SetActive(false);
        Player.GetComponent<PlayerInput>().enabled = true;
    }

    void Update()
    {
        if (Player==null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (active)
            {
                if (settingsActive)
                {
                    pauseMenu.transform.Find("MenuBackground").gameObject.SetActive(true);
                    pauseMenu.transform.Find("SettingsBackground").gameObject.SetActive(false);
                    settingsActive = false;
                }
                else
                {
                    active = false;
                    pauseMenu.SetActive(false);
                    Time.timeScale = 1.0f;
                    Player.GetComponent<PlayerInput>().enabled = true;
                    
                }
            }
            else
            {
                active = true;
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                Player.GetComponent<PlayerInput>().enabled = false;
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
        SceneManager.LoadScene(1);
    }
    public void SettingsBackButton()
    {
        pauseMenu.transform.Find("MenuBackground").gameObject.SetActive(true);
        pauseMenu.transform.Find("SettingsBackground").gameObject.SetActive(false);
    }
    public void ResumeButton()
    {
        active = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1.0f;
        Player.GetComponent<PlayerInput>().enabled = true;
    }
}
