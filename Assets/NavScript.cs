using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Transform[] PatrolPoints = new Transform[4];
    private string state = "Patrol";

    private float searchTimer = 0;
    private float wanderTimer = 0;
    private int currentPatrolPoint = 0;

    public float wanderRadius = 2f;

    public float timer = 4;
    private Vector3 newPos;
    
    private int flow = 1;

    // Start is called before the first frame update
    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 2;
        for (int i = 0; i <= PatrolPoints.Length; i++)
        {
            Debug.Log("initializing patrol points" + i);

            PatrolPoints[i] = GameObject.Find("PatrolPoints" + (i+1)).transform;
        }
    }

    // Update is called once per frame
    void Update() {
        Debug.Log(searchTimer);

        switch (state)
        {
            case "Patrol":
                Patrol();
                break;
            case "Chase":
                Chase();
                break;
            case "Search":
                searchTimer -= Time.deltaTime;
                
                if (searchTimer <= 0)
                {
                    state = "Patrol";
                    agent.speed = 4;
                }
                Search();
                break;
        }
        
    }
    void Patrol() {
        if (Vector3.Distance(transform.position, PatrolPoints[currentPatrolPoint].position) < 2)
        {
            state = "Search";
            searchTimer = Random.Range(8, 15);
            Debug.Log("Search Timer "+searchTimer);
            currentPatrolPoint+=flow;
            if (currentPatrolPoint >= PatrolPoints.Length)
            {
                flow = -1;
                currentPatrolPoint -= 1;
            }
            else if (currentPatrolPoint <= 0)
            {
                flow = 1;
            }
        }
        Debug.Log(currentPatrolPoint);
        Debug.Log(state);

        agent.destination = PatrolPoints[currentPatrolPoint].position;
    }
    void Chase() {
        agent.destination = player.position;
    }

    void Search() {
        timer += Time.deltaTime;
        if (timer >= searchTimer && searchTimer > 3)
        {
            searchTimer = Random.Range(3, 6);
            timer = 0;
            Invoke("RandomNavSphere",(float)Random.Range(0,1));
            newPos = transform.position;
            
        }
        agent.SetDestination(newPos);


    }

    void RandomNavSphere() {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += PatrolPoints[currentPatrolPoint].position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, wanderRadius, -1);
        newPos = navHit.position;
    }
}
