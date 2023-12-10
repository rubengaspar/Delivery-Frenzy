using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContainerObject : MonoBehaviour
{
    [SerializeField] TMP_Text _pointsText;
    [SerializeField] GameObject player;
    [SerializeField] BoxObject.ColorType colorType;
    private int boxesDelivered = 0;
    

    // Trigger detection
    private void OnTriggerEnter(Collider other)
    {
        BoxObject box = other.GetComponent<BoxObject>();
        if (box != null)
        {
            if (box.colorType == this.colorType)
            {
                float boxScore = box.GetCurrentPoints();

                PlayerController playerController = player.GetComponent<PlayerController>();
                if (playerController != null)
                {
                    // Drop Box and Add Score
                    playerController.DropAction();
                    playerController.AddScore(boxScore);
                }
            }

            boxesDelivered++;

            if (_pointsText != null)
            {
                _pointsText.text = $"{boxesDelivered:F0}";
            }
                        

            Destroy(other.gameObject);
        }
    }
}
