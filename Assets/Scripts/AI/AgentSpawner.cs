using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentSpawner : MonoBehaviour
{
  public GameObject spawnPrefab;
  public float spawnRate = 1f;
  public float radius = 25f;

  //public bool canSpawn = true;
  public int maxAgents = 100;
  public int agentCount = 0;

  private float spawnTimer = 0f;

  //public List<GameObject> activeAgents;

  private void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, radius);
  }

  private void Update()
  {
    spawnTimer += Time.deltaTime;
    if(spawnTimer >= 1f / spawnRate)
    {
      if(agentCount < maxAgents)
      {
        Spawn(spawnPrefab);
      }
      spawnTimer = 0f;
    }
  }
  
  public void Spawn(GameObject prefab)
  {
    Vector3 point = GetRandomPointOnTerrain();

    GameObject clone = Instantiate(prefab, point, Quaternion.identity);
    clone.SetActive(true);

    agentCount++;
  }

  //public IEnumerator Spawn(GameObject prefab)
  //{
  //  Vector3 point = GetRandomPointOnTerrain();

  //  GameObject clone = Instantiate(prefab, point, Quaternion.identity);
  //  clone.SetActive(true);

  //  agentCount++;
  //  //activeAgents.Add(clone);

  //  yield return new WaitForSeconds(1f / spawnRate);

  //  if(agentCount < maxAgents)
  //  {
  //    StartCoroutine(Spawn(prefab));
  //  }


  //  //if (activeAgents.Count < maxAgents && canSpawn)
  //  //{
  //  //  StartCoroutine(Spawn(prefab));
  //  //}
  //  //else
  //  //{
  //  //  canSpawn = false; //StopCoroutine(Spawn(prefab));
  //  //  StartCoroutine(CheckAgents());
  //  //}
  //}

  public Vector3 GetRandomPointOnTerrain()
  {
    Vector3 randomPoint = transform.position + Random.insideUnitSphere * radius;
    //NavMeshHit hit;
    //NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas);
    //return hit.position;

    randomPoint.y = Terrain.activeTerrain.SampleHeight(randomPoint);

    return randomPoint;
  }

  public void AgentDeath()
  {
    agentCount--;
  }

  ///public IEnumerator CheckAgents()
  ///{
  ///  for (int i = activeAgents.Count - 1; i > -1; i--)
  ///  {
  ///    if (activeAgents[i] == null)
  ///    {
  ///      activeAgents.RemoveAt(i);
  ///    }
  ///  }
  ///
  ///  agentCount = activeAgents.Count;
  ///
  ///  yield return new WaitForSeconds(10f);
  ///
  ///  if (agentCount > maxAgents && !canSpawn)
  ///  {
  ///    StartCoroutine(CheckAgents());
  ///  }
  ///  else
  ///  {
  ///    StartCoroutine(Spawn(spawnPrefab));
  ///    canSpawn = true; //StopCoroutine(CheckAgents());
  ///  }
  ///}
}
