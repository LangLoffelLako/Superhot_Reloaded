using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    public CharacterController Controller;

    [SerializeField] private float standardTime = 1f;
    [SerializeField] private float freezeTime = 0.1f;
    [SerializeField] private float timeChangeGradient = 3f;

    public static float loadupTruce = 2f;
    
    // Update is called once per frame
    void Update()
    {    
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical") || Input.GetAxis("Mouse X") != 0 ||
            Input.GetAxis("Mouse Y") != 0)
        {
            if (Time.timeScale <= standardTime)
            {
                Time.timeScale += (standardTime - freezeTime) * timeChangeGradient * Time.deltaTime / Time.timeScale;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
        else
        {
            Time.timeScale = freezeTime;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
    }
}