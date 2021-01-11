using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Platfomer/World Tetriminio")]
public class WorldTetriminioController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Rigidbody2D rb = default;
    [SerializeField] private bool b_displayDebug = true;

    [SerializeField] Vector2 positionOffset;

    public Rigidbody2D GetRigidBody => rb;

    private void OnEnable() => FreezableTetriminioManager.allTheTetrisBlocks.Add(this);

    private void OnDisable() => FreezableTetriminioManager.allTheTetrisBlocks.Remove(this);

    public void FreezePosition()
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void ToggleFreezePosition()
    {
        rb.isKinematic = !rb.isKinematic;

        rb.constraints = rb.isKinematic ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.velocity.y >= 0)
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

    private void OnValidate() { if (rb == null) { rb = GetComponent<Rigidbody2D>(); } }

}
