using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    const float INTERACTION_AREA = 1;
    const float DAMAGE_AREA = 0.5f;

    static public bool attacking;
    float attackTimeDelay;
    float _health;
    StarterAssetsInputs input;
    Animator animator;
    GameObject weapon;

    Slider healthBar;
    Text healthBarText;

    public float health
    {
        get { return _health; }
        set { 
            _health = value;
            healthBar.value = value;
            healthBarText.text = "Health: " + value + "%";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();

        healthBar = GameObject.Find("Canvas/HealthBar").GetComponent<Slider>();
        healthBarText = GameObject.Find("Canvas/HealthBar/Fill Area/Text").GetComponent<Text>();
        weapon = GameObject.Find("Broom/Box13");
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (input.interact) 
        {
            checkNpc();
            input.interact = false;
        }

        attack();
    }

    void checkNpc() 
    {
        bool searching = true;
        Vector3 pos = new Vector3(transform.position.x, 0, transform.position.z);
        Collider[] entities = Physics.OverlapSphere(pos, INTERACTION_AREA);

        for (int i = 0; i < entities.Length && searching; i++)
        {
            if (entities[i].TryGetComponent(out Npc npc))
            {
                npc.talk();
                searching = false;
            }
        }
    }

    public void attack()
    {
        if (input.attack && !attacking) 
        {
            print("atacou");
            input.attack = false;
            attacking = true;
            animator.SetTrigger("Attack");
            StartCoroutine(findContact());
        }
    }

    public IEnumerator findContact()
    {
        bool contact = false;

        while (!contact && attacking)
        {
            yield return new WaitForFixedUpdate();

            Collider[] hitColliders = Physics.OverlapSphere(weapon.transform.position, DAMAGE_AREA);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    // MELHORAR O MOMENTO EM QUE ISTO ACONTECE COM O CURVE
                    hitCollider.GetComponent<Enemy>().takeDamage(20);
                    contact = true;
                }
                
            }
        }
    }

    public void endAttack()
    {
        attacking = false;
    }

    public void takeDamage(int damage)
    {
        attackTimeDelay += Time.deltaTime;

        if (attackTimeDelay > 3f)
        {
            health -= damage;
            attackTimeDelay = 0;
        }
        
    }


    public GameObject tempBroom;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(tempBroom.transform.position, DAMAGE_AREA);
    }
}
