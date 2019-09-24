using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShardHealthBar : MonoBehaviour
{
    public ShardController shardController;
    private float fullWidth;
    private float fullHeight;
    private RectTransform rectTransform;

    void Start()
    {
        RectTransform rectTransform = (transform as RectTransform);
        fullWidth = rectTransform.rect.width;
        fullHeight = rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        float hpPercentage = (float)shardController.currentHealth / (float)shardController.startHealth;
        RectTransform barTransform = (transform as RectTransform);
        barTransform.sizeDelta = new Vector2(fullWidth * hpPercentage, fullHeight);

        if (hpPercentage < 0.2f)
        {
            GetComponent<Image>().color = Color.red;
        }
        else if (hpPercentage < 0.5f)
        {
            GetComponent<Image>().color = Color.yellow;
        } else if (hpPercentage > 0.5f)
        {
            GetComponent<Image>().color = Color.green;
        }
        
    }
}
