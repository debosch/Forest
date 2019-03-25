using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private Text gameOverText;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        gameOverText.text = "GAME OVER";
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("MainScene");

        if (Player.Instance.Dead)
        {
            gameOverText.gameObject.SetActive(true);
            Invoke("SceneRestartDelay", 3);
        }
    }

    private void SceneRestartDelay()
    {
        SceneManager.LoadScene("MainScene");
    }
}
