using UnityEngine;
using System.Collections;

public class SpawnEnemy : MonoBehaviour {

    public GameObject upgradePrefab;   // holds object of upgrade
    public float formationX;           // width of the formation box
    public float formationY;           // height of the formation box
    public float formationSpeed;       // speed enemy formation moves at
    private bool spawnUpgrade = false; // used to determine when to spawn the upgrade. True = spawn, false = don't spawn
    private PointSystem pointSystem;   // have access to the score of the game    
	                               

	void Start () {
        pointSystem = GameObject.Find("Points").GetComponent<PointSystem>(); // gets the points attribute from the pointsystem component         
    }

	void Update () {
        transform.Translate(Vector3.down * formationSpeed * Time.deltaTime); // speed of enemy ship
       
        if (EnemyWaveDead() && spawnUpgrade == true) // if entire wave killed
        {
            GameObject upgrade = Instantiate(upgradePrefab, transform.position, Quaternion.identity) as GameObject; // create upgrade
            pointSystem.DoubleScore();                                                                              // double score since entire wave was killed
            spawnUpgrade = false;                                                                                   // assign bool which creates upgrade back to false.
        }
	}

    bool EnemyWaveDead()
    {
        foreach (Transform child in transform)
        {
            if (child.childCount > 0) // if there is an enemy in the child position
            {
                spawnUpgrade = true;
                return false;
            }   
        }
        return true; // all enemies from wave destoryed
    }   

    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(formationX, formationY));
    }
	
}
