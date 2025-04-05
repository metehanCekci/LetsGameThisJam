using UnityEngine;

public class ScheneChangerScriptDontDestroy : MonoBehaviour
{
    private static ScheneChangerScriptDontDestroy instance;
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
