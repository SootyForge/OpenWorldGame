using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDirection : MonoBehaviour
{
    public Transform target;
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(target.forward);
    }
}
