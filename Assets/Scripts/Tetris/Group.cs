
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    // Time since last gravity tick
    float lastFall = 0;

   

    //public GameObject playfieldHolder;
    // Rotation index
    public int rotation = 1;

    // Start is called before the first frame update
    void Start()
    {
        //playfieldCall = playfieldHolder.GetComponent<Playfield>();
        GameManager.Instance.activeBlockTag = this.tag;
        // Default position not valid? Then it's game over
        if (!isValidGridPos())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(1, 0, 0);
        }

        // Move Right
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if valid
            if (isValidGridPos())
                // It's valid. Update grid.
                updateGrid();
            else
                // It's not valid. revert.
                transform.position += new Vector3(-1, 0, 0);
        }

        // Rotate
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
                if (rotation == 1)
                {
                    rotation = 2;
                }
                else if (rotation == 2)
                {
                    rotation = 3;
                }
                else if (rotation == 3)
                {
                    rotation = 4;
                }
                else
                {
                    rotation = 1;
                }
            }
            else
                // It's not valid. revert.
                transform.Rotate(0, 0, 90);
        }

        // Move Downwards and Fall
        else if (Input.GetKeyDown(KeyCode.DownArrow) ||
                 Time.time - lastFall >= 1)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else
            {
                // Remove tag from GameManager
                GameManager.Instance.activeBlockTag = "";
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Playfield.Instance.deleteFullRows();

                // Play sound from SoundManager
                SoundManager.Instance.PlaySound("TetrisLand");
                // Spawn next Group
                GameManager.Instance.spawnBlock = true;

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
        // Deletes block if player pulls
        if (GameManager.Instance.pullBlock)
        {
            GameManager.Instance.pullBlock = false;
            GameManager.Instance.spawnBlock = true;
            Destroy(this.gameObject);
        }
        
    }
    bool isValidGridPos()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.Instance.roundVec2(child.position);

            // Not inside Border?
            if (!Playfield.Instance.insideBorder(v))
                return false;

            //Debug.Log((int)v.x + " " + (int)v.y);
            // Block in grid cell (and not part of same group)?
            if (Playfield.Instance.grid[(int)v.x, (int)v.y] != null &&
                Playfield.Instance.grid[(int)v.x, (int)v.y].parent != transform)
                return false;
        }
        return true;
    }
    void updateGrid()
    {
        // Remove old children from grid
        for (int y = 0; y < Playfield.Instance.h; ++y)
            for (int x = 0; x < Playfield.Instance.w; ++x)
                if (Playfield.Instance.grid[x, y] != null)
                    if (Playfield.Instance.grid[x, y].parent == transform)
                        Playfield.Instance.grid[x, y] = null;

        // Add new children to grid
        foreach (Transform child in transform)
        {
            Vector2 v = Playfield.Instance.roundVec2(child.position);
            Playfield.Instance.grid[(int)v.x, (int)v.y] = child;
        }
    }
}
