using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Transform followTarget;


    private void LateUpdate()
    {
        float diff = Mathf.Abs(transform.position.x - followTarget.position.x);
        if (diff < 0.2f)
            return;

        transform.position = Vector3.Slerp(transform.position, followTarget.position, 0.5f);
    }
}
