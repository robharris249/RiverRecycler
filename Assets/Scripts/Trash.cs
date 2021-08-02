using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    public GameObject target;
    Vector3 direction;

    // Start is called before the first frame update
    void Start() {
        target = GameObject.Find("WayPoint");
        direction = target.transform.position - transform.position;
        direction *= 0.5f;
    }

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(transform.position.x + direction.x * 0.002f, transform.position.y + direction.y * 0.002f, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "WayPoint") {
            target = collision.GetComponent<Waypoint>().nextWaypoint;
            direction = target.transform.position - transform.position;
        } else if (collision.tag == "RiverEnd") {
            Destroy(gameObject);
        }
        


    }
}
