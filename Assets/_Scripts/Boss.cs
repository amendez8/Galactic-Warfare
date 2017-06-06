using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public GameObject Canvas;
    public float entrySpeed;         // speed of boss ship entering camera
    public float movementSpeed;      // speed of boss from left to right
    bool inPosition;   // boss is in position to begin moving side to side
    bool shift = false;
    private BossCore core;

	// Use this for initialization
	void Start () {
        core = GameObject.Find("Core").GetComponent<BossCore>();
	}
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(Enter());
        

        if (core.ShipDestroyed() == true)
        {
            Destroy(gameObject);
            Canvas.SetActive(true);
        }
	}

    IEnumerator Enter()
    {
        yield return new WaitForSeconds(33);
        // ship descending into it's position
        if (transform.position.y > 2.82f)
            transform.Translate(Vector3.down * entrySpeed * Time.deltaTime);
        // if the ship is in position, set bool true
        if (transform.position.y <= 2.82f)
            inPosition = true;
        // if greater than 5.9, set bool to have him move left
        if (transform.position.x > 5.9f)
            shift = true;
        // if too far to left, set bool to false and move to right
        if (transform.position.x < -3.32)
            shift = false;
        // if ship in position, move it
        if (inPosition == true)
            SideToSide();
    }

    void SideToSide()
    {
        if(shift == true)
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        else
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
    }

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }
}
