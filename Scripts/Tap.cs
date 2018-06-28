using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// For screens as to close needs to be clicked
public class Tap : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    [HideInInspector] public bool doit = false;

    // Listen than pointer/touch is down
    public virtual void OnPointerDown(PointerEventData ped) {}

    // Activates the action
    public virtual void OnPointerUp(PointerEventData ped) {
        doit = true;
    }
}
