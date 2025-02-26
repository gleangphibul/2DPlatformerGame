using UnityEngine;

public class BreakableBox : MonoBehaviour
{
    public GameObject hiddenObject;

    public GameObject breakEffect;

    private bool isBroken = false;

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
        if (hiddenObject != null)
        {
            hiddenObject.transform.SetParent(null); // Detach from parent
            hiddenObject.SetActive(true);
        }

        // Destroy the box
        Destroy(gameObject);
    }


    public void TakeHit()
    {
        BreakBox();
    }
}
