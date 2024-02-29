using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class RotateBoard : MonoBehaviour
{
    private Vector2 previousTouchPosition;
    private Vector3 rotationVelocity = Vector3.zero;
    private bool isDragging = false;
    private float autoRotateSpeed = 0.5f; // Sa�dan sola d�n�� h�z�

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Dokunu� ba�lad���nda
            previousTouchPosition = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButton(0))
        {
            if (isDragging)
            {
                // S�r�klenirken
                Vector2 touchPosition = Input.mousePosition;
                Vector2 difference = touchPosition - previousTouchPosition;

                // D�n�� h�z�n� hesapla
                Vector3 deltaRotation = new Vector3(difference.y, -difference.x, 0) * 0.2f;
                rotationVelocity = deltaRotation;

                // Anl�k d�n�� uygula
                transform.Rotate(deltaRotation, Space.World);

                previousTouchPosition = touchPosition;
            }
        }
        else
        {
            if (isDragging)
            {
                // Dokunu� b�rak�ld���nda
                isDragging = false;
            }

            // Momentum efektini uygula
            if (!isDragging && rotationVelocity.magnitude > 0.01f)
            {
                transform.Rotate(rotationVelocity, Space.World);
                rotationVelocity *= 0.99f; // Momentumu yava��a azalt
            }
            else if (!isDragging)
            {
                // Momentum durdu�unda otomatik d�n��e ge�
                rotationVelocity = Vector3.zero; // Momentumu s�f�rla
                transform.Rotate(new Vector3(0, autoRotateSpeed, 0), Space.World); // Otomatik d�n��
            }
        }
    }
}
