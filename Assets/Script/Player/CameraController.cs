using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform PlayerTransform;
    public Rigidbody PlayerBody;
    private float MOUSE_HORIZONTAL;
    private float MOUSE_VERTICAL;
    private float MOUSE_WHEEL;
    private float CameraHorizontalAngle;
    private float CameraCurrentVerticalOffset;
    private float CameraTargetXAxis;
    private float CameraTargetZAxis;
    private float CameraTargetYAxis;
    private float CameraLookAtTargetAdditionHeight;
    //private float CameraFrameXAxis;
    //private float CameraFrameZAxis;
    private float CameraFrameYAxis;
    private float CameraLookAtFrameAdditionHeight;
    private Vector3 CameraFollowFramePosition;
    private static float CameraMaxVerticalOffset = 2;
    private static float CameraDefaultHorizontalMagnitude = 4;
    private static float CameraDefaultVerticalOffset = 6;
    private static float CamareMouseScrollWheelPower = 600;
    private static float CameraMouseSensivity = 4;
    private static float CameraLookAtMinAdditionHeight = 1;
    private static float CameraLookAtMaxAdditionHeight = 3;
    private static float CameraFollowVelocityPower = 1.2f;
    private static float CameraLookAtAdditionalHeigthPower = 10;

    void Update()
    {
        MOUSE_HORIZONTAL = Input.GetAxis("Mouse X");
        MOUSE_VERTICAL = Input.GetAxis("Mouse Y");
        MOUSE_WHEEL = Input.GetAxis("Mouse ScrollWheel");
    }

    private void LateUpdate() {
        RotateCamera();
    }

    private void RotateCamera()
    {
        CameraHorizontalAngle += MOUSE_HORIZONTAL * Time.deltaTime * CameraMouseSensivity;
        CameraCurrentVerticalOffset += MOUSE_WHEEL * Time.deltaTime * CamareMouseScrollWheelPower;
        if (CameraCurrentVerticalOffset > CameraMaxVerticalOffset) 
        {
            CameraCurrentVerticalOffset = CameraMaxVerticalOffset;
        } 
        else if (CameraCurrentVerticalOffset < -CameraMaxVerticalOffset)
        {
            CameraCurrentVerticalOffset = -CameraMaxVerticalOffset;
        }
        CameraLookAtTargetAdditionHeight += MOUSE_VERTICAL * Time.deltaTime * CameraLookAtAdditionalHeigthPower;
        if (CameraLookAtTargetAdditionHeight > CameraLookAtMaxAdditionHeight) 
        {
            CameraLookAtTargetAdditionHeight = CameraLookAtMaxAdditionHeight;
        }
        else if (CameraLookAtTargetAdditionHeight < CameraLookAtMinAdditionHeight)
        {
            CameraLookAtTargetAdditionHeight = CameraLookAtMinAdditionHeight;
        }
        
        CameraTargetXAxis = Mathf.Sin(CameraHorizontalAngle) * CameraDefaultHorizontalMagnitude;
        CameraTargetZAxis = Mathf.Cos(CameraHorizontalAngle) * CameraDefaultHorizontalMagnitude;
        CameraTargetYAxis = PlayerTransform.position.y + CameraCurrentVerticalOffset + CameraDefaultVerticalOffset;

        //CameraFrameXAxis = Mathf.Lerp(CameraFrameXAxis, CameraTargetXAxis, Time.deltaTime);
        //CameraFrameZAxis = Mathf.Lerp(CameraFrameZAxis, CameraTargetZAxis, Time.deltaTime);
        CameraFrameYAxis = Mathf.Lerp(CameraFrameYAxis, CameraTargetYAxis, Time.deltaTime);
        CameraLookAtFrameAdditionHeight = Mathf.Lerp(CameraLookAtFrameAdditionHeight, CameraLookAtTargetAdditionHeight, Time.deltaTime);

        CameraFollowFramePosition.x = Mathf.Lerp(CameraFollowFramePosition.x, PlayerTransform.transform.position.x, Time.deltaTime);
        CameraFollowFramePosition.y = Mathf.Lerp(CameraFollowFramePosition.y, PlayerTransform.transform.position.y, Time.deltaTime);
        CameraFollowFramePosition.z = Mathf.Lerp(CameraFollowFramePosition.z, PlayerTransform.transform.position.z, Time.deltaTime);

        transform.position = PlayerTransform.position + new Vector3(CameraTargetXAxis, CameraFrameYAxis, CameraTargetZAxis);

        transform.LookAt(CameraFollowFramePosition + new Vector3(0, CameraLookAtFrameAdditionHeight, 0));
    }

}
