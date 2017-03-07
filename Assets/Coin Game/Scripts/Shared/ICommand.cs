using UnityEngine;
using UnityEngine.EventSystems;

public interface ICommand : IPointerEnterHandler
{
	void OnTriggerEnter2D(Collider2D collider);

    void OnMouseEnter();

    void Execute();
}

