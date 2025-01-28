using UnityEngine;

public class Map
{
    public enum CellType { Open, Cover, Building }

    public CellType[,] grid; // 2D ������, �������������� �����
    public int size; // ������ ����� (size x size)

    public Map(int size)
    {
        this.size = size;
        grid = new CellType[size, size];
        InitializeMap();
    }

    private void InitializeMap()
    {
        // ��������� ����� ���������� ��������� � ��������
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                if (Random.value < 0.1f) // 10% ���� �� �������
                {
                    grid[x, y] = CellType.Cover;
                }
                else if (Random.value < 0.05f) // 5% ���� �� ������
                {
                    grid[x, y] = CellType.Building;
                }
                else
                {
                    grid[x, y] = CellType.Open;
                }
            }
        }
    }

    public bool IsInCover(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);
        return IsWithinBounds(x, y) && grid[x, y] == CellType.Cover;
    }

    public bool IsInBuilding(Vector2 position)
    {
        int x = Mathf.FloorToInt(position.x);
        int y = Mathf.FloorToInt(position.y);
        return IsWithinBounds(x, y) && grid[x, y] == CellType.Building;
    }

    private bool IsWithinBounds(int x, int y)
    {
        return x >= 0 && x < size && y >= 0 && y < size;
    }
}