using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJuego : MonoBehaviour
{
    [HideInInspector]
    public static ControlJuego administrador;
    public List<string> lista_escenas_niveles;

    public int nivelActual = 0;
    public int nivelActualReal = 0;

    [HideInInspector]
    public bool yatermineniveles = false;

    private void Awake()
    {
        if (administrador == null)
        {
            administrador = this;
            DontDestroyOnLoad(administrador);
        }
        else { Destroy(gameObject); }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
