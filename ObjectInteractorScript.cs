using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractorScript : MonoBehaviour
{
	private GameObject _objectToInteract = null;
	private Transform _objectToInteractTransform = null;
	private bool _isInteracting = false;

	/*
	Pour permettre au joueur de saisir un objet, il suffit de tagger l'objet
	en question avec le tag "Movable" et de lui ajouter un rigidbody.
	Il faut aussi un collider sur le joueur et sur l'objet.
	Il est préférable de mettre le collider de l'objet en trigger pour éviter
	les problèmes de collision.
	La fonction Interact() doit être appelée par un input pour saisir ou lâcher
	l'objet.
	*/
	private void	Update()
	{
		if (_isInteracting)
		{
			Vector3 offset = transform.forward * 1.0f + transform.up * 1.0f; //Permet de positionner automatiquement l'objet devant le joueur
			_objectToInteractTransform.position = transform.position + offset;
		}
	}
	private void	OnCollisionEnter(Collision other)	//Collision est un type de variable qui contient des informations sur la collision
	{													//Dans une variable Collision on peut récupérer le nom de l'objet avec lequel on a collisionné, le tag, le transform, etc...
		if (other.gameObject.CompareTag("Movable") && _isInteracting == false)
			InteractActivate(other.gameObject);
	}
	private void	OnCollisionExit(Collision other)
	{
		if (other.gameObject.CompareTag("Movable") && _isInteracting == false)
			InteractActivate(other.gameObject);
	}
	private void	InteractActivate(GameObject obj)
	{
		if (_objectToInteract == null)
		{
			_objectToInteract = obj;
			_objectToInteractTransform = obj.transform;
		}
		else
		{
			_objectToInteract = null;
			_objectToInteractTransform = null;
		}
	}
	public void		Interact() // Appelé par un input pour saisir ou lâcher l'objet
	{
		if (_objectToInteract != null && _isInteracting == false)
			_isInteracting = true;
		else
			_isInteracting = false;
	}
}
