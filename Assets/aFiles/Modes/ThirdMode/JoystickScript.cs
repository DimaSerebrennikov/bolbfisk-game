using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JoystickScript : MonoBehaviour// размер этого объекта имеет целевое устройство - iphone8+
{
    RectTransform handleRect;
    public GameObject handle;
    Vector2 startPositionForHandle;
    float radius;
    public static  Vector2 directionOfJoyStick;
    public static float directionOfJoyStickDegree;
    float ScreenFactor;//real size less iphone8+ = screen scaling, more iphone6 = physical scaling;
    public RectTransform JoystickPanel;
    float sizeOfJoystickWithImage;
    float inchesIphone8P = 4.79f;
    float heightIphone8P = 1920f;
    float indentForJoystickFromBottom = 1.5f;
    float inches;
    float deviceDpi;
    public static bool firstInput;

    private void Awake()
    {
        //=========================================================== Объявление
        firstInput = false;
        directionOfJoyStickDegree = 0f;
        float screenSize = Screen.height;
        directionOfJoyStick = new Vector2();
        //=========================================================== Объявление
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Если устройство меньше айфона8+, то изображение уменьшается за разрешением экрана
        //если же больше, то физически размер остаётся прежним
        //----------------------------------------------------------- Если устройство не даёт значение dpi
        if (Screen.dpi == 0)
        {
            deviceDpi = 450f;
        }
        else
        {
            deviceDpi = Screen.dpi;
        }
        //----------------------------------------------------------- Если устройство не даёт значение dpi
            //----------------------------------------------------------- Если больше по дюймам iphone8+
            inches = screenSize / deviceDpi;//if bigger 1, it's more than setted inches and it'll fade
            if (inches / inchesIphone8P > 1)
            {
                ScreenFactor = inchesIphone8P / inches;
            }
            //----------------------------------------------------------- Если больше по дюймам iphone8+
            //----------------------------------------------------------- Если меньше по дюймам iphone8+
            else
            {
                ScreenFactor = screenSize / heightIphone8P;
            }
            //----------------------------------------------------------- Если меньше по дюймам iphone8+
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - Если устройство меньше айфона8+, то изображение уменьшается за разрешением экрана
        //если же больше, то физически размер остаётся прежним
        sizeOfJoystickWithImage = JoystickPanel.GetComponent<Image>().sprite.rect.height;
        handleRect = handle.GetComponent<RectTransform>();
        //для планшета 12 = 3 пикселя

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - set location and scale for objects of joystick
        JoystickPanel.localScale = new Vector2(JoystickPanel.localScale.x * ScreenFactor, JoystickPanel.localScale.y * ScreenFactor);
        handleRect.localScale = new Vector2(handleRect.localScale.x * ScreenFactor, handleRect.localScale.y * ScreenFactor);

        JoystickPanel.position = new Vector2(JoystickPanel.position.x,
            sizeOfJoystickWithImage * (indentForJoystickFromBottom + inches/10) * JoystickPanel.localScale.y);

        handleRect.position = new Vector2(JoystickPanel.position.x,
            sizeOfJoystickWithImage * (indentForJoystickFromBottom + inches/10) * JoystickPanel.localScale.y);

        startPositionForHandle = handleRect.position;  
        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - set location and scale for objects of joystick
        radius = ScreenFactor * 250f;
    } 
    private void Update()
    {
        //=========================================================== Джойстик двигается только по радиусу и выдаёт от этого собственно-нормализированное
        if (Input.touchCount > 0)
        {
            firstInput = true;
            Touch touch = Input.GetTouch(0);
            handleRect.position = touch.position - startPositionForHandle;
            handleRect.position = handleRect.position.normalized * radius;
            handleRect.position = new Vector2(handleRect.position.x + startPositionForHandle.x,
                handleRect.position.y + startPositionForHandle.y);
            directionOfJoyStick = new Vector2(handleRect.position.x - startPositionForHandle.x,
                handleRect.position.y - startPositionForHandle.y);
        }
        //=========================================================== Джойстик двигается только по радиусу и выдаёт от этого собственно-нормализированное
    }
}
