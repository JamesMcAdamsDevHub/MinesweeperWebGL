using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private RectTransform canvasRect;
    [SerializeField] private Tile tile;

    // Normalized percentage of cell size relative to board screenSize / width
    private const float CELL_PROPORTION = 0.75f;

    // Normalized percentage of tile size relative to cell size
    private const float TILE_PROPORTION = 0.8f;

    private const float TILE_REVEAL_DELAY = 0.01f;

    private GameState state;

    private Difficulty difficulty = Difficulty.Easy;

    private int boardWidth;

    private int numBombs;

    private int maxRevealedTiles;

    private int numTilesRevealed;

    private bool[,] bombs;
    private Tile[,] tiles;

    public void Setup()
    {
        Setup(difficulty);
    }

    public void Setup(Difficulty difficulty)
    {
        this.difficulty = difficulty;
        InitializeDifficulty();

        state = GameState.Ready;

        ResetBoard();

        float smallerLength = Mathf.Min(
            canvasRect.rect.width,
            canvasRect.rect.height
        );

        float cellSize = smallerLength / boardWidth * CELL_PROPORTION;

        float tileSize = cellSize * TILE_PROPORTION;

        float startOffset = -(boardWidth - 1) * cellSize / 2f;

        for (int row = 0; row < boardWidth; row++)
        {
            for (int col = 0; col < boardWidth; col++)
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
        if (state != GameState.Ready && state != GameState.Playing)
            return;

        if (state == GameState.Ready) {
            GenerateBombs(clickedTile);
            state = GameState.Playing;
        }
        
        RevealTile(clickedTile);
    }

    private void InitializeDifficulty()
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                boardWidth = 8;
                numBombs = 8;
                break;

            case Difficulty.Normal:
                boardWidth = 10;
                numBombs = 16;
                break;

            default:
                boardWidth = 12;
                numBombs = 30;
                break;

        }

        maxRevealedTiles = boardWidth * boardWidth - numBombs;
}

    private void GenerateBombs(Tile clickedTile)
    {
        int rowT = clickedTile.row;
        int colT = clickedTile.col;

        int bombsPlaced = 0;
        while (bombsPlaced < numBombs)
        {
            int randRow = Random.Range(0, boardWidth);
            int randCol = Random.Range(0, boardWidth);

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
            LoseGame();
            return;
        }

        int adjBombsCount = GetAdjacentBombs(clickedTile).Count;

        if (adjBombsCount == 0)
        {
            StartCoroutine(RevealAdjacentZeros(clickedTile));
        }
        else
        {
            clickedTile.Reveal(adjBombsCount);
            IncremenetRevealedTiles();
        }
    }

    private void IncremenetRevealedTiles()
    {
        numTilesRevealed++;
        if (numTilesRevealed >= maxRevealedTiles)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        RevealAllBombs();
        state = GameState.GameOver;
        DisableBoardButtons();
        gameController.ActivateWinScreen();
    }

    private void LoseGame()
    {
        RevealAllBombs();
        state = GameState.GameOver;
        DisableBoardButtons();
        gameController.ActivateLoseScreen();
    }

    private void RevealAllBombs()
    {
        for (int row = 0; row < boardWidth; row++)
        {
            for (int col = 0; col < boardWidth; col++)
            {
                if (bombs[row, col] == true)
                {
                    tiles[row, col].RevealBomb();
                }
            }
        }
    }

    private List<Tile> GetAdjacentTiles(Tile tile)
    {

        List<Tile> adjTiles = new List<Tile>();

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                int r = tile.row - 1 + row;
                int c = tile.col - 1 + col;

                if (IsInBounds(r, c) && !tiles[r, c].isRevealed)
                {
                    if (r == tile.row && c == tile.col) continue;

                    adjTiles.Add(tiles[r, c]);
                }
            }
        }

        return adjTiles;
    }

    private List<Tile> GetAdjacentBombs(Tile tile)
    {
        List<Tile> adjTiles = GetAdjacentTiles(tile);
        List<Tile> adjBombs = new List<Tile>();

        foreach (Tile t in adjTiles)
        {
            if (t.isBomb)
            {
                adjBombs.Add(t);
            }
        }

        return adjBombs;
    }

    private IEnumerator RevealAdjacentZeros(Tile clickedTile)
    {
        state = GameState.Waiting;
        Queue<Tile> queue = new Queue<Tile>();
        queue.Enqueue(clickedTile);

        while (queue.Count > 0)
        {
            Tile curT = queue.Dequeue();
            if (!curT.isRevealed)
            {
                curT.Reveal(GetAdjacentBombs(curT).Count);
                IncremenetRevealedTiles();

                List<Tile> adjTiles = GetAdjacentTiles(curT);

                foreach (Tile t in adjTiles)
                {
                    int adjBombsCount = GetAdjacentBombs(t).Count;
                    if (adjBombsCount == 0)
                    {
                        queue.Enqueue(t);
                    }
                    else
                    {
                        t.Reveal(adjBombsCount);
                        IncremenetRevealedTiles();
                    }
                }
            }
            yield return new WaitForSeconds(TILE_REVEAL_DELAY);
        }

        if (state == GameState.Waiting)
        {
            state = GameState.Playing;
        }
    }

    public void DisableBoardButtons()
    {
        foreach (Tile t in tiles)
        {
            t.DisableButton();
        }
    }

    public void DestroyBoard()
    {
        StopAllCoroutines();
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    private bool IsInBounds(int row, int col)
    {
        return (row >= 0 && col >= 0 && row < boardWidth && col < boardWidth);
    }

    private void ResetBoard()
    {
        DestroyBoard();

        tiles = new Tile[boardWidth, boardWidth];
        bombs = new bool[boardWidth, boardWidth];

        numTilesRevealed = 0;
    }
}