using UnityEngine;

/// <summary>
/// GestureAction performs custom actions based on 
/// which gesture is being performed.
/// </summary>
public class GestureAction : MonoBehaviour
{
    [Tooltip("Rotation max speed controls amount of rotation.")]
    public float RotationSensitivity = 10.0f;

    public float MaxScale = 2f;
    public float MinScale = 0.1f;

    private Vector3 manipulationPreviousPosition;

    private float rotationFactor;

    void Update()
    {
        //每帧都要执行，连续的旋转
       // PerformRotation();
    }

    void PerformZoomUpdate(Vector3 position)
    {
        if (GestureManager.Instance.IsNavigating && HandsManager.Instance.FocusedGameObject == gameObject)
        {
            Vector3 deltaScale = Vector3.zero;
            float ScaleValue = 0.01f;
            //设置每一帧缩放的大小
            if (position.x < 0)
            {
                ScaleValue = -1 * ScaleValue;
            }
            //当缩放超出设置的最大，最小范围时直接返回
            if (transform.localScale.x >= MaxScale && ScaleValue > 0)
            {
                return;
            }
            else if (transform.localScale.x <= MinScale && ScaleValue < 0)
            {
                return;
            }
            //根据比例计算每个方向上的缩放大小
            deltaScale.x = ScaleValue;
            deltaScale.y = (transform.localScale.y / transform.localScale.x) * ScaleValue;
            deltaScale.z = (transform.localScale.z / transform.localScale.x) * ScaleValue;
            transform.localScale += deltaScale;
        }
    }
    private void PerformRotation()
    {
        if (GestureManager.Instance.IsNavigating && HandsManager.Instance.FocusedGameObject == gameObject)
        {
            /* TODO: DEVELOPER CODING EXERCISE 2.c */

            // 2.c: Calculate rotationFactor based on GestureManager's NavigationPosition.X and multiply by RotationSensitivity.
            // This will help control the amount of rotation.
            rotationFactor = GestureManager.Instance.NavigationPosition.x * RotationSensitivity;

            // 2.c: transform.Rotate along the Y axis using rotationFactor.
            transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
        }
    }

    void PerformManipulationStart(Vector3 position)
    {
        manipulationPreviousPosition = position;
    }

    void PerformManipulationUpdate(Vector3 position)
    {
        Debug.Log("PerformMan.......");
        if (GestureManager.Instance.IsManipulating)
        {
            /* TODO: DEVELOPER CODING EXERCISE 4.a */

            Vector3 moveVector = Vector3.zero;
            // 4.a: Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationPreviousPosition;
            // 4.a: Update the manipulationPreviousPosition with the current position.
            manipulationPreviousPosition = position;

            // 4.a: Increment this transform's position by the moveVector.
            transform.position += moveVector;
        }
    }
}