using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

public class Player : MonoBehaviour
{
  public int health = 100;
  public int damage = 20;
  public float runSpeed = 8f;
  public float walkSpeed = 6f;
  [MinMaxSlider(0, 100)]
  public Vector2 speed;
  public float gravity = -10f;
  public float jumpHeight = 15f;
  [Header("Dash")]
  public float dashDuration = .5f;

  [ShowAssetPreview]
  public GameObject prefab;

  private CharacterController controller; // Reference to CharacterController
  private Vector3 motion; // Is the movement offset per frame
  private bool isJumping;
  private float currentJumpHeight, currentSpeed;
  void OnValidate()
  {
    currentJumpHeight = jumpHeight;
    currentSpeed = walkSpeed;
  }
  void Start()
  {
    controller = GetComponent<CharacterController>();
    // Set initial states
    currentSpeed = walkSpeed;
    currentJumpHeight = jumpHeight;
  }
  void Update()
  {
    // Get W, A, S, D or Left, Right, Up, Down Input
    float inputH = Input.GetAxis("Horizontal");
    float inputV = Input.GetAxis("Vertical");
    // Left Shift Input
    bool inputRun = Input.GetKeyDown(KeyCode.LeftShift);
    bool inputWalk = Input.GetKeyUp(KeyCode.LeftShift);
    // Space Bar Input
    bool inputJump = Input.GetButtonDown("Jump");

    // If we need to run
    if (inputRun)
    {
      currentSpeed = runSpeed;
    }

    // If we need to walk
    if (inputWalk)
    {
      currentSpeed = walkSpeed;
    }

    // Move character motion with inputs
    Move(inputH, inputV);

    // Is the Player grounded?
    if (controller.isGrounded)
    {
      // Cancel gravity
      motion.y = 0f;
      // .. And pressing jump?
      if (inputJump)
      {
        Jump(jumpHeight);
      }
      // Pressing Jump Button?
      if (isJumping)
      {
        // Make the Player Jump!
        motion.y = currentJumpHeight;
        // Reset back to false
        isJumping = false;
      }
    }
    // Apply gravity
    motion.y += gravity * Time.deltaTime;
    // Move the controller with motion
    controller.Move(motion * Time.deltaTime);
  }
  void Move(float inputH, float inputV)
  {
    // Generate direction from input
    Vector3 direction = new Vector3(inputH, 0f, inputV);

    // Convert local space to world space direction
    direction = transform.TransformDirection(direction);

    // Check if direction exceeds magnitude of 1
    if (direction.magnitude > 1f)
    {
      // Normalize it!
      direction.Normalize();
    }

    // Apply motion to only X and Z
    motion.x = direction.x * currentSpeed;
    motion.z = direction.z * currentSpeed;
  }
  public IEnumerator SpeedBoost(float boostSpeed, float duration)
  {
    currentSpeed += boostSpeed;

    yield return new WaitForSeconds(duration);

    currentSpeed -= boostSpeed;
  }

  [Button("Run Jump!")]
  public void Jump()
  {
    Jump(50);
  }


  public void Jump(float height)
  {
    isJumping = true; // Tell the controller to jump (at the right time)
    currentJumpHeight = height;
  }

  public void Dash(float boost)
  {
    StartCoroutine(SpeedBoost(boost, dashDuration));
  }

  private void OnTriggerEnter(Collider other)
  {
    Enemy enemy = other.GetComponent<Enemy>();
    if (enemy)
    {
      enemy.TakeDamage(damage);
    }
  }
}
