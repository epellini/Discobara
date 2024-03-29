using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// Camera Pivot
public class CameraPivot : MonoBehaviour
{
    public Transform FollowPlayer;
    public Vector3 Offset;

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, FollowPlayer.position + Offset, Time.deltaTime * 3f);
    }
}