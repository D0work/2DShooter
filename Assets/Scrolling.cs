using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Scrolling : MonoBehaviour
{
    public List<Transform> backgrounds;
    public float scrollSpeed = 2f;
    public float resetPositionX = -20f;
    public float startOffset = 40f;

    private float leftEdge = 0f;
    private Vector3 lastPosition = new Vector3(0f,0f,0f);     

    void Start()
    {
        Camera cam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        this.leftEdge = cam.ViewportToWorldPoint(new Vector3(0, 0.5f, cam.nearClipPlane)).x;

        Transform lastTransform = GetFarthestRightBackground();
        this.lastPosition = lastTransform.position;
    }

    void Update()
    {
        foreach (Transform bg in backgrounds)
        {
            SpriteRenderer sr = bg.GetComponent<SpriteRenderer>();
            float spriteRightEdge = bg.position.x + sr.bounds.extents.x;

            // Continuous movement
            bg.Translate(Vector3.left * scrollSpeed * Time.deltaTime);

            // Scrolling bound
            if (spriteRightEdge < leftEdge)
            {
                Transform farthestBg = GetFarthestRightBackground();

                float width = sr.bounds.size.x;
                Vector3 newPos = bg.position;
                newPos.x = farthestBg.position.x + width;
                bg.position = newPos;
            }
        }
    }

    Transform GetFarthestRightBackground()
    {
        Transform farthest = backgrounds[0];

        foreach (Transform bg in backgrounds)
        {
            if (bg.position.x > farthest.position.x)
            {
                farthest = bg;
            }
        }

        return farthest;
    }
}
