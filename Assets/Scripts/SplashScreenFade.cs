using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SplashScreenFade : MonoBehaviour
{
    [SerializeField] Image splashScreenImage;
    public AudioSource splashSound;
    public AudioSource menuMusic;
    Transform cizgiRoman;

    [SerializeField] float fadeDuration = 1.5f;

    public void Start()
    {
        menuMusic = AudioManager.instance.GetComponent<AudioSource>();
        StartCoroutine(PlaySplashThenFadeOut());
        cizgiRoman = splashScreenImage.transform.GetChild(0);
    }

    IEnumerator PlaySplashThenFadeOut()
    {
        // Müzik ön yükleme (lag önleme)
        if (menuMusic.clip.loadState != AudioDataLoadState.Loaded)
            menuMusic.clip.LoadAudioData();

        // Splash sesi çal
        splashSound.Play();

        // Menü müziðini sessizce baþlat ve durdur
        menuMusic.Play();
        menuMusic.Pause();

        // Splash sesi bitene kadar bekle
        yield return new WaitForSeconds(splashSound.clip.length);

        // Menü müziðini baþlat
        menuMusic.UnPause();

        // Splash ekranýný fade out yap
        float elapsed = 0f;
        Color color = splashScreenImage.color;
        color.a = 1f;
        splashScreenImage.color = color;
        cizgiRoman.gameObject.GetComponent<Image>().color = color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = 1f - Mathf.Clamp01(elapsed / fadeDuration);
            splashScreenImage.color = color;
            cizgiRoman.gameObject.GetComponent<Image>().color = color;
            yield return null;
        }

        splashScreenImage.gameObject.SetActive(false);
    }
}
