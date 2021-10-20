using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CollisionTrigger : MonoBehaviour
{
    private bool first_time_collison = false;
    private CharacterMovement character_movement = null;

    private MMFeedbacks mifeel;

    private EventosFeel feel;

    private void Start()
    {
        character_movement = GameObject.FindGameObjectWithTag("MyCharacterMovement").GetComponent<CharacterMovement>();
        feel = GameObject.FindGameObjectWithTag("feels").GetComponent<EventosFeel>();
        mifeel = gameObject.GetComponent<MMFeedbacks>();

    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            if (!first_time_collison)
            {
                first_time_collison = true;

                if (character_movement.distanceGroud() > 1.8f)
                {
                    character_movement.impulsar();
                }                

                print("Hola");
                Invoke("activar", 3f); 


                mifeel.PlayFeedbacks();
            }

        }
    }
    public void activar()
    {
        first_time_collison = false;
    }
}


