using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
     Author: Daniel Vindhjärta
     Last updated: 2018-12-26 
     Purpose:
     Allows for easy access and visibility of a GameObject without a graphical representation in the scene, by
     placing a gizmo on said GameObject.
     
     Usage:
     Simply attach to any GameObject. Settings for symbol and colour are available.
     
     Dependencies:
     Unity
     
*/
[ExecuteInEditMode]
public class GizmoMarker : MonoBehaviour
{

    public enum EGizmoTypes { WireSphere = 0, WireCube, Sphere, Cube, Icon }

    public EGizmoTypes type = EGizmoTypes.WireSphere;
    public float size = 0.5f;
    public Color color = Color.red;
    public string iconName;

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        switch (type)
        {
            case EGizmoTypes.WireSphere:
                Gizmos.DrawWireSphere(transform.position, size);
                break;
            case EGizmoTypes.WireCube:
                Gizmos.DrawWireCube(transform.position, new Vector3(size, size, size));
                break;
            case EGizmoTypes.Sphere:
                Gizmos.DrawSphere(transform.position, size);
                break;
            case EGizmoTypes.Cube:
                Gizmos.DrawCube(transform.position, new Vector3(size, size, size));
                break;
            case EGizmoTypes.Icon:
                Gizmos.DrawIcon(transform.position, iconName);
                break;
        }
        

    }

    void OnDrawGizmosSelected()
    {
        OnDrawGizmos();
    }
}