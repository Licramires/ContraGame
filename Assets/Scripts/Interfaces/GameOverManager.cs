using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    
    public Button restartButton;     
    public Button menuButton;        


    public string gameSceneName = "Game";    
    public string menuSceneName = "Menu";     

    void Start()
    {
       
        if (restartButton != null)
            restartButton.onClick.AddListener(RestartGame);

        if (menuButton != null)
            menuButton.onClick.AddListener(GoToMenu);
    }

   
    public void RestartGame()
    {
     
        SceneManager.LoadScene(gameSceneName);
    }

    public void GoToMenu()
    {
       
        SceneManager.LoadScene(menuSceneName);
    }
}