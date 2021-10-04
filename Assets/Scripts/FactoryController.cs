using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FactoryController : MonoBehaviour {

    public GridBuildingSystem factoryGridBuildingSystem;
    private float buildingRotation = 0;

    private float spawnerTimer = 1.0f;
    public TrashSpawner trashSpawner;
    public GameObject metalBin;
    public GameObject plasticBin;
    public GameObject paperBin;

    public int bonusMoney;
    public int trashToBeSorted;
    public int trashCorrectlySorted;
    public int trashIncorrectlySorted;
    public int trashBeingProcessed;

    public Text txtBonusMoney;
    public Text txtTrashToBeSorted;
    public Text txtTrashSorted;

    public GameObject conveyorGhost;
    public GameObject blenderGhost;
    public GameObject metalSorterGhost;
    public GameObject plasticSorterGhost;
    public GameObject paperSorterGhost;
    public GameObject buildingGhost;

    public bool timerStarted = false;

    // Update is called once per frame
    void Update()  {
        if (Time.timeScale == 1) {

            if(timerStarted) {
                spawnerTimer -= Time.deltaTime;
            }

            if(spawnerTimer < 0) {
                if(trashToBeSorted - (trashCorrectlySorted + trashIncorrectlySorted + trashBeingProcessed) > 0) {
                    trashSpawner.SpawnFactoryTrash();
                    trashBeingProcessed++;
                } else {
                    timerStarted = false;
                }
                spawnerTimer = 1.0f;
            }

            if (factoryGridBuildingSystem.GetSelectedBuilding() != null) {                 //if there is a building selected
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    //get world position from mouse position
                buildingGhost.transform.position = new Vector3(mousePos.x, mousePos.y, 0f);//update buildingGhost position to mouseposition

                if (Input.GetKeyDown(KeyCode.R)) {//If R key is pressed
                    buildingRotation -= 90;       //Rotate Building 90 degrees
                    if (buildingRotation == 360) {//if rotated a full circle
                        buildingRotation = 0;     //clamp back down to 0
                    }
                    buildingGhost.transform.rotation = Quaternion.Euler(0, 0, buildingRotation);//rotate buildingGhost accoridngly
                }
            }

            if (Input.GetMouseButtonDown(0)) {                                //If left clicked
                if (factoryGridBuildingSystem.GetSelectedBuilding() != null) {//and if a building is selected
                    if (factoryGridBuildingSystem.Build(buildingRotation)) {  //build the building, if build was successful
                        FindObjectOfType<GameController>().UpdateStats();     //update player stats
                        FindObjectOfType<AudioManager>().Play("Click");       //play build sound
                    } 
                    else {                                                    //if player can't build
                        FindObjectOfType<AudioManager>().Play("Cant Build");  //play build error sound
                    }
                }
            } 
            else if (Input.GetMouseButtonDown(1)) {            //If right clicked
                factoryGridBuildingSystem.DeselectObjectType();//Deselect building
                toggleFactoryGridDisplay(false);               //hide grid
                Destroy(buildingGhost);                        //Remove buildingGhost
                buildingRotation = 0;                          //Reset Building Rotation back down to 0;
            }
        }
    }

    public void toggleFactoryGridDisplay(bool state) {
        factoryGridBuildingSystem.transform.GetChild(0).gameObject.SetActive(state);//toggle horizontal lines
        factoryGridBuildingSystem.transform.GetChild(1).gameObject.SetActive(state);//toggle horizontal lines
    }

    public void ConveyorButton() {
        factoryGridBuildingSystem.SetSelectedBuilding(0);
        buildingGhost = Instantiate(conveyorGhost, new Vector3(0f, 0f, 0f), Quaternion.identity);
        toggleFactoryGridDisplay(true);
    }

    public void BlenderButton() {
        factoryGridBuildingSystem.SetSelectedBuilding(1);
        buildingGhost = Instantiate(blenderGhost, new Vector3(0f, 0f, 0f), Quaternion.identity);
        toggleFactoryGridDisplay(true);
    }

    public void MetalSorterButton() {
        factoryGridBuildingSystem.SetSelectedBuilding(2);
        buildingGhost = Instantiate(metalSorterGhost, new Vector3(0f, 0f, 0f), Quaternion.identity);
        toggleFactoryGridDisplay(true);
    }

    public void PlasticSorterButton() {
        factoryGridBuildingSystem.SetSelectedBuilding(3);
        buildingGhost = Instantiate(plasticSorterGhost, new Vector3(0f, 0f, 0f), Quaternion.identity);
        toggleFactoryGridDisplay(true);
    }

    public void PaperSorterButton() {
        factoryGridBuildingSystem.SetSelectedBuilding(4);
        buildingGhost = Instantiate(paperSorterGhost, new Vector3(0f, 0f, 0f), Quaternion.identity);
        toggleFactoryGridDisplay(true);
    }

    public void setUpFactory(Vector2Int metalBinPos, Vector2Int plasticBinPos, Vector2Int paperBinPos) {
        Vector2 position;
        Vector2 offset = new Vector2(factoryGridBuildingSystem.cellSize / 2, factoryGridBuildingSystem.cellSize / 2);

        //Place Metal Bin
        position = factoryGridBuildingSystem.grid.GetWorldPosition(metalBinPos.x, metalBinPos.y);
        position += offset;
        Instantiate(metalBin, position, Quaternion.identity);
        factoryGridBuildingSystem.grid.GetGridObject(metalBinPos.x, metalBinPos.y).occupied = true;

        //Place Plastic Bin
        position = factoryGridBuildingSystem.grid.GetWorldPosition(plasticBinPos.x, plasticBinPos.y);
        position += offset;
        Instantiate(plasticBin, position, Quaternion.identity);
        factoryGridBuildingSystem.grid.GetGridObject(plasticBinPos.x, plasticBinPos.y).occupied = true;

        //Place Paper Bin
        position = factoryGridBuildingSystem.grid.GetWorldPosition(paperBinPos.x, paperBinPos.y);
        position += offset;
        Instantiate(paperBin, position, Quaternion.identity);
        factoryGridBuildingSystem.grid.GetGridObject(paperBinPos.x, paperBinPos.y).occupied = true;
    }

    public void ClearFactory() {

        GameObject[] machines = GameObject.FindGameObjectsWithTag("FactoryMachine");
        for (int i = 0; i < machines.Length; i++) {
            Destroy(machines[i]);
        }

        GameObject[] trash = GameObject.FindGameObjectsWithTag("Trash");
        for (int i = 0; i < trash.Length; i++) {
            Destroy(trash[i]);
        }

        for (int x = 0; x < factoryGridBuildingSystem.gridWidth; x++) {
            for(int y = 0; y < factoryGridBuildingSystem.gridHeight; y++) {
                factoryGridBuildingSystem.grid.GetGridObject(x, y).occupied = false;
            }
        }

        //reset Variables
        trashCorrectlySorted = 0;
        trashIncorrectlySorted = 0;
        trashBeingProcessed = 0;
        bonusMoney = 0;
    }

    public void StartTrash() {
        timerStarted = true;
    }

    public void checkLevelCleared() {
        if(trashCorrectlySorted + trashIncorrectlySorted == trashToBeSorted) {
            FindObjectOfType<GameController>().CheckGameWon();
        } 
    }
}
