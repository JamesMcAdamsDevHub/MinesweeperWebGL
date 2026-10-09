using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardController board;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public void Start()
    {
        board.Setup();
    }

    public void StartNewGame()
    {
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        restartButton.SetActive(false);
        board.Setup();
    }

    public void ActivateWinScreen()
    {
        winScreen.SetActive(true);
        restartButton.SetActive(true);
    }

    public void ActivateLoseScreen()
    {
        loseScreen.SetActive(true);
        restartButton.SetActive(true);
    }
}
