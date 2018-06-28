using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacle : MonoBehaviour {
    private PlayerStats pStats;
    private GameObject player;

    // Gets player components
    void Start () {
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        if (pStats == null) Debug.LogError("404: pStats in tentacle");
        player = GameObject.Find("Player");
        if (pStats == null) Debug.LogError("404: player in tentacle");
    }

    // Orders to destroy the tentacle depending of its position
    void Update() {
        if (this.transform.position.x < player.transform.position.x - 25) Destroy(this.gameObject);
    }

    // Destroy the tentacle
    public void DestroyMe() {
        pStats.enemies++;
        Settings.enemies = pStats.enemies;
        Destroy(this.gameObject);
    }
}
