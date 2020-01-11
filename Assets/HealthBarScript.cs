using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



    public class HealthBarScript : MonoBehaviour
    {
    Image HealthBar;
    static float maxHealth = 100f;
    public static bool isMortal = true;
    private static float Health;

    public static float getHealth()
    {
        return Health;
    }

    public static void setHealth(float currHealth)
    {
        if (isMortal)
        {
            Health = currHealth;
        }
    }

    public static void restoreHealth()
    {
        Health = maxHealth;
    }

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

