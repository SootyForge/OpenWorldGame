﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Seek", menuName = "SteeringBehaviours/Seek", order = 1)]
public class Seek : SteeringBehaviour
{
  public float stoppingDistance = 1f; // Requires that oyu set "NavMeshAgent.stoppingDistance" to zero

  public override void OnDrawGizmosSelected(AI owner)
  {
    Gizmos.color = Color.blue;
    float distance = Vector3.Distance(owner.target.position, owner.transform.position);
    Gizmos.DrawWireSphere(owner.transform.position, distance - stoppingDistance);
  }

  public override Vector3 GetForce(AI owner)
  {
    // Create a value to return later
    Vector3 force = Vector3.zero;

    // Get distance between owner and targets
    float distance = Vector3.Distance(owner.transform.position, owner.target.position);
    // If AI is further away from Target
    if (distance >= stoppingDistance)
    {
      if (owner.hasTarget) // target != null
      {
        // Get direction from AI agent to Target
        force += owner.target.position - owner.transform.position;
      }
    }

    // Return normalized value
    return force.normalized;
  }
}
