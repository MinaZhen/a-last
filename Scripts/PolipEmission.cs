using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolipEmission : MonoBehaviour {

    private Material polip;
    private float h, s, v, rand;
    

    // Gets polip material and sets the color
    void Start() {
        polip = this.gameObject.GetComponent<Renderer>().material;
        rand = Random.Range(3f, 5f);
        Color.RGBToHSV(polip.color, out h, out s, out v);
        h = 0f;
        s = 1f;
        v = 1f;
        polip.color = Color.HSVToRGB(h, s, v);
        polip.SetColor("_EmissionColor", Color.HSVToRGB(h, s, v));
    }

    // Order to change the polip emission color
    void Update() {
        ColChange();
    }

    // Calculates the blink color
    private void ColChange() {
        Color c;
        Color.RGBToHSV(polip.color, out h, out s, out v);
        float t = Mathf.Sin(Time.time * rand);
        h = s = 0f;
        v = Mathf.Lerp(0.5f, 0.7f, t);
        c = Color.HSVToRGB(h, s, v);
        polip.SetColor("_EmissionColor", Color.HSVToRGB(h, s, v));

    }
}
