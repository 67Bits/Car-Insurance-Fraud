using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public bool yachoque = false;
    [SerializeField] private GameObject obj_golpe;
   
    public void darGolpe()
    {
        obj_golpe.SetActive(true);
        CancelInvoke("quitargolpe");
        Invoke("quitargolpe", 5f);
    }
    public void quitargolpe()
    {
        obj_golpe.SetActive(false);
    }
    public void activarChoque()
    {
        yachoque = false;
    }
}
