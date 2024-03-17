using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameOver gameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("HELP");
            pauseMenu.SetActive(true);
        }
        if (GameController.GameControllerInstance.gameEnded)
        {
            Debug.Log("QUIT");
            gameOver.loadMainMenu();
        }
    }

}
