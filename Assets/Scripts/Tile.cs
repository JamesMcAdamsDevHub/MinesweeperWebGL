using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class Tile : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject FlagIcon;
    [SerializeField] private GameObject BombIcon;
    [SerializeField] private GameObject ExplosionIcon;

    public int row;
    public int col;

    public bool isBomb = false;
    public bool isRevealed = false;
    public bool isFlagged = false;

    private BoardController board;

    public void Setup(int row, int col, float x, float y, float size, BoardController board)
    {
        this.row = row;
        this.col = col;
        this.board = board;

        button.image.color = Color.cadetBlue;

        RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(x, y);
        rectTransform.sizeDelta = new Vector2(size, size);

        buttonRectTransform.sizeDelta = new Vector2(size, size);
    }

    public void OnClick()
    {
        board.TileClicked(this);
    }

    public void Reveal(int adjBombs)
    {
        string tileText = adjBombs == 0 ? "" : adjBombs.ToString();
        if (adjBombs > 0)
        {
            Color textColor = Color.black;
            switch(adjBombs)
            {
                case 1:
                    textColor = Color.green;
                    break;
                case 2:
                    textColor = Color.yellow;
                    break;
                case 3:
                    textColor = Color.orange;
                    break;
                case 4:
                    textColor = Color.darkOrange;
                    break;
                case 5:
                    textColor = Color.orangeRed;
                    break;
                case 6:
                    textColor = Color.red;
                    break;
                case 7:
                    textColor = Color.darkRed;
                    break;
                case 8:
                    textColor = Color.purple;
                    break;
            }
            text.color = textColor;
        }
        UpdateRevealedTile(Color.gray, tileText);
    }

    public void RevealBomb(bool wonGame)
    {
        if (isFlagged)
        {
            UpdateRevealedTile(Color.green);
            return;
        }

        BombIcon.SetActive(!ExplosionIcon.activeSelf);
        UpdateRevealedTile(wonGame ? Color.green : Color.red);
    }

    public void DisableButton()
    {
        button.interactable = false;
    }

    public void ToggleFlag()
    {
        if (isRevealed) return;

        isFlagged = !isFlagged;
        FlagIcon.SetActive(isFlagged);
    }

    public void Explode()
    {
        ExplosionIcon.SetActive(true);
    }

    private void UpdateRevealedTile(Color color, string tileText = "")
    {
        isRevealed = true;
        text.text = tileText;
        button.image.color = color;
        DisableButton();
    }
    
}