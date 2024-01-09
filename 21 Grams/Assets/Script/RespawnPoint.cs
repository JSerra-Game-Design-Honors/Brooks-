using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public static Vector3 respawnPosition;

    private void Start()
    {
        respawnPosition = transform.position;
    }
}
