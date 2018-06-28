using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Manages the main menu
public class Menu : MonoBehaviour {

    private Settings settings;
    private Buttons b_play, b_options, b_credits, b_exit, b_yes, b_no, b_back;
    private Tap b_tap;
    private GameObject cnv_Menu, cnv_Exit, cnv_Options, cnv_Credits, cnv_Wait;
    private Toggle mutFX, mutM, wall;
    private Slider volFX, volM, lvl;
    private AudioManager aM;
    private Image cred, bg;

    private bool music, creditBool = false;
    private float timer = 0f;

    void Awake() {
        settings = GameObject.Find("Main").GetComponent<Settings>();
        if (GameObject.Find("AudioManager").GetComponent<AudioManager>() == null) {
            aM = GameObject.Find("AudioManager").AddComponent<AudioManager>();
            music = false;
        } else {
            aM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            music = true;
        }
    }

    // Initialization
    void Start() {

        lvl = GameObject.Find("Speed").GetComponent<Slider>();
        lvl.value = Settings.level;

        volFX = GameObject.Find("Svol").GetComponent<Slider>();
        mutFX = GameObject.Find("Smute").GetComponent<Toggle>();
        volM = GameObject.Find("Mvol").GetComponent<Slider>();
        mutM = GameObject.Find("Mmute").GetComponent<Toggle>();
        wall = GameObject.Find("Walls").GetComponent<Toggle>();

        volFX.value = Settings.volumFX;
        volM.value = Settings.volum;
        mutFX.isOn = Settings.muteFX;
        mutM.isOn = Settings.mute;
        wall.isOn = Settings.walls;

        Settings.gears = 0;
        Settings.enemies = 0;
        Settings.totalScore = 0;
        Settings.lvlScore = 0;

        b_play = GameObject.Find("bPlay").GetComponent<Buttons>();
        b_options = GameObject.Find("bOptions").GetComponent<Buttons>();
        b_credits = GameObject.Find("bCredits").GetComponent<Buttons>();
        b_exit = GameObject.Find("bExit").GetComponent<Buttons>();
        b_yes = GameObject.Find("bYes").GetComponent<Buttons>();
        b_no = GameObject.Find("bNo").GetComponent<Buttons>();
        b_back = GameObject.Find("bBack").GetComponent<Buttons>();
        b_tap = GameObject.Find("bTap").GetComponent<Tap>();

        bg = GameObject.Find("BG").GetComponent<Image>();
        cred = GameObject.Find("credits").GetComponent<Image>();

        cnv_Exit = GameObject.Find("Exit");
        cnv_Exit.SetActive(false);
        cnv_Menu = GameObject.Find("Menu");
        cnv_Menu.SetActive(true);
        cnv_Credits = GameObject.Find("Credits");
        cnv_Credits.SetActive(false);
        cnv_Options = GameObject.Find("Options");
        cnv_Options.SetActive(false);
        cnv_Wait = GameObject.Find("Wait");
        cnv_Wait.SetActive(false);
    }

    // Manages menu screens
    void Update() {
        if (!music) { aM.Play(0); music = true; }

        if (cnv_Menu.activeInHierarchy) {
            if (b_play.doit) {
                cnv_Wait.SetActive(true);
                b_play.doit = false;
                cnv_Menu.SetActive(false);               

            } else if (b_options.doit) {
                cnv_Options.SetActive(true);
                b_options.doit = false;
                cnv_Menu.SetActive(false);

            } else if (b_credits.doit) {
                cnv_Credits.SetActive(true);
                b_credits.doit = false;
                cnv_Menu.SetActive(false);
                bg.enabled = false;

            } else if (b_exit.doit) {
                cnv_Exit.SetActive(true);
                b_exit.doit = false;
                cnv_Menu.SetActive(false);
            }
        }
        
        if (cnv_Exit.activeInHierarchy) {

            if (b_yes.doit) {
                Application.Quit();
                b_yes.doit = false;

            } else if (b_no.doit) {
                cnv_Menu.SetActive(true);
                b_no.doit = false;
                cnv_Exit.SetActive(false);

            } 
        }

        if (cnv_Credits.activeInHierarchy) {
            Credits();
            if (b_tap.doit) {
                cnv_Menu.SetActive(true);
                b_tap.doit = false;
                cnv_Credits.SetActive(false);
                creditBool = false;
                bg.enabled = true;
            }
        }

        if (cnv_Options.activeInHierarchy) {
            Options();
            if (b_back.doit) {
                cnv_Menu.SetActive(true);
                b_back.doit = false;
                cnv_Options.SetActive(false);
            }
        }

        if (cnv_Wait.activeInHierarchy) {
            timer += Time.deltaTime;
            if (timer > 2f) {
                timer = 0f;
                SceneManager.LoadScene("A_last");
            }
        }
    }

    // Menu options manager
    private void Options() {
        Settings.volum = volM.value;
        Settings.volumFX = volFX.value;

        Settings.mute = mutM.isOn;
        Settings.muteFX = mutFX.isOn;

        Settings.walls = wall.isOn;

        Settings.level = lvl.value;
        switch (lvl.value.ToString("")) {
            case "2":
                Settings.acc = 0.007f;
                break;
            case "3":
                Settings.acc = 0.015f;
                break;
            case "4":
                Settings.acc = 0.027f;
                break;
            case "5":
                Settings.acc = 0.045f;
                break;
        }
    }

    // Changes position to show credits behind the chicken
    private void Credits() {
        if (!creditBool) {
            cred.rectTransform.anchoredPosition = new Vector2(0f, -2258f);
            creditBool = true;
        }
        cred.rectTransform.anchoredPosition = cred.rectTransform.anchoredPosition + new Vector2(0f, 1f);
    }
}

