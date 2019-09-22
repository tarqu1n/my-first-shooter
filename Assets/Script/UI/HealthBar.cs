using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        (transform as RectTransform).sizeDelta = new Vector2(fullWidth * (float)playerController.currentHealth / (float)playerController.currentMaxHealth, fullHeight);
        // rectTransform.rect.width = fullWidth * (float)playerController.currentHealth;
    }
}
