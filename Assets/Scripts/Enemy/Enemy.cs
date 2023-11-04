using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{


    [Range(0, 360)] public float viewAngle = 180f;
    public GameObject player;
    public Transform view;
    public NavMeshAgent agent;
    float viewDistance = 100f;
    public float detectionDistance = 3f;
    public float distanceToPlayer;
    float speed = 3.5f;
    float _realAngle;

    public bool isTakeDamage;

    public List<Transform> pointsPatrol = new();
    public List<Transform> pointsBack = new();
    public float health = 100f;


    private void Start()
    {
        //    Transform pointObject = GameObject.FindGameObjectWithTag("PatrolPoints").transform;
        //    foreach (Transform t in pointObject)
        //        pointsPatrol.Add(t);

        //    Transform pointObject1 = GameObject.FindGameObjectWithTag("Burrow").transform;
        //    foreach (Transform t in pointObject1)
        //        pointsBack.Add(t);
        agent = GetComponent<NavMeshAgent>();
        GetComponent<NavMeshAgent>().speed = speed;
    }

    private void Update()
    { 
        distanceToPlayer = Vector3.Distance(player.transform.position, agent.transform.position);
        _realAngle = Vector3.Angle(view.forward, player.transform.position - view.position);
    }


    public bool IsInView() 
    {
        RaycastHit hit;
        if (_realAngle <= viewAngle / 2f && distanceToPlayer <= viewDistance)
        {
            if (Physics.Raycast(view.position, player.transform.position - view.position, out hit))
            {
                if (hit.collider.gameObject.layer == 11)
                {
                    return true;
                }
            }
        }
            
        return false;
    }
    
    public void Follow()
    {
        speed = 6f;
        GetComponent<NavMeshAgent>().speed = speed;
        agent.SetDestination(player.transform.position);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        isTakeDamage = true;
    }

    public void Back()
    {
        agent.SetDestination(pointsBack[Random.Range(0, pointsBack.Count)].position);
    }

    public void Patrol()
    {
        speed = 3.5f;
        GetComponent<NavMeshAgent>().speed = speed;
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(pointsPatrol[Random.Range(0, pointsPatrol.Count)].position);
    }

    public IEnumerator Damage()
    {
        yield return new WaitForSeconds(2);
        isTakeDamage = false;
    }

    public IEnumerator Appear()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        transform.position = new Vector3(-100, -100, -100);
        yield return new WaitForSeconds(Random.Range(10, 20));
        transform.position = pointsBack[Random.Range(0, pointsBack.Count)].position;
        GetComponent<NavMeshAgent>().enabled = true;
        health = 100;
    }


    public void SetHealth(float helth)
    {
        health = helth;
    }

    public float GetHealth()
    {
        return health;
    }

    public void SetDetectionDistance(float detectionDistance)
    {
        this.detectionDistance = detectionDistance;
    }

    public void SetSpeed(float speed)
    {
        agent.speed = speed;    
    }
}
