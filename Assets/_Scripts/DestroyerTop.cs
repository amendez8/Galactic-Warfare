using UnityEngine;
using System.Collections;

public class DestroyerTop : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerBullet bullet = collider.gameObject.GetComponent<PlayerBullet>();
        EnemyBullet ebullet = collider.gameObject.GetComponent<EnemyBullet>();
        if (bullet || ebullet)
            Destroy(collider.gameObject);
    }
}
