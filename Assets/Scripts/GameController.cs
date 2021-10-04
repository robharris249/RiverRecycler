using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;

public class GameController : MonoBehaviour {

    public RiverController riverController;
    public FactoryController factoryController;

    public TrashSpawner trashSpawner;
    private int stage = 1;
    
    public GameObject contraptionsUIMenu;
    public GameObject statsUIMenu;
    public GameObject machinesUIMenu;
    public GameObject factoryUI;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject RiverlevelWonMenu;
    public GameObject FactorylevelWonMenu;

    //Game Over Menu Elements
    public Text txtGameOverPercentage;
    public Text txtGameOverMoney;
    public Text txtGameOverBuildings;

    //River Complete Menu Elements
    public Text txtRiverPercentage;
    public Text txtRiverMoney;
    public Text txtRiverBuildings;

    //Factoy Complete Menu Elements
    public Text txtFactoryPercentage;
    public Text txtFactoyMoney;

    // Start is called before the first frame update
    void Start() {
        Level1RiverSetup();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {//If player presses ESC
            if(pauseMenu.activeSelf) {         //and pause menu is active
                Time.timeScale = 1;            //set timescale to 1 (play)
                pauseMenu.SetActive(false);    //hide pause menu
            } 
            else {                        //if pause menu is not active
                Time.timeScale = 0;       //set timescale to 0 (paused)
                pauseMenu.SetActive(true);//show pause menu
            }
        }
    }

    public void Level1RiverSetup() {
        riverController.money = 200;
        riverController.trashPassed = 0;
        riverController.maxTrashPassed = 5;
        riverController.totalLevelTrash = 10;
        riverController.trashEliminated = 0;
        riverController.trashInRiver = 0;
        UpdateStats();
    }

    public void Level1FactorySetup() {
        switchLevels();
        factoryController.setUpFactory(new Vector2Int(1, 4), new Vector2Int(2, 6), new Vector2Int(3, 8));
        UpdateStats();
    }

    public void Level2RiverSetup() {
        switchLevels();
        riverController.trashPassed = 0;
        riverController.maxTrashPassed = 4;
        riverController.totalLevelTrash = 15;
        riverController.trashEliminated = 0;
        riverController.trashInRiver = 0;
        UpdateStats();
    }

    public void Level2FactorySetup() {
        factoryController.ClearFactory();
        switchLevels();
        factoryController.setUpFactory(new Vector2Int(0,5), new Vector2Int(6,5), new Vector2Int(6,7));
        UpdateStats();
    }

    public void Level3RiverSetup() {
        switchLevels();
        riverController.trashPassed = 0;
        riverController.maxTrashPassed = 3;
        riverController.totalLevelTrash = 20;
        riverController.trashEliminated = 0;
        riverController.trashInRiver = 0;
        UpdateStats();
    }

    public void Level3FactorySetup() {
        factoryController.ClearFactory();
        switchLevels();
        factoryController.setUpFactory(new Vector2Int(6,4), new Vector2Int(0,2), new Vector2Int(0,4));
        UpdateStats();
    }

    public void switchLevels() {
        if(riverController.gameObject.activeSelf) { //Swapping from River to Factory

            //River elements
            riverController.gameObject.SetActive(false);
            contraptionsUIMenu.SetActive(false);
            statsUIMenu.SetActive(false);
            riverController.clearRiverExcess();

            //Factory elements
            factoryController.gameObject.SetActive(true);
            factoryUI.SetActive(true);
            machinesUIMenu.SetActive(true);
            factoryController.bonusMoney = 0;
            factoryController.trashToBeSorted = riverController.trashEliminated;
            factoryController.trashCorrectlySorted = 0;
            factoryController.timerStarted = false;

            //Move Camera
            Camera.main.transform.position = new Vector3(-155, 40, -10);

            RiverlevelWonMenu.SetActive(false);
        } 
        else {//Swapping from Factory to River

            //River Elements
            riverController.gameObject.SetActive(true);
            contraptionsUIMenu.SetActive(true);
            statsUIMenu.SetActive(true);
            riverController.money += factoryController.bonusMoney;
            riverController.resetRiverPollution();
            riverController.startTimer();

            //Factory elements
            factoryController.gameObject.SetActive(false);
            factoryUI.SetActive(false);
            machinesUIMenu.SetActive(false);

            //Move Camera
            Camera.main.transform.position = new Vector3(66, 40, -10);

            FactorylevelWonMenu.SetActive(false);
        }
    }

    public void UpdateStats() {
        if(riverController.gameObject.activeSelf) {//Update River Stats
            riverController.txtMoney.text = riverController.money.ToString();
            riverController.txtTrashPassed.text = riverController.trashPassed.ToString();
            riverController.txtMaxTrashPassed.text = riverController.maxTrashPassed.ToString();
        } else {//Update Factory Stats
            factoryController.txtBonusMoney.text = factoryController.bonusMoney.ToString();
            factoryController.txtTrashToBeSorted.text = (factoryController.trashToBeSorted - (factoryController.trashCorrectlySorted +
                factoryController.trashIncorrectlySorted)).ToString();
            factoryController.txtTrashSorted.text = factoryController.trashCorrectlySorted.ToString();
        }
    }

    public void UpdateMenuStats() {
        txtGameOverPercentage.text = (((float)riverController.trashEliminated / riverController.totalLevelTrash) * 100).ToString() + "%";
        txtGameOverMoney.text = riverController.money.ToString();
        txtGameOverBuildings.text =
            (GameObject.FindGameObjectsWithTag("Grabbapult").Length +
            GameObject.FindGameObjectsWithTag("AlgaeChucker").Length).ToString();

        txtRiverPercentage.text = (((float)riverController.trashEliminated / riverController.totalLevelTrash) * 100).ToString() + "%";
        txtRiverMoney.text = riverController.money.ToString();
        txtRiverBuildings.text = (GameObject.FindGameObjectsWithTag("Grabbapult").Length +
            GameObject.FindGameObjectsWithTag("AlgaeChucker").Length).ToString();

        txtFactoryPercentage.text = (((float)factoryController.trashCorrectlySorted / factoryController.trashToBeSorted) * 100).ToString() + "%";
        txtFactoyMoney.text = factoryController.bonusMoney.ToString();
    }

    public void IncreaseMoney(int increase) {
        riverController.money += increase;
        UpdateStats();
    }

    public void DecreaseMoney(int decrease) {
        riverController.money -= decrease;
        UpdateStats();
    }

    public int GetMoney() {
        return riverController.money;
    }

    public void TrashExitedRiver() {
        riverController.trashPassed++;
        riverController.trashInRiver--;
        UpdateStats();
    }

    public int GetTrashPassed() {
        return riverController.trashPassed;
    }

    public int GetMaxTrashPassed() {
        return riverController.maxTrashPassed;
    }    

    public void ResumeButton() {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void RestartLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene("RiverScene");
    }

    public void MainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void CheckGameOver() {
        if(riverController.trashPassed > riverController.maxTrashPassed) {
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().Play("Game Over");
            gameOverMenu.SetActive(true);
            txtGameOverPercentage.text = (((float)riverController.trashEliminated / riverController.totalLevelTrash) * 100).ToString() + "%";
            txtGameOverMoney.text = riverController.money.ToString();
            txtGameOverBuildings.text = 
                (GameObject.FindGameObjectsWithTag("Grabbapult").Length + 
                GameObject.FindGameObjectsWithTag("AlgaeChucker").Length).ToString();
        }
    }

    public void CheckGameWon() {
        Time.timeScale = 0;

        UpdateMenuStats();
        if(riverController.gameObject.activeSelf) {
            RiverlevelWonMenu.SetActive(true);
        } else {
            FactorylevelWonMenu.SetActive(true);
        }
    }

    public void NextLevel() {
        Time.timeScale = 1;

        switch(stage) {
            case 1:
                stage++;
                Level1FactorySetup();
                break;

            case 2:
                stage++;
                Level2RiverSetup();
                break;

            case 3:
                stage++;
                Level2FactorySetup();
                break;

            case 4:
                stage++;
                Level3RiverSetup();
                break;

            case 5:
                stage++;
                Level3FactorySetup();
                break;

            case 6:
                SceneManager.LoadScene("GameWon");
                break;
        }
    }
}
