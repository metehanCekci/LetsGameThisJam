using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Bu nesne sahne de�i�se bile silinmez
        }
        else
        {
            Destroy(gameObject); // E�er zaten varsa, yenisini yok et
        }
    }
}
