using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class RiverController : MonoBehaviour {

    public GridBuildingSystem riverGridBuildingSystem;
    private float buildingRotation = 0;
    public int money;
    public int trashPassed;
    public int maxTrashPassed;
    public int totalLevelTrash;
    public int trashEliminated;
    public int trashInRiver;
    public Text txtMoney;
    public Text txtTrashPassed;
    public Text txtMaxTrashPassed;

    
    public TrashSpawner trashSpawner;

    public GameObject grabbapultGhost;
    public GameObject algaeChuckerGhost;
    public GameObject magicMagnetGhost;
    public GameObject buildingGhost;

    public GameObject background;
    public Sprite riverBackground100;
    public Sprite riverBackground95;
    public Sprite riverBackground90;
    public Sprite riverBackground85;
    public Sprite riverBackground80;
    public Sprite riverBackground75;
    public Sprite riverBackground70;
    public Sprite riverBackground65;
    public Sprite riverBackground60;
    public Sprite riverBackground55;
    public Sprite riverBackground50;
    public Sprite riverBackground45;
    public Sprite riverBackground40;
    public Sprite riverBackground35;
    public Sprite riverBackground30;
    public Sprite riverBackground25;
    public Sprite riverBackground20;
    public Sprite riverBackground15;
    public Sprite riverBackground10;
    public Sprite riverBackground5;
    public Sprite riverBackground0;

    private float spawnerTimer = 3.5f;
    private bool timerStarted = true;

    // Update is called once per frame
    void Update() {
        if (Time.timeScale == 1) {

            if (timerStarted) {
                spawnerTimer -= Time.deltaTime;
            }

            if (spawnerTimer < 0) {
                if (totalLevelTrash - (trashEliminated + trashPassed + trashInRiver) > 0) {
                    trashSpawner.SpawnRiverTrash();
                    trashInRiver++;
                } else {
                    timerStarted = false;
                }
                spawnerTimer = 3.5f;
            }

            if (riverGridBuildingSystem.GetSelectedBuilding() != null) {                   //if there is a building selected
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //get world position from mouse position
                buildingGhost.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);//update buildingGhost position to mouseposition

                if(Input.GetKeyDown(KeyCode.R)) { //If R key is pressed
                    buildingRotation -= 90;       //Rotate Building 90 degrees
                    if (buildingRotation == 360) {//if rotated a full circle
                        buildingRotation = 0;    //clamp back down to 0
                    }
                    buildingGhost.transform.rotation = Quaternion.Euler(0, 0, buildingRotation);//rotate buildingGhost accoridngly
                }
            }

            if (Input.GetMouseButtonDown(0)) {                                         //If left clicked
                if (riverGridBuildingSystem.GetSelectedBuilding() != null) {           //and if a building is selected
                    if (riverGridBuildingSystem.GetSelectedBuildingCost() <= money) {  //and player has enough money
                        if (riverGridBuildingSystem.Build(money, buildingRotation)) {  //build the building, if build was successful
                            money -= riverGridBuildingSystem.GetSelectedBuildingCost();//reduce money by building cost
                            FindObjectOfType<GameController>().UpdateStats();          //update player stats
                            FindObjectOfType<AudioManager>().Play("Click");            //play build sound
                        }
                    } else {                                                                  //if player can't afford building
                        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();           //Get the mouse position
                        UtilsClass.CreateWorldTextPopup("Cannot afford this!", mousePosition);//display error text
                        FindObjectOfType<AudioManager>().Play("Cant Build");                  //play build error sound
                    }
                }
            } else if (Input.GetMouseButtonDown(1)) {        //If right clicked
                riverGridBuildingSystem.DeselectObjectType();//Deselect building
                toggleRiverGridDisplay(false);               //hide grid
                Destroy(buildingGhost);                      //Remove buildingGhost
                buildingRotation = 0;                        //Reset Building Rotation back down to 0;
            }
        }
    }

    public void toggleRiverGridDisplay(bool state) {
        riverGridBuildingSystem.transform.GetChild(0).gameObject.SetActive(state);//toggle horizontal lines
        riverGridBuildingSystem.transform.GetChild(1).gameObject.SetActive(state);//toggle vertical lines
    }

    public void GrabbapultButton() {
        riverGridBuildingSystem.SetSelectedBuilding(0);
        buildingGhost = Instantiate(grabbapultGhost, new Vector3(0f ,0f ,0f), Quaternion.identity);
        toggleRiverGridDisplay(true);
    }

    public void AlgaeChuckerButton() {
        riverGridBuildingSystem.SetSelectedBuilding(1);
        buildingGhost = Instantiate(algaeChuckerGhost, new Vector3(0f, 0f, 0f), Quaternion.identity);
        toggleRiverGridDisplay(true);
    }

    public void MagicMagneticButton() {
        riverGridBuildingSystem.SetSelectedBuilding(2);
        buildingGhost = Instantiate(magicMagnetGhost, new Vector3(0f, 0f, 0f), Quaternion.identity);
        toggleRiverGridDisplay(true);
    }

    public void resetRiverPollution() {
        background.GetComponent<SpriteRenderer>().sprite = riverBackground100;
    }

    public void IncreaseTrashEliminated() {
        trashEliminated++;
        trashInRiver--;
        if(trashInRiver < 0) {
            trashInRiver = 0;
        }

        float completedPercentage = ((float)trashEliminated / totalLevelTrash) * 100;
        completedPercentage = 100 - completedPercentage;

        if (completedPercentage <= 100 && completedPercentage > 95) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground100;
        } else if (completedPercentage <= 95 && completedPercentage > 90) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground95;
        } else if (completedPercentage <= 90 && completedPercentage > 85) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground90;
        } else if (completedPercentage <= 85 && completedPercentage > 80) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground85;
        } else if (completedPercentage <= 80 && completedPercentage > 75) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground80;
        } else if (completedPercentage <= 75 && completedPercentage > 70) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground75;
        } else if (completedPercentage <= 70 && completedPercentage > 65) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground70;
        } else if (completedPercentage <= 65 && completedPercentage > 60) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground65;
        } else if (completedPercentage <= 60 && completedPercentage > 55) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground60;
        } else if (completedPercentage <= 55 && completedPercentage > 50) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground55;
        } else if (completedPercentage <= 50 && completedPercentage > 45) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground50;
        } else if (completedPercentage <= 45 && completedPercentage > 40) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground45;
        } else if (completedPercentage <= 40 && completedPercentage > 35) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground40;
        } else if (completedPercentage <= 35 && completedPercentage > 30) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground35;
        } else if (completedPercentage <= 30 && completedPercentage > 25) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground30;
        } else if (completedPercentage <= 25 && completedPercentage > 20) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground25;
        } else if (completedPercentage <= 20 && completedPercentage > 15) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground20;
        } else if (completedPercentage <= 15 && completedPercentage > 10) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground15;
        } else if (completedPercentage <= 10 && completedPercentage > 5) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground10;
        } else if (completedPercentage <= 5 && completedPercentage > 0) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground5;
        } else if (completedPercentage <= 0) {
            background.GetComponent<SpriteRenderer>().sprite = riverBackground0;
        }
        checkLevelCleared();
    }

    public void checkLevelCleared() {
        if (trashEliminated + trashPassed == totalLevelTrash) {
            FindObjectOfType<GameController>().CheckGameWon();
        }
    }

    public void clearRiverExcess() {//Removes any left over game elements
        GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrow");
        for (int i = 0; i < arrows.Length; i++) {
            Destroy(arrows[i]);
        }

        GameObject[] bombs = GameObject.FindGameObjectsWithTag("AlgaeBomb");
        for (int i = 0; i < bombs.Length; i++) {
            Destroy(bombs[i]);
        }

        GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
        for (int i = 0; i < trash.Length; i++) {
            Destroy(trash[i]);
        }
    }

    public void startTimer() {
        timerStarted = true;
    }
}
