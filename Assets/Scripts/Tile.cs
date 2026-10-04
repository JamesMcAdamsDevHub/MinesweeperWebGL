using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;

    public int row;
    public int col;

    public bool isBomb = false;
    public bool isRevealed = false;

    public int numAdjacentBombs = 0;

    private BoardController board;

    public void Setup(int row, int col, float x, float y, float size, BoardController board)
    {
        this.row = row;
        this.col = col;
        this.board = board;

        RectTransform rectTransform = GetComponent<RectTransform>();
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = new Vector2(x, y);
        rectTransform.sizeDelta = new Vector2(size, size);

        buttonRectTransform.sizeDelta = new Vector2(size, size);
    }

    public void Reveal(int adjBombs)
    {
        numAdjacentBombs = adjBombs;
        isRevealed = true;

        text.text = adjBombs.ToString();
    }

    public void OnClick()
    {
        board.TileClicked(this);
    }


}