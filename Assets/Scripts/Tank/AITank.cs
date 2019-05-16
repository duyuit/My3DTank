using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITank : MonoBehaviour
{
    private NavMeshAgent agent;
    private GameObject[] listDestinationAI;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        listDestinationAI = GameObject.FindGameObjectsWithTag("DestinationAI");

        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = Color.red;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(listDestinationAI.Length == 0)
        {
            listDestinationAI = GameObject.FindGameObjectsWithTag("DestinationAI");
        }else
        {
            if(!agent.pathPending && agent.remainingDistance < agent.stoppingDistance)
            {
                agent.SetDestination(listDestinationAI[Random.Range(0, listDestinationAI.Length)].transform.position);
            }
        }
    }
}
