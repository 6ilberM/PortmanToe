using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTetraScript : MonoBehaviour
{
    Color baseColor;
    Color newColor;
    public float parentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            SpriteRenderer SR = child.GetComponent<SpriteRenderer>();
            baseColor = SR.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        parentVelocity = transform.parent.GetComponent<Rigidbody2D>().velocity.x;
        if (transform.parent.GetComponent<Rigidbody2D>().velocity.x >= 0.1f)
        {
            transform.localPosition = new Vector2(1.5f, transform.localPosition.y);
        } else if(transform.parent.GetComponent<Rigidbody2D>().velocity.x <= -0.1f)
        {
            transform.localPosition = new Vector2(-1.5f, transform.localPosition.y);
        }
        if (GameManager.Instance.destroyFake)
        {
            Debug.Log("Bye");
            GameManager.Instance.destroyFake = false;
            Destroy(this.gameObject);
        }
        foreach (Transform child in transform)
        {
            SpriteRenderer SR = child.GetComponent<SpriteRenderer>();
            SR.color = newColor;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        newColor = Color.red;
        GameManager.Instance.canPlace = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        newColor = Color.green;
        GameManager.Instance.canPlace = true;
    }
}
