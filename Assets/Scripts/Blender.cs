using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour {

    public GameObject metal1;
    public GameObject metal2;
    public GameObject plastic1;
    public GameObject plastic2;
    public GameObject paper1;
    public GameObject paper2;

    private Vector3 output;

    // Start is called before the first frame update
    void Start() {
        output = gameObject.transform.GetChild(0).transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Trash") {
            int trashOption = Random.Range(0, 6);
            GameObject g;
            Destroy(collision.gameObject);

            switch (trashOption) {

                case 0:
                    g = Instantiate(metal1, transform.position, Quaternion.identity);
                    g.GetComponent<FactoryTrash>().direct(output);
                    break;

                case 1:
                    g = Instantiate(metal2, transform.position, Quaternion.identity);
                    g.GetComponent<FactoryTrash>().direct(output);
                    break;

                case 2:
                    g = Instantiate(plastic1, transform.position, Quaternion.identity);
                    g.GetComponent<FactoryTrash>().direct(output);
                    break;

                case 3:
                    g = Instantiate(plastic2, transform.position, Quaternion.identity);
                    g.GetComponent<FactoryTrash>().direct(output);
                    break;

                case 4:
                    g = Instantiate(paper1, transform.position, Quaternion.identity);
                    g.GetComponent<FactoryTrash>().direct(output);
                    break;

                case 5:
                    g = Instantiate(paper2, transform.position, Quaternion.identity);
                    g.GetComponent<FactoryTrash>().direct(output);
                    break;
            }
        }
    }
}
