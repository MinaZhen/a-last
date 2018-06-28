using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour {
    private SkinnedMeshRenderer smr;
    private PlayerStats pStats;
    private float timer = 0f;
    private Bomb bmb;
    private GameObject explosion, player;
    private bool destroy, once = false;

    // Initializes bomb and its material and finding player
    void Start () {
        player = GameObject.Find("Player");

        smr = this.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        if (pStats == null) Debug.LogError("404: pStats in bomb");

        bmb = this.GetComponent<Bomb>();
        if (bmb == null) Debug.LogError("404: Bmb in bomb");
        explosion = this.transform.GetChild(1).gameObject;
        if (explosion == null) Debug.LogError("404: Explosion in bomb");
    }

    // Calculates if needs to destroy
   void Update() {
        if (this.transform.position.x < (player.transform.position.x - 2)) {
            destroy = true;
        }
        if (this.transform.position.x < player.transform.position.x - 25) Destroy(this.gameObject);

        if (destroy && !once) {
            pStats.enemies++;
            Settings.enemies = pStats.enemies;
            DestroyMe();
            once = true;
        }
    }

    // Self-destroying
    public void DestroyMe() {
        smr.enabled = false;
        explosion.SetActive(true);
    }

    // Takes player's amount of life and destroy itself
    void OnTriggerEnter(Collider other) {
        if (other.transform.name == "Collide"){ 
            DestroyMe();
            pStats.life -= 10;
        }
    }
}
