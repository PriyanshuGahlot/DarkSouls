using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour {
    private Camera _cam;
    public float damageRate;
    [SerializeField] private Line _linePrefab;

    public const float RESOLUTION = .1f;

    private Line _currentLine;

    void Start()
    {
         _cam = Camera.main;
    }


    void Update() {
        if (FindObjectOfType<Player>().gameObject.GetComponent<Player>().health>1)
        {
            Vector2 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

            if (Input.GetMouseButtonDown(0))
            {
                _currentLine = Instantiate(_linePrefab, mousePos, Quaternion.identity);
            }

            if (Input.GetMouseButton(0)) _currentLine.SetPosition(mousePos);
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            FindObjectOfType<Player>().gameObject.GetComponent<Player>().health -= Time.deltaTime * damageRate;
        }
    }

}
