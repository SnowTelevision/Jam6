using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Cameras;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerController : MonoBehaviour
{
    public GameObject thirdPerson;
    public GameObject thirdPersonCam;
    public float airMoveSpeed;
    public float maxWidth;
    public float maxLength;
    public float invincibleTime;
    public float airBulletDuration;
    public GameProcess gameManager;
    public GameObject airBlocker;
    public float lockTime;

    public GameObject airBullet;
    public float airShootInterval;
    public Quaternion airShootAngle;
    public float spreadAngle;
    
    public Quaternion originalRotation;
    public bool isInvincible;
    public Coroutine shootAir;

    // Use this for initialization
    void Start ()
    {
        isInvincible = false;

        originalRotation.eulerAngles = new Vector3(0, 0, 0);

        //StartCoroutine(shootAirBullet(airShootInterval));
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        
        if (gameManager.isGameOver)
        {
            StopAllCoroutines();
            return;
        }
        
        if(Input.GetButtonDown("Jump"))
        {
            shootAir = StartCoroutine(shootAirBullet(0.25f));
        }

        if (Input.GetButtonUp("Jump"))
        {
            StopCoroutine(shootAir);
        }
    }

    private void FixedUpdate()
    {
        if (gameManager.isGameOver)
        {
            StopAllCoroutines();
            return;
        }

        transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * airMoveSpeed * Time.deltaTime, transform.position.y, transform.position.z);

        if (transform.position.x > maxWidth)
        {
            transform.position = new Vector3(maxWidth, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -maxWidth)
        {
            transform.position = new Vector3(-maxWidth, transform.position.y, transform.position.z);
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Input.GetAxis("Vertical") * airMoveSpeed * Time.deltaTime);

        if (transform.position.z > maxLength - 245.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, maxLength - 245.5f);
        }

        if (transform.position.z < - 245.5f - maxLength)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -245.5f - maxLength);
        }
    }

    public IEnumerator shootAirBullet(float interval)
    {
        while (true)
        {
            airShootAngle.eulerAngles = new Vector3(0, 0, 0);
            GameObject middleBullet = Instantiate(airBullet, transform.position, airShootAngle);
            middleBullet.GetComponentInChildren<Rigidbody>().AddForce(middleBullet.transform.forward * 8f, ForceMode.Impulse);
            Destroy(middleBullet, 10f);

            airShootAngle.eulerAngles = new Vector3(0, 0 + spreadAngle, 0);
            GameObject rightBullet = Instantiate(airBullet, transform.position, airShootAngle);
            rightBullet.GetComponentInChildren<Rigidbody>().AddForce(rightBullet.transform.forward * 8f, ForceMode.Impulse);
            Destroy(rightBullet, 10f);

            airShootAngle.eulerAngles = new Vector3(0, 0 - spreadAngle, 0);
            GameObject leftBullet = Instantiate(airBullet, transform.position, airShootAngle);
            leftBullet.GetComponentInChildren<Rigidbody>().AddForce(leftBullet.transform.forward * 8f, ForceMode.Impulse);
            Destroy(leftBullet, 10f);

            yield return new WaitForSeconds(interval);
        }
    }
}
