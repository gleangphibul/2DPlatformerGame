using UnityEngine;

public class GhostWallController : MonoBehaviour
{
    private BoxCollider2D wallCollider;
    private SpriteRenderer wallSpriteRenderer;


    void Start()
    {
        wallCollider = GetComponent<BoxCollider2D>();
        wallSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ToggleTrigger(bool isGhost)
    {
        wallCollider.isTrigger = isGhost;
        if (isGhost) {
            wallSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else {
            wallSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

    }
}
