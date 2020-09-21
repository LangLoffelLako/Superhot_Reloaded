using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class Fire : MonoBehaviour
{
    [Header("Bullets & Spawn Point")]
    public GameObject prefabBullet;
    public Transform bulletSpawnPoint;
    
    [Header("Firearm Characteristic")]
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private float bulletImpuls = 6.4f;

    private float _loadupTruce;

    public bool shot;
    private GameObject bullet;

    private void Start()
    {
        _loadupTruce = GameObject.FindWithTag("Player").GetComponent<TimeFreeze>().loadupTruce;
        StartCoroutine(FirePause(_loadupTruce));
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