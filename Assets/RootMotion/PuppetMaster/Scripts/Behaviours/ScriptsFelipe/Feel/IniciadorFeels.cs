    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;
public class IniciadorFeels : MonoBehaviour
{
    private MMFeedbacks feel;
    void Start()
    {
        feel = gameObject.GetComponent<MMFeedbacks>();
        Invoke("activar", 0.2f);
    }
    public void activar()
    {
        feel.PlayFeedbacks();
    }
}
