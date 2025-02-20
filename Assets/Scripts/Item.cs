using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    
    public enum InteractionType { NONE, PickUp, Examine}
    public InteractionType type;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        // Must be item layer
        gameObject.layer = 8;
    }

    public void Interact()
    {
        switch(type)
        {
            case InteractionType.PickUp:
                Debug.Log("PICK UP");
                break;
            case InteractionType.Examine:
                // Display an Examine Window
                // Show the item's image in the middle
                // Write description text underneath the image
                Debug.Log("EXAMINE");
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }
    }

}
