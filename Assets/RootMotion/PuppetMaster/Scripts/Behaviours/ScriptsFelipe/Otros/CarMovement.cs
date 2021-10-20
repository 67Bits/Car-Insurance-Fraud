using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float speed_car;
    private void Update()
    {
        transform.position += -transform.forward * speed_car * Time.deltaTime;
    }
   
   
}
