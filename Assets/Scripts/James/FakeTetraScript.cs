using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTetraScript : MonoBehaviour
{
    Color newColor;
    PlayerController owner = default;
    [SerializeField] SpriteRenderer[] childRenderers;

    private void Awake()
    {
        owner = transform.parent.GetComponent<PlayerController>();
        GameManager.Instance.onPlaceBlock.AddListener(MarkForDeath);
    }

    private void MarkForDeath()
    {
        Debug.Log("Destroy This Object");
        GameManager.Instance.destroyFake = false;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < childRenderers.Length; i++) { childRenderers[i].color = Color.red; }
        GameManager.Instance.canPlace = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < childRenderers.Length; i++) { childRenderers[i].color = Color.green; }
        GameManager.Instance.canPlace = true;
    }

    private void OnValidate()
    {
        if (!Application.isPlaying) { return; }

        if (childRenderers.Length == 0) { childRenderers = this.GetComponentsInChildren<SpriteRenderer>(); }
    }
}
