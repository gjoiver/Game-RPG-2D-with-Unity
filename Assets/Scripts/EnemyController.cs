using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterStats))]
[RequireComponent(typeof(HealthManager))]
public class EnemyController : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento del enemigo")]
    public float speed = 1;
    private Rigidbody2D enemyRigidbody;

    private bool isMoving;

    [Tooltip("Tiempo que tarda un enemigo entre pasos sucesivos")]
    public float timeBetweenSteps;
    private float timeBetweenStepsCounter;

    [Tooltip("Tiempo que tarda un enemigo en dar un paso")]
    public float timeToMakeStep;
    private float timeToMakeStepCounter;

    public const string AXIS_H = "Horizontal", AXIS_V = "Vertical",LAST_H="LastH",LAST_V="LastV",MOVING="isMoving";

    public Vector2 directionToMove;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        timeBetweenStepsCounter = timeBetweenSteps*Random.Range(0.5f,1.5f);
        timeToMakeStepCounter = timeToMakeStep*Random.Range(0.5f,1.5f);

    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            timeToMakeStepCounter -= Time.deltaTime;
            enemyRigidbody.velocity = directionToMove * speed;
            //Cuando me quedo sin tiempo de movimiento
            //Paro al enemigo
            if (timeToMakeStepCounter < 0)
            {
                isMoving = false;
                timeBetweenStepsCounter = timeBetweenSteps;
                enemyRigidbody.velocity = Vector2.zero;
            }
        }
        else
        {
            timeBetweenStepsCounter -= Time.deltaTime;
            //Cuando me quedo sin tiempo de estar parado
            //arrancar al enemigo para que de un paso
            if (timeBetweenStepsCounter < 0)
            {
                isMoving = true;
                timeToMakeStepCounter = timeToMakeStep;
                directionToMove = new Vector2(Random.Range(-1,2),Random.Range(-1,2));
            }
        }
    }
    private void LateUpdate()
    {
        _animator.SetFloat(AXIS_H, directionToMove.x);
        _animator.SetFloat(AXIS_V, directionToMove.y);
        _animator.SetBool(MOVING, isMoving);
        _animator.SetFloat(LAST_H, directionToMove.x);
        _animator.SetFloat(LAST_V, directionToMove.y);
    }
}
