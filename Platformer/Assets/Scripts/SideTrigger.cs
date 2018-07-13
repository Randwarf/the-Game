using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrigger : MonoBehaviour {

    public bool touching;

	void Start () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        touching = true;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        touching = false;
    }

    void Update () {
		
	}
}
