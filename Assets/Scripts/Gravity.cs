// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class Gravity : MonoBehaviour
// {
//     [SerializeField] private Rigidbody _rb;
//
//     private RotatingPlatform currentPlatform;
//     
//     void Awake () {
//         _rb = GetComponent<Rigidbody> ();
//
//         // Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
//         _rb.useGravity = false;
//         _rb.constraints = RigidbodyConstraints.FreezeRotation;
//     }
//
//     private void OnCollisionEnter(Collision other)
//     {
//         if (other.gameObject.TryGetComponent(out RotatingPlatform RP))
//         {
//             print("girdik");
//             if (currentPlatform != null)
//             {
//                 if (RP != currentPlatform)
//                 {
//                     currentPlatform.StopAttract();
//                 }
//             }
//             currentPlatform = RP;
//             //transform.SetParent(currentPlatform.transform);
//             currentPlatform.StartAttract(_rb);
//         }
//         
//         if (other.transform.CompareTag("Platform"))
//         {
//
//             if (currentPlatform != null)
//             {
//                 currentPlatform.StopAttract();
//
//             }
//         }
//     }
//
// }
