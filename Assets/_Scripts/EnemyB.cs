using UnityEngine;
using System.Collections;

public class EnemyB : MonoBehaviour {
    public GameObject laserB;
    public GameObject upgradePrefab2;
    public GameObject explosion;
    public float laserSpeedB;
    public float shootingRateB;
    public int enemyPointValueB = 200;
    public AudioClip laserSoundB;
    public AudioClip deathSoundB;
    private PointSystem pointSystemB;

    public Transform target;    // a target which the enemies will fly towards
    public float speed;         // speed the enemies move at
    public float amplitude;     // how far the enemy sway to the side
    public float frequency;     // the frequency at which they sway
    private float startime;     // time counter
    private Vector3 direction;  // direction they'll fly in
    private Vector3 orthogonal; // orthogonal vector for the sway movement.
	
	void Start () {
        pointSystemB = GameObject.Find("Points").GetComponent<PointSystem>();

        startime = Time.time;
        direction = (target.position - transform.position).normalized;
        orthogonal = new Vector3(-direction.x, 0, 0);
	}

    void FixedUpdate()
    {
        float time = Time.time - startime;
        transform.position += direction * speed + orthogonal * amplitude * Mathf.Sin(frequency * time);
    }

	void Update () {
        float prob = Time.deltaTime * shootingRateB;
        if (Random.value < prob)
            ShootB();
	}

    void ShootB()
    {
        Vector3 startingPosition = transform.position + new Vector3(0.0f, -0.5f, 0.0f);
        GameObject bullet = Instantiate(laserB, startingPosition, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -laserSpeedB, 0.0f);
        AudioSource.PlayClipAtPoint(laserSoundB, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerBullet bullet = collider.gameObject.GetComponent<PlayerBullet>();
        if (bullet)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(deathSoundB, transform.position);
            pointSystemB.Points(enemyPointValueB);
            GameObject shipExploding = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(shipExploding, 0.5f);
        }

    }
}
