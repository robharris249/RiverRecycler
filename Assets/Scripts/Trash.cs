using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour {

    public GameObject target;
    public GameObject healthBar;
    public GameObject particles;
    public Vector3 particlesTarget;
    private Vector3 direction;
    private float speed = 0.3f;
    private float health;
    public bool slowed = false;
    private float slowedTimer = 0.0f;
    private bool eliminated = false;

    // Start is called before the first frame update
    void Start() {
        target = GameObject.Find("RiverStart");
        direction = target.transform.position - transform.position;//create direction to target
        direction.Normalize();                                     //normalise the directional vector
        health = 100;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if(Time.timeScale == 1) {
            if (slowed) {
                transform.position += direction * (speed / 2.5f); //move towards the next waypoint at half speed
                slowedTimer -= Time.deltaTime;
            } else {
                transform.position += direction * speed; //move towards the next waypoint at current speed
            }

            if (slowed && slowedTimer < 0) {//If has been slowed and timer is up
                slowed = false;             //trash is no longer slowed
            }

            if(particles.activeSelf) {
                //For rotating particles to move towards turret
                Vector3 vectorToTarget = particlesTarget - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                particles.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2000);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "WayPoint") {                                //if collided with a way point
            target = collision.GetComponent<Waypoint>().nextWaypoint;     //target the next waypoint
            direction = target.transform.position - transform.position;   //update the direction of travel
            direction.Normalize();                                        //normalise the directional vector
        } 
        else if (collision.tag == "RiverEnd") {                           //if collided with RiverEnd             
            FindObjectOfType<GameController>().TrashExitedRiver();     //increase trash passed
            Destroy(gameObject);                                          //delete this piece of trash
            FindObjectOfType<GameController>().CheckGameOver();           //Check if player has failed
        }
        else if (collision.tag == "Arrow") {                              //if collided with an arrow
            onHit(collision.gameObject.GetComponent<Arrow>().GetDamage());//call "onHit" func, with the arrow's damage passed in
            Destroy(collision.gameObject);                                //delete the arrow
        }
        else if(collision.tag == "AlgaeBomb") {
            slowed = true;
            slowedTimer = 3.0f;
            Destroy(collision.gameObject);
        }
    }

    public void onHit(float damage) {
        health -= damage;                               //reduce health by damage amount
        healthBar.transform.localScale = new Vector3(   //reduce the X scale of health bar equal to health amount
            0.02f * health,                             //0.02f magic number to put original scale to "2" and then health will put it as a percentage of that
            healthBar.transform.localScale.y,           //this stays the same
            healthBar.transform.localScale.z);          //and so does this

        if(health <= 100 && health > 75) {                                                       //if health is more than 75 & less than 101
            healthBar.GetComponent<SpriteRenderer>().color = new Color(0.0f, 1.0f, 0.0f, 1.0f);  //health bar colour is green
        } 
        else if(health <= 75 && health > 50) {                                                   //if health is more than 50 & less than 76
            healthBar.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.647f, 0.0f, 1.0f);//health bar colour is yellow
        }
        else if(health <= 50 && health > 25) {                                                   //if health is more than 25 & less than 51
            healthBar.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 0.0f, 1.0f);  //health bar colour is orange
        }
        else if (health <= 25 && health > 0) {                                                   //if health is more than 0 & less than 26
            healthBar.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);  //health bar colour is red
        }
        else if(health <= 0 && !eliminated){                               //if health is 0 or less AND trash has not already been eliminated
            eliminated = true;                                             //this prevents trash getting eliminated more than once
            FindObjectOfType<GameController>().IncreaseMoney(25);          //increase player's money
            FindObjectOfType<RiverController>().IncreaseTrashEliminated(); //increase level's eliminated trash                   
            Destroy(this.gameObject);                                      //delete this piece of trash
        }
    }

    public void activateParticles(Vector3 target) {
        particles.SetActive(true);
        particles.GetComponent<ParticleSystem>().Play();
        particlesTarget = target;
    }
}
