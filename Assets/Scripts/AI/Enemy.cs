using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference things in UI from Unity (i.e, Toggle, Button, Slider, etc)
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
  public int maxHealth = 100;
  public Transform healthBarParent; // Refers to Empty Canvas element
  public GameObject healthBarUIPrefab; // Healthbar Prefab to spawn
  public Transform healthBarPoint; // Refers to transform for following health bar 

  private int health = 0;
  private Slider healthSlider;
  private Renderer rend;

  // Start is called before the first frame update
  void Start()
  {
    // Spawn a new Healthbar under 'HealthBarParent'
    // Get Slider component from new Healthbar
    // Set health to maxHealth
    // Get the renderer attached to Enemy
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

  public void TakeDamage(int damage)
  {
    // Reduce health with damage
    // Update Health Slider
    // If health reaches zero
    // Destroy the GameObject
  }
}
