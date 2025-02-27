using UnityEngine;
using System.Collections.Generic;

public class PlayerFormController : MonoBehaviour
{
    public bool isGhost = false;
    public float ShiftCoolDown = 1;
    public float nextShiftTime = 5;
    public KeyCode tab = KeyCode.Tab;
    private List<GhostWallController> ghostWalls = new();

    public SpriteRenderer playerSpriteRenderer;
    public BoxCollider2D boxCollider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerSpriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        GameObject[] wallObjects = GameObject.FindGameObjectsWithTag("GhostWall");
        foreach (GameObject obj in wallObjects)
        {
            GhostWallController wall = obj.GetComponent<GhostWallController>();
            if (wall != null)
            {
                ghostWalls.Add(wall);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        nextShiftTime -= Time.deltaTime;
        if (Input.GetKeyDown(tab) && nextShiftTime <= 0) {
            ShiftForm();
        }
        
    }

    private void ShiftForm() {
        isGhost = !isGhost;
        nextShiftTime = 5;
        if (isGhost) {
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 0.5f);
        } else {
            playerSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        foreach (GhostWallController wall in ghostWalls) {
            wall.ToggleTrigger(isGhost);
        }
    }
    
}
