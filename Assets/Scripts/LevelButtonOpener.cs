using UnityEngine;

public class LevelButtonOpener : MonoBehaviour
{

    public GameObject buton2;
    public GameObject buton3;
    public GameObject buton4;
    public GameObject buton5;
    public GameObject buton6;

    private void Awake()
    {

    }

    public void Update()
    {
        if (GameManagerScript.instance.level <= 1)
        {
            buton2.transform.Find("Image").gameObject.SetActive(true);
            buton3.transform.Find("Image").gameObject.SetActive(true);
            buton4.transform.Find("Image").gameObject.SetActive(true);
            buton5.transform.Find("Image").gameObject.SetActive(true);
            buton6.transform.Find("Image").gameObject.SetActive(true);
        }
        if (GameManagerScript.instance.level == 2)
        {
            buton2.transform.Find("Image").gameObject.SetActive(false);
            buton3.transform.Find("Image").gameObject.SetActive(true);
            buton4.transform.Find("Image").gameObject.SetActive(true);
            buton5.transform.Find("Image").gameObject.SetActive(true);
            buton6.transform.Find("Image").gameObject.SetActive(true);
        }
        if (GameManagerScript.instance.level == 3)
        {
            buton2.transform.Find("Image").gameObject.SetActive(false);
            buton3.transform.Find("Image").gameObject.SetActive(false);
            buton4.transform.Find("Image").gameObject.SetActive(true);
            buton5.transform.Find("Image").gameObject.SetActive(true);
            buton6.transform.Find("Image").gameObject.SetActive(true);
        }
        if (GameManagerScript.instance.level == 4)
        {
            buton2.transform.Find("Image").gameObject.SetActive(false);
            buton3.transform.Find("Image").gameObject.SetActive(false);
            buton4.transform.Find("Image").gameObject.SetActive(false);
            buton5.transform.Find("Image").gameObject.SetActive(true);
            buton6.transform.Find("Image").gameObject.SetActive(true);
        }
        if (GameManagerScript.instance.level == 5)
        {
            buton2.transform.Find("Image").gameObject.SetActive(false);
            buton3.transform.Find("Image").gameObject.SetActive(false);
            buton4.transform.Find("Image").gameObject.SetActive(false);
            buton5.transform.Find("Image").gameObject.SetActive(false);
            buton6.transform.Find("Image").gameObject.SetActive(true);
        }
        if (GameManagerScript.instance.level == 6)
        {
            buton2.transform.Find("Image").gameObject.SetActive(false);
            buton3.transform.Find("Image").gameObject.SetActive(false);
            buton4.transform.Find("Image").gameObject.SetActive(false);
            buton5.transform.Find("Image").gameObject.SetActive(false);
            buton6.transform.Find("Image").gameObject.SetActive(false);
        }
    }
}
