using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorAI : MonoBehaviour {

    public GameObject projectilePrefabLeft;
    public GameObject projectilePrefabRight;
    private List<GameObject> Projectiles = new List<GameObject>();
    private List<int> DirectionMultiplier = new List<int>();

    Rigidbody2D rb;
    Animator animator;

    public float moveSpeed;
    public float projectileSpeed;
    public float cooldown;

    const int StateIdleLeft   = 0; //animation states
    const int StateWalkLeft   = 1;
    const int StateSwingLeft  = 2;
    const int StateIdleRight  = 3;
    const int StateWalkRight  = 4;
    const int StateSwingRight = 5;

    private Transform target;
    float nextShot; //timer when the next shot is available

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nextShot = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        int directionX = 0;
        if ((target.position.x < transform.position.x) && (target.position.x + 2 < transform.position.x))
        {
            directionX = -1;
            animator.SetInteger("state", StateWalkLeft);
        }
        else if ((target.position.x > transform.position.x) && (target.position.x - 2 > transform.position.x))
        {
            directionX = 1;
            animator.SetInteger("state", StateWalkRight);
        }
        else
        {

            directionX = 0;
            if(target.position.x > transform.position.x)
            {
                if(Time.time > nextShot)
                {
                    GameObject bolt = (GameObject)Instantiate(projectilePrefabRight, transform.position, Quaternion.identity);
                    Projectiles.Add(bolt);
                    DirectionMultiplier.Add(1);
                    animator.SetInteger("state", StateSwingRight);
                    nextShot = Time.time + cooldown;
                }
                else
                {
                    animator.SetInteger("state", StateIdleRight);
                }
            }
            else
            {
                if (Time.time > nextShot)
                {
                    GameObject bolt = (GameObject)Instantiate(projectilePrefabLeft, transform.position, Quaternion.identity);
                    Projectiles.Add(bolt);
                    DirectionMultiplier.Add(-1);
                    animator.SetInteger("state", StateSwingLeft);
                    nextShot = Time.time + cooldown;
                }
                else
                {
                    animator.SetInteger("state", StateIdleLeft);
                }
               
            }
        }

        for (int i = 0; i < Projectiles.Count; i++) //projectile moving
        {
            GameObject goBolt = Projectiles[i];
            if (goBolt != null)
            {
                goBolt.transform.Translate(new Vector2(1 * DirectionMultiplier[i], 0) * Time.deltaTime * projectileSpeed);
            }
        }

        rb.velocity = new Vector2(directionX*moveSpeed, rb.velocity.y);

    }
}
