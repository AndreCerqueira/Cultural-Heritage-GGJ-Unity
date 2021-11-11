using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{
    const float INTERACTION_AREA = 1;

    StarterAssetsInputs input;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (input.interact) 
        {
            checkNpc();
            input.interact = false;
        }
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, INTERACTION_AREA);
    }
}
