using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class But_pressed : MonoBehaviour, IPointerDownHandler {

    [HideInInspector] public bool doit = false;

    // When action button is pressed, sets the boolean to true (animation will turn it false)
    public virtual void OnPointerDown(PointerEventData ped) {  doit = true; }
}

