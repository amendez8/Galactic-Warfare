using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

    private int hellCounter = 0; // spawn bullet back at bottom for hell mode, ends at 3
    public GameObject explosion;
    public AudioClip explosionSound;

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController playerShip = collider.gameObject.GetComponent<PlayerController>(); // if player bullet hits player ship
        EnemyA enemy1 = collider.gameObject.GetComponent<EnemyA>();                         // if bullet hits enemy A
        EnemyB enemy2 = collider.gameObject.GetComponent<EnemyB>();                         // if bullet hits enemy B
        HellDestroyerTop destroyer = collider.gameObject.GetComponent<HellDestroyerTop>();  // The garbage bin at the top of hell mode
        BossRight rightWing = collider.gameObject.GetComponent<BossRight>();                // the boss's right wing
        BossLeft leftWing = collider.gameObject.GetComponent<BossLeft>();                   // boss left wing
        BossCore core = collider.gameObject.GetComponent<BossCore>();                       // boss core

        if (playerShip || enemy1 || enemy2)
            Destroy(gameObject);

        if (rightWing || leftWing || core)
        {
            Destroy(gameObject);
            GameObject bulletExploding = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            Destroy(bulletExploding, 0.2f);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);

        }
        
        if (destroyer)
        {
            hellCounter++;
            if (hellCounter != 4)
                transform.position = new Vector3(transform.position.x, -5.2f, transform.position.z); 
            else
                Destroy(gameObject);
        }
    }
   /* public void DestroyShip()
    {
        Destroy(gameObject);
    }  */
}
