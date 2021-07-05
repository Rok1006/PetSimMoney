using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class DropCondition
{
	public abstract bool Check(DragDrop draggable);
}
