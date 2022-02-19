using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoad : MonoBehaviour
{
    public void ToOne()
    {
        SceneManager.LoadScene("One");
    }
    public void ToZero()
    {
        SceneManager.LoadScene("Zero");
    }
}
