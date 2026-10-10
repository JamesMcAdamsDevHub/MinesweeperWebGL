using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardController board;
    [SerializeField] private GameObject titleHeader;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public void StartNewGame()
    {
        DeactivateModals();
        board.Setup();
    }

    public void StartNewGame(string difficultyStr)
    {
        DeactivateModals();

        Difficulty difficulty = 
            DifficultyParser.GetDifficultyFromString(difficultyStr);

        board.Setup(difficulty);
    }

    public void ActivateWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void ActivateLoseScreen()
    {
        loseScreen.SetActive(true);
    }

    public void ActivateMainMenu()
    {
        DeactivateModals();
        board.DestroyBoard();
        mainMenu.SetActive(true);
        titleHeader.SetActive(false);
    }

    private void DeactivateModals()
    {
        mainMenu.SetActive(false);
        winScreen.SetActive(false);
        loseScreen.SetActive(false);
        titleHeader.SetActive(true);
    }
}
