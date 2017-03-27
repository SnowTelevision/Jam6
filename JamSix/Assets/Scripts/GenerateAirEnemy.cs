using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System;
using UnityEngine;

public class GenerateAirEnemy : MonoBehaviour
{

    public GameObject airEnemy;
    public Transform player;
    public PlayerController playerStatus;
    public float airHeight;
    public float airEnemyDuration;
    public float minimumAirFrequency;

    public float frequency;
    public Quaternion generateRotation;
    public Vector3 generatePosition;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(generateAir());
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    IEnumerator generateAir()
    {
        while (true)
        {
            generatePosition = new Vector3(transform.position.x + betterRandom(-6, 6), airHeight, transform.position.z);
            generateRotation.eulerAngles = new Vector3(0, 0, 0);
            GameObject newEnemy = Instantiate(airEnemy, generatePosition, generateRotation);

            newEnemy.GetComponent<Rigidbody>().AddForce(-newEnemy.transform.forward * 1f, ForceMode.Impulse);
            

            Destroy(newEnemy, 30f);

            yield return new WaitForSeconds(frequency);
        }
    }

    #region Better random number generator

    private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

    public static int betterRandom(int minimumValue, int maximumValue)
    {
        byte[] randomNumber = new byte[1];

        _generator.GetBytes(randomNumber);

        double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

        // We are using Math.Max, and substracting 0.00000000001, 
        // to ensure "multiplier" will always be between 0.0 and .99999999999
        // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
        double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

        // We need to add one to the range, to allow for the rounding done with Math.Floor
        int range = maximumValue - minimumValue + 1;

        double randomValueInRange = Math.Floor(multiplier * range);

        return (int)(minimumValue + randomValueInRange);
    }
    #endregion
}
