using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// required for collision detection
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ProductionTile : MonoBehaviour
{
    [SerializeField] Material tileMaterial;
    [SerializeField] LayerMask collisionLayers;
    [SerializeField] Color noCollisionColor;
    [SerializeField] Color collisionColor;

    public bool colliding { get; private set; } 

	void Start ()
    {
        GetComponent<Renderer>().material.CopyPropertiesFromMaterial(tileMaterial);
        SetColor(noCollisionColor);
    }

    void SetColor(Color c)
    {
        GetComponent<Renderer>().material.SetColor("_TintColor", c);
    }

    void OnTriggerEnter(Collider other)
    {
        if(collisionLayers == (collisionLayers | (1 << other.gameObject.layer)))
        {
            if(other.gameObject.transform.root.gameObject.GetInstanceID() != transform.root.gameObject.GetInstanceID())
            {
                SetColor(collisionColor);
                colliding = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(collisionLayers == (collisionLayers | (1 << other.gameObject.layer)))
        {
            SetColor(noCollisionColor);
            colliding = false;
        }
    }
}
