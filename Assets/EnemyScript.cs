using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Transform leftBound;
    public Transform rightBound;
    public float distance;
    public float speed;
    private bool movingLeft = true;
    bool canMove = true;

    // Update is called once per frame
    void Update()
    {

        if (canMove) {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        RaycastHit2D leftRay = Physics2D.Raycast(leftBound.position, Vector2.left, distance);
        RaycastHit2D rightRay = Physics2D.Raycast(rightBound.position, Vector2.right, distance);
        if (leftRay.collider == true && !leftRay.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit left wall");
            if (movingLeft)
            {
                speed = -speed;
                movingLeft = false;
            }
        }
        if(rightRay.collider == true && !rightRay.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit right wall");
            if (!movingLeft)
            {
                speed = -speed;
                movingLeft = true;
            }
        } 
        if(rightRay.collider && leftRay.collider)
        {
            Debug.Log("Stuck");
            canMove = false;
        }
        
    }
}
