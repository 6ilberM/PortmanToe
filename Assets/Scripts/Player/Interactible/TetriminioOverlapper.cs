using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetriminioOverlapper : MonoBehaviour
{
    private PlayerController owner = default;

    [SerializeField] private SpriteRenderer[] childRenderers;

    [SerializeField] private Color cachedColor;

    [SerializeField, HideInInspector()] private WorldTetriminioController worldTetriminio = null;
    [SerializeField, HideInInspector()] private Rigidbody2D rb = null;

    private void Awake()
    {
        owner = transform.parent.GetComponent<PlayerController>();

        GameManager.Instance.onPlaceBlock.AddListener(OnTetriminioPlaced);
        GameManager.Instance.onGameOver.AddListener(DestroyHelper);

        rb.simulated = false;
        rb.isKinematic = true;

    }

    private void Update()
    {

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
        for (int i = 0; i < childRenderers.Length; i++)
        {
            Color col = targetColor;
            col.a = Mathf.Clamp01(alpha);
            childRenderers[i].color = col;
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
        if (worldTetriminio == null) { worldTetriminio = GetComponent<WorldTetriminioController>(); }
    }

}
