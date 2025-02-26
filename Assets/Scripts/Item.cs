using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    
    public enum InteractionType { NONE, PickUp, Chest, Enemy}
    public InteractionType type;
    public Sprite image;

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
                // Add the object to the PickedUpItems list
                Object.FindAnyObjectByType<InteractionSystem>().PickUpItem(gameObject);
                // Disable the object
                gameObject.SetActive(false);
                break;
            case InteractionType.Chest:
                // Destroy the chest
                // Create a key object
                Object.FindAnyObjectByType<InteractionSystem>().OpenChest(gameObject);
                break;

            case InteractionType.Enemy:
                Debug.Log("Enemy");
                break;
            default:
                Debug.Log("NULL ITEM");
                break;
        }
    }

}
