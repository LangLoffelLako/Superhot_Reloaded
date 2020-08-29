using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Fire : MonoBehaviour
{
    public bool weaponHeld;
    
    [Header("Bullets & Spawn Point")]
    public GameObject prefabBullet;
    public Transform bulletSpawnPoint;
    
    [Header("Firearm Characteristic")]
    [SerializeField]
    private int fireRate = 2;
    [SerializeField]
    private float bulletImpuls = 6.4f;

    private bool shot;
    private GameObject bullet;

    private void Start()
    {
        
    }

    public void Equip(GameObject Equipper)
    {
        if (Equipper.tag == "Player")
        {
            PlayerEventManager.OnFire.AddListener(FireBullet);
        }
        else if (Equipper.tag == "Enemy")
        {
            Equipper.GetComponent<EnemyEventManager>().OnFire.AddListener(FireBullet);
        }
    }
    private void FireBullet()
    {
        if (!shot)
        {
            Vector3 spawnPoint = bulletSpawnPoint.position;
            Quaternion shootDirection = bulletSpawnPoint.rotation;

            //spawn bullet
            bullet = Instantiate(prefabBullet, spawnPoint, shootDirection);

            //shoot bullet
            Vector3 force = bulletImpuls * bullet.transform.forward.normalized;
            bullet.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);

            StartCoroutine(FirePause());
        }
    }
    private IEnumerator FirePause ()
    {
        shot = true;
        yield return new WaitForSeconds(1 / fireRate);
        shot = false;
    }
}
