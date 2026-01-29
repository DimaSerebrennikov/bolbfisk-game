using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BigFishScript : MonoBehaviour
{
    public Rigidbody2D player;
    public Transform playerTransform;
    public static float zeroPlayerVelocity = 2.25f;
    public static float playerVelocity = zeroPlayerVelocity;
    public float rotatingSpeed = 15f;
    public static float velocityMult = 1.3f;
    public Transform targetDirection;
    float targetDirectionDegree;
    EnomAnimationScript EnomAnimationGameObject;
    private void Awake()
    {
        EnomAnimationGameObject = gameObject.GetComponent<EnomAnimationScript>();
        targetDirectionDegree = 0f;
    }
    private void FixedUpdate()
    {
            targetDirection.right = new Vector2(JoystickScript.directionOfJoyStick.x, JoystickScript.directionOfJoyStick.y);
            targetDirectionDegree = targetDirection.rotation.eulerAngles.z;
            player.angularVelocity = 10000f * Time.fixedDeltaTime * CDSaScript.ChooseDirectionAndStabilization
            (playerTransform.rotation.eulerAngles.z, targetDirectionDegree);
        if (JoystickScript.firstInput)
        {
            player.velocity = player.transform.right * playerVelocity;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            EnomAnimationGameObject.StartCorDeathAnimation();
        }
    }
}
