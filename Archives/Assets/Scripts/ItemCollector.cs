using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int apples = 0;
    [SerializeField] private TextMeshProUGUI itemText;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CollectableItem"))
        {
            Destroy(collision.gameObject);
            apples++;
            itemText.text = "Items: " + apples;
            
        }
    }
}
