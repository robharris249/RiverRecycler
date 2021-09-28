using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Ammo {

    // Update is called once per frame
    void FixedUpdate() {
        if(Time.timeScale == 1) {
            if (target != null) {
                //Move arrow to target
                direction = target.transform.position - transform.position;
                direction.Normalize();
                transform.position += direction;

                //Rotate arrow in direction of travel
                Vector3 vectorToTarget = target.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2000);
                transform.Rotate(0, 0, 180, Space.Self);
            } else {
                Destroy(gameObject);
            }
        }
    }
}
