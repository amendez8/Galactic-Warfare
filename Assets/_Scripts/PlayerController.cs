using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;            // speed of ship
    public float laserSpeed;       // speed of laser beams
    public float rateOfFire;       // rate lasers are fired
    public GameObject Laser;       // get the actual laser
    public GameObject Laser2;      // left laser for three bullets
    public GameObject Laser3;      // right laser for three bullets
    public GameObject explosion;   // annimation when ship explodes
    public AudioClip laserSound;   // sound of laser firing
    public AudioClip deathSound;   // sound when player dies
    public GameObject canvas;      // losing canvas
    private float laserCount = 1;  // 1, 2, 3... if reaches 0, game over
    GameObject LaserBeam1;         // assign laser to this game object
    GameObject LaserBeam2;         // assign a second laser to this object
    GameObject LaserBeam3;         // assign a third laser to this object

    // Use this for initialization
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        // if 4 upgrades have been taken, reset it back to 3
        if (laserCount == 4) 
            laserCount = 3;

        //clamp ship to screen
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp(pos.x, 0.05f, 0.95f);
        pos.y = Mathf.Clamp(pos.y, 0.09f, 0.9f);
        transform.position = Camera.main.ViewportToWorldPoint(pos);

        // shoot as long as u hold space
        if (Input.GetKeyDown(KeyCode.Space))
            InvokeRepeating("Shoot", 0.00001f, rateOfFire); //method, time it takes to activate, repeat rate

        //stop shooting when shift is released
        if (Input.GetKeyUp(KeyCode.Space))
            CancelInvoke("Shoot");
        
        // ship movements
        if (Input.GetKey("d"))
            transform.position += Vector3.right * speed * Time.deltaTime;
        if (Input.GetKey("a"))
            transform.position += Vector3.left * speed * Time.deltaTime;
        if (Input.GetKey("w"))
            transform.position += Vector3.up * speed * Time.deltaTime;
        if (Input.GetKey("s"))
            transform.position += Vector3.down * speed * Time.deltaTime;
    }

    //if bullet hits ship
    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerBullet pBullet = collider.gameObject.GetComponent<PlayerBullet>(); // if ship hit by own bullet (bullet hell)
        EnemyBullet eBullet = collider.gameObject.GetComponent<EnemyBullet>();   // if ship hit by enemy bullet
        BossBullet bBullet = collider.gameObject.GetComponent<BossBullet>();     // if ship hit by boss bullet
        BossLeft bLeft = collider.gameObject.GetComponent<BossLeft>();           // if ship hits boss left gun
        BossRight bRight = collider.gameObject.GetComponent<BossRight>();        // if ship hits boss right gun
        BossCore bCore = collider.gameObject.GetComponent<BossCore>();           // if ship hits boss core
        Upgrade upgrade = collider.gameObject.GetComponent<Upgrade>();           // if ship hits an upgrade
        
        if (pBullet || eBullet || bBullet || bLeft || bRight || bCore)
        {
            laserCount--;
            AudioSource.PlayClipAtPoint(deathSound, transform.position);
            if (laserCount == 0)
            {
                Destroy(gameObject);
                GameObject shipExploding = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
                Destroy(shipExploding, 1.0f);
                canvas.SetActive(true);
            }
        }

        if (upgrade)
        {
            laserCount++;
        }
    }

    public void Shoot()
    {
        if (laserCount == 1)
        {
            speed = 8;                                                                             // speed of ship
            laserSpeed = 15;                                                                       // speed of lasers
            Vector3 startingPosition = transform.position + new Vector3(0.0f, 0.8f, 0.0f);         // making sure laser is ahead of ship
            LaserBeam1 = Instantiate(Laser, startingPosition, Quaternion.identity) as GameObject;  // instantiate laser to ship
            LaserBeam1.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, laserSpeed, 0.0f); // give laser velocity upwards
            AudioSource.PlayClipAtPoint(laserSound, transform.position);                           // play laser sound at location of ship
        }

        if (laserCount == 2)
        {
            speed = 5;                                                                             // speed of ship
            laserSpeed = 25;                                                                       // speed of lasers
            Vector3 startingPosition1 = transform.position + new Vector3(0.4f, 0.5f, 0.0f);        // starting position of right lasers
            Vector3 startingPositoin2 = transform.position + new Vector3(-0.4f, 0.5f, 0.0f);       // starting position of left lasers
            LaserBeam1 = Instantiate(Laser, startingPosition1, Quaternion.identity) as GameObject; // instantiating right lasers 
            LaserBeam2 = Instantiate(Laser, startingPositoin2, Quaternion.identity) as GameObject; // instantiating left lasers
            LaserBeam1.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, laserSpeed, 0.0f); // velocity upwards
            LaserBeam2.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, laserSpeed, 0.0f); // velocity upwards
            AudioSource.PlayClipAtPoint(laserSound, transform.position);                           // play laser sound at location of ship
        }

        if (laserCount == 3)
        {
            speed = 5;                                                                              // speed of ship
            laserSpeed = 25;                                                                        // speed of lasers
            Vector3 startingPosition1 = transform.position + new Vector3(0.0f, 0.8f, 0.0f);         // starting position of middle lasers
            Vector3 startingPositoin2 = transform.position + new Vector3(-0.4f, 0.5f, 0.0f);        // starting position of left lasers
            Vector3 startingPosition3 = transform.position + new Vector3(0.4f, 0.5f, 0.0f);         // starting position of right laser
            LaserBeam1 = Instantiate(Laser, startingPosition1, Quaternion.identity) as GameObject;  // instantiating middle lasers 
            LaserBeam2 = Instantiate(Laser, startingPositoin2, Quaternion.identity) as GameObject;  // instantiating left lasers
            LaserBeam3 = Instantiate(Laser, startingPosition3, Quaternion.identity) as GameObject;  // instantiating right laser
            LaserBeam1.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, laserSpeed, 0.0f);  // velocity upwards
            LaserBeam2.GetComponent<Rigidbody2D>().velocity = new Vector3(-2.0f, laserSpeed, 0.0f); // velocity upwards/left
            LaserBeam3.GetComponent<Rigidbody2D>().velocity = new Vector3(2.0f, laserSpeed, 0.0f);  // velocity upwards/right
            AudioSource.PlayClipAtPoint(laserSound, transform.position);                            // play laser sound at location of ship
        }
    }
}
