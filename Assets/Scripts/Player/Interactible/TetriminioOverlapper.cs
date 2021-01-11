using System.Collections.Generic;
using UnityEngine;

public class TetriminioOverlapper : MonoBehaviour
{
    [System.Serializable]
    public struct OffsetsPerRotation
    {
        public int id;
        public Vector2 offset;
    }

    public List<OffsetsPerRotation> offsetAndRotations = new List<OffsetsPerRotation>();

    private PlayerController owner = default;

    [SerializeField] private SpriteRenderer[] childRenderers;

    [SerializeField] private Color cachedColor;

    [SerializeField, HideInInspector()] private WorldTetriminioController worldTetriminio = null;
    [SerializeField, HideInInspector()] private Rigidbody2D rb = null;
    [SerializeField, HideInInspector()] private CompositeCollider2D compositeCollider = null;

    private void Awake()
    {
        owner = transform.parent.GetComponent<PlayerController>();

        GameManager.Instance.onPlaceBlock.AddListener(OnTetriminioPlaced);
        GameManager.Instance.onGameOver.AddListener(DestroyHelper);

        rb.isKinematic = true;
        compositeCollider.isTrigger = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    private void Update()
    {
        transform.position = owner.transform.position /*+ GameManager.Instance.activeBlockRot */ * (owner.GetSpriteRenderer.flipX ? -1 : 1);
    }

    private void DestroyHelper()
    {
        //ToDO: play Particle Effect?
        Destroy(this.gameObject);
    }

    private void OnTetriminioPlaced()
    {
        SetChildrenColorAndAlpha(cachedColor);

        rb.simulated = true;
        rb.isKinematic = false;
        compositeCollider.isTrigger = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.transform.SetParent(null);

        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetChildrenColorAndAlpha(Color.red, .5f);
        GameManager.Instance.canPlace = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SetChildrenColorAndAlpha(Color.green, .5f);

        GameManager.Instance.canPlace = true;
    }

    private void SetChildrenColorAndAlpha(Color targetColor, float alpha = 1f)
    {
        Color col = targetColor;
        col.a = Mathf.Clamp01(alpha);
        foreach (SpriteRenderer v in childRenderers)
        {
            v.color = col;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.onPlaceBlock.RemoveListener(OnTetriminioPlaced);
        GameManager.Instance.onGameOver.RemoveListener(DestroyHelper);
    }

    private void OnValidate()
    {
        if (Application.isPlaying) { return; }

        if (childRenderers.Length == 0) { childRenderers = this.GetComponentsInChildren<SpriteRenderer>(); }
        if (childRenderers.Length >= 1) { cachedColor = childRenderers[0].color; }
        if (rb == null) { rb = GetComponent<Rigidbody2D>(); }
        if (compositeCollider == null) { compositeCollider = GetComponent<CompositeCollider2D>(); }
        if (worldTetriminio == null) { worldTetriminio = GetComponent<WorldTetriminioController>(); }
    }

}
