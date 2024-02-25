using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;

public class zmianaMaterialu : MonoBehaviour
{
    private InteractionController cont;
    public SkinnedMeshRenderer dlon;
    public Material zielony, pogrubiony;
    void Start()
    {
        cont = GetComponent<InteractionController>();
    }

    void Update()
    {
        Debug.Log(cont);
        if (cont != null)
        {
            if(cont.isPrimaryHovering)
                dlon.material = pogrubiony;

            if (cont.isGraspingObject)
                dlon.material = zielony;
        }
    }
}
