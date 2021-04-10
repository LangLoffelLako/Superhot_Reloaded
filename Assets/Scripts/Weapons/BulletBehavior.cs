using UnityEngine;


public class BulletBehavior : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        //player impact
        if (other.transform.tag == "Player")
        {
            Debug.Log("Hit Player");
            PlayerEventManager.onShot.Invoke();
        }
        //enemy impact
        else if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit Enemy");
            other.gameObject.GetComponentInParent<EnemyEventManager>().onBeingShot.Invoke();
        }
        else
        {
            //to be filled later on with object collision
        }
    }
}
