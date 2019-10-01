using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject playerObject;

    private PlayerController playerController;
    private float fullWidth;
    private float fullHeight;
    private RectTransform rectTransform;

    void Start()
    {
        playerController = playerObject.GetComponent<PlayerController>();
        RectTransform rectTransform = (transform as RectTransform);
        fullWidth = rectTransform.rect.width;
        fullHeight = rectTransform.rect.height;
    }

    void FixedUpdate()
    {
        float hpPercentage = (float)playerController.currentHealth / (float)playerController.currentMaxHealth;
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
