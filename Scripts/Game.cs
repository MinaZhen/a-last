using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Main class of playable game
public class Game : MonoBehaviour {

    public bool start = false;
    private CamMov camMov;
    private GameObject controls, tap, end, score, gameover, win ;
    private PlayerControl pCtrl;
    private Tap tapBut;
    private PlayerStats pStats;

    private Text txtEnem, txtGear, txtScore;

    private float timer, tim = 0f;
    private bool endBool, toWinG, toWinE = false;

    // Shows first screens and get components on level
    void Start () {

        gameover = GameObject.Find("GameOver");
        win = GameObject.Find("WIN");
        txtEnem = GameObject.Find("TextM").GetComponent<Text>();
        txtGear = GameObject.Find("TextG").GetComponent<Text>();
        txtScore = GameObject.Find("TextScore").GetComponent<Text>();

        win.SetActive(false);
        gameover.SetActive(false);
        
        pStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        if (pStats == null) Debug.LogError("404: pStats in game");
        camMov = GameObject.Find("Main Camera").GetComponent<CamMov>();
        if (camMov == null) Debug.LogError("404: camMov in Game");
        camMov.enabled = false;
        controls = GameObject.Find("Controls");
        if (controls == null) Debug.LogError("404: controls in Game");
        controls.SetActive(false);
        pCtrl = GameObject.Find("Player").GetComponent<PlayerControl>();
        if (pCtrl == null) Debug.LogError("404: pCtrl in Game");
        pCtrl.enabled = false;

        score = GameObject.Find("score");
        score.SetActive(false);
        end = GameObject.Find("End");
        end.SetActive(false);
        tap = GameObject.Find("Tap");
        tapBut = tap.transform.GetComponentInChildren<Tap>();
    }
	
	// Check states and inizializes game when screen is tapped
	void Update () {
		if (tapBut.doit) {
            start = true;
            tapBut.doit = false;
            tap.SetActive(false);
        }
        if (start) {
            camMov.enabled = true;
            controls.SetActive(true);
            pCtrl.enabled = true;
        }
        if (endBool) {
            Score();
        }
        
	}

    // Endings (gameover and level finished)
    public void Ending() {
        if (pStats.life < 0f) { // Shows game over and return to menu
            timer += Time.deltaTime;
            end.SetActive(true);
            gameover.SetActive(true);
            if (timer > 3) {
                SceneManager.LoadScene("menu");
            }
        } else { // Calculates the score, shows screen and order to show punctuations
            timer += Time.deltaTime;
            if (timer > 3) {
                end.SetActive(true);
                score.SetActive(true);
                if (!Settings.walls) {
                    Settings.totalScore = ((int)Settings.gears + (int)Settings.enemies) * (int)Settings.level;
                } else {
                    Settings.totalScore = (((int)Settings.gears + (int)Settings.enemies) * 2) * (int)Settings.level;
                }
                if (Settings.gears > (Settings.key - 5) * 0.7) {
                    txtGear.color = Color.green;
                    toWinG = true;
                } else txtGear.color = Color.red;
                if (Settings.enemies > (Settings.key - 5) * 0.7) {
                    txtEnem.color = Color.green;
                    toWinE = true;
                } else txtEnem.color = Color.red;
                txtEnem.text = Settings.enemies.ToString("");
                txtGear.text = Settings.gears.ToString("");
                endBool = true;
            }
        }  
    }

    // Shows the punctuation and returns to menu
    private void Score() {
        Settings.lvlScore += 2;
        txtScore.text = Settings.lvlScore.ToString("");
        if (Settings.lvlScore > Settings.totalScore) {
            Settings.lvlScore = Settings.totalScore;
            txtScore.text = Settings.lvlScore.ToString("");
            tim += Time.deltaTime;
            if (tim > 2f) {
                if (toWinE & toWinG) {
                    win.SetActive(true);
                } else {
                    gameover.SetActive(true);
                }
                if (tim > 5) SceneManager.LoadScene("menu");
            }
        }
    }


}
