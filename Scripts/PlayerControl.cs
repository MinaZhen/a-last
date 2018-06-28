using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private VirtualJoystic joystic;
    private Arrow arrow;
    private Vector3 vectorMovement = Vector3.zero;
    private Animator anim;
    private But_pressed b_atk;
    private CamMov cm;

    public bool isAttacking = false;

    private float walkSpd;

    // Gets animator, camera and controls
    void Start () {
        joystic = GameObject.Find("JoysticBG").GetComponent<VirtualJoystic>();
        if (joystic == null) Debug.LogError("404: joystic in PlayerCtrl");
        arrow = GameObject.Find("Arrows").GetComponent<Arrow>();
        if (arrow == null) Debug.LogError("404: arrow in PlayerCtrl");
        b_atk = GameObject.Find("Atk").GetComponent<But_pressed>();
        if (b_atk == null) Debug.LogError("404: b_atk in PlayerCtrl");
        anim = GameObject.Find("Pollo").GetComponent<Animator>();
        if (anim == null) Debug.LogError("404: anim in PlayerCtrl");
        cm = GameObject.Find("Main Camera").GetComponent<CamMov>();
        if (cm == null) Debug.LogError("404: cm in PlayerCtrl");
    }
	
	// Specifies variables of movement and manages attack
	void Update () {
        vectorMovement = Direccion();
        walkSpd = cm.camSpd + 2;
        if (Input.GetKeyDown(KeyCode.X)) b_atk.doit = true;
        if (b_atk.doit)  anim.SetBool("atack", true); else anim.SetBool("atack", false);
        if ((anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))){
            isAttacking = true;
            vectorMovement.x += 1.5f;
            if (anim.GetBool("atack")) b_atk.doit = false;
        } else isAttacking = false;
    }

    // Makes move the player
    void FixedUpdate() {
        Move();
    }

    // Calculates player movements and apply it
    private void Move() {
        if (vectorMovement != Vector3.zero) {
            this.transform.Translate(Vector3.forward * (walkSpd * vectorMovement.x) * Time.deltaTime);
            this.transform.Translate(Vector3.up * (walkSpd * vectorMovement.y) * Time.deltaTime);
            this.transform.Translate(Vector3.left * (walkSpd * vectorMovement.z) * Time.deltaTime);
        }
    }

    // Gets the vector of movement
    private Vector3 Direccion() {
        Vector3 move = Vector3.zero;

        move.x = joystic.Horizontal();
        move.y = joystic.Vertical();
        move.z = arrow.Deep();

        if (move.magnitude > 1) move.Normalize();

        return move;
    }
}
