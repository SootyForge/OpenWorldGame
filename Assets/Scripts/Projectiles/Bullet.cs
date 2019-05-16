﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;

namespace Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : Projectile
    {
        public float speed = 50f;
        [BoxGroup("References")] public GameObject bulletHolePrefab;
        [BoxGroup("References")] public Transform line;

        private Rigidbody rigid;
        private Vector3 start, end;

        void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }
        void Start()
        {
            start = transform.position;
        }
        void Update()
        {
            line.rotation = Quaternion.LookRotation(rigid.velocity);
        }
        void OnCollisionEnter(Collision col)
        {
            end = transform.position;
            // Get contact point from collision
            ContactPoint contact = col.contacts[0];

            // Get bulletDirection
            Vector3 bulletDir = end - start;

            // Jordan
            Quaternion lookRotation = Quaternion.LookRotation(bulletDir);
            Quaternion rotation = lookRotation * Quaternion.AngleAxis(-90, Vector3.right);

            // Ben
            // Spawn a BulletHole on that contact point
            GameObject bHole = Instantiate(bulletHolePrefab, contact.point, rotation);
            // Get angle between normal and bullet dir
            float impactAngle = 180 - Vector3.Angle(bulletDir, contact.normal);
            bHole.transform.localScale = bHole.transform.localScale / (1 + impactAngle / 45);

            // Destroy self
            Destroy(gameObject);
        }
        public override void Fire(Vector3 lineOrigin, Vector3 direction)
        {
            // Set line position to origin
            line.position = lineOrigin;
            // Set bullet flying in direction with speed
            rigid.AddForce(direction * speed, ForceMode.Impulse);
        }
    } 
}
