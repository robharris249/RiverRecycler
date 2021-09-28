using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbapult : Tower {

    private int cost = 50;
    

    public int GetCost() {
        return cost;
    }

    // Start is called before the first frame update
    void Start() {
        cooldownTimer = 4.0f;
        timer.GetComponent<SpriteRenderer>().enabled = false;
        damage = 20;
        cooldown = 4.0f;
    }
}
