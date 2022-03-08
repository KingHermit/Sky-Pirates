using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage;
    public PlayerController player;

    public void UpdateHealthBar(float damage)
    {
        //healthBarImage.fillAmount = healthBarImage.fillAmount - 0.05f;
        healthBarImage.fillAmount -= damage;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
