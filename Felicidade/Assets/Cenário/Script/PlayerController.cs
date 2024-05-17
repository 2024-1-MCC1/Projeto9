using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float velocity=10f;
	public float rotation = 90f;

	void Start ()
	{
		Cursor.lockState = CursorLockMode.Locked;
	
	}
	void FixedUpdate ()
	{
		float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
		float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

		Vector3 dir = new Vector3(x, 0, y) * velocity;

        transform.Translate(dir * Time.deltaTime);

        transform.Rotate(new Vector3(
            mouseY * rotation,
            mouseX * rotation,
			0
			));

    }
}