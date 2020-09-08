using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static bool playerCreated;

    public bool canMove=true;

    public bool isTalking;

    public float speed = 5.0f;
    private bool walking = false;
    private bool attacking = false;

    public Vector2 lastMovement = Vector2.zero;

    public const string AXIS_H = "Horizontal", AXIS_V = "Vertical",ATT="Attacking",LAST_H="LastH",LAST_V="LastV",WALK="Walking";

    private Animator _animator;
    private Rigidbody2D playerRigidbody;
    public string nextUuid;

    public float attackTime;
    public float attackTimeCounter;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCreated = true;
        isTalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTalking)
        {
            playerRigidbody.velocity = Vector2.zero;
            return;
        }
        this.walking = false;

        if (!canMove)
        {
            return;
        }
        if (attacking)
        {
            attackTimeCounter -= Time.deltaTime;
            if (attackTimeCounter < 0)
            {
                attacking = false;
                _animator.SetBool(ATT, attacking);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            attacking = true;
            attackTimeCounter = attackTime;
            playerRigidbody.velocity = Vector2.zero;
            _animator.SetBool(ATT, attacking);
        }
        if (Mathf.Abs(Input.GetAxisRaw(AXIS_H)) > 0.2f)
        {
            //Vector3 translation = new Vector3(Input.GetAxisRaw(AXIS_H) * speed * Time.deltaTime, 0, 0);
            //this.transform.Translate(translation);

            playerRigidbody.velocity = new Vector2(Input.GetAxisRaw(AXIS_H), playerRigidbody.velocity.y).normalized*speed;
            walking = true;
            lastMovement = new Vector2(Input.GetAxisRaw(AXIS_H), 0);
        }
        if (Mathf.Abs(Input.GetAxisRaw(AXIS_V)) > 0.2f)
        {
            //Vector3 translation = new Vector3(0, Input.GetAxisRaw(AXIS_V) * speed * Time.deltaTime, 0);
            //this.transform.Translate(translation);
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, Input.GetAxisRaw(AXIS_V)).normalized*speed;
            walking = true;
            lastMovement = new Vector2(0, Input.GetAxisRaw(AXIS_V));
        }

    }
    private void LateUpdate()
    {
        if (!walking)
        {
            playerRigidbody.velocity = Vector2.zero;
        }
        _animator.SetFloat(AXIS_H, Input.GetAxisRaw(AXIS_H));
        _animator.SetFloat(AXIS_V, Input.GetAxisRaw(AXIS_V));
        _animator.SetBool(WALK, walking);
        _animator.SetFloat(LAST_H, lastMovement.x);
        _animator.SetFloat(LAST_V, lastMovement.y);
    }

}
