using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Group : MonoBehaviour
{

    public int[] permutationOneX;
    public int[] permutationOneY;
    public int[] permutationTwoX;
    public int[] permutationTwoY;
    public int[] permutationThreeX;
    public int[] permutationThreeY;
    public int[] permutationFourX;
    public int[] permutationFourY;
    //public int[][] permutationArray;
    public int currentPermutation = 0;
	// Time since last gravity tick
	float lastFall = 0;

	bool isValidGridPos ()
	{
		foreach (Transform child in transform) {
			Vector2 v = Grid.roundVec2 (child.position);

			if (!Grid.insideBorder (v))
				return false;

			if (Grid.grid [(int)v.x, (int)v.y] != null &&
				Grid.grid [(int)v.x, (int)v.y].parent != transform)
				return false;
		}
		return true;
	}

	void updateGrid ()
	{
		for (int y= 0; y < Grid.h; ++y) {
			for (int x = 0; x < Grid.w; ++x) {
				if (Grid.grid [x, y] != null) {
					if (Grid.grid [x, y].parent == transform) {
						Grid.grid [x, y] = null;
					}
				}
			}
		}

		foreach (Transform child in transform) {
			Vector2 v = Grid.roundVec2 (child.position);
			Grid.grid [(int)v.x, (int)v.y] = child;
		}
	}

    void rotateGroup()
    {
        int[] permutationCurrentX;
        int[] permutationCurrentY;
        if (currentPermutation == 0)
        {
            permutationCurrentX = permutationOneX;
            permutationCurrentY = permutationOneY;
        }
        else if (currentPermutation == 1)
        {
            permutationCurrentX = permutationTwoX;
            permutationCurrentY = permutationTwoY;
        }
        else if (currentPermutation == 2)
        {
            permutationCurrentX = permutationThreeX;
            permutationCurrentY = permutationThreeY;
        }
        else
        {
            permutationCurrentX = permutationFourX;
            permutationCurrentY = permutationFourY;
        }
        Transform childOne = transform.GetChild(0);
        Transform childTwo = transform.GetChild(1);
        Transform childThree = transform.GetChild(2);
        Transform childFour = transform.GetChild(3);

        childOne.localPosition = new Vector2(permutationCurrentX[0], permutationCurrentY[0]);
        childTwo.localPosition = new Vector2(permutationCurrentX[1], permutationCurrentY[1]);
        childThree.localPosition = new Vector2(permutationCurrentX[2], permutationCurrentY[2]);
        childFour.localPosition = new Vector2(permutationCurrentX[3], permutationCurrentY[3]);
    }

    // KICK SYSTEM
    bool kick(int prior, int next)
    {
        if (transform.name != "GroupI 1(Clone)")
        {
            if (prior == 0 && next == 1)
            {
                if (tryTest(-1, 0)) return true;
                else if (tryTest(-1, 1)) return true;
                else if (tryTest(0, -2)) return true;
                else if (tryTest(-1, -2)) return true;
            }
            else if (prior == 1 && next == 0)
            {
                if (tryTest(1, 0)) return true;
                else if (tryTest(1, -1)) return true;
                else if (tryTest(0, 2)) return true;
                else if (tryTest(1, 2)) return true;
            }
            else if (prior == 1 && next == 2)
            {
                if (tryTest(1, 0)) return true;
                else if (tryTest(1, -1)) return true;
                else if (tryTest(0, 2)) return true;
                else if (tryTest(1, 2)) return true;
            }
            else if (prior == 2 && next == 1)
            {
                if (tryTest(-1, 0)) return true;
                else if (tryTest(-1, 1)) return true;
                else if (tryTest(0, -2)) return true;
                else if (tryTest(-1, -2)) return true;
            }
            else if (prior == 2 && next == 3)
            {
                if (tryTest(1, 0)) return true;
                else if (tryTest(1, 1)) return true;
                else if (tryTest(0, -2)) return true;
                else if (tryTest(1, -2)) return true;
            }
            else if (prior == 3 && next == 2)
            {
                if (tryTest(-1, 0)) return true;
                else if (tryTest(-1, -1)) return true;
                else if (tryTest(0, 2)) return true;
                else if (tryTest(-1, 2)) return true;
            }
            else if (prior == 3 && next == 0)
            {
                if (tryTest(-1, 0)) return true;
                else if (tryTest(-1, -1)) return true;
                else if (tryTest(0, 2)) return true;
                else if (tryTest(-1, 2)) return true;
            }
            else if (prior == 0 && next == 3)
            {
                if (tryTest(1, 0)) return true;
                else if (tryTest(1, 1)) return true;
                else if (tryTest(0, -2)) return true;
                else if (tryTest(1, -2)) return true;
            }
            return false;
        }

        else
        {
            print("hehe");
            if (prior == 0 && next == 1)
            {
                if (tryTest(-2, 0)) return true;
                else if (tryTest(1, 0)) return true;
                else if (tryTest(-2, -1)) return true;
                else if (tryTest(1, 2)) return true;
            }
            else if (prior == 1 && next == 0)
            {
                if (tryTest(2, 0)) return true;
                else if (tryTest(-1, 0)) return true;
                else if (tryTest(2, 1)) return true;
                else if (tryTest(-1, -2)) return true;
            }
            else if (prior == 1 && next == 2)
            {
                if (tryTest(-1, 0)) return true;
                else if (tryTest(2, 0)) return true;
                else if (tryTest(-1, 2)) return true;
                else if (tryTest(2, -1)) return true;
            }
            else if (prior == 2 && next == 1)
            {
                if (tryTest(1, 0)) return true;
                else if (tryTest(-2, 0)) return true;
                else if (tryTest(1, -2)) return true;
                else if (tryTest(-2, 1)) return true;
            }
            else if (prior == 2 && next == 3)
            {
                if (tryTest(2, 0)) return true;
                else if (tryTest(-1, 0)) return true;
                else if (tryTest(2, 1)) return true;
                else if (tryTest(-1, -2)) return true;
            }
            else if (prior == 3 && next == 2)
            {
                if (tryTest(-2, 0)) return true;
                else if (tryTest(1, 0)) return true;
                else if (tryTest(-2, -1)) return true;
                else if (tryTest(1, 2)) return true;
            }
            else if (prior == 3 && next == 0)
            {
                if (tryTest(1, 0)) return true;
                else if (tryTest(-2, 0)) return true;
                else if (tryTest(1, -2)) return true;
                else if (tryTest(-2, 1)) return true;
            }
            else if (prior == 0 && next == 3)
            {
                if (tryTest(-1, 0)) return true;
                else if (tryTest(2, 0)) return true;
                else if (tryTest(-1, 2)) return true;
                else if (tryTest(2, -1)) return true;
            }
            return false;
        }
    }

    bool tryTest(int x, int y)
    {
        transform.position += new Vector3(x, y, 0);
        if (isValidGridPos())
        {
            updateGrid();
            return true;
        }
        else transform.position += new Vector3(-(x), -(y), 0);
        return false;
    }
	// Use this for initialization
	void Start ()
	{
		// Default position not valid? Then it's game over
		if (!isValidGridPos ()) {
			Debug.Log ("GAME OVER");
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
        //GameObject ghostPiece = Instantiate(this.gameObject, transform.position += new Vector3(0, -5, 0), Quaternion.identity) as GameObject;
        //SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        //sprite.material.color = new Color(1.0f, 1.0f, 1.0f, .1f);
        
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			transform.position += new Vector3 (-1, 0, 0);

			if (isValidGridPos ())
				updateGrid ();
			else
				transform.position += new Vector3 (1, 0, 0);
		} else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			// Modify position
			transform.position += new Vector3 (1, 0, 0);

			// See if valid
			if (isValidGridPos ())
                // It's valid. Update grid.
				updateGrid ();
			else
                // It's not valid. revert.
				transform.position += new Vector3 (-1, 0, 0);
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
            int storedCurrent = currentPermutation;
            if (currentPermutation != 3) currentPermutation++;
            else currentPermutation = 0;
            rotateGroup();
            //print(transform.name);
            //transform.Rotate (0, 0, -90);
            //foreach (Transform child in transform)
            //{
            //    //print("Hello");
            //    child.Rotate(0, 0, 90);
            //}

			// See if valid
			if (isValidGridPos ())
                // It's valid. Update grid.
				updateGrid ();
			else
            {
                // It's not valid. revert.     
                if (!kick(storedCurrent, currentPermutation))
                {
                    if (currentPermutation != 0) currentPermutation--;
                    else currentPermutation = 3;
                    rotateGroup();
                }
                //transform.Rotate(0, 0, 90);
                //foreach (Transform child in transform)
                //{
                //    child.Rotate(0, 0, -90);
                //}
                
            }
                
            
		}
        // Fall
        else if (Input.GetKey (KeyCode.DownArrow) || Time.time - lastFall >= 1) {
			// Modify position
			transform.position += new Vector3 (0, -1, 0);

			// See if valid
			if (isValidGridPos ()) {
				// It's valid. Update grid.
				updateGrid ();
			} else {
				// It's not valid. revert.
				transform.position += new Vector3 (0, 1, 0);

				// Clear filled horizontal lines
				Grid.deleteFullRows ();

				// Spawn next Group
				FindObjectOfType<Spawner> ().spawnNext ();

				// Disable script
				enabled = false;
			}

			lastFall = Time.time;
		}

	}
}
