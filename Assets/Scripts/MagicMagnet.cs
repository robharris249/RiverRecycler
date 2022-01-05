using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMagnet : MonoBehaviour {

    protected float cooldown;
    protected float cooldownTimer;
    protected bool onCoolDown = false;
    public GameObject particlesPrefab;
    public GameObject particles;
    protected GameObject target;
    public GameObject timer;
    public GameObject top;
    public Sprite section1;
    public Sprite section2;
    public Sprite section3;
    public Sprite section4;
    public Sprite section5;
    public Sprite section6;
    public Sprite section7;
    public Sprite section8;
    protected float damage;
    private int cost = 200;

    public int GetCost() {
        return cost;
    }

    // Start is called before the first frame update
    void Start() {
        cooldownTimer = 1.0f;
        timer.GetComponent<SpriteRenderer>().enabled = false;
        damage = 10;
        cooldown = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Time.timeScale == 1) {
            if (onCoolDown) {
                cooldownTimer -= Time.deltaTime;

                float timerPercentage = (cooldownTimer / cooldown) * 100;

                if (timerPercentage <= 100 && timerPercentage > 87.5) {
                    timer.GetComponent<SpriteRenderer>().sprite = section1;
                } else if (timerPercentage <= 87.5 && timerPercentage > 75) {
                    timer.GetComponent<SpriteRenderer>().sprite = section2;
                } else if (timerPercentage <= 75 && timerPercentage > 62.5) {
                    timer.GetComponent<SpriteRenderer>().sprite = section3;
                } else if (timerPercentage <= 62.5 && timerPercentage > 50) {
                    timer.GetComponent<SpriteRenderer>().sprite = section4;
                } else if (timerPercentage <= 50 && timerPercentage > 37.5) {
                    timer.GetComponent<SpriteRenderer>().sprite = section5;
                } else if (timerPercentage <= 37.5 && timerPercentage > 25) {
                    timer.GetComponent<SpriteRenderer>().sprite = section6;
                } else if (timerPercentage <= 25 && timerPercentage > 12.5) {
                    timer.GetComponent<SpriteRenderer>().sprite = section7;
                } else if (timerPercentage <= 12.5 && timerPercentage > 0) {
                    timer.GetComponent<SpriteRenderer>().sprite = section8;
                } else if (timerPercentage <= 0) {
                    onCoolDown = false;
                    cooldownTimer = cooldown;
                    timer.GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            if (target != null) {
                //For rotating Turret to face target
                Vector3 vectorToTarget = target.transform.position - transform.position;
                float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                top.transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 2000);
                top.transform.Rotate(0, 0, 180, Space.Self);
            }
        }
    }


    protected void OnTriggerStay2D(Collider2D collision) {
        if (collision.tag == "Trash") {
            if (target == null) {               //if no target
                target = collision.gameObject; //chose new target
                collision.gameObject.GetComponent<Trash>().activateParticles(transform.position);
                particles = Instantiate(particlesPrefab, collision.gameObject.transform.position, Quaternion.identity);
                particles.transform.SetParent(collision.gameObject.transform);
                particles.GetComponent<Particles>().particlesTarget = transform.position;
                particles.transform.localScale = new Vector3(2.5f, 2.5f, 1.0f);
            }
            if (!onCoolDown) {
                collision.gameObject.GetComponent<Trash>().onHit(damage);
                timer.GetComponent<SpriteRenderer>().enabled = true;
                onCoolDown = true;
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject == target) { //if the target goes out of range
            //collision.gameObject.GetComponent<Trash>().particles.GetComponent<ParticleSystem>().Stop();//stop emitting particles
            Destroy(particles);
            target = null;                   //remove target
        }
    }
}