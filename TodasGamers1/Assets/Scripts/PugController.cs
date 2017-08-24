using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Rigidbody2D))]
public class PugController : MonoBehaviour {

    public float speed;
    public float jumpSpeed;
    

    private Animator animator;
    private Rigidbody2D rigi;
    private SpriteRenderer srPUG;
    private bool canJump;
    private Score score;

	void Start () {
        animator = GetComponent<Animator>();
        rigi = GetComponent<Rigidbody2D>();
        srPUG = GetComponent<SpriteRenderer>();
        score = GetComponent<Score>();
	}
	

	void Update () {

        if (Input.GetKey(KeyCode.D)) {
            transform.Translate(speed * Time.deltaTime, 0, 0);
            if (srPUG.flipX == true) {
                srPUG.flipX = false;
            }
            StartAnimation("IsMoving");
        }

        //Siempre que no se presione algo para las animaciones
        if (Input.anyKey == false) {
            StopAnimation("IsMoving");
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
            if (srPUG.flipX == false) {
                srPUG.flipX = true;
            }
            StartAnimation("IsMoving");
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            rigi.AddForce(Vector3.up * jumpSpeed);
            canJump = false;
            StartAnimation("IsJumping");

        }

    }


    protected void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.CompareTag("Walkable")) {
            canJump = true;
            StopAnimation("IsJumping");
        }
        if (collision.gameObject.CompareTag("Spike")) {
            score.Lose();
        }
    }


    protected void StartAnimation(string animationType) {
        animator.SetBool(animationType, true);
    }

    protected void StopAnimation(string animationType) {
        animator.SetBool(animationType, false);
    }
}
