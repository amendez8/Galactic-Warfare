using UnityEngine;
using System.Collections;

public class BossLeft : MonoBehaviour {

    public float laserSpeed;
    public float shootingRate;
    public GameObject laserPrefab;
    public AudioClip laserSound;
    private BossCore core;
    float damageCounter = 10;
    bool hit;
    GameObject laser1;
    GameObject laser2;
    GameObject laser3;
    Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        core = GameObject.Find("Core").GetComponent<BossCore>();
	}
	
	// Update is called once per frame
	void Update () {
        float prob = Time.deltaTime * shootingRate;
        if (transform.parent.position.y < 2.82f)   // if entire ship is in place
            if (Random.value < prob && core.LeftCantShoot() != true)
                Shoot();

        animator.SetBool("isHit", hit);
	}

    public void Shoot()
    {
        Vector3 startingPosition1 = transform.position + new Vector3(0.0f, -1.5f, 0.0f);         // starting position of middle lasers
        Vector3 startingPositoin2 = transform.position + new Vector3(-0.4f, -1.5f, 0.0f);        // starting position of left lasers
        Vector3 startingPosition3 = transform.position + new Vector3(0.4f, -1.5f, 0.0f);         // starting position of right laser
        laser1 = Instantiate(laserPrefab, startingPosition1, Quaternion.identity) as GameObject;  // instantiating middle lasers 
        laser2 = Instantiate(laserPrefab, startingPositoin2, Quaternion.identity) as GameObject;  // instantiating left lasers
        laser3 = Instantiate(laserPrefab, startingPosition3, Quaternion.identity) as GameObject;  // instantiating right laser
        laser1.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, -laserSpeed, 0.0f);  // velocity upwards
        laser2.GetComponent<Rigidbody2D>().velocity = new Vector3(-1.0f, -laserSpeed, 0.0f); // velocity upwards/left
        laser3.GetComponent<Rigidbody2D>().velocity = new Vector3(1.0f, -laserSpeed, 0.0f);  // velocity upwards/right
        AudioSource.PlayClipAtPoint(laserSound, transform.position);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerBullet bullet = collider.gameObject.GetComponent<PlayerBullet>();
        
        if (damageCounter <= 0)
            hit = true;
        if (bullet && transform.parent.position.y <= 2.82)
        {
            Destroy(collider.gameObject);
            damageCounter--;
        }
    }

    public bool LeftShieldDown()
    {
        return hit;
    }

    public void ResetLeftShield()
    {
        hit = false;
    }

    public void SetHit(bool shieldStatus)
    {
        hit = shieldStatus;
        damageCounter = 10;
    }
}
