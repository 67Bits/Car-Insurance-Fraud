using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GAManager : MonoBehaviour
{
    public static GAManager instance;
    public ControlJuego juegoControl;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        juegoControl = GameObject.FindGameObjectWithTag("ControlJuego").GetComponent<ControlJuego>();
        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level" + (juegoControl.nivelActual + 1));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLevelComplete (int level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level " + level);
        print("Level " + level);
    }
}
