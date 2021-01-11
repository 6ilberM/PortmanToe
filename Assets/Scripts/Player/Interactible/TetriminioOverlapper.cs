using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TetriminioOverlapper : MonoBehaviour
{
    [System.Serializable]
    public class OffsetsPerRotation
    {
        public int id;
        public Vector2 offset;
        public float spawnPointOffset;
    }

    public List<OffsetsPerRotation> offsetAndRotations = new List<OffsetsPerRotation>();

    private PlayerController owner = default;

    [SerializeField] private SpriteRenderer[] childRenderers;

    [SerializeField] private Color cachedColor;

    [SerializeField, HideInInspector()] private WorldTetriminioController worldTetriminio = null;
    [SerializeField, HideInInspector()] private Rigidbody2D rb = null;
    [SerializeField, HideInInspector()] private CompositeCollider2D compositeCollider = null;

    public int cachedValue = 0;
    OffsetsPerRotation cachedOffset;
    private void Awake()
    {
        owner = transform.parent.GetComponent<PlayerController>();

        GameManager.Instance.onPlaceBlock.AddListener(OnTetriminioPlaced);
        GameManager.Instance.onGameOver.AddListener(DestroyHelper);

        rb.isKinematic = true;
        compositeCollider.isTrigger = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }
    private void Start()
    {
        cachedValue = offsetAndRotations.FirstOrDefault(obj => obj.id == GameManager.Instance.activeBlockRot / -90).id;
    }

    private void Update()
    {
        transform.position = owner.transform.position
                             + new Vector3(
                                 (owner.GetSpriteRenderer.flipX ? -offsetAndRotations[cachedValue].spawnPointOffset : 0)
                                 + offsetAndRotations[cachedValue].offset.x * (owner.GetSpriteRenderer.flipX ? -1 : 1),
                                 offsetAndRotations[cachedValue].offset.y,
                                 0);
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
        worldTetriminio.enabled = true;

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
