using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardController board;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public void Start()
    {
        board.Setup();
    }

    public void StartNewGame()
    {
        mainMenu.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        board.Setup();
    }

    public void ActivateWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void ActivateLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
