using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRepair : MonoBehaviour
{
    public float repaairTime = 5f;
    private float preTime;
    private bool switchOn = false;
    private Vector3Int pos;
    public GameObject slidder;
    public Vector3 offsetPos;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (GameManager.getTile(GameManager.tilemap.WorldToCell(transform.position) + Vector3Int.up))
            {
                slidder.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.position)+offsetPos;
                pos = GameManager.tilemap.WorldToCell(transform.position);
                preTime = Time.time;
                switchOn = true;
                slidder.SetActive(true);
                slidder.GetComponent<Slider>().value = 0f;
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            slidder.GetComponent<Slider>().value = (Time.time - preTime) / repaairTime;
            if (Time.time - preTime > repaairTime && GameManager.tilemap.WorldToCell(transform.position) == pos && switchOn)
            {
                GameManager.fixTile(pos + Vector3Int.up);
                switchOn = false;
                slidder.SetActive(false);
            }
        }
        if (Input.GetKeyUp(KeyCode.R)|| GameManager.tilemap.WorldToCell(transform.position) != pos)
        {
            switchOn = false;
            slidder.SetActive(false);
        }
           
    }
}
