using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeBomb : Ammo {

    // Update is called once per frame
    void FixedUpdate() {
        if(Time.timeScale == 1) {
            if (target != null) {
                //Move bomb to target
                direction = target.transform.position - transform.position;
                direction.Normalize();
                transform.position += (direction * 0.8f);

                //Rotate bomb in circles
                transform.Rotate(0, 0, 8, Space.Self);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
