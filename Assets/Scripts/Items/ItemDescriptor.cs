using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptor : MonoBehaviour
{
    [SerializeField] string descriptionOfItem;

    public string GetDescription()
    {
        return descriptionOfItem;
    }
}
