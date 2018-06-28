using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    
    public int life = 100;
    public uint gears, enemies = 0;
    public Material lifeMat;
    private float h, s, v;

	// Sets the color belt
	void Start () {
        lifeMat = GameObject.Find("Superpollo").GetComponent<Renderer>().materials[1];
        if (lifeMat == null) Debug.LogError("404: Life material in PlayerControl.");
        Color.RGBToHSV(lifeMat.color, out h, out s, out v);
        h = 0f;
        s = 1f;
        v = 1f;
        lifeMat.color =  Color.HSVToRGB(h, s, v);
        lifeMat.SetColor("_EmissionColor", Color.HSVToRGB(h, s, v));
    }
	
	// Changes belt color if player gets damage
	void Update () {  
        if (life > 0) ColChange();
    }

    // Calculates belt color according to life
    private void ColChange() {
        Color.RGBToHSV(lifeMat.color, out h, out s, out v);
        h = life * 0.003f;

        lifeMat.color = Color.HSVToRGB(h, s, v);
        lifeMat.SetColor("_EmissionColor", Color.HSVToRGB(h, s, v));

    }
}
