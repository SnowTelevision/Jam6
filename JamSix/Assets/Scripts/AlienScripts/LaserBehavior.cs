using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public float attackDelay;
    public float laserRange;
    public float attackInterval;
    public GameObject laserBeam;
    public SpriteRenderer marker;
    public SelectWeapon attackPattern;

    public GameObject topMarker;
    public GameObject bottomMarker;
    public GameObject leftMarker;
    public GameObject rightMarker;

    public GameObject laserStartRotation;
    public GameObject laserEndRotation;
    public Vector3 laserStartEuler;
    public Vector3 laserEndEuler;
    public bool isAttacking;
    public GameProcess gameManager;
    //public int attackPattern;

    // Use this for initialization
    void Start ()
    {
        isAttacking = false;

        attackPattern = GetComponentInParent<SelectWeapon>();
        gameManager = FindObjectOfType<GameProcess>();

        if (attackPattern.attackPattern == 0) //Top right
        {
            laserStartRotation = rightMarker;
            laserEndRotation = topMarker;
            marker.color = Color.red;
        }

        else if (attackPattern.attackPattern == 1) //Top left
        {
            laserStartRotation = leftMarker;
            laserEndRotation = topMarker;
            marker.color = Color.blue;
        }

        else if (attackPattern.attackPattern == 2) //Bottom left
        {
            laserStartRotation = bottomMarker;
            laserEndRotation = leftMarker;
            marker.color = Color.yellow;
        }

        else if (attackPattern.attackPattern == 3) //Bottom right
        {
            laserStartRotation = bottomMarker;
            laserEndRotation = rightMarker;
            marker.color = Color.green;
        }

        laserBeam.transform.LookAt(laserStartRotation.transform);
        laserStartEuler = laserBeam.transform.eulerAngles;
        laserBeam.transform.LookAt(laserEndRotation.transform);
        laserEndEuler = laserBeam.transform.eulerAngles;

        if (laserStartEuler.y < laserEndEuler.y)
        {
            laserEndEuler.y -= 360;
        }

        StartCoroutine(shootLaser(attackDelay, attackInterval));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (attackPattern.attackPattern == 0) //Top right
        {
            laserStartRotation = rightMarker;
            laserEndRotation = topMarker;
            marker.color = Color.red;
        }

        else if (attackPattern.attackPattern == 1) //Top left
        {
            laserStartRotation = leftMarker;
            laserEndRotation = topMarker;
            marker.color = Color.blue;
        }

        else if (attackPattern.attackPattern == 2) //Bottom left
        {
            laserStartRotation = bottomMarker;
            laserEndRotation = leftMarker;
            marker.color = Color.yellow;
        }

        else if (attackPattern.attackPattern == 3) //Bottom right
        {
            laserStartRotation = bottomMarker;
            laserEndRotation = rightMarker;
            marker.color = Color.green;
        }


        if (laserStartEuler.y < laserEndEuler.y)
        {
            laserEndEuler.y -= 360;
        }

        if (isAttacking)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, laserBeam.transform.forward, out hit, Mathf.Infinity))
            {

                if (hit.transform.name == "AirBody")
                {
                    print("Ray hits:" + hit.transform.name + ", distance:" + hit.distance);
                    //player get hit
                    gameManager.score--;
                }
            }
        }
	}

    public IEnumerator shootLaser(float delay, float interval)
    {
        yield return new WaitForSeconds(delay);

        isAttacking = true;

        while(true)
        {
            StartCoroutine(laserAni());
            yield return new WaitForSeconds(interval);
            laserBeam.transform.LookAt(laserStartRotation.transform);
            laserStartEuler = laserBeam.transform.eulerAngles;
            laserBeam.transform.LookAt(laserEndRotation.transform);
            laserEndEuler = laserBeam.transform.eulerAngles;
        }
    }

    public IEnumerator laserAni()
    {
        laserBeam.transform.LookAt(laserStartRotation.transform);
        laserBeam.SetActive(true);

        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1f)
        {
            Vector3 newRotation = new Vector3(laserBeam.transform.eulerAngles.x, Mathf.LerpAngle(laserBeam.transform.eulerAngles.y, laserEndEuler.y, t), laserBeam.transform.eulerAngles.z);

            laserBeam.transform.eulerAngles = newRotation;

            yield return null;
        }

        laserBeam.SetActive(false);
    }
}
