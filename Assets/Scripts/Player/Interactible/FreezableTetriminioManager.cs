using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezableTetriminioManager : MonoBehaviour
{
    public static List<WorldTetriminioController> allTheTetrisBlocks = new List<WorldTetriminioController>();

    private void Awake() => GameManager.Instance.onGameOver.AddListener(DestoyAllTetrisBlocks);

    private void DestoyAllTetrisBlocks()
    {
        foreach (WorldTetriminioController tetriminio in allTheTetrisBlocks) { Destroy(tetriminio); }
    }

    private void OnDestroy()
    {
        GameManager.Instance.onGameOver.RemoveListener(DestoyAllTetrisBlocks);
        allTheTetrisBlocks.Clear();
    }

}
