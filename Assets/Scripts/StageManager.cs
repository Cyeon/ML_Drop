using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startFloor = null;
    [SerializeField]
    private Renderer floor = null;

    public TextMeshPro text = null;

    private Material originalMaterial = null;
    private Material changeMaterial = null;

    [SerializeField]
    private List<MovingObject> movingObjects = new List<MovingObject>();
    //[SerializeField]
    //private List<Cannon> cannons = new List<Cannon>();

    public void Init()
    {
        originalMaterial = floor.material;
        foreach (MovingObject movingObject in movingObjects)
        {
            movingObject.Init();
        }
    }

    public void StageSet()
    {
        startFloor.SetActive(true);
        foreach (MovingObject obj in movingObjects)
        {
            obj.Move();
        }

        //foreach (Cannon cannon in cannons)
        //{
        //    cannon.SetFire(true);
        //    StartCoroutine(cannon.Fire());
        //}
        StartCoroutine(FloorSet());
    }

    private IEnumerator FloorSet()
    {
        for (int i = 3; i > 0; i--)
        {
            text.SetText(i.ToString());
            yield return new WaitForSeconds(1f);
        }
        text.SetText("DROP!");

        startFloor.SetActive(false);
    }

    private IEnumerator FloorColorChange()
    {
        floor.material = changeMaterial;
        yield return new WaitForSeconds(1f);
        floor.material = originalMaterial;
    }

    public void FloorColor(Material material)
    {
        changeMaterial = material;
        StartCoroutine(FloorColorChange());
    }

    public void AllObjectStop()
    {
        foreach (MovingObject obj in movingObjects)
        {
            obj.ObjectStop();
        }

        //foreach (Cannon cannon in cannons)
        //{
        //    cannon.SetFire(false);
        //}
    }
}