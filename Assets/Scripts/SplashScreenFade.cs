using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreenFade : MonoBehaviour
{
    [SerializeField] Image splashScreenImage;
    public AudioSource splashSound;
    public AudioSource menuMusic;

    [SerializeField] float fadeDuration = 1.5f;

    public void Start()
    {
        menuMusic = AudioManager.instance.GetComponent<AudioSource>();
        StartCoroutine(PlaySplashThenFadeOut());
    }

    IEnumerator PlaySplashThenFadeOut()
    {
        // M�zik �n y�kleme (lag �nleme)
        if (menuMusic.clip.loadState != AudioDataLoadState.Loaded)
            menuMusic.clip.LoadAudioData();

        // Splash sesi �al
        splashSound.Play();

        // Men� m�zi�ini sessizce ba�lat ve durdur
        menuMusic.Play();
        menuMusic.Pause();

        // Splash sesi bitene kadar bekle
        yield return new WaitForSeconds(splashSound.clip.length);

        // Men� m�zi�ini ba�lat
        menuMusic.UnPause();

        // Splash ekran�n� fade out yap
        float elapsed = 0f;
        Color color = splashScreenImage.color;
        color.a = 1f;
        splashScreenImage.color = color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            splashScreenImage.color = color;
            yield return null;
        }

        splashScreenImage.gameObject.SetActive(false);
    }
}
