using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipFirstScene : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(1);
    }
}
