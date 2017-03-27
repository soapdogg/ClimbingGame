using UnityEngine;
using UnityEngine.EventSystems;

public interface ICommand : IPointerEnterHandler
{
	void OnTriggerEnter(Collider collider);

    void OnMouseEnter();

    void Execute();
}

