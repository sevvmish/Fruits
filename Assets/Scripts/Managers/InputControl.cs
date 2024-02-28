using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputControl : MonoBehaviour
{
    private Camera _camera;

    private Ray ray;
    private RaycastHit hit;
    private readonly float cameraRayCast = 30f;
    private GameManager gm;
    private GameObject lastPlaceForAudioListener;
    private float _cooldown;
    private Vector3 prevPos;

    public void SetData(Camera c)
    {
        _camera = c;
        setAudioListener(_camera.gameObject);
        gm = GameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {        
        if (!gm.IsGameStarted) return;
        if (_cooldown > 0) _cooldown -= Time.deltaTime;

        
        if (Input.GetMouseButton(0) && _cooldown <= 0)
        {
            ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, cameraRayCast))
            {
                /*
                bool isHitPlayer = hit.collider.TryGetComponent(out PlayerControl pc);

                
                if (hit.collider.CompareTag("Player") && isHitPlayer)
                {
                    if (playerUnderControl != null && playerUnderControl.Equals(pc)) return;

                    setAudioListener(pc.gameObject);

                    if (playerUnderControl != null)
                    {
                        playerUnderControl.SetOutline(false);
                    }

                    playerUnderControl = pc;
                    playerUnderControl.SetOutline(true);
                    //gm.GetUI.Lead(playerUnderControl.transform);
                    //print(hit.collider.gameObject.name);
                }
                else if (hit.collider.CompareTag("Ground"))
                {                    
                    if (playerUnderControl != null)
                    {
                        playerUnderControl.OnDestinationPointReached = null;
                        playerUnderControl.GoToPoint(hit.point);
                    }

                    //print(hit.collider.gameObject.name);
                }
                else if (hit.collider.CompareTag("Interactable"))
                {
                    if (playerUnderControl != null)
                    {
                        hit.collider.gameObject.GetComponent<Interactable>().Interact
                            (playerUnderControl);
                    }

                    print(hit.collider.gameObject.name);
                }

                _cooldown = 0.1f;*/
            }
        }
    }

    private void setAudioListener(GameObject g)
    {
        if (lastPlaceForAudioListener != null && lastPlaceForAudioListener.Equals(g)) return;

        if (lastPlaceForAudioListener != null && lastPlaceForAudioListener.TryGetComponent(out AudioListener al))
        {
            al.enabled = false;
        }  
        
        if (g.TryGetComponent(out AudioListener al1))
        {
            al1.enabled = true;
        }
        else
        {
            g.AddComponent<AudioListener>();
        }

        lastPlaceForAudioListener = g;
        
    }
        
}
