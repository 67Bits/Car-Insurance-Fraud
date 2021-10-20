using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using MoreMountains.Feedbacks;

public class ControlCanvas : MonoBehaviour
{
    [Header("Anexos UI")]
    [SerializeField] public TextMeshProUGUI tex_all_coins;
    [SerializeField] public TextMeshProUGUI tex_current_coins;
    [SerializeField] public TextMeshProUGUI tex_combo;

    [SerializeField] public GameObject panel_principal;
    [SerializeField] public GameObject panel_victoria;
    [SerializeField] public Slider sliderProgreso = null;

    [SerializeField] public TextMeshProUGUI tex_level;

    public GameObject container_car;
    public GameObject Canvas_Inicio;

    private bool primeravez = false;

    public MMFeedbacks feed_ui_coin;
    private bool yahicefeel = false;

    private void Start()
    {
        container_car.SetActive(false);
        Canvas_Inicio.SetActive(true);
    }

    private void Update()
    {
        if (sliderProgreso.value >= 100 && !yahicefeel)
        {
            feed_ui_coin.PlayFeedbacks();
            yahicefeel = true;
        }
    }

    public void iniciarJuego()
    {
        if (!primeravez)
        {
            container_car.SetActive(true);
            Canvas_Inicio.SetActive(false);
            panel_principal.SetActive(true);
            primeravez = true;
        }
       
    }
    public void replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


  
}
