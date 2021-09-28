using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

    public GameObject Trash1;
    public GameObject Trash2;
    public GameObject Trash3;

    public void SpawnRiverTrash() {
        int trashOption = Random.Range(0, 3);//make a random choice out of 3

        switch (trashOption) {
            case 0:                                                          //if option 1
                Instantiate(Trash1, transform.position, Quaternion.identity);//Spawn trash 1 prefab
                break;
            case 1:                                                          //if option 2
                Instantiate(Trash2, transform.position, Quaternion.identity);//Spawn trash 2 prefab
                break;
            case 2:                                                          //if option 3 
                Instantiate(Trash3, transform.position, Quaternion.identity);//Spawn trash 3 prefab
                break;
        }
    }

    public void SpawnFactoryTrash() {
        int trashOption = Random.Range(0, 3);//make a random choice out of 3
        GameObject g;

        switch(trashOption) {
            case 0:                                                          //if option 1
                g = Instantiate(Trash1, transform.position, Quaternion.identity);//Spawn trash 1 prefab
                g.GetComponent<FactoryTrash>().direct(new Vector3(-140f, 8f, 0f));
                break;
            case 1:                                                          //if option 2
                g = Instantiate (Trash2, transform.position, Quaternion.identity);//Spawn trash 2 prefab
                g.GetComponent<FactoryTrash>().direct(new Vector3(-140f, 8f, 0f));
                break;
            case 2:                                                          //if option 3 
                g =Instantiate(Trash3, transform.position, Quaternion.identity);//Spawn trash 3 prefab
                g.GetComponent<FactoryTrash>().direct(new Vector3(-140f, 8f, 0f));
                break;
        }
    }
}
