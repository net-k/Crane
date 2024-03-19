using System; //EventHandlerに必要
using UnityEngine;
using System.Collections;
using TouchScript.Gestures.TransformGestures; //ScreenTransformGestureに必要
using TouchScript.Gestures.TransformGestures.Base; //DealtaPositionに必要

 

public class CameraController : MonoBehaviour
{
    [SerializeField]
    ScreenTransformGesture transformGesture;
    
    //回転させるオブジェクト。
    [SerializeField]
    Transform controlObject; 
  
    //タッチ入力分だけしか回転させないなら必要ない。
    float rotateSpeed = 20.0f;
   
    //カメラの回転に制限をつけないなら必要がない。
    //この場合は左右110度までしか回転せず、方向転換はできない。
    [SerializeField]
    float limitRotateX = 110.0f, limitRotateY = 90.0f;
    float startRotateX, startRotateY;

    void Start()
    {
        startRotateX = controlObject.transform.rotation.eulerAngles.x; 
        startRotateY = controlObject.transform.rotation.eulerAngles.y;
    }

    void OnEnable()
    {
        transformGesture.Transformed += OnRotating;
    }

    void OnDisable()
    {
        transformGesture.Transformed -= OnRotating;
    }

    void OnRotating(object sender, EventArgs e)
    {
        Vector3 deltaPos = transformGesture.DeltaPosition;
        float rotateX = deltaPos.x * rotateSpeed;
        float rotateY = deltaPos.y * rotateSpeed;

        //transform.eulerAnglesに直接座標を指定してるのでここがなければ常にdeltaの座標を向くようになる。
        startRotateX = startRotateX + rotateY;
        startRotateY = startRotateY + rotateX;
 
        controlObject.transform.eulerAngles = new Vector3(Mathf.Clamp(-startRotateX, -limitRotateX, limitRotateX), Mathf.Clamp(startRotateY, -limitRotateY, limitRotateY), 0);
    }
}
