#if UNITY_EDITOR
using UnityEngine;

public class GizmoTrigger : MonoBehaviour
{

    public Color GizmoColor;


    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}

#endif
