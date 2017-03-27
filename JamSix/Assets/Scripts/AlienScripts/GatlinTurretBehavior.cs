using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlinTurretBehavior : MonoBehaviour
{
    public float attackDelay;
    public float shootInterval;
    public float bulletDuration;
    public GameObject bullet;

    // Use this for initialization
    void Start ()
    {
        attackDelay = GetComponentInParent<GatlinBehavior>().attackDelay;
        StartCoroutine(shootBullet(attackDelay, shootInterval));
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public IEnumerator shootBullet(float delay, float interval)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            yield return new WaitForSeconds(interval);

            Quaternion shootAngle = new Quaternion();
            shootAngle.eulerAngles = transform.forward;
            GameObject middleBullet = Instantiate(bullet, transform.position, shootAngle);
            middleBullet.GetComponent<Rigidbody>().AddForce(transform.forward * 8f, ForceMode.Impulse);
            Destroy(middleBullet, bulletDuration);
        }
    }
}
