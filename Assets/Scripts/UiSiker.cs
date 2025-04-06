using UnityEngine;

public class UiSiker : MonoBehaviour
{
    public UiDontDestroy UiDontDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (UiDontDestroy == null)
        {
            UiDontDestroy = FindFirstObjectByType<UiDontDestroy>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UiDontDestroy.gameObject.SetActive(false);
    }
}
