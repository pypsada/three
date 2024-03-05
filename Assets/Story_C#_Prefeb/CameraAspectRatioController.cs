using UnityEngine;


[RequireComponent(typeof(Camera))]
public class CameraAspectRatioController : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        UpdateCameraViewport();
    }

    void Update()
    {
        // ��Update�м�ⴰ�ڴ�С�ı仯����ʵʱ����������ӿ�
        if (Screen.width != cam.pixelWidth || Screen.height != cam.pixelHeight)
        {
            UpdateCameraViewport();
        }
    }

    void UpdateCameraViewport()
    {
        float targetAspect = 16f / 9f; // Ŀ���߱�

        float windowAspect = (float)Screen.width / (float)Screen.height; // ��ǰ���ڿ�߱�

        float scaleHeight = windowAspect / targetAspect; // ���㵱ǰ���ڿ�߱���Ŀ���߱ȵı�ֵ

        Rect rect = cam.rect;

        if (scaleHeight < 1.0f) // ������ڿ�߱�С��Ŀ���߱ȣ���ζ�Ŵ��ڹ���
        {
            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;
        }
        else // ������ڿ�߱ȴ���Ŀ���߱ȣ���ζ�Ŵ��ڹ�խ
        {
            float scaleWidth = 1.0f / scaleHeight; // �����ȱ�ֵ
            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;
        }

        cam.rect = rect; // ����������ӿ�
        cam.ResetAspect();
    }
}
