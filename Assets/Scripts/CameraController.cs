using UnityEngine;

// Camera follows player
public class CameraController : MonoBehaviour
{

    [SerializeField] private float speed;
    private float currentPosX;

    private Vector3 velocity = Vector3.zero;

    [Header("Follow the Player")]

    [SerializeField] private Transform player;

    [SerializeField] private float aheadDistance;

    [SerializeField] private float cameraSpeed;

    private float lookAheadX;

    private float lookAheadY;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x + lookAheadX, player.position.y + lookAheadY, transform.position.z);
        lookAheadX = Mathf.Lerp(lookAheadX, (aheadDistance * player.localScale.x), Time.deltaTime * cameraSpeed);
        lookAheadY = Mathf.Lerp(lookAheadY, (aheadDistance * player.localScale.y), Time.deltaTime * cameraSpeed);
    }

    public void MoveToNewRoom(Transform newRoom)
    {
        currentPosX = newRoom.position.x;
    }
}
