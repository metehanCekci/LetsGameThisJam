using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public byte level = 0;
    public byte health;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // varsa
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
