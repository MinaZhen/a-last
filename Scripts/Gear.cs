using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {

    private PlayerStats pStats;
    public float rotatespeed = 1f;
    public float floatspeed = 0.001f;

    void Start() {
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        if (pStats == null) Debug.LogError("404: pStats in Gear");
    }

    // Gear rotation
	void Update () {
        this.transform.Rotate(0f, 0f, rotatespeed);
    }

    // If player touches the gear, add to stats and destroy it
    void OnTriggerEnter(Collider other) {
        if (other.transform.name == "Collide") {
            pStats.gears++;
            Settings.gears = pStats.gears;
            Destroy(this.gameObject);
        }
    }
}
