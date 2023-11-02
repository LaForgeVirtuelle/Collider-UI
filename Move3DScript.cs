using System.Collections;							//Librairie de base de Unity
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;						//Librairie pour les inputs
using TMPro;										//Librairie pour les textes

public class Move3DScript : MonoBehaviour			//Classe pour le déplacement du joueur qui hérite de MonoBehaviour
{
	#region private variables
	private float angleRad = -45f * Mathf.Deg2Rad;	//Angle de rotation en radian
	private float deplacementX = 0f;				//Déplacement en X
	private float deplacementZ = 0f;				//Déplacement en Z
	private Rigidbody rbcube;						//Rigidbody du joueur
	[SerializeField] private float speed = 5f;		//Vitesse du joueur
	#endregion
	#region public variables
	public TextMeshProUGUI deplacementXText;		//Texte pour le déplacement en X
	public TextMeshProUGUI deplacementYText;		//Texte pour le déplacement en Z
	#endregion

	private void Start()							//Fonction au lancement du jeu
	{
		rbcube = GetComponent<Rigidbody>();			//On récupère le rigidbody du joueur
	}

	private void FixedUpdate()								//Fonction appelée à chaque frame
	{
		float diagonalSpeed = Mathf.Sqrt(speed * speed / 2); // La vitesse en diagonale sera sqrt(0.5 * speed^2)
		Vector3 movement = new Vector3(deplacementX, 0, deplacementZ).normalized;		//On récupère le vecteur de déplacement

		if (movement.magnitude > 0)	//Si le vecteur de déplacement est supérieur à 0 (si le joueur se déplace)
		{
			rbcube.velocity = movement * (movement.magnitude > 0.5f ? diagonalSpeed : speed); //On applique la vitesse au joueur
			Quaternion rotation = Quaternion.LookRotation(movement, Vector3.up); //On récupère la rotation du joueur pour qu'il regarde dans la direction du déplacement
			transform.rotation = rotation;	//On applique la rotation au joueur
		}
		else
			rbcube.velocity = Vector3.zero;	//Sinon on met la vitesse à 0
	}
	public void EcouteMouvementWASD(InputAction.CallbackContext context) //Fonction pour récuperer les inputs
	{
		float deplacementXRaw = context.ReadValue<Vector2>().x;	//On récupère le déplacement en X, la valeur est comprise entre -1 et 1
		float deplacementZRaw = context.ReadValue<Vector2>().y;	//On récupère le déplacement en Z, la valeur est comprise entre -1 et 1
		deplacementX = Mathf.Cos(angleRad) * deplacementXRaw - Mathf.Sin(angleRad) * deplacementZRaw; //On applique la rotation au déplacement
		deplacementZ = Mathf.Sin(angleRad) * deplacementXRaw + Mathf.Cos(angleRad) * deplacementZRaw; //On applique la rotation au déplacement
		deplacementXText.text = "Value x " + deplacementX.ToString();	//On affiche la valeur du déplacement en X dans un texte qui est sur l'UI
		deplacementYText.text = "Value y " + deplacementZ.ToString();	//On affiche la valeur du déplacement en Z dans un texte qui est sur l'UI
	}
}
