using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotateBoard : MonoBehaviour
{
    private Vector2 previousTouchPosition;
    private Vector3 rotationVelocity = Vector3.zero;
    private bool isDragging = false;
    private float autoRotateSpeed = 0.5f; // Saðdan sola dönüþ hýzý

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Dokunuþ baþladýðýnda
            previousTouchPosition = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (isDragging)
            {
                // Sürüklenirken
                Vector2 touchPosition = Input.mousePosition;
                Vector2 difference = touchPosition - previousTouchPosition;

                // Dönüþ hýzýný hesapla
                Vector3 deltaRotation = new Vector3(difference.y, -difference.x, 0) * 0.2f;
                rotationVelocity = deltaRotation;

                // Anlýk dönüþ uygula
                transform.Rotate(deltaRotation, Space.World);

                previousTouchPosition = touchPosition;
            }
        }
        else
        {
            if (isDragging)
            {
                // Dokunuþ býrakýldýðýnda
                isDragging = false;
            }

            // Momentum efektini uygula
            if (!isDragging && rotationVelocity.magnitude > 0.01f)
            {
                transform.Rotate(rotationVelocity, Space.World);
                rotationVelocity *= 0.99f; // Momentumu yavaþça azalt
            }
            else if (!isDragging)
            {
                // Momentum durduðunda otomatik dönüþe geç
                rotationVelocity = Vector3.zero; // Momentumu sýfýrla
                transform.Rotate(new Vector3(0, autoRotateSpeed, 0), Space.World); // Otomatik dönüþ
            }
        }
    }
}
