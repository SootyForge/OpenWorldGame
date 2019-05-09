using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using NaughtyAttributes;

[RequireComponent(typeof(Player))]
public class Shooting : MonoBehaviour
{
    [BoxGroup("Weapon System")] public Weapon currentWeapon;
    [BoxGroup("Weapon System")] public List<Weapon> weapons = new List<Weapon>();
    [BoxGroup("Weapon System")] public int currentWeaponIndex = 0;

    private Player player;
    private CameraLook cameraLook;

    void Awake()
    {
        player = GetComponent<Player>();
        cameraLook = GetComponent<CameraLook>();
    }

    void Start()
    {
        // Get all weapons attached to Player
        weapons = GetComponentsInChildren<Weapon>().ToList();

        // Select first one
        SelectWeapon(0);
    }

    void FixedUpdate()
    {
        // If there is a weapon
        if (currentWeapon)
        {
            bool fire1 = Input.GetButton("Fire1");
            if (fire1)
            {
                // Check if weapon can shoot
                if (currentWeapon.canShoot)
                {
                    // Shoot the weapon
                    currentWeapon.Shoot();
                    // Apply Weapon Recoil
                    Vector3 euler = Vector3.up * 2f;
                    // Randomize the pitch
                    euler.x = Random.Range(-1f, 1f);
                    // Apply offset to camera using weapon recoil
                    cameraLook.SetTargetOffset(euler * currentWeapon.recoil);
                }
            }
        }
    }

    void DisableAllWeapons()
    {
        // Loop through all weapons
        foreach (var item in weapons)
        {
            // Disable each GameObject
            item.gameObject.SetActive(false);
        }
    }

    void SelectWeapon(int index)
    {
        // Check if index is within bounds
        if (index >= 0 && index < weapons.Count)
        {
            // Disable all weapons
            DisableAllWeapons();
            // Select currentWeapon
            currentWeapon = weapons[index];
            // Enable currentWeapon
            currentWeapon.gameObject.SetActive(true);
            // Update currentWeaponIndex
            currentWeaponIndex = index;
        }
    }
}
