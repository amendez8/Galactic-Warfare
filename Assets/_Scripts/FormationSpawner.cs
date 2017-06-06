using UnityEngine;
using System.Collections;

public class FormationSpawner : MonoBehaviour {

    public GameObject enemyPosition1; // prefab of first enemy formation
    

	// Use this for initialization
    void Start()
    {
        // create enemy A
        InvokeRepeating("CreateEnemyPosition1", 2, 3); 
        StartCoroutine(EnemyWaveComplete());
      
        // create boundaries with camera
        float distanceFromCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftWall = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distanceFromCamera));
        Vector3 rightWall = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distanceFromCamera));
    }

    public void CreateEnemyPosition1()
    {
        float randomNum = Random.Range(-4, 4);                                                                          //keep them within the camera view
        Vector3 formationPosition = new Vector3(randomNum, transform.position.y, 0.0f);                                 //position which 5 ships will be created at
        transform.position = formationPosition;                                                                         //assign it to enemy position box 
        GameObject enemyFormation = Instantiate(enemyPosition1, transform.position, Quaternion.identity) as GameObject; //spawn them where 'position' have been located.
    }    

    IEnumerator EnemyWaveComplete()
    {
        yield return new WaitForSeconds(30);
        CancelInvoke("CreateEnemyPosition1");
    }
}
