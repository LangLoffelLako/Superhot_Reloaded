using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Bullets & Spawn Point")]
    public GameObject prefabBullet;
    public Transform bulletSpawnPoint;
    
    [Header("Firearm Characteristic")]
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletImpuls = 6.4f;

    private float loadupTruce;

    public bool shot;
    private GameObject bullet;

    [Header("Audio")] 
    public AudioSource shootAudio; 
    public AudioClip clip; 
    public AudioClip clip2;

    private void Start()
    {
        loadupTruce = TimeFreeze.loadupTruce;
        StartCoroutine(FirePause(loadupTruce));
    }
    
    public void Equip(GameObject Equipper)
    {
        if (Equipper.tag == "Player")
        {
            PlayerEventManager.onFire.AddListener(FireBullet);
        }
        else if (Equipper.tag == "Enemy")
        {
            Equipper.GetComponent<EnemyEventManager>().onFire.AddListener(FireBullet);
        }
    }
    private void FireBullet()
    {
        if (!shot)
        {
            Vector3 spawnPoint = bulletSpawnPoint.position;
            Quaternion shootDirection = bulletSpawnPoint.rotation;

            //spawn bullet
            bullet = Instantiate(original: prefabBullet, position: spawnPoint, rotation: shootDirection); //, parent: bulletSpawnPoint);

            //shoot bullet
          
            Vector3 force = bulletImpuls * bullet.transform.forward.normalized;
            bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            shootAudio.PlayOneShot(clip, 1f);

            StartCoroutine(FirePause(1/fireRate));
        }
    }
    public IEnumerator FirePause (float pauseLength)
    {
        shot = true;
        yield return new WaitForSeconds(pauseLength);
        shot = false;
    }
}