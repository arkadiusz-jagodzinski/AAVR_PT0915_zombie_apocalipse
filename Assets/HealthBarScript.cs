using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



    public class HealthBarScript : MonoBehaviour
    {
    Image HealthBar;
    float maxHealth = 100f;
    public static float Health;

    private void Start()
    {
        HealthBar = GetComponent<Image> ();
        Health = maxHealth;
    }
    private void Update()
    {
        HealthBar.fillAmount = Health / maxHealth;
    }
}

