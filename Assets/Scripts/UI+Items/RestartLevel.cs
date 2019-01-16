using UnityEngine;
using UnityEngine.SceneManagement;


public class RestartLevel : MonoBehaviour {
    public static RestartLevel restartInstance;

    private void Start()
    {
        restartInstance = this;
    }



    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
