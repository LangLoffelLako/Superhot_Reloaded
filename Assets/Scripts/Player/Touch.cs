using UnityEngine;

public class Touch : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Something");
        Debug.Log(other.gameObject.name);
        if (other.transform.tag == "Player")
        {
            PlayerEventManager.onShot.Invoke();
        }
    }
}
