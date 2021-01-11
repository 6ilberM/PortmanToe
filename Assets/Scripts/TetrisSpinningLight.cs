using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisSpinningLight : MonoBehaviour
{
    Vector3 rotationVec;
    // Start is called before the first frame update
    void Start()
    {
        rotationVec = transform.rotation.eulerAngles;
        StartCoroutine("Spin");
    }

    IEnumerator Spin()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            rotationVec.z += 1f;
            transform.rotation = Quaternion.Euler(rotationVec);
        }
    }
}
