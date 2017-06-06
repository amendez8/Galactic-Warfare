using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController playerShip = collider.gameObject.GetComponent<PlayerController>();

        if (playerShip)
            Destroy(gameObject);
    }
    
    /*public void DestroyShip()
    {
        Destroy(gameObject);
    } */
}
