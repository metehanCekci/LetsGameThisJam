using UnityEngine;

public class PauseControllerDontDestroyScript : MonoBehaviour
{
    private static PauseControllerDontDestroyScript instance;
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
