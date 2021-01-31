using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyType
    {
        Static,
        Patroller,
        Shooter
    }

    public EnemyType myEnemyType;
    SpriteRenderer mySpriteRenderer;
    [Header("Patroller Settings")]
    public List<Transform> PatrolPoints;
    public Transform targetPosition;
    public float speed;
    public float baseWaitingTime;
    float timerBetweenPatrolling;
    [Header("Shooter Settings")]
    public float firingRate;
    float timeBeforeShoot;
    public float projectileSpeed;
    public GameObject prefabBullet;
    public Transform shootingPivot;
    public Vector2 shootDirection;
    // Start is called before the first frame update
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        switch (myEnemyType)
        {
            case EnemyType.Static:
                
                break;
            case EnemyType.Patroller:
                targetPosition = PatrolPoints[0];
                timerBetweenPatrolling = baseWaitingTime;
                break;
            case EnemyType.Shooter:
                timeBeforeShoot = firingRate;
                break;
        }

       

    }

    void StaticBehaviour()
    {
        //Just play the idle animation and make the enemy able to detech wwhen a  player is nearby with a physics 2d cast

    }

    void PatrollerBehaviour()
    {

        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, step);
        if (transform.position.x == targetPosition.position.x)
        {
            timerBetweenPatrolling -= Time.deltaTime;
            if (timerBetweenPatrolling <= 0)
            {
                FlipAsset();
                if (targetPosition == PatrolPoints[0])
                {
                    targetPosition = PatrolPoints[1];
                }
                else
                {
                    targetPosition = PatrolPoints[0];
                }
                timerBetweenPatrolling = baseWaitingTime;
            }
        }
    }

    void ShotterBehaviour()
    {
        timeBeforeShoot -= Time.deltaTime;
        if(timeBeforeShoot <= 0)
        {
            GameObject buller = GameObject.Instantiate(prefabBullet,shootingPivot.position,Quaternion.identity);
            buller.GetComponent<Bullet>().StartMoving(shootDirection, projectileSpeed);
            timeBeforeShoot = firingRate;
        }
    }


    void FlipAsset()
    {
        if(mySpriteRenderer.flipX)
        {
            mySpriteRenderer.flipX = false;
        }
        else
        {
            mySpriteRenderer.flipX = true;
        }
       
    }


    // Update is called once per frame
    void Update()
    {
        switch(myEnemyType)
        {
            case EnemyType.Static:
                StaticBehaviour();
                break;
            case EnemyType.Patroller:
                PatrollerBehaviour();
                break;
            case EnemyType.Shooter:
                ShotterBehaviour();
                break;
        }
    }
}
