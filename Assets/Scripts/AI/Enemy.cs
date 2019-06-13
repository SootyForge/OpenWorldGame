using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;
using NaughtyAttributes;
// Reference things in UI from Unity (i.e, Toggle, Button, Slider, etc)
//using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
  public Transform target;
  [ProgressBar("Health", 100, ProgressBarColor.Red)]
  public int health = 100;

  private NavMeshAgent agent;

  // public int maxHealth = 100;
  // public Transform healthBarParent; // Refers to Empty Canvas element
  // public GameObject healthBarUIPrefab; // Healthbar Prefab to spawn
  // public Transform healthBarPoint; // Refers to transform for following health bar 

  // public int health = 100;
  // private Slider healthSlider;
  // private Renderer rend;

  // Start is called before the first frame update
  void Start()
  {
    agent = GetComponent<NavMeshAgent>();
    // Spawn a new Healthbar under 'HealthBarParent'
    // Get Slider component from new Healthbar
    // Set health to maxHealth
    // Get the renderer attached to Enemy
  }

  // Update is called once per frame
  void Update()
  {
    agent.SetDestination(target.position);
  }

  // When the GameObject gets Destroyed
  void OnDestroy()
  {
    // If healthSlider exists
    // Take the HealthSlider with it
  }

  // Update is called once per frame
  void LateUpdate()
  {
    // healthSlider.gameObject.SetActive(rend.isVisible);
    // Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarPoint.position);
    // healthSlider.transform.position = screenPosition;

    // OR

    // If the renderer (Enemy) is visible 
    // Activate the HealthBar
    // Update position of healthbar with enemy
    // If it is NOT visible
    // Deactivate the HealthBar
  }

  public void Heal(int heal)
  {
    health += heal;
  }

  public void TakeDamage(int damage)
  {
    // Reduce health with damage
    health -= damage;
    // Update Health Slider
    // If health reaches zero
    if (health <= 0)
    {
      // Destroy the GameObject
      Destroy(gameObject);
    }
  }
}
