using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//needed to be here
public class DropArea : MonoBehaviour
{
  	public List<DropCondition> DropConditions = new List<DropCondition>();
	public event Action<DragDrop> OnDropHandler;

	public bool Accepts(DragDrop draggable)
	{
		return DropConditions.TrueForAll(cond => cond.Check(draggable));
	}

	public void Drop(DragDrop draggable)
	{
		OnDropHandler?.Invoke(draggable);
	}
}
