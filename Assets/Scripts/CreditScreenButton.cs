using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScreenButton : MonoBehaviour
{
    public void Close()
    {
        SceneManager.LoadScene(0);
    }

    public void Website()
    {
        Application.OpenURL("https://wiiirhung.com");
    }
}
