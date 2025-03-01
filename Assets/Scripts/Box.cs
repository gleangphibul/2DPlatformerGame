using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject hiddenObject;

    public GameObject breakEffect;
    public GameObject keyPrefab;
    public GameObject swordPrefab;
    public GameObject enemyObject;
    private static GameObject[] allBoxes;

    private bool isBroken = false;

    void Start()
    {
        AssignRandomItem();
    }

    private void AssignRandomItem() {
        // Get all boxes in the scene
        allBoxes = GameObject.FindGameObjectsWithTag("Box");
        if (allBoxes.Length == 1) { // Level 0
            Box box = GetComponent<Box>();
            box.hiddenObject = keyPrefab;
        } else {
            Box[] boxes = new Box[allBoxes.Length];
            for (int i = 0; i < allBoxes.Length; i++)
            {
                boxes[i] = allBoxes[i].GetComponent<Box>();
            }

            // Randomize box order
            ShuffleArray(boxes);

            // Assign items
            boxes[0].hiddenObject = keyPrefab;        
            boxes[1].hiddenObject = swordPrefab;
            boxes[2].hiddenObject = boxes[2].enemyObject;
            boxes[3].hiddenObject = boxes[3].enemyObject;
        }
    }

    private void BreakBox()
    {
        if (isBroken) return; // Prevent multiple breaks
        isBroken = true;

        // Play break effect if assigned
        if (breakEffect != null)
        {
            Instantiate(breakEffect, transform.position, Quaternion.identity);
        }
        // Reveal the manually assigned hidden object
        
        if (hiddenObject != null) {
            GameObject instantiatedObject = Instantiate(hiddenObject, transform.position, Quaternion.identity);
            
            if (hiddenObject == keyPrefab) {
                Key keyScript = instantiatedObject.GetComponent<Key>();
                // Call methods on Key script if necessary
            } else if (hiddenObject == swordPrefab) {
                Sword swordScript = instantiatedObject.GetComponent<Sword>();
                // Call methods on Sword script if necessary
            } else if (hiddenObject == enemyObject) {
                ActivateEnemy();
            }
        }

        // Destroy the box
        Destroy(gameObject);
    }


    public void TakeHit()
    {
        BreakBox();
    }

    private void ActivateEnemy() {
        if (enemyObject == null) return;
        enemyObject.SetActive(true);
    }

    private void ShuffleArray(Box[] array) {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int rand = Random.Range(0, i + 1);
            Box temp = array[i];
            array[i] = array[rand];
            array[rand] = temp;
        }
    }
}
