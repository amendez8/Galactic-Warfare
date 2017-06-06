using UnityEngine;
using System.Collections;

public class EnemyA : MonoBehaviour {
    public GameObject laser;          // laser prefab attacked to gameobject
    public GameObject upgradePrefab;  // holds object of upgrade
    public GameObject explosion;
    public float laserSpeed;          // speed lasers move at
    public float shootingRate;        // shots per second
    public int enemyPointValue = 100; // points gained when enemy destroyed
    public AudioClip laserSound;      // sound of laser firing
    public AudioClip deathSound;      // sound when enemy ship destroyed
    private PointSystem pointSystem;  // save points into this variable

	void Start () {
       pointSystem = GameObject.Find("Points").GetComponent<PointSystem>();
	}
	
	void Update () {        
        float prob = Time.deltaTime * shootingRate; // probability used to determine shooting rate
        if (Random.value < prob)                    // works between 0 and 1
            shoot();
    }

    void shoot()
    {
        Vector3 startingPosition = transform.position + new Vector3(0.0f, -0.5f, 0.0f);              // making sure laser is ahead of enemy ship
        GameObject bullet = Instantiate(laser, startingPosition, Quaternion.identity) as GameObject; // instantiating laser to ship
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -laserSpeed, 0.0f);          // make lasers move down
        AudioSource.PlayClipAtPoint(laserSound, transform.position);                                 // play laser sound at position of enemy ship
    }

    // if bullet hits enemy ship
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerBullet bullet = collider.gameObject.GetComponent<PlayerBullet>();

        if (bullet)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSound, transform.position); // play death sound when enemy destroyed
            pointSystem.Points(enemyPointValue);                         // send points over to pointSystem script
            GameObject shipExploding = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(shipExploding, 0.5f);
        }

    }
}
