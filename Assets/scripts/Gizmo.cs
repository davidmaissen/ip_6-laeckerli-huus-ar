using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Source:https://www.youtube.com/watch?v=itIh8PYGP7w
public class Gizmo : MonoBehaviour
{
    public float gizmoSize = .75f;
    public Color gizmoColor = Color.yellow;
    // Start is called before the first frame update
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireSphere(transform.position, gizmoSize);
    }
}
