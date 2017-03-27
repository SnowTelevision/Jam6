using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemyAI : MonoBehaviour
{
    public GameObject bullet;
    public float shootInterval;
    public Quaternion shootAngle;
    public float spreadAngle;
    public float bulletDuration;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(shootBullet(shootInterval));
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator shootBullet(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            shootAngle.eulerAngles = new Vector3(0, -180f, 0);
            GameObject middleBullet = Instantiate(bullet, transform.position, shootAngle);
            middleBullet.GetComponent<Rigidbody>().AddForce(middleBullet.transform.forward * 5f, ForceMode.Impulse);
            Destroy(middleBullet, 5f);

            shootAngle.eulerAngles = new Vector3(0, -180f + spreadAngle, 0);
            GameObject rightBullet = Instantiate(bullet, transform.position, shootAngle);
            rightBullet.GetComponent<Rigidbody>().AddForce(rightBullet.transform.forward * 5f, ForceMode.Impulse);
            Destroy(rightBullet, 5f);

            shootAngle.eulerAngles = new Vector3(0, -180f - spreadAngle, 0);
            GameObject leftBullet = Instantiate(bullet, transform.position, shootAngle);
            leftBullet.GetComponent<Rigidbody>().AddForce(leftBullet.transform.forward * 5f, ForceMode.Impulse);
            Destroy(leftBullet, 5f);
        }
    }
}
