using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{

   [Header("Detection Parameters")]
   // Detection point
   public Transform detectionPoint;
   // Detection radius
   private const float detectionRadius = 0.2f;
   // Detection layer
   public LayerMask detectionLayer;

   // Cached Trigger Object
   public GameObject detectedObject;

   [Header("Pickup-able Items")]

   // List of picked items
   public List<GameObject> pickedItems = new List<GameObject>();

   [Header("For Chest Interactions")]

   public GameObject key;

//    [Header("For Enemy interaction")]

//    public GameObject enemy;


    // Update is called once per frame
    void Update()
    {
        if(DetectObject())
        {
            if(InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if (obj==null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
     Gizmos.color = Color.green;
     Gizmos.DrawSphere(detectionPoint.position, detectionRadius);   
    }

    public void PickUpItem(GameObject item)
    {
        pickedItems.Add(item);
        Debug.Log("Item picked up");
    }

    public void OpenChest(GameObject item)
    {
        Destroy(item);
        Debug.Log("Chest destroyed");
        Instantiate<GameObject>(key, transform.position, Quaternion.identity);
        Debug.Log("Key created");
    }

    public void AttackEnemy(GameObject enemy)
    {
        // Insert combat code here
    }

    

}
