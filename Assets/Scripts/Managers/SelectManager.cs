using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectManager : MonoBehaviour
{
    public static SelectManager Instance { get; private set; }

    public List<CanSelectObject> SelectedObjects = new List<CanSelectObject>();
    GameObject DragBox;

    void Awake()
    {
        if (Instance != null) 
        {
            Destroy(this);
            return;
        } 
        Instance = this;

        DragBox = Resources.Load<GameObject>("DragBox");
        DragBox = Instantiate(DragBox);
        DragBox.SetActive(false);
    }

    void Start()
    {
        InputManager.Instance.playerInput.InGame.M_LeftClick.started += TrySelect;
        InputManager.Instance.playerInput.InGame.M_LeftClick.started += Dragging;
        InputManager.Instance.playerInput.InGame.Command.started += TrySelectPoint;
    }

    // 단일 선택
    private void TrySelect(InputAction.CallbackContext ctx)
    {
        List<CanSelectObject> copy = new List<CanSelectObject> ();
        foreach (CanSelectObject obj in SelectedObjects) 
        {
            copy.Add(obj);
        }
        SelectedObjects.Clear();

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] hitObjects = Physics2D.RaycastAll(mousePosition,Vector2.zero);
        AddCore addCore = null;

        foreach (RaycastHit2D hit in hitObjects) 
        {
            if (hit.collider.GetComponent<CanSelectObject>() && !copy.Contains(hit.collider.GetComponent<CanSelectObject>()))
            {
                SelectObject( hit.collider.GetComponent<CanSelectObject>() );
                StatOverCanvas();
                return;
            }
            if (hit.collider.GetComponent<AddCore>()) 
            {
                addCore = hit.collider.GetComponent<AddCore>();
            }
        }

        if (addCore != null) 
        {
            UIManager.Instance.InstallCore.OpenCanvas(addCore.GetCurrentRoom());
        }

        StatOverCanvas();
    }

    void SelectObject(CanSelectObject Object) 
    {
        SelectedObjects.Add(Object);
    }

    // 드래그
    private void Dragging(InputAction.CallbackContext ctx)
    {
        StartCoroutine(DrawDragBox());
    }

    IEnumerator DrawDragBox() 
    {
        Vector2 startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 endPos = Vector2.zero;
        DragBox.SetActive(true);

        while (InputManager.Instance.playerInput.InGame.M_LeftClick.IsPressed()) 
        {
            endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 min = Vector2.Min(startPos, endPos);
            Vector2 max = Vector2.Max(startPos, endPos);

            DragBox.transform.position = (min + max) / 2;
            DragBox.transform.localScale = new Vector3(max.x - min.x, max.y - min.y);

            yield return null;
        }

        if (DragBox.transform.localScale.x > 0.1f && DragBox.transform.localScale.y > 0.1f) 
        {
            SelectObjectAll(Physics2D.OverlapAreaAll(startPos, endPos));
        }
        DragBox.SetActive(false);
    }

    void SelectObjectAll(Collider2D[] DragedObjects)
    {
        SelectedObjects.Clear();

        foreach (Collider2D col in DragedObjects)
        {
            if (col.GetComponent<Monster>())
            {
                SelectedObjects.Add(col.GetComponent<CanSelectObject>());
            }
        }

        StatOverCanvas();
    }

    void StatOverCanvas() 
    {
        if (SelectedObjects.Count == 1)
        {
            UIManager.Instance.OverInfoState(SelectedObjects[0]);
        }
        else
        {
            UIManager.Instance.SelectObjectInfo.GetComponent<Canvas>().enabled = false;
        }
    }

    // 이동또는 공격명령(우클릭)
    enum selectedPlace { none, room, enemy, Core };
    void TrySelectPoint(InputAction.CallbackContext ctx) 
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Room room = null;
        RaycastHit2D[] hitObjects = Physics2D.RaycastAll(mousePosition, Vector2.zero);
        selectedPlace selected = selectedPlace.none;

        GameObject target = null;

        foreach (RaycastHit2D hit in hitObjects)
        {
            Debug.Log(hit.collider.GetInstanceID());
            // 방만 클릭되었을 때
            if (hit.collider.GetComponent<Room>())
            {
                selected = selectedPlace.room;
                room = hit.collider.GetComponent<Room>();
            }
            // 적(모험가) 클릭 시
            else if (hit.collider.GetComponent<Adventurer>() && !SelectedObjects.Contains(hit.collider.GetComponent<CanSelectObject>()))
            {
                selected = selectedPlace.enemy;
                target = hit.collider.gameObject;
                break;
            }
            // 시설 클릭 시
            else if (hit.collider.GetComponent<CoreFacilities>() && !SelectedObjects.Contains(hit.collider.GetComponent<CanSelectObject>())) 
            {
                selected = selectedPlace.Core;
                target = hit.collider.gameObject;
                break;
            }
        }

        switch (selected) 
        {
            case selectedPlace.none:
                break;
            case selectedPlace.room:
                MoveCommand(mousePosition, room);
                break;
            case selectedPlace.enemy:
                AttackCommand(target.GetComponent<CanSelectObject>());
                break;
            case selectedPlace.Core:
                UseCoreCommand(target);
                break;
        }
    }

    void UseCoreCommand(GameObject targetCore) 
    {
        print("Use : " + targetCore.name);
        foreach (CanSelectObject monster in SelectedObjects)
        {
            if (monster == null) 
            {
                continue;
            }
            // 몬스터 아니면 스킵
            if (!monster.GetComponent<Monster>()) 
            {
                continue;
            } 
            monster.GetComponent<Monster>().setTargetCore(targetCore);
            print("monsterID : " + monster.GetInstanceID() + "/ targetCore : " + targetCore.GetInstanceID());
            monster.GetComponent<Monster>().Path = RoomManager.Instance.FindPath(monster.GetComponent<Monster>(), targetCore.GetComponent<CanSelectObject>().currentRoom);
        }
    }

    void MoveCommand(Vector2 targetPos, Room targetRoom)
    {
        foreach (CanSelectObject monster in SelectedObjects) 
        {
            if (monster == null)
            {
                continue;
            }
            // 몬스터 아니면 스킵
            if (!monster.GetComponent<Monster>()) 
            {
                continue;
            }
            monster.GetComponent<Monster>().setTargetPos(targetPos, targetRoom);
            print("Move to " + targetRoom.name + " : " + targetRoom.GetInstanceID());
            monster.GetComponent<Monster>().Path = RoomManager.Instance.FindPath(monster.GetComponent<Monster>(), targetRoom);
        }
    }

    void AttackCommand(CanSelectObject targetEnemy) 
    {
        print("Attack!" + targetEnemy.name);
        foreach (CanSelectObject monster in SelectedObjects) 
        {
            if (monster == null)
            {
                continue;
            }
            // 몬스터 아니면 스킵
            if (!monster.GetComponent<Monster>())
            {
                continue;
            }
            monster.GetComponent<Monster>().setTargetObject(targetEnemy.gameObject);
            monster.GetComponent<Monster>().Path = RoomManager.Instance.FindPath(monster.GetComponent<Monster>(), targetEnemy.GetComponent<CanMoveObjects>().currentRoom);
        }
    }
}
