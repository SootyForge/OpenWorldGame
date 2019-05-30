using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

[CreateAssetMenu(fileName = "Flee", menuName = "SteeringBehaviours/Flee", order = 1)]
public class Flee : SteeringBehaviour
{
  public float stoppingDistance = 1f; // Requires that you set "NavMeshAgent.stoppingDistance" to zero

  public override void OnDrawGizmosSelected(AI owner)
  {
    Gizmos.color = Color.red;
    float distance = Vector3.Distance(owner.target.position, owner.transform.position);
    Gizmos.DrawWireSphere(owner.target.position, stoppingDistance - distance);
  }

  public override Vector3 GetForce(AI owner)
  {
    // Create a value to return later
    Vector3 force = Vector3.zero;

    if (owner.hasTarget) // target != null
    {
      // Get direction from AI agent to Target
      force += owner.transform.position - owner.target.position;
    }

    // Return normalized value
    return force.normalized;
  }
}
