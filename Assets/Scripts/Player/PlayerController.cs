using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CapsuleCollider2D)), DisallowMultipleComponent()]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private Rigidbody2D rb;

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            return;
        }

        if (capsuleCollider == null) { capsuleCollider = GetComponent<CapsuleCollider2D>(); }
        if (rb == null) { rb = GetComponent<Rigidbody2D>(); }
    }
}
