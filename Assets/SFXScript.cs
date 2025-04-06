using UnityEngine;

public class SFXScript : MonoBehaviour
{
    public static SFXScript Instance { get; private set; }
    public AudioClip Slash;
    public AudioClip Hit;
    public AudioClip Hurt;
    private void Awake()
    {
        // If an instance already exists and it's not this one, destroy this one
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set the instance and make it persistent
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySlash()
    {
        this.GetComponent<AudioSource>().PlayOneShot(Slash);
    }

    public void PlayHit()
    {
        this.GetComponent<AudioSource>().PlayOneShot(Hit);
    }

    public void PlayHurt()
    {
        this.GetComponent<AudioSource>().PlayOneShot(Hurt);
    }

    // You can add your SFX methods here later
}
