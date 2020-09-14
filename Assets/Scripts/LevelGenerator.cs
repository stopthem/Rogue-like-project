using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGenerator : MonoBehaviour
{
    public GameObject layoutRoom;
    public Color startColor, endColor;
    public int distanceToEnd;
    public Transform generationPoint;
    public enum Direction {up, right, down, left};
    public Direction selectedDirection;
    public float xOffset = 18f, yOffset = 10f;
    public LayerMask whatIsRoom;
    private GameObject endRoom;
    private List<GameObject> layoutRoomObjects = new List<GameObject>();
    public RoomPrefabs roomPrefabs;
    private List<GameObject> generatedOutlines = new List<GameObject>();
    public RoomCenter centerStart, centerEnd;
    public RoomCenter[] potentialCenters;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(layoutRoom,generationPoint.position,generationPoint.rotation).GetComponent<SpriteRenderer>().color = startColor;
        selectedDirection = (Direction)Random.Range(0, 4);
        MoveGenerationPoint();
        for (int i = 0; i < distanceToEnd; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generationPoint.position, generationPoint.rotation);
            layoutRoomObjects.Add(newRoom);
            if (i + 1 == distanceToEnd)
            {
               newRoom.GetComponent<SpriteRenderer>().color = endColor;
               layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
               endRoom = newRoom;
            }
            selectedDirection = (Direction)Random.Range(0, 4);
            MoveGenerationPoint();
            while(Physics2D.OverlapCircle(generationPoint.position, .2f, whatIsRoom))
            {
                MoveGenerationPoint();
            }
        }

        CreateRoomOutlines(Vector3.zero);

        foreach (GameObject room in layoutRoomObjects)
        {
            CreateRoomOutlines(room.transform.position);
        }

        CreateRoomOutlines(endRoom.transform.position);

        foreach (GameObject outline in generatedOutlines)
        {
            bool generateCenter = true;
            if (outline.transform.position == Vector3.zero)
            {
                Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generateCenter = false;
            }
            if (outline.transform.position == endRoom.transform.position)
            {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
                generateCenter = false;
            }
            if (generateCenter)
            {
                int centerSelect = Random.Range(0, potentialCenters.Length);

                Instantiate(potentialCenters[centerSelect], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Room>();
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        #if !UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endif
    }
    public void MoveGenerationPoint()
    {
        switch(selectedDirection)
        {
            case Direction.up:
                generationPoint.position += new Vector3(0f,yOffset,0f);
                break;

            case Direction.down:
                generationPoint.position += new Vector3(0f,-yOffset,0f);
                break;

            case Direction.right:
                generationPoint.position += new Vector3(xOffset,0f,0f);
                break;

            case Direction.left:
                generationPoint.position += new Vector3(-xOffset,0f,0f);
                break;
        }
    }
    public void CreateRoomOutlines(Vector3 roomPosition)
    {
        bool roomAbove = Physics2D.OverlapCircle(roomPosition + new Vector3 (0f,yOffset,0f), .2f, whatIsRoom);
        bool roomBelow = Physics2D.OverlapCircle(roomPosition + new Vector3 (0f,-yOffset,0f), .2f, whatIsRoom);
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3 (-xOffset,0f,0f), .2f, whatIsRoom);
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3 (xOffset,0f,0f), .2f, whatIsRoom);
        int directionCount = 0;
        if (roomAbove)
        {
            directionCount++;
        }
        if (roomBelow)
        {
            directionCount++;
        }
        if (roomRight)
        {
            directionCount++;
        }
        if (roomLeft)
        {
            directionCount++;
        }

        switch(directionCount)
        {
            case 0:
                Debug.LogError("Found no exits");
                break;
            
            case 1:
                if (roomAbove)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.singleUp, roomPosition, transform.rotation));
                }
                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.singleLeft, roomPosition, transform.rotation));
                }
                if (roomBelow)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.singleDown, roomPosition, transform.rotation));
                }
                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.singleRight, roomPosition, transform.rotation));
                }
                break;

            case 2:
                if (roomAbove && roomBelow)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.doubleUpDown, roomPosition, transform.rotation));
                }
                if (roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.doubleRightLeft, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.doubleUpRight, roomPosition, transform.rotation));
                }
                if (roomAbove && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.doubleUpLeft, roomPosition, transform.rotation));
                }
                if (roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.doubleDownLeft, roomPosition, transform.rotation));
                }
                if (roomBelow && roomRight)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.doubleDownRight, roomPosition, transform.rotation));
                }
                break;

            case 3:
                if (roomAbove && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.tripleDownUpLeft, roomPosition, transform.rotation));
                }
                if (roomAbove && roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.tripleUpRightLeft, roomPosition, transform.rotation));
                }
                if (roomRight && roomBelow && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.tripleDownRightLeft, roomPosition, transform.rotation));
                }
                if (roomAbove && roomBelow && roomRight)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.tripleUpRightDown, roomPosition, transform.rotation));
                }
                break;

            case 4:
                if (roomAbove && roomBelow && roomRight && roomLeft)
                {
                    generatedOutlines.Add(Instantiate(roomPrefabs.fourWay, roomPosition, transform.rotation));
                }
                break;

        }
    }
}

[System.Serializable]
public class RoomPrefabs
{
    public GameObject singleUp, singleDown, singleRight, singleLeft, 
        doubleUpDown, doubleRightLeft, doubleUpRight, doubleUpLeft , doubleDownRight, doubleDownLeft, 
        tripleUpRightLeft, tripleDownRightLeft, tripleDownUpLeft, tripleUpRightDown, 
        fourWay;
}
