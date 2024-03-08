using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeId : MonoBehaviour
{
    [field: SerializeField] public GameObject owner { get; private set; } = null;
}
