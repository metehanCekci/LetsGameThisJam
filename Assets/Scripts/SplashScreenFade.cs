using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashScreenFade : MonoBehaviour
{
    [SerializeField] Image splashScreenImage;

    public AudioSource splashSound;
    public AudioSource menuMusic;
    public float fadeDuration = 1.0f;
    public float fadeStartBeforeEnd = 1f; // Fade'in müzik bitiþinden kaç saniye önce baþlayacaðý

    void Awake()
    {
        PlaySound();
    }

    void PlaySound()
    {
        splashSound.Play();
        StartCoroutine(StartFadeBeforeMusicEnds());
    }

    IEnumerator StartFadeBeforeMusicEnds()
    {
        float waitTime = splashSound.clip.length - fadeStartBeforeEnd;
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(FadeOut());

        yield return new WaitForSeconds(0.5f);

      
        menuMusic.Play();
        // Kalan süre kadar bekle, sonra menü müziðini baþlat
        yield return new WaitForSeconds(fadeStartBeforeEnd);
        
    }

    IEnumerator FadeOut()
    {
        float t = 0f;
        Color color = splashScreenImage.color;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            splashScreenImage.color = color;
            yield return null;
        }

        color.a = 0f;
        splashScreenImage.color = color;
        splashScreenImage.gameObject.SetActive(false);
    }
}
