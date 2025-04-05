using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Sahneye yeni gelen kopya silinir
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
