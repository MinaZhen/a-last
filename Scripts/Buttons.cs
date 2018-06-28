using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Buttons : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {

    private Image _button;
    private Text _text;
    public Sprite but_up, but_dwn;
    [HideInInspector]
    public bool doit = false;
    public Color cTxtUp, cTxtDwn;


    // Use this for initialization
    void Start() {
        _button = this.gameObject.GetComponent<Image>();
        _text = this.transform.GetChild(0).GetComponent<Text>();
        _text.fontStyle = FontStyle.Normal;
        _text.color = cTxtUp;

    }

    // Change styles
    public virtual void OnDrag(PointerEventData ped) {
        _text.color = cTxtDwn;
    }

    // Changes styles
    public virtual void OnPointerDown(PointerEventData ped) {
        OnDrag(ped);
        _button.sprite = but_dwn;
        _text.color = Color.black;

    }

    // Changes stiles and do action
    public virtual void OnPointerUp(PointerEventData ped) {
        _text.fontStyle = FontStyle.Normal;
        _text.color = cTxtUp;
        doit = true;
        _button.sprite = but_up;
    }


}