using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavZone : Singleton<NavZone>
{
    public Vector3 GetNavigablePoint()
    {
        Vector3 _pos = transform.position;
        int _sizeX = Mathf.CeilToInt(transform.localScale.x / 2);
        int _sizeZ = Mathf.CeilToInt(transform.localScale.z / 2);
        int _x = Random.Range(-_sizeX, _sizeX);
        int _z = Random.Range(-_sizeZ, _sizeZ);
        Vector3 _point = transform.position + new Vector3(_x, transform.localScale.y / 2, _z);
        return _point;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, .3f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

}
