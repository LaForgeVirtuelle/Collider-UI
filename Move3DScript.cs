using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Move3DScript : MonoBehaviour
{
	#region private variables
	private float angleRad = -45f * Mathf.Deg2Rad;
	private float deplacementX = 0f;
	private float deplacementZ = 0f;
	private Rigidbody rbcube;
	[SerializeField] private float speed = 5f;
	#endregion
	#region public variables
	public TextMeshProUGUI deplacementXText;
	public TextMeshProUGUI deplacementYText;

	#endregion


	private void Start()
	{
		rbcube = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		float diagonalSpeed = Mathf.Sqrt(speed * speed / 2); // La vitesse en diagonale sera sqrt(0.5 * speed^2)

		Vector3 movement = new Vector3(deplacementX, 0, deplacementZ).normalized;

		if (movement.magnitude > 0)
		{
			rbcube.velocity = movement * (movement.magnitude > 0.5f ? diagonalSpeed : speed);
			Quaternion rotation = Quaternion.LookRotation(movement, Vector3.up);
			transform.rotation = rotation;
		}
		else
		{
			rbcube.velocity = Vector3.zero;
		}
	}
	public void EcouteMouvementWASD(InputAction.CallbackContext context)
	{
		float deplacementXRaw = context.ReadValue<Vector2>().x;
		float deplacementZRaw = context.ReadValue<Vector2>().y;
		deplacementX = Mathf.Cos(angleRad) * deplacementXRaw - Mathf.Sin(angleRad) * deplacementZRaw;
		deplacementZ = Mathf.Sin(angleRad) * deplacementXRaw + Mathf.Cos(angleRad) * deplacementZRaw;
		deplacementXText.text = "Value x " + deplacementX.ToString();
		deplacementYText.text = "Value y " + deplacementZ.ToString();
	}
	public void EcouteMouvementJUMP(InputAction.CallbackContext context)
	{
	}
}
