using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgaeChucker : Tower {

    private int cost = 100;
    public GameObject ammoSprite;

    public int GetCost() {
        return cost;
    }

    // Start is called before the first frame update
    void Start() {
        cooldownTimer = 8.0f;
        timer.GetComponent<SpriteRenderer>().enabled = false;
        damage = 0;
        cooldown = 8.0f;
    }
}
