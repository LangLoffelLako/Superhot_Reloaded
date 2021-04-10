using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] private float minDistance = 10f;
    [SerializeField] private float angularSpeed = 120f;
    [SerializeField] private GameObject target;
    private NavMeshAgent navigator;
   
    
    [Header("Equipped Weapon")]
    public GameObject weapon;
    [SerializeField] private bool hasWeapon = true;
    
    [Header("Respawn")]
    public bool willRespawn = false;

    public bool reportLog = false;
    
    
    void Start()
    {
        //setting variables
        if (navigator == null)
        {
            navigator = GetComponent<NavMeshAgent>();
        }

        if (hasWeapon == true)
        {
            //equip possible weapon
            if (weapon != null)
            {
                weapon.transform.Find("Rifle").GetComponent<Fire>().Equip(gameObject);
                //weapon.GetChild(1).GetComponent<Fire>().Equip(gameObject);
            }
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

                if (hasWeapon == true)
                {
                    //Shooting
                    StartCoroutine(Shoot());
                    //GetComponent<EnemyEventManager>().onFire.Invoke();
                }
            }
            else
            {
                if (navigator.isStopped)
                    navigator.isStopped = false;
            }

            if (navigator.destination != null)
            {
                TurnToPos(target);
            }
        }
        
    }


    private void TurnToPos(GameObject myTarget)
    {
        //Vector3 angle = (myTarget.transform.position - transform.position) - transform.forward;
        Vector3 optiDirection = (myTarget.transform.position - transform.position);
  

        float timeStep = Time.deltaTime * angularSpeed;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, optiDirection, timeStep, 0.0f);
        
        transform.rotation = Quaternion.LookRotation(newDirection);

        //transform.Rotate(timedAngle.x, 0, timedAngle.z, Space.World);

        //Quaternion weaponAngle = Quaternion.Euler(myTarget.transform.position - weapon.transform.position);

        //weapon.transform.localRotation = (weaponAngle);
    }

    private IEnumerator Shoot()
    {
        yield return new WaitForSeconds(1);
        GetComponent<EnemyEventManager>().onFire.Invoke();
    }
    
}
