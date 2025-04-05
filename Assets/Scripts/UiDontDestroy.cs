using UnityEngine;

public class UiDontDestroy : MonoBehaviour
{
    private static UiDontDestroy instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
