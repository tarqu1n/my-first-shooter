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

    // Update is called once per frame
    void Update()
    {
        float hpPercentage = (float)playerController.currentHealth / (float)playerController.currentMaxHealth;
        RectTransform barTransform = (transform as RectTransform);
        barTransform.sizeDelta = new Vector2(fullWidth * hpPercentage, fullHeight);

        if (hpPercentage < 0.2f)
        {
            GetComponent<Image>().color = new Color(288f, 194f, 7f);
        }
        else if (hpPercentage < 0.5f)
        {
            GetComponent<Image>().color = new Color(184f, 52f, 0f);
        }
        
    }
}
