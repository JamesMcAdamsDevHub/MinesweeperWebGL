using UnityEngine;
using UnityEngine.UI;

public class BoardController : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private Tile tile;
    [SerializeField] private Image boardBG;
    [SerializeField] private Image gameBG;

    GameState state;

    private const int BOARD_WIDTH = 9;

    private const float BORDER_WIDTH = 4;

    private const int NUM_BOMBS = 10;

    private bool[,] bombs = new bool[BOARD_WIDTH, BOARD_WIDTH];
    private Tile[,] tiles = new Tile[BOARD_WIDTH, BOARD_WIDTH];

    public void Setup()
    {
        state = GameState.Ready;

        ResetBoard();

        float smallerLength = Mathf.Min(
            canvasRect.rect.width,
            canvasRect.rect.height
        );

        float cellSize = smallerLength / BOARD_WIDTH * 0.9f;

        float tileSize = cellSize * 0.9f;

        float startOffset = -1 * (BOARD_WIDTH / 2) * cellSize;

        gameBG.rectTransform.anchoredPosition = Vector2.zero;
        gameBG.rectTransform.sizeDelta = canvasRect.rect.size;

        float boardSize = cellSize * BOARD_WIDTH + (BORDER_WIDTH * 2);
        boardBG.rectTransform.anchoredPosition = Vector2.zero;
        boardBG.rectTransform.sizeDelta = new Vector2(boardSize + BORDER_WIDTH, boardSize + BORDER_WIDTH);

        for (int row = 0; row < BOARD_WIDTH; row++)
        {
            for (int col = 0; col < BOARD_WIDTH; col++)
            {
                float x = startOffset + (col * cellSize);
                float y = startOffset + (row * cellSize);

                Tile newTile = Instantiate(tile, transform);

                newTile.Setup(row, col, x, y, tileSize, this);

                tiles[row, col] = newTile;
            }
        }
    }

    public void TileClicked(Tile clickedTile)
    {
        if (state == GameState.Won || state == GameState.Lost)
            return;

        if (state == GameState.Ready) {
            GenerateBombs(clickedTile);
            state = GameState.Playing;
        }
        
        RevealTile(clickedTile);
    }

    private void GenerateBombs(Tile clickedTile)
    {
        int rowT = clickedTile.row;
        int colT = clickedTile.col;

        int bombsPlaced = 0;
        while (bombsPlaced < NUM_BOMBS)
        {
            int randRow = Random.Range(0, BOARD_WIDTH);
            int randCol = Random.Range(0, BOARD_WIDTH);

            if (randRow == rowT && randCol == colT) continue;

            if (!bombs[randRow, randCol])
            {
                bombs[randRow, randCol] = true;
                tiles[randRow, randCol].isBomb = true;
                bombsPlaced++;
            }
        }
    }

    private void RevealTile(Tile clickedTile)
    {
        if (clickedTile.isRevealed) return;

        if (clickedTile.isBomb)
        {
            // TODO: lose game
        }

        int adjBombs = GetAdjacentBombCount(clickedTile);

        clickedTile.Reveal(adjBombs);

        if (adjBombs == 0)
        {
            RevealAdjacentZeros(clickedTile);
        }
    }

    private int GetAdjacentBombCount(Tile tile)
    {
        int adjBombs = 0;

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                int r = tile.row - 1 + row;
                int c = tile.col - 1 + col;

                if (IsInBounds(r, c) && tiles[r, c].isBomb)
                {
                    adjBombs++;
                }
            }
        }

        return adjBombs;
    }

    private void RevealAdjacentZeros(Tile clickedTile)
    {
        // TODO: Breadth First Tile Reveal
    }

    private bool IsInBounds(int row, int col)
    {
        return (row >= 0 && col >= 0 && row < BOARD_WIDTH && col < BOARD_WIDTH);
    }

    private void ResetBoard()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        tiles = new Tile[BOARD_WIDTH, BOARD_WIDTH];
        bombs = new bool[BOARD_WIDTH, BOARD_WIDTH];
    }
}