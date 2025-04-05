using UnityEngine;
using TMPro;


public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript instance;

    public byte level = 1;

    public float MaxStamina;
    public float Stamina;
    public float MaxHealth;
    public float Health;
    public float AttackPower;
    public float GoldAmount;
    public float Lifesteal;

    public float WalkSpeed;
    public float RunSpeed;
    public float AttackCooldown;
    public float goldMultiplier;
    public float AttackRange;

    public bool hasLifesteal = false;
    public bool hasElectricSword = false;





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

    private void Update()
    {
       
    }

    
}
