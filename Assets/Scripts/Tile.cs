using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class Tile : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    public int row;
    public int col;

    public bool isBomb = false;
    public bool isRevealed = false;

    public int numAdjacentBombs = 0;

    private const string BOMB = "BOMB";

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
        numAdjacentBombs = adjBombs;
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
        UpdateRevealedTile(tileText.ToString(), Color.gray);
    }

    public void RevealBomb()
    {
        UpdateRevealedTile(BOMB, Color.red);
    }

    public void DisableButton()
    {
        button.interactable = false;
    }

    private void UpdateRevealedTile(string tileText, Color color)
    {
        isRevealed = true;
        if (tileText.Equals(BOMB))
        {
            text.text = "*";
        }
        else
        {
            text.text = tileText;
        }
        button.image.color = color;
        DisableButton();
    }
    
}