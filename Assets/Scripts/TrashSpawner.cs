using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashSpawner : MonoBehaviour {

    public GameObject Trash1;
    public GameObject Trash2;
    public GameObject Trash3;



    float timer = 2.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        timer -= Time.deltaTime;

        if(timer < 0) {
            SpawnTrash();
            timer = 2.0f;
        }
    }

    void SpawnTrash() {
        int trashOption = Random.Range(0, 3);

        switch(trashOption) {
            case 0:
                Instantiate(Trash1, transform.position, Quaternion.identity);
                Debug.Log("Trash 1");
                break;

            case 1:
                Instantiate(Trash2, transform.position, Quaternion.identity);
                Debug.Log("Trash 2");
                break;

            case 2:
                Instantiate(Trash3, transform.position, Quaternion.identity);
                Debug.Log("Trash 3");
                break;
        }
    }
}
