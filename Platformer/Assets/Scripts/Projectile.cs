using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    Animator animator;

    const int StatePrime = 0;
    const int StateRight = 1;
    const int StateLeft = 2;

    void Start ()
    {
        animator = GetComponent<Animator>();

        GameObject Player = GameObject.Find("Player");
        Player_Movement player_Movement = Player.GetComponent<Player_Movement>();
        if(player_Movement.isFacingLeft == true)
        {
            animator.SetInteger("state", StateLeft);
        }
        else
        {
            animator.SetInteger("state", StateRight);
        }
    }
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

	void OnCollisionEnter2D (Collision2D col)
    {
        if (col.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
	
	void Update ()
    {
	
	}
    
}
