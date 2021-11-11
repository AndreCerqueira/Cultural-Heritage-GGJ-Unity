using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] 
    Transform[] waypoints;
    [SerializeField]
    float waitingTime;
    NavMeshAgent agent;
    GameObject player;
    int index;
    float dist = 5;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        changeDestination();
        StartCoroutine(patrol());
    }

    // Update is called once per frame
    void Update()
    {
        chase();
    }

    IEnumerator patrol() 
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            if (Vector3.Distance(transform.position, agent.destination) < 3f)
            { 
                yield return new WaitForSeconds(1f);
                changeDestination();
            }
        }
    }

    void changeDestination() 
    {
        index = (index == waypoints.Length - 1) ? 0 : index + 1;
        agent.destination = waypoints[index].position;
    }

    void chase()
    {
        
        if (Vector3.Distance(transform.position, player.transform.position) < dist)
            agent.destination = player.transform.position;

    }

}
