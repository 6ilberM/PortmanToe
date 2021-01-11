using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezableTetriminioManager : MonoBehaviour
{
    public static List<WorldTetriminioController> allTheTetrisBlocks = new List<WorldTetriminioController>();

    private void Start() => GameManager.Instance.onGameOver.AddListener(DestoyAllTetrisBlocks);

    private void DestoyAllTetrisBlocks()
    {
        for (int i = 0; i < allTheTetrisBlocks.Count; i++)
        {
            WorldTetriminioController tetriminio = allTheTetrisBlocks[i];
            Destroy(tetriminio.gameObject);
            allTheTetrisBlocks.Clear();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.onGameOver.RemoveListener(DestoyAllTetrisBlocks);
        allTheTetrisBlocks.Clear();
    }

}
