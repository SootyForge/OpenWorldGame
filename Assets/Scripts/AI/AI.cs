using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
  public bool hasTarget;
  [ShowIf("hasTarget")] public Transform target;
  public float maxVelocity = 15f;
  public float maxDistance = 10f;
  public SteeringBehaviour[] behaviours;
  protected NavMeshAgent agent;

  private Vector3 velocity;
  private void Awake()
  {
    agent = GetComponent<NavMeshAgent>();
  }

  private void OnDrawGizmosSelected()
  {
    Vector3 desiredPosition = transform.position + velocity * Time.deltaTime;
    Gizmos.color = Color.red;
    Gizmos.DrawSphere(desiredPosition, .1f);


    // Render all behaviours
    foreach (var behaviour in behaviours)
    {
      behaviour.OnDrawGizmosSelected(this);
    }
  }

  private void Update()
  {
    // Step 1). Zero out Velocity
    velocity = Vector3.zero;

    // Step 2). Loop through all behaviours and get forces
    foreach (var behaviour in behaviours)
    {
      // Apply normalized force to force
      float percentage = maxVelocity * behaviour.weighting;
      velocity += behaviour.GetForce(this) * percentage;
    }

    // Step 3). Limit velocity to max velocity
    velocity = Vector3.ClampMagnitude(velocity, maxVelocity);

    // Step 4). Apply velocity to NavMeshAgent destination
    Vector3 desiredPosition = transform.position + velocity * Time.deltaTime;
    NavMeshHit hit;
    // Check if desired position is within NavMesh
    if (NavMesh.SamplePosition(desiredPosition, out hit, maxDistance, -1))
    {
      // Set agent's destination to hit point
      agent.SetDestination(hit.position);
    }
  }
}
