using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject Boss;            
    public Image healthBarFill;         
    private int maxHealth = 100;        

    private Health healthScript;

    [Tooltip("Thresholds Hp.")]
    private List<int> triggeredThresholds = new List<int>();


    private void Start()
    {
        if (Boss != null)
        {
            healthScript = Boss.GetComponent<Health>();
            maxHealth = healthScript.maximumHealth;
        }
    }

    private void Update()
    {
        if (healthScript != null && healthBarFill != null)
        {
            int currentHp = healthScript.currentHealth;
            float hpPercent = (float)currentHp / maxHealth * 100f;

            healthBarFill.fillAmount = hpPercent / 100f;
            CheckHealthTriggers(hpPercent);
        }
    }

    private void CheckHealthTriggers(float hpPercent)
    {
        int slice = Mathf.FloorToInt(hpPercent / 18f) * 12;

        if (!triggeredThresholds.Contains(slice))
        {
            Boss.GetComponent<Enemy>().moveSpeed += 1;
            triggeredThresholds.Add(slice);
            ActivateChildAtIndex(triggeredThresholds.Count - 1);

            BoostChildrenShooting();
            UpdateEnemyGuns();
        }

        if (hpPercent <= 50f && !triggeredThresholds.Contains(-50)) 
        {
            triggeredThresholds.Add(-50);

            Enemy enemyScript = Boss.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.movementMode = Enemy.MovementModes.FollowTarget;
            }
        }
    }

    private void ActivateChildAtIndex(int index)
    {
        if (Boss.transform.childCount > index)
        {
            Transform child = Boss.transform.GetChild(index);
            child.gameObject.SetActive(true);
        }
    }

    private void BoostChildrenShooting()
    {
        foreach (Transform child in Boss.transform)
        {
            ShootingController shooting = child.GetComponent<ShootingController>();
            if (shooting != null)
            {
                shooting.fireRate -= 0.20f;       
                shooting.projectileSpread += 0.20f;  
            }
        }
    }

    private void UpdateEnemyGuns()
    {
        Enemy enemyScript = Boss.GetComponent<Enemy>();
        if (enemyScript == null) return;

        enemyScript.guns.Clear(); 

        foreach (Transform child in Boss.transform)
        {
            ShootingController sc = child.GetComponent<ShootingController>();
            if (sc != null && sc.isActiveAndEnabled)
            {
                enemyScript.guns.Add(sc);
                Debug.Log("Ajout du gun: " + child.name); 
            }
        }
    }

}