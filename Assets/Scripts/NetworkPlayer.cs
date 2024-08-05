using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayer : NetworkBehaviour
{
    


    void Update()
    {
        if (!IsOwner) return;


        Vector3 Movment = Vector3.zero; 
        
        if (Input.GetKey(KeyCode.W)) Movment.z = 1f;
        if (Input.GetKey(KeyCode.S)) Movment.z = -1f;
        if (Input.GetKey(KeyCode.A)) Movment.x = -1f;
        if (Input.GetKey(KeyCode.D)) Movment.x = 1f;
        if (Input.GetKey(KeyCode.Space)) Movment += Vector3.up;

        
        transform.position += Movment * Time.deltaTime * 5f;
        
    }
}
