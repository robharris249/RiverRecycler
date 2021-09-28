using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : MonoBehaviour {

    private FactoryController factoryController;

    void Start() {
        factoryController = FindObjectOfType<FactoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (gameObject.name.Contains("MetalBin")) {
            if (collision.tag == "MetalTrash") {
                TrashCorrectlySorted(collision.gameObject);
            } else {
                TrashIncorrectlySorted(collision.gameObject);
            }
        } else if (gameObject.name.Contains("PlasticBin")) {
            if (collision.tag == "PlasticTrash") {
                TrashCorrectlySorted(collision.gameObject);
            } else {
                TrashIncorrectlySorted(collision.gameObject);
            }
        } else if (gameObject.name.Contains("PaperBin")) {
            if (collision.tag == "PaperTrash") {
                TrashCorrectlySorted(collision.gameObject);
            } else {
                TrashIncorrectlySorted(collision.gameObject);
            }
        }
    }

    private void TrashCorrectlySorted(GameObject trash) {
        UtilsClass.CreateWorldTextPopup("Good Job! +5 Eco Bucks!", transform.position + new Vector3(-15f,0,0), 20);
        factoryController.bonusMoney += 5;
        factoryController.trashCorrectlySorted++;
        factoryController.trashBeingProcessed--;
        FindObjectOfType<GameController>().UpdateStats();
        Destroy(trash);
        factoryController.checkLevelCleared();
    }

    private void TrashIncorrectlySorted(GameObject trash) {
        UtilsClass.CreateWorldTextPopup("Incorrect Trash!", transform.position, 20);
        factoryController.trashIncorrectlySorted++;
        factoryController.trashBeingProcessed--;
        FindObjectOfType<GameController>().UpdateStats();
        Destroy(trash);
        factoryController.checkLevelCleared();
    }

}
