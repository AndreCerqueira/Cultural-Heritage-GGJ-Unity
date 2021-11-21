using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    const float CHASE_DIST = 5;
    const float ATTACK_DIST = 2;

    [SerializeField] 
    Transform[] waypoints;
    [SerializeField]
    float waitingTime;
    NavMeshAgent agent;
    GameObject player;
    Animator animator;
    Slider healthBar;
    int index;

    float _health;
    public float health
    {
        get { return _health; }
        set
        {
            _health = value;
            healthBar.value = value;

            if (_health <= 0)
            {
                death();
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        healthBar = GetComponentInChildren<Slider>();
        health = 100;
        player = GameObject.Find("Player");
        changeDestination();
        StartCoroutine(patrol());
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Andar", agent.velocity.magnitude);

        chase();

        attack();
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
        if (Vector3.Distance(transform.position, player.transform.position) < CHASE_DIST)
            agent.destination = player.transform.position;
    }

    void attack()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= ATTACK_DIST) 
        {
            Player _player = player.GetComponent<Player>();
            animator.SetTrigger("Attack");

            if (_player.health > 0)
                Invoke("dealDamage", 0.1f);
        }
    }

    void dealDamage()
    {
        player.GetComponent<Player>().takeDamage(10);
    }

    void death()
    {
        Destroy(gameObject);
    }

    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
