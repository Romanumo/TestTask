using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float verticalSpeed = 1;
    [SerializeField] float verticalSpeedIncriment = 1f;
    float verticalSpeedIncrease = 0;

    void Start()
    {
        TimerManager.manager.AddTimer(() => IncreaseVerticalSpeed(verticalSpeedIncriment), 15f, true);
    }

    void Update()
    {
        VerticalMovement();
    }

    public void RestartSpeed() => verticalSpeedIncrease = 0;

    void IncreaseVerticalSpeed(float verticalIncrease) => verticalSpeedIncrease += verticalIncrease;

    void VerticalMovement()
    {
        float vertical = (Input.GetKey(KeyCode.W) ? 1 : -1) * (verticalSpeed + verticalSpeedIncrease);
        this.transform.position += new Vector3(0, vertical, 0) * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
            GlobalLibrary.gameManager.GameOver();
    }
}
