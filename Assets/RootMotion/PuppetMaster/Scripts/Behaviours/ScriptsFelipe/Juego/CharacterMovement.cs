using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CharacterMovement : MonoBehaviour
{
    [SerializeField] public float speed_normal;
    [HideInInspector] public float speed_air = 0;
    [SerializeField] public float force_air;
    [SerializeField] public float force_impulse;
    [SerializeField] public float force_impulse_car;

    [HideInInspector] public float speed;
    [SerializeField] float distance_ray;

    [HideInInspector] public bool canMove = false;

    [HideInInspector] public Joystick joystick;

    [HideInInspector] public GameObject character_controler;
    [SerializeField] public GameObject cuerpo;
    [HideInInspector] public ControlNivel control_nivel;

    [HideInInspector] public bool air = false;
    public GameObject chest;

    [SerializeField] private GameObject indicador;

    // Control Material Personaje
    [SerializeField] private SkinnedMeshRenderer personaje_material;
    [SerializeField] private List<Material> list_materiales_personaje;
    private int nivel_heridas = 0;

    public bool yachoquepiso = true;

    public EventosFeel feel;


    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem par_money;
    private Animator control_camera = null;

    ////////////////////////// Textura del personaje //////////////////////////////////////////////////
    public void herir()
    {
        nivel_heridas++;
        if (nivel_heridas < list_materiales_personaje.Count)
        {
            personaje_material.material = list_materiales_personaje[nivel_heridas];
        }
    }
    public void herir2(int ide)
    {
        nivel_heridas = ide;
        if (nivel_heridas < list_materiales_personaje.Count)
        {
            personaje_material.material = list_materiales_personaje[nivel_heridas];
        }
    }
    public void sanarTotalmente()
    {
        personaje_material.material = list_materiales_personaje[0];
        nivel_heridas = 0;
    }
    ////////////////////////////////////////////////////////////////////////////


    private void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        control_nivel = GameObject.FindGameObjectWithTag("controlnivel").GetComponent<ControlNivel>();
        character_controler = GameObject.FindGameObjectWithTag("Player");
        indicador.SetActive(false);

        feel = GameObject.FindGameObjectWithTag("feels").GetComponent<EventosFeel>();
        control_camera = GameObject.FindGameObjectWithTag("cameraController").GetComponent<Animator>();

        //sanarTotalmente();
    }

    private void Update()
    {
        if (distanceGroud() > 3.6f)
        {
            air = true;
            indicador.SetActive(true);
            if (!feel.yaactivetrail)
            {
                feel.activarTrail();
            }
        }
        else
        {
            air = false;
            indicador.SetActive(false);
            if (feel.yaactivetrail)
            {
                feel.desactivarTrail();
            }
        }

        if (distanceGroud() < 1.8f)
        {
            if (control_nivel.current_coins > 1000)
            {
                control_nivel.UpdateAllCoins();
            }
        }

        if (joystick.Vertical > 0.2f)
        {
            if (!control_nivel.bool_win)
            {
                if (canMove)
                {                   
                    if (air)
                    {
                        if (!OnGround())
                        {
                            Vector3 dir = new Vector3(0, 0, 1);
                            chest.GetComponent<Rigidbody>().AddForce(dir * force_air * 10000 * Time.deltaTime);
                        }
                       
                    }               
                }
            }

        }
        if (joystick.Vertical < -0.2f)
        {
            if (!control_nivel.bool_win)
            {
                if (canMove)
                {
                    if (air)
                    {
                        if (!OnGround())
                        {
                            Vector3 dir = new Vector3(0, 0, -1);
                            chest.GetComponent<Rigidbody>().AddForce(dir * force_air * 10000 * Time.deltaTime * Mathf.Abs(joystick.Vertical));
                        }
                    }
           
                }
            }

        }
        if (joystick.Horizontal < -0.2f)
        {
            if (!control_nivel.bool_win)
            {
                if (canMove)
                {
                    if (air)
                    {
                        if (!OnGround())
                        {
                            Vector3 dir = new Vector3(-1, 0, 0);
                            chest.GetComponent<Rigidbody>().AddForce(dir * force_air * 10000 * Time.deltaTime * Mathf.Abs(joystick.Horizontal));
                        }
                    }

                }
            }
        }
        if (joystick.Horizontal > 0.2f)
        {
            if (!control_nivel.bool_win)
            {
                if (canMove)
                {
                    if (air)
                    {
                        if (!OnGround())
                        {
                            Vector3 dir = new Vector3(1, 0, 0);
                            chest.GetComponent<Rigidbody>().AddForce(dir * force_air * 10000 * Time.deltaTime * Mathf.Abs(joystick.Horizontal));
                        }
                    }
                 
                }
            }

        }
   
    }

    public void miraracamara()
    {
       // cuerpo.transform.DORotate(new Vector3(2, 3, 4), 2);
        //cuerpo.transform.rotation = Quaternion.Euler(0, 0, 0);
        cuerpo.transform.DORotateQuaternion(Quaternion.Euler(0, 80, 0), 5);
        print("rotando");
    }

    public void correr()
    {
        if (!air)
        {
            feel.correr();
        }
      
    }
    public void desactivarIndicador()
    {
        indicador.SetActive(false);
    }

    public void activarChocarPiso()
    {
        yachoquepiso = false;
    }
    public void chocarConPiso()
    {
        if (!yachoquepiso)
        {
            yachoquepiso = true;
            feel.activarShakePiso();
            feel.impactarConPiso();
        }
    }

    public bool OnGround()
    {
        bool respuesta = false;
        int mask = (1 << 30) | (1 << 04);
        RaycastHit hit;
        var ray = new Ray(new Vector3(chest.transform.position.x, chest.transform.position.y + 1, chest.transform.position.z), Vector3.down);
        if (Physics.Raycast(ray, out hit, distance_ray, mask))
        {
            respuesta = true;
        }
        return respuesta;
    }
    public float distanceGroud()
    {
        float respuesta = 0;
        int mask = (1 << 30) | (1 << 04);
        RaycastHit hit;
        var ray = new Ray(new Vector3(chest.transform.position.x, chest.transform.position.y + 1, chest.transform.position.z), Vector3.down);
        if (Physics.Raycast(ray, out hit, 100, mask))
        {
            respuesta = hit.distance;
        }
        return respuesta;
    }

    public void impulsar()
    {
        //if (air)
        //{
            Vector3 dir = new Vector3(0, 1, 0);
            chest.GetComponent<Rigidbody>().velocity = dir.normalized * force_impulse;
        //}

    }
    public void impulsar2()
    {
        //if (air)
        //{
        Vector3 dir = new Vector3(0, 1, 0);
        chest.GetComponent<Rigidbody>().velocity = dir.normalized * force_impulse_car;
        //}

    }
    public void win()
    {
        anim.SetTrigger("win");
        control_camera.SetTrigger("3");
        miraracamara();
    }
    public void activarAnim()
    {
        var emision = par_money.emission;
        emision.enabled = true;
        print("dinero");
        Invoke("desactivarAnim", 0.6f);
    }
    public void desactivarAnim()
    {
        var emision = par_money.emission;
        emision.enabled = false;
        Invoke("activarAnim", 2.06f);
    }
}
