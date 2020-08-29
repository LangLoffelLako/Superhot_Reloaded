using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * collision event with player
 * collision event with enemy
 * collision event with other surfaces
 *
 * destroy bullet after some time laying still
 * 
*/

public class BulletBehavior : MonoBehaviour
{
    /* don't want bullets to be destroyed
    void Update()
    {
        //destroys bullet after it stops
        if (gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
            Destroy(gameObject, destroyAfter);
            
    }
    */
    
    private void OnCollisionEnter(Collision collision)
    {
        //player impact
        if (collision.transform.tag == "Player")
        {
            Debug.Log("Hit Player");
            PlayerEventManager.OnDie.Invoke();
        }
        //enemy impact
        else if (collision.transform.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            collision.transform.GetComponent<EnemyEventManager>().OnDie.Invoke();
        }
        else
        {
            //to be filled later on with object collision
        }


    }
}
