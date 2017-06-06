using UnityEngine;
using System.Collections;

public class BossCore : MonoBehaviour {

    public GameObject deathExplosion; // explosion animation
    public AudioClip explosionSound;  // sound for explosion
    AudioSource Sirens;          // plays before ship expodes
    float damageCounter = 30;         // boss health
    bool coreOpen;                    // if core is open, set true
    bool isDead;                      // when ship finally explodes
    bool leftStopShooting;            // when boss stops moving, stop shooting
    bool rightStopShooting;           // when boss stops moving, stop shooting
    private BossRight rightWing;      // have access to right wing
    private BossLeft leftWing;        // have access to left wing
    private Boss wholeShip;           // access to whole ship
    private float randomNum;          // used for death explosions
    private float newSpeed;           // speed of ship when its dying
    Animator animator;                // have access to animator parameters

	// Use this for initialization
	void Start () {
        // have access to animators of individual boss parts
        animator = GetComponent<Animator>();
        rightWing = GameObject.Find("RightWing").GetComponent<BossRight>();
        leftWing = GameObject.Find("LeftWing").GetComponent<BossLeft>();
        wholeShip = GameObject.Find("Boss").GetComponent<Boss>();
        Sirens = GetComponent<AudioSource>();
       
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetBool("isOpen", coreOpen);

        // if both wings are down
        if (rightWing.RightShieldDown() == true && leftWing.LeftShieldDown() == true)
        {
            coreOpen = true;
        }

        // if core open, activate function which closes core in 5 seconds
        if (coreOpen == true)
        {
            StartCoroutine(ActivateShields());
        }

        // if ship has no health, activate function which slows it down, and call another function which blows it up
        if (damageCounter <= 0)   //if dies
        {
            BossDying();
        }
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerBullet bullet = collider.gameObject.GetComponent<PlayerBullet>();
        if (bullet && coreOpen == true)
        {
            damageCounter--;
        }
    }

    // ship slowing down to blow up
    void BossDying()
    {
        newSpeed = wholeShip.GetMovementSpeed();
        if (wholeShip.GetMovementSpeed() != 0)
        {
            newSpeed--;
            wholeShip.SetMovementSpeed(newSpeed);
        }

        // when ship stopped, call function to destory it
        if (wholeShip.GetMovementSpeed() == 0)
        {
            StartCoroutine(DestroyBoss());
            leftStopShooting = true;
            rightStopShooting = true;
            Sirens.Play();            
        }
    }
    
    // reactivates shields if ship not destroyed in 5 seconds
    IEnumerator ActivateShields()
    {
        yield return new WaitForSeconds(5);
        coreOpen = false;
        rightWing.SetHit(false);
        leftWing.SetHit(false);
    }

    // ship blowing up
    IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(3);
        Sirens.Stop();  
        GameObject death = Instantiate(deathExplosion, transform.position, Quaternion.identity) as GameObject;
        Destroy(death, 0.6f);
        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        isDead = true;
          
    }
    // bool used for parent of three ship commonents to destory everything.
    public bool ShipDestroyed()
    {
        return isDead;
    }

    // return value whether left wing can shoot or not
    public bool LeftCantShoot()
    {
        return leftStopShooting;
    }

    // return value whether right wing can shoot or not
    public bool RightCantShoot()
    {
        return rightStopShooting;
    }
}
