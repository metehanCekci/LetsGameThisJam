using TMPro;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    public TextMeshProUGUI goldText;

    void Start()
    {
        
    }

  
    void Update()
    {
        UpdateGoldUI();
    }
    void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = $"Alt�n: {GameManagerScript.instance.GoldAmount}";

        }
    }
}
