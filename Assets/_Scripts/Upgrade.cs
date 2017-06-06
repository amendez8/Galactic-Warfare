using UnityEngine;
using System.Collections;

public class Upgrade : MonoBehaviour {

    public AudioClip upgradeSound;
    void Start()
    {
        Destroy(gameObject, 3.0f); //destroy upgrade if not taken within 3 seconds
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController ship = collider.gameObject.GetComponent<PlayerController>();
        if (ship)
        {
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(upgradeSound, transform.position);
        }
    }
}
