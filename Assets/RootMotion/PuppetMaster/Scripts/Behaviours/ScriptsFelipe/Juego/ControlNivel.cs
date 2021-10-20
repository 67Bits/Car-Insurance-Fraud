using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ControlNivel : MonoBehaviour
{
    public ControlCanvas control_canvas;

    [HideInInspector] public int point_level;
    // Start is called before the first frame update
    [HideInInspector] public int all_coins;
    [HideInInspector] public int current_coins;

    [SerializeField] public float seconds_Increment_coin;
    [SerializeField] public int number_Increment_coin;
    [SerializeField] public int number_goal_coin;

    // [HideInInspector] public bool increment_active = false;

    [HideInInspector] public int combo;
    public float tiemporesultadoenpantalla;
    private Slider sliderProgreso = null;

    private CharacterMovement character_movement = null;

    private ControlJuego controljuego;

    public bool bool_win;



    private int monedas_temp;
    private void Start()
    {
        control_canvas = GameObject.FindGameObjectWithTag("controlcanvas").GetComponent<ControlCanvas>();
        character_movement = GameObject.FindGameObjectWithTag("MyCharacterMovement").GetComponent<CharacterMovement>();
        controljuego = GameObject.FindGameObjectWithTag("ControlJuego").GetComponent<ControlJuego>();

        control_canvas.tex_combo.text = "";
        bool_win = false;
        sliderProgreso = control_canvas.sliderProgreso;

        control_canvas.tex_level.text = (controljuego.nivelActualReal + 1).ToString();
    }


    /////////////////////////////////////////////////////
    public void startIncrementCoin()
    {
        StopCoroutine("coinIncrement");
        StartCoroutine("coinIncrement");         
    }

    IEnumerator coinIncrement()
    {
        yield return new WaitForSeconds(seconds_Increment_coin);

        // Actualiza las monedas del personaje
        current_coins += number_Increment_coin;
        control_canvas.tex_current_coins.text = "$ " + current_coins.ToString();

        // Actualiza las monedas totales del juego
        all_coins += number_Increment_coin;
        control_canvas.tex_all_coins.text = "$ " + all_coins.ToString();
        // Actualiza el slider 
        //sliderProgreso.value = (all_coins * 100) / number_goal_coin;
        sliderProgreso.DOValue((all_coins * 100) / number_goal_coin, seconds_Increment_coin);


        StartCoroutine("coinIncrement");
    }
    public void animarBarraProgreso()
    {
        sliderProgreso.DOValue((all_coins * 100) / number_goal_coin, 1f);
    }

   


    /////////////////////////////////////////////////////







    public void UpdateAllCoins()
    {
        StopCoroutine("coinIncrement");


        if (combo == 0)
        {
            all_coins += current_coins;
        }
        else
        {
            current_coins = current_coins * combo;
            all_coins += current_coins;
        }
        // Invoke("updateAllCoinUI", 0.1f);
        updateAllCoinUI();

        combo = 0;
        //control_canvas.tex_current_coins.text = "$ " + current_coins.ToString();
        current_coins = 0;

        if (all_coins >= (3 * (number_goal_coin / 4)))
        {
            // Tetura 3
            character_movement.herir2(3);
        }
        else if (all_coins >= (2 * (number_goal_coin / 4)))
        {
            // Tetura 2
            character_movement.herir2(2);
        }
        else if (all_coins >= ((number_goal_coin / 4)))
        {
            // Tetura 1
            character_movement.herir2(1);
        }
        if (all_coins >= number_goal_coin)
        {
            win();
        }
    }
    public void updateAllCoinUI()
    {
        control_canvas.tex_all_coins.text = "$ " + all_coins.ToString();
        control_canvas.tex_combo.text = "";
        control_canvas.tex_current_coins.text = "";
        animarBarraProgreso();
    }

    public void updateCombo()
    {
        combo++;
        if (combo > 1)
        {
            control_canvas.tex_combo.text = "Combo X" + combo.ToString();
        }
    }

   
 

    public void win()
    {
        character_movement.win();
        //control_canvas.panel_principal.SetActive(false);
        control_canvas.panel_victoria.SetActive(true);
        control_canvas.container_car.SetActive(false);
        bool_win = true;
    }
    public void win2()
    {
        control_canvas.panel_principal.SetActive(false);
        control_canvas.panel_victoria.SetActive(true);
    }

}
