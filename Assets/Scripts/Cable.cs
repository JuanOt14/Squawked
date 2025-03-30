using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public SpriteRenderer wireEnd;
    Vector3 startPoint;
    Vector3 startPosition;
    private Victoria victoria;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = transform.parent.position;
        startPosition = transform.parent.position;
        victoria = transform.root.gameObject.GetComponent<Victoria>();
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(mousePosition, .2f);
        foreach(Collider2D collider in colliders){
            if(collider.gameObject != gameObject){
                UpdateWire(collider.transform.position);
                if (transform.parent.name.Equals(collider.transform.parent.name)){
                    collider.GetComponent<Cable>()?.Done();
                    Done();
                    victoria.conexionesVictoria++;
                    victoria.ComprobarVictoria();
                }
                return;
            }
        }

        UpdateWire(mousePosition);
        
    }

    void Done(){
        Destroy(this);
    }

    private void OnMouseUp(){
        UpdateWire(startPosition);
    }

    void UpdateWire(Vector3 mousePosition){
        transform.position = mousePosition;

        Vector2 direction = mousePosition - startPoint;
        transform.right = direction * transform.lossyScale.x;

        float dist = Vector2.Distance(startPoint, mousePosition);
        wireEnd.size = new Vector2(dist, wireEnd.size.y);

    }
    
}
