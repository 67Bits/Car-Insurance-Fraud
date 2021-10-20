using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollisionEvents : MonoBehaviour
{
    private bool first_time_collison = false;
    private ControlNivel control_nivel = null;
    private CarControl padre;
    private EventosFeel feel;
    private CharacterMovement character_movement = null;

    public void Start()
    {
        control_nivel = GameObject.FindGameObjectWithTag("controlnivel").GetComponent<ControlNivel>();
        padre = transform.parent.GetComponent<CarControl>();
        feel = GameObject.FindGameObjectWithTag("feels").GetComponent<EventosFeel>();

        character_movement = GameObject.FindGameObjectWithTag("MyCharacterMovement").GetComponent<CharacterMovement>();

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (!first_time_collison)
            {
                if (!padre.yachoque)
                {
                    control_nivel.startIncrementCoin();

                    padre.yachoque = true;                   
                    first_time_collison = true;
                    padre.Invoke("activarChoque", 3f);
                    Invoke("activarChoque", 3f);
                    character_movement.impulsar2();

                    feel.impactarConVehiculo();
                    feel.activarTrail();
                    padre.darGolpe();

                    feel.Invoke("activarShakeCarro", 0.1f);

                    if (control_nivel.combo < 1)
                    {
                        Invoke("collisonAction2", 0.2f);
                    }
                    else
                    {
                        control_nivel.updateCombo();
                    }

                }
              
            }
        }
    }

    public void collisonAction2()
    {
        if (character_movement.air)
        {
            control_nivel.updateCombo();
        }

    }
    public void activarChoque()
    {
        first_time_collison = false;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag.Equals("ParedCarros"))
    //    {
    //        transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, 170);
    //        first_time_collison = false;
    //    }
    //}
}
