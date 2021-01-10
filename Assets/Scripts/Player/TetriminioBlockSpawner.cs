using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[AddComponentMenu("Platformer/Tetriminio Spawner")]
public class TetriminioBlockSpawner : MonoBehaviour
{
    public GameObject IBlock = default;
    public GameObject JBlock = default;
    public GameObject LBlock = default;
    public GameObject OBlock = default;
    public GameObject SBlock = default;
    public GameObject TBlock = default;
    public GameObject ZBlock = default;
    public GameObject IBlockF = default;
    public GameObject JBlockF = default;
    public GameObject LBlockF = default;
    public GameObject OBlockF = default;
    public GameObject SBlockF = default;
    public GameObject TBlockF = default;
    public GameObject ZBlockF = default;

    private GameObject tetrminio;
    private bool hasFake;
    private string FakeID;

    public GameObject Tetrminio => tetrminio;

    public bool TryPopulateTetriminio()
    {
        bool result = false;

        if (GameManager.Instance.pullCharge > 0 && !GameManager.Instance.tetrisPaused && !hasFake)
        {
            switch (GameManager.Instance.activeBlockTag)
            {
                case "I":
                    tetrminio = IBlockF;
                    FakeID = "I";
                    break;

                case "J":
                    tetrminio = JBlockF;
                    FakeID = "J";
                    break;
                case "L":
                    tetrminio = LBlockF;
                    FakeID = "L";
                    break;
                case "O":
                    tetrminio = OBlockF;
                    FakeID = "O";
                    break;
                case "S":
                    tetrminio = SBlockF;
                    FakeID = "S";
                    break;
                case "T":
                    tetrminio = TBlockF;
                    FakeID = "T";
                    break;
                case "Z":
                    tetrminio = ZBlockF;
                    FakeID = "Z";
                    break;
            }
            result = true;
            hasFake = true;
            GameManager.Instance.onPullBlock?.Invoke();
            SoundManager.Instance.PlaySound("SpawnBlock");
        } else if (GameManager.Instance.pullCharge > 0 && !GameManager.Instance.tetrisPaused && hasFake)
        {
            switch (FakeID)
            {
                case "I":
                    tetrminio = IBlock;
                    break;

                case "J":
                    tetrminio = JBlock;
                    break;
                case "L":
                    tetrminio = LBlock;
                    break;
                case "O":
                    tetrminio = OBlock;
                    break;
                case "S":
                    tetrminio = SBlock;
                    break;
                case "T":
                    tetrminio = TBlock;
                    break;
                case "Z":
                    tetrminio = ZBlock;
                    break;
            }
            result = true;
            hasFake = false;
            
            SoundManager.Instance.PlaySound("SpawnBlock");
        }

        return result;
    }

}
