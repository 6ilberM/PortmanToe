
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Group : MonoBehaviour
{
    // Time since last gravity tick
    float lastFall = 0;

    //public GameObject playfieldHolder;
    // Rotation index
    public int rotation = 0;

    public bool buttonDownHeld { get; private set; }

    private float fastFallTimer;

    private void Awake()
    {
        GameManager.Instance.onPullBlock.AddListener(PullBlock);
    }

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
        if (!GameManager.Instance.tetrisPaused)
        {
            GameManager.Instance.activeBlockRot = rotation * 90;

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

                    rotation++;

                    if (rotation > 3)
                    {
                        rotation = 0;
                    }

                }
                else
                    // It's not valid. revert.
                    transform.Rotate(0, 0, 90);
            }

            // Move Downwards and Fall
            else if (Input.GetKeyDown(KeyCode.DownArrow)
                || (Input.GetKey(KeyCode.DownArrow) && fastFallTimer <= 0)
                || Time.time - lastFall >= 1)
            {
                buttonDownHeld = true;

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
                    GameManager.Instance.onSpawnBlock?.Invoke();

                    // Disable script
                    enabled = false;
                }

                lastFall = Time.time;

                fastFallTimer = 0.135f;

            }

            if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                buttonDownHeld = false;
                fastFallTimer = 1.2f;
            }

            if (buttonDownHeld && fastFallTimer > 0) { fastFallTimer -= Time.deltaTime; }
        }

    }

    /// <summary>
    /// DeletesBlock if PlayerPulls
    /// </summary>
    private void PullBlock()
    {
        if (enabled)
        {
            GameManager.Instance.onSpawnBlock?.Invoke();
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
            {
                Debug.Log("Boundary Collssion Detection");
                return false;
            }
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

    private void OnDestroy()
    {
        GameManager.Instance.onPullBlock.RemoveListener(PullBlock);
    }
}
