
using UnityEngine.SceneManagement;
using UnityEngine;


public class Game : MonoBehaviour
{
    public Board board;
    public int width = 10;
    public int height = 10;
    public int mines = 10;

    private Cell[,] state;

    public GameObject RestartButton;

    private bool gameOver;

    private void Awake()
    {
        RestartButton.SetActive(false);
        board = GetComponentInChildren<Board>();
        if (GameSetting.Instance)
        {
            width = GameSetting.Instance.width;
            height = GameSetting.Instance.height;
            mines = GameSetting.Instance.mines;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        gameOver = false;
        state = new Cell[width, height];
        Camera.main.transform.position = new Vector3(width / 2, height / 2, -10);
        Camera.main.orthographicSize = height / 2 + 1;
        GenerateCell();
        GenerateMines();
        GenerateNumbers();
        board.Draw(state);
    }

    

    private void GenerateCell()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                state[x, y] = new Cell
                {
                    position = new Vector2Int(x, y),
                    type = Cell.Type.Empty
                };
            }
        }
    }
    private void GenerateMines()
    {
        int minesToPlace = mines;
        while (minesToPlace > 0)
        {
            int x = UnityEngine.Random.Range(0, width);
            int y = UnityEngine.Random.Range(0, height);
            if (state[x, y].type == Cell.Type.Empty)
            {
                state[x, y].type = Cell.Type.Mine;
                minesToPlace--;
            }
        }
    }
    private void GenerateNumbers()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (state[x, y].type == Cell.Type.Mine)
                {
                    continue;
                }
                int count = countMine(x, y);
                if (count == 0)
                {
                    state[x, y].type = Cell.Type.Empty;
                    continue;
                }
                state[x, y].type = Cell.Type.Number;
                state[x, y].number = count;

            }
        }
    }
    private int countMine(int Cellx, int Celly)
    {
        int count = 0;
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0)
                {
                    continue;
                }
                int nx = Cellx + dx;
                int ny = Celly + dy;
                if (nx < 0 || nx >= width || ny < 0 || ny >= height)
                {
                    continue;
                }
                if (state[nx, ny].type == Cell.Type.Mine)
                {
                    count++;
                }
            }
        }
        return count;
    }
    private void Update()
    {

        if (gameOver)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RevealCell();
        }
        if (Input.GetMouseButtonDown(1))
        {
            FlagCell();
        }
        WinCondition();
    }

    

    private void RevealCell()
    {
        if (!board.tilemap)
        {
            return;
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cellPosition = board.tilemap.WorldToCell(mousePosition);
        Cell cell = GetCell((int)cellPosition.x, (int)cellPosition.y);
        if (cell.type == Cell.Type.Invalid || cell.isFlagged || cell.isRevealed)
        {
            return;
        }
        switch (cell.type)
        {
            case Cell.Type.Empty:
                Flood(cell);
                break;
            case Cell.Type.Mine:
                cell.exploded = true;
                GameOver();
                break;
        }
        cell.isRevealed = true;
        state[(int)cellPosition.x, (int)cellPosition.y] = cell;
        board.Draw(state);
    }

    private void Flood(Cell cell)
    {
        if (cell.isFlagged
         || cell.isRevealed
         || cell.type == Cell.Type.Mine
         || cell.type == Cell.Type.Invalid)
        {
            return;
        }
        cell.isRevealed = true;
        state[cell.position.x, cell.position.y] = cell;
        if (cell.type == Cell.Type.Empty)
        {
            Flood(GetCell(cell.position.x - 1, cell.position.y));
            Flood(GetCell(cell.position.x + 1, cell.position.y));
            Flood(GetCell(cell.position.x, cell.position.y - 1));
            Flood(GetCell(cell.position.x, cell.position.y + 1));
        }
        board.Draw(state);
    }

    private void FlagCell()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 cellPosition = board.tilemap.WorldToCell(mousePosition);
        Cell cell = GetCell((int)cellPosition.x, (int)cellPosition.y);
        if (cell.type == Cell.Type.Invalid)
        {
            return;
        }
        cell.isFlagged = !cell.isFlagged;
        state[(int)cellPosition.x, (int)cellPosition.y] = cell;
        board.Draw(state);
    }
    private Cell GetCell(int x, int y)
    {
        if (!IsValidCell(x, y))
        {
            return new Cell();
        }
        Cell cell = state[x, y];
        if (cell.isRevealed)
        {
            return new Cell();
        }
        return cell;
    }
    private bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < width && y >= 0 && y < height;
    }
    private void GameOver()
    {
        gameOver = true;
        RestartButton.SetActive(true);
        Debug.Log("Game Over");
    }
    private void WinCondition()
    {
        bool win = true;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];
                if (cell.type != Cell.Type.Mine && !cell.isRevealed)
                {
                    win = false;
                    break;
                }
            }
        }
        if (win)
        {
            Win();
        }
    }
    public void Restart()
    {
        RestartButton.SetActive(false);
        NewGame();
    }
    public void Win()
    {
        SceneManager.LoadScene("YouWin");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
