using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class AimCoreToMouse : MonoBehaviour
{
    public float RotationSpeed;

    float RotAngle;

    // Update is called once per frame
    void Update()
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
        // var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(angle - 90, 0, 0), 0.5f);
    }
}
