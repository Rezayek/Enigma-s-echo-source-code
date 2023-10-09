using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ThirdPersonCharacter))]
public class AgentMovements : MonoBehaviour
{
    private NavMeshAgent agent;
    private ThirdPersonCharacter character;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        character = GetComponent<ThirdPersonCharacter>();
        agent.updateRotation = false;
    }

    public void SetDistination(Vector3 destination)
    {
        agent.destination = destination;
        if(agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }

        
    }

   
    public void StopAgent()
    {
        // Stop the agent's movement
        agent.isStopped = true;
    }

 
    public void ResumeAgent()
    {
        // Resume the agent's movement
        agent.isStopped = false;
    }





}
