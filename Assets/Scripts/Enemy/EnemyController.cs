using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private float minDistance = 10f;
    [SerializeField] private float angularSpeed = 120f;
    [SerializeField] private float maxWeaponAngle = 30f;
    private NavMeshAgent navigator;
    
    [Header("Equipped Weapon")]
    [SerializeField] private GameObject weapon;
    
    [Header("Respawn")]
    public bool willRespawn = false;
    
    private GameObject target;
    
    void Start()
    {
        //setting variables
        if (navigator == null)
        {
            navigator = GetComponent<NavMeshAgent>();
        }

        //equip possible weapon
        if (weapon != null)
        {
            weapon.transform.Find("Rifle").GetComponent<Fire>().Equip(gameObject);
            //weapon.GetChild(1).GetComponent<Fire>().Equip(gameObject);
        }

        if (target == null)
        {
            target = GameObject.FindWithTag("Player");
        }
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 direction = target.transform.position - transform.position;
        
        //enemy checking, if he sees the player
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
        { 
            if (hit.collider.tag == "Player")
            {
                //Movement
                navigator.destination = target.transform.position;
                    
                if (hit.distance >= minDistance)
                {
                    navigator.isStopped = false;
                }
                else
                {
                    navigator.isStopped = true;
                } 
                //Shooting
                GetComponent<EnemyEventManager>().onFire.Invoke();
            }
            else
            {
                if (navigator.isStopped)
                    navigator.isStopped = false;
            }

            if (navigator.destination != null)
            {
                TurnToPos(direction,target);
            }
        }
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Hit Player");
            PlayerEventManager.onShot.Invoke();
        }
    }

    private void TurnToPos(Vector3 direction, GameObject Target)
    {
        Vector3 angle = direction - transform.forward;
        transform.Rotate(Vector3.up,angle.y * Time.deltaTime * angularSpeed);

        Quaternion weaponAngle = Quaternion.Euler(Target.transform.position - weapon.transform.position);

        weapon.transform.localRotation = (weaponAngle);
    }

}
