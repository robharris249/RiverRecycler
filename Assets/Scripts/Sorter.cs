using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorter : MonoBehaviour {

    private Vector3 filteredOutput;
    private Vector3 unfilteredOutput;

    // Start is called before the first frame update
    void Start() {
        filteredOutput = gameObject.transform.GetChild(0).transform.position;
        unfilteredOutput = gameObject.transform.GetChild(1).transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(gameObject.name.Contains("MetalSorter")) {
            if (collision.tag == "MetalTrash") {
                collision.gameObject.GetComponent<FactoryTrash>().direct(filteredOutput);
            } else {
                collision.gameObject.GetComponent<FactoryTrash>().direct(unfilteredOutput);
            }
        }
        else if(gameObject.name.Contains("PlasticSorter")) {
            if (collision.tag == "PlasticTrash") {
                collision.gameObject.GetComponent<FactoryTrash>().direct(filteredOutput);
            } else {
                collision.gameObject.GetComponent<FactoryTrash>().direct(unfilteredOutput);
            }
        }
        else if(gameObject.name.Contains("PaperSorter")) {
            if (collision.tag == "PaperTrash") {
                collision.gameObject.GetComponent<FactoryTrash>().direct(filteredOutput);
            } else {
                collision.gameObject.GetComponent<FactoryTrash>().direct(unfilteredOutput);
            }
        }
    }
}
