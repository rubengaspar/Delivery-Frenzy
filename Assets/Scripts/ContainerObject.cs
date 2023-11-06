using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ContainerObject : MonoBehaviour
{
    [SerializeField] TMP_Text _pointsText;
    private float totalPoints = 0;

    // List to store the boxes that are currently on top of the container
    private List<BoxObject> boxesOnContainer = new List<BoxObject>();

    // Trigger detection
    private void OnTriggerEnter(Collider other)
    {
        BoxObject box = other.GetComponent<BoxObject>();
        if (box != null)
        {
            Debug.Log("Box Delivered with Status: " + box.deliveryStatus);

            if (box.deliveryStatus == BoxObject.DeliveryStatus.NotDelivered)
            {
                box.deliveryStatus = BoxObject.DeliveryStatus.Delivered;
            }

            boxesOnContainer.Add(box);
            UpdatePoints(box, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        BoxObject box = other.GetComponent<BoxObject>();
        if (box != null)
        {
            boxesOnContainer.Remove(box);
            UpdatePoints(box, false);
            box.deliveryStatus = BoxObject.DeliveryStatus.Stolen;
        }
    }

    private void UpdatePoints(BoxObject box, bool boxEntered)
    {
        if (boxEntered)
        {
            totalPoints += box.GetCurrentPoints();
        }
        else
        {
            totalPoints -= box.GetCurrentPoints();
        }

        if (_pointsText != null)
        {
            _pointsText.text = $"{totalPoints:F1}";
        }

    }
}
