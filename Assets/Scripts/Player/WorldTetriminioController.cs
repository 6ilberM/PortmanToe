using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Platfomer/World Tetriminio")]
public class WorldTetriminioController : MonoBehaviour
{
    private LayerMask groundLayer;
    private Rigidbody2D m_rb = default;
    private bool b_displayDebug = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_rb.velocity.y >= 0)
        {
            foreach (var item in collision.contacts)
            {
                if (groundLayer == item.collider.gameObject.layer && Mathf.Acos(Vector2.Dot(item.normal, Vector2.up)) * Mathf.Rad2Deg <= 20)
                {
#if UNITY_EDITOR
                    if (b_displayDebug) { Debug.DrawRay(item.point, item.normal, Color.white, .232f); }
#endif
                    //ToDo: Do something here

                    break;
                }
            }
        }
    }

    private void OnValidate() { if (m_rb == null) { m_rb = GetComponent<Rigidbody2D>(); } }

}
