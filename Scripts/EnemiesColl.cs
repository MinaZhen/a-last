using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesColl : MonoBehaviour {

    private Rigidbody rB;
    private PlayerStats pStats;
    private PlayerControl pCtrl;
    private Tentacle tnt;

    public bool destroyMe = false;

    // Find player and tentacles components
    void Start () {
        rB = GameObject.Find("Pollo").GetComponent<Rigidbody>();
        if (rB == null) Debug.LogError("404: rB in enemiesColl");
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        if (pStats == null) Debug.LogError("404: pStats in enemiesColl");
        pCtrl = GameObject.Find("Player").GetComponent<PlayerControl>();
        if (pCtrl == null) Debug.LogError("404: pCtrl in enemiesColl");
        tnt = gameObject.GetComponentInParent<Tentacle>();
        if (tnt == null) Debug.LogError("404: tnt in enemiesColl");
    }

    // If player is attacking and hits a tentacle, destroy it.
    void OnTriggerEnter(Collider other) {
        if ((other.transform.name == "Collide") && ((this.transform.name == "col1") || (this.transform.name == "col2"))){
            if (pCtrl.isAttacking) {
                tnt.DestroyMe();
            } else {
                pStats.life -= 10;
            }
        }
    }
}
