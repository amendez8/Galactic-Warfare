using UnityEngine;
using System.Collections;

public class FormationSpawner2 : MonoBehaviour {

    public GameObject enemyPosition2; // prefab of seconds enemy formation

	// Use this for initialization
	void Start () {

        // create enemy b wave
        InvokeRepeating("CreateEnemyPosition2", 4, 3);
        StartCoroutine(EnemyWaveComplete());

        // create boundaries with camera
        float distanceFromCamera = transform.position.z - Camera.main.transform.position.z;
        Vector3 leftWall = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distanceFromCamera));
        Vector3 rightWall = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0f, distanceFromCamera));
	}

    public void CreateEnemyPosition2()
    {
        //greater than or equal to 0, spawn in right. If less then 0, spawn to the left
        float randomNum = Random.Range(-4, 4);
        if (randomNum >= 0)
            randomNum = 2.0f;
        if (randomNum < 0)
            randomNum = -2.0f;

        Vector3 formationPosition = new Vector3(randomNum, transform.position.y, 0.0f);                                 //position which 5 ships will be created at
        transform.position = formationPosition;                                                                         //assign it to enemy position box 
        GameObject enemyFormation = Instantiate(enemyPosition2, transform.position, Quaternion.identity) as GameObject; //spawn them where 'position' have been located.
    }

    IEnumerator EnemyWaveComplete()
    {
        yield return new WaitForSeconds(30);
        CancelInvoke("CreateEnemyPosition2");
    }
}
