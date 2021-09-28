using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryTrash : MonoBehaviour {

    //private Vector3 target;
    public Vector3 direction;
    public Vector3 target;
    private float speed = 0.2f;

    // Update is called once per frame
    void FixedUpdate() {
        if(Time.timeScale == 1) {
            transform.position += direction * speed;
        }

        Vector3 testDirection = target - transform.position;
        testDirection.Normalize();

        if(Vector3.Dot(direction, testDirection) < 0) {
            Destroy(gameObject);
            FindObjectOfType<FactoryController>().trashIncorrectlySorted++;
            FindObjectOfType<GameController>().UpdateStats();
            FindObjectOfType<FactoryController>().checkLevelCleared();
            UtilsClass.CreateWorldTextPopup("Trash not sorted!", transform.position, 20);
        }

    }

    public void direct(Vector3 newTarget) {
        target = newTarget;
        direction = target - transform.position;
        direction.Normalize();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name.Contains("Conveyor")) {
            target = collision.gameObject.transform.GetChild(0).transform.position;
            direction = target - transform.position;
            direction.Normalize();
        }
    }
}
