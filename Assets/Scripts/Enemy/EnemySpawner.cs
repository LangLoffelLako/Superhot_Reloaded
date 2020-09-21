using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Respawn Variables")]
    public GameObject enemyToRespawn;
    public GameObject spawnPrefab;
    [SerializeField] private float respawnTime = 5f;
    
    private Vector3 respawnPosition;
    private Quaternion respawnOrientation;
    // Start is called before the first frame update
    void Start()
    {
        respawnPosition = enemyToRespawn.transform.position;
        respawnOrientation = enemyToRespawn.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyToRespawn == null)
        {
            enemyToRespawn = Instantiate(spawnPrefab, respawnPosition, respawnOrientation);
            StartCoroutine(ActivateRespawn(respawnTime));
        }
    }

    private IEnumerator ActivateRespawn(float waitTillRespawn)
    {
        enemyToRespawn.SetActive(false);
        yield return new WaitForSeconds(waitTillRespawn);// new WaitForSeconds(1);
        enemyToRespawn.SetActive(true);
    }
}
