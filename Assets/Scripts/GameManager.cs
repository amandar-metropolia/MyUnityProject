using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour

{
    [SerializeField] MainMenuManager _mainMenuManager;
    [SerializeField] InGameUIManager _inGameUIManager;
    [SerializeField] CanvasGroup _inGameUICanvas;

    public static GameManager Instance {get; private set; }
   void Awake()
    {
       if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
       
       Time.timeScale = 0f; 
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        _inGameUIManager.ShowInGameUI();
        _inGameUICanvas.alpha = 1f;
        _inGameUICanvas.interactable = true;
        _inGameUICanvas.blocksRaycasts = true;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        _inGameUIManager.ShowGameOverPanel();
    }

     public void ResetGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
        

}
