using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public byte level = 0;

    public int MaxStamina;
    public int Health;
    public int AttackPower;

    public int WalkSpeed;
    public int RunSpeed;
    public float AttackCooldown; 
    public float goldMultiplier;
    public float AttackRange;


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
