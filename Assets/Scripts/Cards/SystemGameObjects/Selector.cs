using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{
    CardInteraction _hoveredCard;
    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 50);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            //On first contact;
            CardInteraction ci = hit.transform.gameObject.GetComponent<CardInteraction>();
            if (ci != null && _hoveredCard == null)
            {
                ci.StartHover();
                _hoveredCard = ci;
            }
            else if (ci != null && _hoveredCard == ci)
            {
                //Nothing
            }
            else if (ci != null && _hoveredCard != ci)
            {
                _hoveredCard.EndHover();
                ci.StartHover();
                _hoveredCard = ci;
            }
        }
        else
        {
            if(_hoveredCard != null)
            {
                _hoveredCard.EndHover();
                _hoveredCard = null;
            }
        }
    }
}
