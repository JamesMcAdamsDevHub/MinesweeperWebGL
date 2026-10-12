using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private BoardController board;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private TMP_Text bombCountText;
    [SerializeField] private TMP_Text flagCountText;
    [SerializeField] private TMP_Text difficultyText;
    [SerializeField] private Button revealButton;
    [SerializeField] private Button flagButton;

    private Difficulty difficulty = Difficulty.Normal;

    public void StartNewGame()
    {
        ActivateGameUI();
        board.Setup(flagCountText);
    }

    public void StartNewGame(string difficultyStr)
    {
        difficulty = 
            DifficultyParser.GetDifficultyFromString(difficultyStr);

        ActivateGameUI();

        board.Setup(difficulty, flagCountText);
    }

    public void ActivateMainMenu()
    {
        board.DestroyBoard();
        mainMenu.SetActive(true);
        gameUI.SetActive(false);
    }

    public void SetFlagging(bool flag)
    {
        revealButton.image.color = flag ? Color.dimGray : Color.purple;
        flagButton.image.color = flag ? Color.purple : Color.dimGray;

        board.SetClickState(flag ? ClickState.Flag : ClickState.Reveal);
    }

    private void ActivateGameUI()
    {
        difficultyText.text = difficulty.ToString();
        difficultyText.color =
            DifficultyParser.GetTextColorByDifficulty(difficulty);
        bombCountText.text = 
            DifficultyParser.GetBombCountByDifficulty(difficulty).ToString();
        flagCountText.text = bombCountText.text;
        SetFlagging(false);
        mainMenu.SetActive(false);
        gameUI.SetActive(true);
    }
}
