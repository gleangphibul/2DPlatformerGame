using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
   // Detection point
   public Transform detectionPoint;
   // Detection radius
   private const float detectionRadius = 0.2f;
   // Detection layer
   public LayerMask detectionLayer;

   // Cached Trigger Object
   public GameObject detectedObject;

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
}
