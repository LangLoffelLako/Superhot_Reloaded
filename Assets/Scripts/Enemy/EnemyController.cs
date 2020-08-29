using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    public Transform target;

    [SerializeField] private float minDistance = 10f;
    [SerializeField] private float angularSpeed = 120f;
    [SerializeField] private float maxWeaponAngle = 30f;
    private NavMeshAgent navigator;
    private Transform weapon;

    // Start is called before the first frame update
    void Start()
    {
        //setting variables
        if (navigator == null) 
            navigator = GetComponent<NavMeshAgent>();
        
        //equip possible weapon
        weapon = transform.Find("Weapon");
        weapon.GetChild(1).Equip(gameObject);
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;
        
        //enemy checking, if he sees the player
        if (Physics.Raycast(transform.position, direction, out hit, Mathf.Infinity))
        { 
            if (hit.collider.tag == "Player")
            {
                //Movement
                navigator.destination = target.position;
                    
                if (hit.distance >= minDistance)
                {
                    navigator.isStopped = false;
                }
                else
                {
                    navigator.isStopped = true;
                } 
                //Shooting
                GetComponent<EnemyEventManager>().OnFire.Invoke();
                
                Debug.Log("Destination: " + navigator.destination+ " target: " + target.position);
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

    private void TurnToPos(Vector3 direction,Transform Target)
    {
        Vector3 angle = direction - transform.forward;
        transform.Rotate(Vector3.up,angle.y * Time.deltaTime * angularSpeed);

        Quaternion weaponAngle = Quaternion.Euler(target.position - weapon.position);

        weapon.localRotation = (weaponAngle);
    }

}
