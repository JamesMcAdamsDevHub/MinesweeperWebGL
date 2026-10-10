using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardController board;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMP_Text bombCountText;
    [SerializeField] private TMP_Text difficultyText;

    private Difficulty difficulty = Difficulty.Normal;

    public void StartNewGame()
    {
        ActivateGameUI();
        board.Setup();
    }

    public void StartNewGame(string difficultyStr)
    {
        difficulty = 
            DifficultyParser.GetDifficultyFromString(difficultyStr);

        ActivateGameUI();

        board.Setup(difficulty);
    }

    public void ActivateWinScreen()
    {
        // TODO: Make win game effect
    }

    public void ActivateLoseScreen()
    {
        // TODO: Make lose game effect
    }

    public void ActivateMainMenu()
    {
        board.DestroyBoard();
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
    }

    private void ActivateGameUI()
    {
        difficultyText.text = difficulty.ToString();
        difficultyText.color =
            DifficultyParser.GetTextColorByDifficulty(difficulty);
        bombCountText.text = 
            DifficultyParser.GetBombCountByDifficulty(difficulty).ToString();
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
    }
}
