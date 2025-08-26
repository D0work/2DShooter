using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BuffSpwner : MonoBehaviour
{
    public int id_type_buff = 0;

    private void Start()
    {
        Destroy(gameObject, 7.5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShootingController shooting = other.GetComponent<ShootingController>();

            if (shooting != null && id_type_buff == 0)
            {
                shooting.fireRate /= 2f;
            }
            else if (id_type_buff == 1)
            {
                Transform playerTransform = other.transform;
                List<Transform> inactiveChildren = new List<Transform>();

                foreach (Transform child in playerTransform)
                {
                    if (!child.gameObject.activeSelf)
                    {
                        inactiveChildren.Add(child);
                    }
                }

                if (inactiveChildren.Count > 0)
                {
                    int index = Random.Range(0, inactiveChildren.Count);
                    inactiveChildren[index].gameObject.SetActive(true);
                }
            }

            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

    private void OnSceneUnloaded(Scene scene)
    {
        if (this != null)
            Destroy(gameObject);
    }
}
