using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemProjectile : MonoBehaviour
{

    public float timeTillBurnOut;
    float burnOutTime;

    void Start()
    {
        burnOutTime = timeTillBurnOut + Time.time;
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag != "enemy")
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (burnOutTime <= Time.time)
        {
            Destroy(gameObject);
        }
    }
}

