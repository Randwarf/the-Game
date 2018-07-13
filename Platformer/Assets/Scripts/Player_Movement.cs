using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    public GameObject projectilePrefab;
    private List<GameObject> Projectiles = new List<GameObject>();
    private List<int> DirectionMultiplier = new List<int>();

    public float moveSpeed;
    public float jumpSpeed;
    public float projectileSpeed;

    Rigidbody2D rb;
    Animator animator;

    const int StateIdleRight = 0;
    const int StateIdleLeft = 1;
    const int StateWalkRight = 2;
    const int StateWalkLeft = 3;
    public bool isFacingLeft;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

	void Update ()
    {
        if(Input.GetKey(KeyCode.A))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            animator.SetInteger("state",StateWalkLeft);
            isFacingLeft = true;
        }

        else if(Input.GetKeyUp(KeyCode.A))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetInteger("state",StateIdleLeft);
        }


        else if(Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            animator.SetInteger("state",StateWalkRight);
            isFacingLeft = false;
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            animator.SetInteger("state",StateIdleRight);
        }


        if (Input.GetKeyDown(KeyCode.W))
        {
           if (rb.velocity.y == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, +jumpSpeed);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bolt = (GameObject)Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Projectiles.Add(bolt);
            if(isFacingLeft == true)
            {
                DirectionMultiplier.Add(-1);
            }
            else
            {
                DirectionMultiplier.Add(1);
            }
        }
        for (int i = 0; i < Projectiles.Count; i++)
        {
            GameObject goBolt = Projectiles[i];
            if(goBolt != null)
            {
                goBolt.transform.Translate(new Vector2(1*DirectionMultiplier[i], 0) * Time.deltaTime * projectileSpeed);
            }
        }
    }
}
