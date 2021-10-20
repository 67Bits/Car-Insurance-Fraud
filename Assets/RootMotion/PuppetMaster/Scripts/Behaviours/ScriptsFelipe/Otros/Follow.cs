using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject followed_object;

    // Start is called before the first frame update
    void Start()
    {
        
    }
   
    private void FixedUpdate()
    {
        transform.position = new Vector3(followed_object.transform.position.x, 0, followed_object.transform.position.z + 1);
    }
}
