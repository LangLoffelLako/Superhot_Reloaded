using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController Controller;
    public float speed = 12f;

    public GameObject weapon; 
    
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    private Vector3 velocity;

    public float enemyDetectionRadius = 1f;
    
    private void Start()
    {
       //equip possible weapon
        if (weapon != null)
        {
            weapon.transform.Find("Rifle").GetComponent<Fire>().Equip(gameObject);
            Debug.Log("Weapon equipped");
        }
    }
    

    void Update()
    {
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z);
        velocity.y += gravity * Time.deltaTime;

        if (Controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetKeyDown("o") && Controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        Controller.Move(move * speed * Time.deltaTime);
        Controller.Move(velocity * Time.deltaTime);
        
        DidTouch(transform.position, enemyDetectionRadius);
    }

    void DidTouch(Vector3 center, float radius)
    {
        Collider[] hitcolliders = Physics.OverlapSphere(center, radius);

        foreach (var hitcollider in hitcolliders)
        {
            if (hitcollider.gameObject.tag == "Enemy")
            {
                Debug.Log("Touched Player");
                PlayerEventManager.onShot.Invoke();
            }
        }
    }

   
}
