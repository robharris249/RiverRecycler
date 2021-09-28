using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour {

    public GameObject target;
    protected Vector3 direction;
    protected float damage;

    public void SetTarget(GameObject target) {
        this.target = target;
    }

    public void SetDamage(float damage) {
        this.damage = damage;
    }

    public float GetDamage() {
        return damage;
    }
}
