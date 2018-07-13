using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlutonAI : MonoBehaviour {

    public GameObject projectilePrefabLeft;
    public GameObject projectilePrefabRight;
    private List<GameObject> Projectiles = new List<GameObject>();
    private List<int> DirectionMultiplier = new List<int>();

    Rigidbody2D rb;
    Animator animator;

    public float moveSpeed;
    public float rollSpeed;
    public float projectileSpeed;
    public float cooldown;

    const int StateIdle      = 0; //animation states
    const int StateWalkLeft  = 1;
    const int StateWalkRight = 2;
    const int StateSmash     = 3;
    const int StateFall      = 4;
    const int StateRoll      = 5;
    const int StateStand     = 6;

    private Transform target;
    float nextShot; //timer when the next shot is available
    bool isRollingLeft;
    bool isRollingRight;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        nextShot = Time.time;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Tile")
        {
            Vector3 projectilePos = new Vector3(transform.position.x, transform.position.y - 1);
            GameObject bolt = (GameObject)Instantiate(projectilePrefabRight, projectilePos, Quaternion.identity);
            Projectiles.Add(bolt);
            DirectionMultiplier.Add(1);

            GameObject bolt2 = (GameObject)Instantiate(projectilePrefabLeft, projectilePos, Quaternion.identity);
            Projectiles.Add(bolt2);
            DirectionMultiplier.Add(-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Trigger = GameObject.Find("SideTrigger");
        SideTrigger sideTrigger = Trigger.GetComponent<SideTrigger>();
        int directionX = 0;
        int rollDirectionX = 0;
        int state = animator.GetInteger("state");
        if ((target.position.x < transform.position.x) && (target.position.x + 2 < transform.position.x) && (target.position.x + 5 > transform.position.x) && (isRollingLeft == false) && (isRollingRight == false))
        {
            directionX = -1;
            rollDirectionX = 0;
            animator.SetInteger("state", StateWalkLeft);
        }
        else if ((target.position.x > transform.position.x) && (target.position.x - 2 > transform.position.x) && (target.position.x - 5 < transform.position.x) && (isRollingLeft == false) && (isRollingRight == false))
        {
            directionX = 1;
            rollDirectionX = 0;
            animator.SetInteger("state", StateWalkRight);
        }
        else if ((target.position.x + 5 < transform.position.x )|| (isRollingLeft == true))
        {
            rollDirectionX = -1;
            isRollingLeft = true;
            if(sideTrigger.touching == true && state != StateRoll)
            {
                directionX = -1;
                rollDirectionX = 0;
                animator.SetInteger("state", StateWalkLeft);
                isRollingLeft = false;
            }
        }
        else if ((target.position.x - 5 > transform.position.x) || (isRollingRight == true))
        {
            rollDirectionX = 1;
            isRollingRight = true;
            if (sideTrigger.touching == true && state != StateRoll)
            {
                directionX = 1;
                rollDirectionX = 0;
                animator.SetInteger("state", StateWalkRight);
                isRollingRight = false;
            }
        }
        else
        {
            rollDirectionX = 0;
            directionX = 0;
            if (Time.time > nextShot)
            {
                animator.SetInteger("state", StateSmash);
                nextShot = Time.time + cooldown;
                rb.velocity = new Vector2(rb.velocity.x, 3);
            }
            else
            {
                animator.SetInteger("state", StateIdle);
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
        if (directionX != 0)
        {
            rb.velocity = new Vector2(directionX * moveSpeed, rb.velocity.y); //Walk
        }

        if (rollDirectionX != 0) //Roll
        {
            if((state != StateFall) && (state != StateRoll) && (state != StateStand))
            {
                animator.SetInteger("state", StateFall);
            }
            else if(state == StateFall)
            {
                animator.SetInteger("state", StateRoll);
            }
           else if(state == StateRoll)
           {
                rb.velocity = new Vector2(rollDirectionX * rollSpeed, rb.velocity.y);
                if (sideTrigger.touching == true)
                {
                    animator.SetInteger("state", StateStand);
                    isRollingLeft = false;
                    isRollingRight = false;
                }
            }
        }
    }
}
