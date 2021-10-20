using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
using Cinemachine;

public class EventosFeel : MonoBehaviour
{
    public MMFeedbacks vfx_impactarvehiculo, vfx_correr, vfx_impacto_con_el_piso;
    public CinemachineImpulseSource shake;
    public float valor_vibracion_carro;
    public float valor_vibracion_piso;

    public GameObject par_polvopiso;

    public TrailRenderer trail;

    [HideInInspector] public ControlNivel control_nivel;


    [HideInInspector] public bool yaactivetrail;
    // Hechos
    public void impactarConVehiculo()
    {
        vfx_impactarvehiculo.PlayFeedbacks();
    }
    // Deben instanciarse particulas en los pies
    public void correr()
    {
        vfx_correr.PlayFeedbacks();
    }
    // Hecho
    public void impactarConPiso()
    {
        vfx_impacto_con_el_piso.PlayFeedbacks();
    }

    private void Start()
    {
        control_nivel = GameObject.FindGameObjectWithTag("controlnivel").GetComponent<ControlNivel>();
    }



    // Ajustar ganancia para cada shake 
    public void activarShakeCarro()
    {
        shake.m_ImpulseDefinition.m_AmplitudeGain = valor_vibracion_carro;
        shake.GenerateImpulse();
    }

    // Ajustar ganancia para cada shake 
    public void activarShakePiso()
    {
        shake.m_ImpulseDefinition.m_AmplitudeGain = valor_vibracion_piso;
        shake.GenerateImpulse();
    }

    public void activarTrail()
    {
        trail.emitting = true;
        yaactivetrail = true;
    }
    public void desactivarTrail()
    {
        trail.emitting = false;
        yaactivetrail = false;
    }

    public void crarParticulaPolvo(Vector3 pos)
    {

        GameObject a = Instantiate(par_polvopiso) as GameObject;
        a.transform.position = pos;

    }

}
