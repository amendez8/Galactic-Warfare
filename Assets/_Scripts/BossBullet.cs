using UnityEngine;
using System.Collections;

public class BossBullet : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerController playerShip = collider.gameObject.GetComponent<PlayerController>();
        if (playerShip)
        {
            Destroy(gameObject);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
