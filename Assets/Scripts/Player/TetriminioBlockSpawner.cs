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

    private GameObject tetrminio;

    public GameObject Tetrminio => tetrminio;

    public bool TryPopulateTetriminio()
    {
        bool result = false;

        if (GameManager.Instance.pullCharge > 0 && !GameManager.Instance.tetrisPaused)
        {
            switch (GameManager.Instance.activeBlockTag)
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

            GameManager.Instance.onPullBlock?.Invoke();
            SoundManager.Instance.PlaySound("SpawnBlock");
        }

        return result;
    }

}
