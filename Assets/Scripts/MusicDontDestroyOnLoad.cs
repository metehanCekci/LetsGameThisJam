using UnityEngine;

public class MusicDontDestroyOnLoad : MonoBehaviour
{
    private static MusicDontDestroyOnLoad instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu nesne sahne deðiþse bile silinmez
        }
        else
        {
            Destroy(gameObject); // Eðer zaten varsa, yenisini yok et
        }
    }
}
