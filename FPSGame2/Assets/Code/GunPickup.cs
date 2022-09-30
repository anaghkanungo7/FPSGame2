using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunPickup : MonoBehaviour
{
    public TextMeshProUGUI gunObjectiveText;


    private void OnTriggerStay(Collider other) {
        if (Input.GetKeyDown(KeyCode.F) && (other.CompareTag("Player"))) {
            Debug.Log(other.name);
            other.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            gunObjectiveText.fontStyle = FontStyles.Strikethrough;

        }

    }
}
