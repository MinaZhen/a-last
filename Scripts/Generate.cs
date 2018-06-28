using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Procedural creation, in game, of the scene
public class Generate : MonoBehaviour {
    private PlayerStats pStats;

    public GameObject[] ces;
    public GameObject polipo, ship, gear, e1, e2;
    private GameObject cam;
    private CamMov cm;
    private Game game;

    private float mesure = 22.67f;
    private float adding, camPos, timer = 0f;
    private uint id, idBg, add, key = 0;
    
    
	// Get components and order creates the first section of scene
	void Start () {
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        if (pStats == null) Debug.LogError("404: pStats in generate");
        game = GameObject.Find("Game").GetComponent<Game>();
        cm = GameObject.Find("Main Camera").GetComponent<CamMov>();
        key = Settings.key;
        cam = GameObject.Find("Main Camera");
        if (cam == null) Debug.Log("404: cam in Generate");
        adding = -mesure;
        Add();
        Add();
        Add();
        Add();
        camPos = mesure;
    }
	
	// Checking camera position order delete older section and create a new one 
	void Update () {
        if (cam.transform.position.x >= camPos) Add(); Delete();
        if (cam.transform.position.x > ((mesure * key) - mesure - 2f)){
            cm.camSpd = Mathf.Lerp(cm.camSpd, 0f, Time.deltaTime);
            if (cm.camSpd < 0.5f) cm.camSpd = 0f;
                
        }
        if ((cm.camSpd == 0f) || (pStats.life < 0)) {
            game.Ending();
        }

    }

    // Instantiate vein and order instantiate elements
    private void Add() {
        GameObject o;
        o = Instantiate(ces[idBg], new Vector3(adding, 0f, 10f), Quaternion.Euler(-90f, 0f, 90f)) as GameObject;
        o.transform.parent = this.transform;
        idBg++;
        idBg %= 8;
        id ++;
        o.transform.name = "BG " + id;
        if (id == key) Ship(); else if (id < key) Adding(id);
        adding += mesure;
        camPos += mesure;
    }

    // Delete passed elements
    private void Delete() {
        GameObject gO = GameObject.Find("BG " + (id - 4));
        GameObject pol = GameObject.Find("P " + (id - 4));
        if (id % 2 == 0) { 
            GameObject gea = GameObject.Find("Gear " + (id - 4));
            Destroy(gea);
        }
        Destroy(gO);
        Destroy(pol);
    }

    // Instantiate enemies and gears in random positions
    private void Adding(uint id) {
        int rand = Random.Range(0, 5);
        float size = Random.Range(1.3f, 1.7f); 
        Vector3 pos = Vector3.zero;
        
        GameObject p, g, e;
        
        switch(rand) {

            case (0): //down back
                pos = new Vector3(Random.Range(adding, adding + mesure), -4.07f, 12f);
                p = Instantiate(polipo, pos, Quaternion.Euler(-60f, (rand * 72f), 0f)) as GameObject;
                p.transform.parent = this.transform;
                p.transform.localScale = new Vector3(size *1.1f, size * 1.1f, size * 1.1f);
                p.transform.name = "P " + id;
                break;

            case (1): // down
                pos = new Vector3(Random.Range(adding, adding + mesure), -6.58f, Random.Range(0f, 6.66f));
                p = Instantiate(polipo, pos, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                p.transform.parent = this.transform;
                p.transform.Rotate(new Vector3(0f, (rand * 72f), 0f));
                p.transform.localScale = new Vector3(size, size, size);
                p.transform.name = "P " + id;
                break;

            case (2):// up
                pos = new Vector3(Random.Range(adding, adding + mesure), 6.69f, Random.Range(0f, 6.66f));
                p = Instantiate(polipo, pos, Quaternion.Euler(0, 0f, 180f)) as GameObject;
                p.transform.parent = this.transform;
                p.transform.Rotate(new Vector3(0f, (rand * 72f), 0f));
                p.transform.localScale = new Vector3(size, size, size);
                p.transform.name = "P " + id;
                break;

            case (3):  // back
                pos = new Vector3(Random.Range(adding, adding + mesure), -0.6f, 13.23f);
                p = Instantiate(polipo, pos, Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
                p.transform.Rotate(new Vector3(0f, (rand * 72f), 0f));
                p.transform.parent = this.transform;
                p.transform.localScale = new Vector3(size * 1.2f, size * 1.2f, size * 1.2f);
                p.transform.name = "P " + id;
                break;

            case (4): // up back
                pos = new Vector3(Random.Range(adding, adding + mesure), 3f, 12f);
                p = Instantiate(polipo, pos, Quaternion.Euler(-120f, 0f, 0f)) as GameObject;
                p.transform.Rotate(new Vector3(0f, (rand * 72f), 0f));
                p.transform.parent = this.transform;
                p.transform.localScale = new Vector3(size * 1.1f, size * 1.1f, size * 1.1f);
                p.transform.name = "P " + id;
                break;
        }
        

        if ((id % 2 == 0) && (id > 1)) { 
            g = Instantiate(gear, new Vector3((adding + (mesure * 0.5f)), 0f, Random.Range(1f, 6f)), Quaternion.Euler(-90f, 0f, 0f)) as GameObject;
            g.transform.parent = this.transform;
            g.transform.name = "Gear " + id;
        }

        if (id > 1) {
            if (rand > 2.5f) {
                e = Instantiate(e1, new Vector3((adding + mesure), 
                    Random.Range(Random.Range(-5f, -2f), Random.Range(2f, 5f)), 
                    Random.Range(Random.Range(-6f, -2f), Random.Range(2f, 6f))), 
                    Quaternion.Euler(0, 0, 90f)) as GameObject;
                e.transform.localRotation = Quaternion.Euler(Random.Range(-70f, -20f), 90f, 0f);
                e.transform.parent = this.transform;
                e.transform.name = "Tnt " + id;
            } else {
                e = Instantiate(e2, new Vector3((adding + mesure),
                    Random.Range(Random.Range(-5f, -2f), Random.Range(2f, 5f)),
                    Random.Range(Random.Range(-6f, -2f), Random.Range(2f, 6f))),
                    Quaternion.Euler(0f, 180, 0f)) as GameObject;
               
                e.transform.parent = this.transform;
                e.transform.name = "Bomb";
            }
        }
    }

    // At the end of gived lenght, instantiates the spaceship
    private void Ship() {
        GameObject o;
        Vector3 p = ship.transform.position;
        p.x = (mesure * key) - mesure;

        o = Instantiate(ship, p, ship.transform.rotation) as GameObject;
    }
}
