using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreenFade : MonoBehaviour
{
    [SerializeField] Image splashScreenImage;

    public AudioSource splashSound;
    public AudioSource menuMusic;

    void Awake()
    {
        StartCoroutine(PlaySplashThenMenuMusic());
    }

    IEnumerator PlaySplashThenMenuMusic()
    {
        // Preload the menu music to avoid lag
        if (menuMusic.clip.loadState != AudioDataLoadState.Loaded)
            menuMusic.clip.LoadAudioData();

        // Play splash sound
        splashSound.Play();

        // Start menu music silently and paused
        menuMusic.Play();
        menuMusic.Pause();

        // Wait until splashSound is over
        yield return new WaitForSeconds(splashSound.clip.length);

        // Unpause menu music exactly after splash ends
        menuMusic.UnPause();

        // Hide splash screen
        splashScreenImage.gameObject.SetActive(false);
    }
}
