using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCube : MonoBehaviour
{
    public float yRotation = 20f;
    private Vector3 Rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation = new Vector3(0f, yRotation * Time.deltaTime, 0f);
        transform.Rotate(Rotation,Space.Self);
    }
}
