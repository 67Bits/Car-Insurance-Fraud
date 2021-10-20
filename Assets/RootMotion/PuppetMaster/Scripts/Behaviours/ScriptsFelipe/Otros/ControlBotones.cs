using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlBotones : MonoBehaviour
{

    private ControlJuego controljuego;
    private GAManager gamanager;

    private void Start()
    {
        controljuego = GameObject.FindGameObjectWithTag("ControlJuego").GetComponent<ControlJuego>();
        gamanager = GameObject.FindGameObjectWithTag("GAManager").GetComponent<GAManager>();
    }

    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void nextLevel()
    {
        gamanager.OnLevelComplete((controljuego.nivelActual+1));

        if (!controljuego.yatermineniveles)
        {
            if (controljuego.nivelActual < 4)
            {
                controljuego.nivelActual++;
                controljuego.nivelActualReal++;
                SceneManager.LoadScene(controljuego.lista_escenas_niveles[controljuego.nivelActual]);
            }
            else
            {
                controljuego.nivelActualReal++;
                controljuego.yatermineniveles = true;

                int ran = Random.Range(0, 5);
                while (ran == controljuego.nivelActual)
                {
                    ran = Random.Range(0, 5);
                }

                controljuego.nivelActual = ran;
                SceneManager.LoadScene(controljuego.lista_escenas_niveles[controljuego.nivelActual]);
            }
        }
        else
        {
            int ran = Random.Range(0, 5);
            while (ran == controljuego.nivelActual)
            {
                ran = Random.Range(0, 5);
            }
            print(ran);
            controljuego.nivelActualReal++;
        
            
            controljuego.nivelActual = ran;
            SceneManager.LoadScene(controljuego.lista_escenas_niveles[controljuego.nivelActual]);
        }

    }
}
