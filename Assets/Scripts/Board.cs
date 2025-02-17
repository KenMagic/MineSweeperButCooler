
using UnityEngine;
using UnityEngine.Tilemaps;

public class Board : MonoBehaviour
{
    public Tilemap tilemap { get; set; }
    public Tile flagTile;
    public Tile mineTile;
    public Tile numberTile1;
    public Tile numberTile2;
    public Tile numberTile3;
    public Tile numberTile4;
    public Tile numberTile5;
    public Tile numberTile6;
    public Tile numberTile7;
    public Tile numberTile8;
    public Tile explodedTile;
    public Tile emptyTile;

    public Tile revealedTile;
    public Tile unknownTile;

    private void Awake()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void Draw(Cell[,] state)
    {
        int width = state.GetLength(0);
        int height = state.GetLength(1);

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                Cell cell = state[x, y];
                Vector3Int position = new Vector3Int(x, y, 0);
                TileBase tile = GetTile(cell);
                tilemap.SetTile(position, tile);
            }
        }
    }

    private Tile GetTile(Cell cell)
    {
        if(cell.isRevealed)
        {
            return GetRevealedTile(cell);
        }
        else
        {
            return cell.isFlagged ? flagTile : emptyTile;
        }

    }

    private Tile GetRevealedTile(Cell cell)
    {
        switch(cell.type)
        {
            case Cell.Type.Empty:
                return revealedTile;
            case Cell.Type.Mine:
                return cell.exploded ? explodedTile : mineTile;
            case Cell.Type.Number:
                return GetNumberTile(cell.number);
            default: return null;
        }
    }
    private Tile GetNumberTile(object number)
    {
        switch(number)
        {
            case 1: return numberTile1;
            case 2: return numberTile2;
            case 3: return numberTile3;
            case 4: return numberTile4;
            case 5: return numberTile5;
            case 6: return numberTile6;
            case 7: return numberTile7;
            case 8: return numberTile8;
            default: return null;
        }
    }
}
