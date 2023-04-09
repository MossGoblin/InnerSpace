using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;


public class MapChunk : MonoBehaviour
{
    public Transform chunkTransform;
    public SpriteRenderer spriteRenderer;
    private Sprite chunkSprite;
    private float chunkDecayLevel;
    private float chunkMinimumScaleFactor;
    private double tileSize;
    public Address address;
    private Vector3 mapOffset;
    private bool scaleAdjusted;
    public TMP_Text debugText;

    void Start()
    {
        scaleAdjusted = false;
        debugText.text = $"{address.addrP} / {address.addrQ} / {address.addrR}";
    }

    void Update()
    {
        // set sprite
        spriteRenderer.sprite = chunkSprite;

        if (!scaleAdjusted)
        {
            float scaleFactor = 1 - chunkDecayLevel;
            float adjustedScale = chunkMinimumScaleFactor + scaleFactor / (1 / (1 - chunkMinimumScaleFactor));
            Vector3 newScale = new Vector3(chunkTransform.localScale.x * adjustedScale, chunkTransform.localScale.x * adjustedScale);
            chunkTransform.localScale = newScale;
            scaleAdjusted = true;
        }
    }

    public void SetMinimumDecayLevel(float decayLevel)
    {
        chunkDecayLevel = decayLevel;
    }

    public void SetSprite(Sprite newSprite)
    {
        chunkSprite = newSprite;
    }

    public void SetScaleFactor(float minimumScaleFactor)
    {
        chunkMinimumScaleFactor = minimumScaleFactor;
    }
}
