using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour {

    public Transform[] Backgrounds;
    private float[] ParallaxScales;
    public float smoothing = 1f;
    private Transform cam;
    private Vector3 previousCamPos;

    void Awake ()
    {
        cam = Camera.main.transform;
    }
	// Use this for initialization
	void Start ()
    {
        previousCamPos = cam.position;
        ParallaxScales = new float[Backgrounds.Length];
        for (int i = 0; i < Backgrounds.Length; i++)
        {
            ParallaxScales[i] = Backgrounds[i].position.z * -1;
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
		for (int i = 0; i < Backgrounds.Length; i++)
        {
            float parallaxx = (previousCamPos.x - cam.position.x) * ParallaxScales[i];
            float parallaxy = (previousCamPos.y - cam.position.y) * ParallaxScales[i];
            float targetPosX = Backgrounds[i].position.x + parallaxx;
            float targetPosY = Backgrounds[i].position.y + parallaxy;
            Vector3 targetPos = new Vector3(targetPosX, targetPosY, Backgrounds[i].position.z);
            Backgrounds[i].position = Vector3.Lerp(Backgrounds[i].position, targetPos, smoothing * Time.deltaTime);
        }
        previousCamPos = cam.position;
	}
}
