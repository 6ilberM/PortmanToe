using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playfield : MonoBehaviour
{
    private static Playfield _instance;
    public GameObject mainCamera;
    public GameObject CameraAnchor;
    // The Grid itself
    public int w = 10;
    public int h = 25;
    public Transform[,] grid;

    public static Playfield Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    private void Start()
    {
        grid = new Transform[w, h];
    }
    public Vector2 roundVec2(Vector2 v)
    {

        if(mainCamera.transform.position != CameraAnchor.transform.position)
        {
            float diffX = mainCamera.transform.position.x - CameraAnchor.transform.position.x;
            float diffY = mainCamera.transform.position.y - CameraAnchor.transform.position.y;
            v.x = v.x - diffX;
            v.y = v.y - diffY;
        }
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public bool insideBorder(Vector2 pos)
    {
        return ((int)pos.x >= 0 &&
                (int)pos.x < w &&
                (int)pos.y >= 0 && 
                (int)pos.y <= 24);
    }

    public void deleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
        }
    }

    public void decreaseRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                // Move one towards bottom
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                // Update Block position
                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }
    public void decreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
            decreaseRow(i);
    }
    public bool isRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
            if (grid[x, y] == null)
                return false;
        return true;
    }
    public void deleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                --y;
            }
        }
    }
}
