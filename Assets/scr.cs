using Leap;
using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr : MonoBehaviour
{
    public GameObject obiekt;
    private Vector3 poczRozmiar, lewaPoz, prawaPoz;
    private bool zmianaRozmiaru = false;
    private float poczatkowyDyst, aktualnyDyst;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hand lewaReka = Hands.Provider.GetHand(Chirality.Left); 
        Hand prawaReka = Hands.Provider.GetHand(Chirality.Right);

        if(lewaReka!=null && prawaReka!=null)
        {
            if (lewaReka.IsPinching() && prawaReka.IsPinching())
                obiekt.transform.localScale = new Vector3(0.5f,0.5f,0.5f);


        }
        
        


        //if (_pinchStrength > 0.8)
        //{
        //    bool _isPinching = true; // or do the rest of your code here, hand is grabbing.
        //}
    }
}
