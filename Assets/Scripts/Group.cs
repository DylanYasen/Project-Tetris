using UnityEngine;
using System.Threading;
using System.Collections;
using System.Collections.Generic;


public class Group : MonoBehaviour
{
    public static bool aimode;
    public List<move> moves;

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
	public float lastFall = 0;

    public static int rowscompleted = 0;

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

    // DENMARK STUFF
    public struct move
    {
        public int piece;
        public int rotation;
        public int translation;
        public int landingheight, rowtrans, coltrans, rowseliminated, holes, wellsums;
        public double rating;

        public move(int p, int rot, int trans, int l, int rowt, int colt, int rows, int ho, int wells, double rat)
        {
            this.piece = p;
            this.rotation = rot;
            this.translation = trans;
            this.landingheight = l;
            this.rowtrans = rowt;
            this.coltrans = colt;
            this.rowseliminated = rows;
            this.holes = ho;
            this.rating = rat;
            this.wellsums = wells;
        }
    }

    public move lastmove;

    public int ComputeLandingHeight(int piece, int rotation, int translation) //
    {
        int landingheight = 0;
        int currentheight = 15;
        int steps = 1;
        bool valid = true;

        if (rotation > 0) // Rotate piece
        {
            for (int i = 0; i < rotation; i++) 
            {
                //transform.Rotate(0, 0, -90);
                int storedCurrent = currentPermutation;
                if (currentPermutation != 3) currentPermutation++;
                else currentPermutation = 0;
                rotateGroup();

                if (isValidGridPos())
                    updateGrid();
                else
                {
                    if (!kick(storedCurrent, currentPermutation))
                    {
                        if (currentPermutation != 0) currentPermutation--;
                        else currentPermutation = 3;
                        rotateGroup();
                    }
                }
            }
        }

        while (valid)
        { // Get number of steps to fall
            foreach (Transform child in transform)
            {
                Vector2 v = Grid.roundVec2(child.position);
                v.x += translation;
                //print("LH " + v.x + ", " + v.y);
                v.y -= steps;
                if ((!Grid.insideBorder(v)) || (Grid.grid[(int)v.x, (int)v.y] != null &&
                    Grid.grid[(int)v.x, (int)v.y].parent != transform))
                {
                    valid = false;
                    break;
                }
            }
            if (valid)
                steps++;
            else
                break;
        }

        currentheight -= steps; // Get current height

        // Get landing height. This currently does not take into account the number of rows eliminated
        if (piece == 0) // I
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 1;
            else
                landingheight = currentheight + 4;
        }
        else if (piece == 1 || piece == 2) // J, L
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 2;
            else
                landingheight = currentheight + 3;
        }
        else if (piece == 3) // O
        {
            landingheight = currentheight + 2;
        }
        else if (piece == 4 || piece == 6) // S, Z
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 2;
            else
                landingheight = currentheight + 3;
        }
        else if (piece == 6) // T
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 2;
            else
                landingheight = currentheight + 3;
        }

        //print("landingheight: " + landingheight);
        return landingheight;
    }


    public move ComputeRowTransitions(int piece, int rotation, int translation)
    {
        move thismove = new move(); 
        int rowtrans = 0, coltrans = 0, holes = 0, wellsums = 0;
        bool lastfilled = true;
        bool valid = true;
        int steps = 0;

        if (rotation > 0) // 1. Rotate piece
        {
            for (int i = 0; i < rotation; i++) 
            {
                //transform.Rotate(0, 0, -90);
                int storedCurrent = currentPermutation;
                if (currentPermutation != 3) currentPermutation++;
                else currentPermutation = 0;
                rotateGroup();

                if (isValidGridPos())
                    updateGrid();
                else
                {
                    if (!kick(storedCurrent, currentPermutation))
                    {
                        if (currentPermutation != 0) currentPermutation--;
                        else currentPermutation = 3;
                        rotateGroup();
                    }
                }
            }
        }

        
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            //print("PRE " + v.x + ", " + v.y);
        } 

        while (valid) // Get number of steps to fall
        {
            foreach (Transform child in transform)
            {
                Vector2 v = Grid.roundVec2(child.position);
                v.x += translation; // 

                v.y -= steps;

                if ((!Grid.insideBorder(v)) || (Grid.grid[(int)v.x, (int)v.y] != null &&
                    Grid.grid[(int)v.x, (int)v.y].parent != transform))
                {
                    //child.Rotate(0, 0, -90);

                    valid = false;
                    break;
                }
            }
            if (valid)
                steps++;
            else
                break;
        }

        steps--;
        foreach (Transform child in transform) // Translate and drop piece
        {
            Vector2 v = Grid.roundVec2(child.position);

            v.x += translation;
            v.y -= steps;
            //print("DROPPED: "+v.x + "," + v.y);
            Grid.grid[(int)v.x, (int)v.y] = child; // 
        }

        /* Now that piece is in place, run computations */

        int rowseliminated = 0; // Compute number of rows eliminated
        for (int i = 0; i < 15; i++)
        {
            /*
            bool fullrow = true;
            for (int j = 0; j < 10; j++)
            {
                if (Grid.grid[j, i] == null)
                    fullrow = false;
            }
            if (fullrow)
                rowseliminated++;
                */
            if (Grid.isRowFull(i))
                rowseliminated++;
        }
        thismove.rowseliminated = rowseliminated;

        for (int i = 0; i < 15; i++) // Compute number of row transitions
        {
            bool emptyrow = true;
            bool fullrow = true;
            lastfilled = true;
            int thisrow = 0;

            for (int j = 0; j < 10; j++) // Skip row if full
            {
                if (Grid.grid[j, i] == null)
                    fullrow = false;
            }
            if (fullrow)
                continue;

            for (int k = 0; k < 10; k++) // Skip row if empty
            {
                if (Grid.grid[k, i] != null)
                    emptyrow = false;
            }
            if (emptyrow)
                break;

            for (int j = 0; j < 10; j++)
            {

                if (lastfilled)
                {
                    if (Grid.grid[j, i] == null)
                    {
                        thisrow++;
                        lastfilled = false;
                    }
                    else
                    {
                        lastfilled = true;
                    }
                }
                else
                {
                    if (Grid.grid[j, i] == null)
                    {
                        lastfilled = false;
                    }
                    else
                    {
                        thisrow++;
                        lastfilled = true;
                    }
                }

                if (thisrow > 0 && j == 9 && Grid.grid[j, i] == null)
                    thisrow++;
            }
            //print("row " + i + ": " + thisrow);
            rowtrans += thisrow;
        }
        thismove.rowtrans = rowtrans;
        //print("rowtrans: " + rowtrans);

        // 5. Compute number of column transitions
        for (int i = 0; i < 10; i++)
        {
            bool emptycol = true;
            lastfilled = true;
            int thiscol = 0;

            for (int k = 0; k < 15; k++)
            {
                if (Grid.grid[i, k] != null)
                    emptycol = false;
            }
            if (emptycol)
            {
                thiscol++;
                continue;
            }

            for (int j = 0; j < 15; j++)
            {
                if (lastfilled)
                {
                    if (Grid.grid[i, j] == null)
                    {
                        thiscol++;
                        lastfilled = false;
                    }
                    else
                    {
                        lastfilled = true;
                    }
                }
                else
                {
                    if (Grid.grid[i, j] == null)
                    {
                        lastfilled = false;
                    }
                    else
                    {
                        thiscol++;
                        lastfilled = true;
                    }
                }
            }

            //print("col " + i + ": " + thiscol);
            coltrans += thiscol;
        }
        thismove.coltrans = coltrans;

        // 6. Compute number of holes
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                if (Grid.isRowFull(i))
                    continue;
                if (Grid.grid[i, j] == null && Grid.grid[i, j + 1] != null)
                    holes++;
            }
        }
        thismove.holes = holes;

        // 7. Compute well sums
        for (int i = 0; i < 10; i++)
        {
            int thiscol = 0;
            bool well = false;
            lastfilled = true;
            for (int j = 0; j < 15; j++)
            {
                if (lastfilled)
                {
                    if (Grid.grid[i, j] == null)
                    {
                        if (i == 0) // Leftmost column
                        {
                            if (Grid.grid[i + 1, j] != null)
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    thiscol++;
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                            else
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                        }
                        else if (i == 9) // Rightmost column
                        {
                            if (Grid.grid[i - 1, j] != null)
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    thiscol++;
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                            else
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                        }
                        else // In between leftmost and rightmost columns
                        {
                            if (Grid.grid[i - 1, j] != null && Grid.grid[i + 1, j] != null)
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    thiscol++;
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                            else
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        well = false;
                        lastfilled = true;
                    }
                }
                else
                {
                    if (Grid.grid[i, j] == null)
                    {
                        if (i == 0) // Leftmost column
                        {
                            if (Grid.grid[i + 1, j] != null)
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    thiscol++;
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                            else
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                        }
                        else if (i == 9) // Rightmost column
                        {
                            if (Grid.grid[i - 1, j] != null)
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    thiscol++;
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                            else
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                        }
                        else // In between leftmost and rightmost columns
                        {
                            if (Grid.grid[i - 1, j] != null && Grid.grid[i + 1, j] != null)
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    thiscol++;
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                            else
                            {
                                if (Grid.grid[i, j + 1] == null)
                                {
                                    well = true;
                                    lastfilled = false;
                                }
                                else
                                {
                                    well = false;
                                    lastfilled = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        well = false;
                        lastfilled = true;
                    }
                }
            }
            if (well)
                wellsums += thiscol;
            //print("col" + i + ": " + thiscol);
        }
        thismove.wellsums = wellsums;

        // Get landing height
        int currentheight = 14 - steps;
        int landingheight = 0;
        if (piece == 0) // I
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 1;
            else
                landingheight = currentheight + 2;
        }
        else if (piece == 1 || piece == 2) // J, L
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 2;
            else
                landingheight = currentheight + 2;
        }
        else if (piece == 3) // O
        {
            landingheight = currentheight + 2;
        }
        else if (piece == 4) // S
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 2;
            else
                landingheight = currentheight + 2;
        }
        else if (piece == 5) // T
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 2;
            else
                landingheight = currentheight + 4;
        }
        else if (piece == 6) // Z
        {
            if (rotation == 0 || rotation == 2)
                landingheight = currentheight + 1;
            else
                landingheight = currentheight + 1;
        }
        thismove.landingheight = landingheight;

        // Remove piece
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            v.x += translation;
            v.y -= steps;

            Grid.grid[(int)v.x, (int)v.y] = null;
        }

        // Rotate transform back to initial state if rotated
        if (rotation == 1 || rotation == 3)
        {
            //transform.Rotate(0, 0, -90);
            
            for (int i = 0; i < 3; i++)
            {
                int storedCurrent = currentPermutation;
                if (currentPermutation != 3) currentPermutation++;
                else currentPermutation = 0;
                rotateGroup();

                if (isValidGridPos())
                    updateGrid();
                else
                {
                    if (!kick(storedCurrent, currentPermutation))
                    {
                        if (currentPermutation != 0) currentPermutation--;
                        else currentPermutation = 3;
                        rotateGroup();
                    }
                }
            }
            
        }

        // print("DONE piece: " + thismove.piece + ", rotation: " + thismove.rotation + ", translation: " + thismove.translation + ", landingheight: " + thismove.landingheight + ", rowseliminated: " + thismove.rowseliminated + ", rowtrans: " + thismove.rowtrans + ", coltrans: " + thismove.coltrans + ", holes: " + thismove.holes + ", wellsums: " + thismove.wellsums + ", rating: " + thismove.rating);

        return thismove;
    }

    public move ComputeBestMove(int piece)
    {
        List<move> moves = new List<move>();
        //move currentmove = new move(); 
        // Compute rating for each possible placement of given piece

        for (int i = 0; i < 4; i++) // Iterate thru 4 rotations
        {
            if (piece == 0) // I PIECE
            {
                if (i != 0 && i != 1)
                    continue;
                if (i == 0) // *
                {
                    for (int j = -5; j < 2; j++) // Iterate thru column translations from leftmost to rightmost
                    {
                        move newmove = new move(); // print("piece: " + piece + ", rot: " + i + ", tran: " + j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 1) // * 
                {
                    for (int j = -7; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
            }
            else if (piece == 1) // J PIECE
            {
                if (i == 0) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 1) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 2) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 3) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
            }
            else if (piece == 2) // L PIECE
            {
                if (i == 0) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }

                else if (i == 1) // *
                {
                    for (int j = -6; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 2) // *
                {
                    for (int j = -5; j < 2; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 3) // *
                {
                    for (int j = -6; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
            }
            else if (piece == 3) // O PIECE
            {
                if (i != 0)
                {
                    continue;
                }
                for (int j = -5; j < 4; j++) // *
                {
                    move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                    newmove = ComputeRowTransitions(piece, i, j);
                    double rating = (-4.5 * newmove.landingheight) +
                             (50 * newmove.rowseliminated) +
                             (-3.2 * newmove.rowtrans) +
                             (-9.3 * newmove.coltrans) +
                             (-40 * newmove.holes) +
                             (-3.4 * newmove.wellsums);
                    moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                    //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                }
            }
            else if (piece == 4) // S PIECE
            {
                if (i != 0 && i != 1)
                    continue;
                if (i == 0) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 1) // *
                {
                    for (int j = -6; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
            }
            else if (piece == 5) // T PIECE
            {
                if (i == 0) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 1) // *
                {
                    for (int j = -6; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 2) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 3) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
            }
            else if (piece == 6) // Z PIECE
            {
                if (i != 0 && i != 1)
                    continue;
                if (i == 0) // *
                {
                    for (int j = -5; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
                else if (i == 1) // *
                {
                    for (int j = -6; j < 3; j++)
                    {
                        move newmove = new move(); //print("piece: "+piece+", rot: "+i+", tran: "+j);
                        newmove = ComputeRowTransitions(piece, i, j);
                        double rating = (-4.5 * newmove.landingheight) +
                                 (50 * newmove.rowseliminated) +
                                 (-3.2 * newmove.rowtrans) +
                                 (-9.3 * newmove.coltrans) +
                                 (-40 * newmove.holes) +
                                 (-3.4 * newmove.wellsums);
                        moves.Add(new move(piece, i, j, newmove.landingheight, newmove.rowtrans, newmove.coltrans, newmove.rowseliminated, newmove.holes, newmove.wellsums, rating));
                        //print("piece: " + piece + ", rotation: " + i + ", translation: " + j + ", landingheight: " + newmove.landingheight + ", rowseliminated: " + newmove.rowseliminated + ", rowtrans: " + newmove.rowtrans + ", coltrans: " + newmove.coltrans + ", holes: " + newmove.holes + ", wellsums: " + newmove.wellsums + ", rating: " + rating);
                    }
                }
            }
        }

        // Get move with highest rating
        move bestmove;
        //print("moves.Count: " + moves.Count);
        moves.Sort((s1, s2) => s1.rating.CompareTo(s2.rating));
        bestmove = moves[moves.Count - 1];
        return bestmove;
    }

    public void DoMove(int rotation, int translation)
    {
        // Rotate
        for (int i = 0; i < rotation; i++)
        {
            //StartCoroutine(Rotate());
            transform.Rotate(0, 0, -90);
            foreach (Transform child in transform)
            {
                child.Rotate(0, 0, 90);
            }

            if (isValidGridPos())
                updateGrid();
            else
            {
                transform.Rotate(0, 0, 90);
                foreach (Transform child in transform)
                {
                    child.Rotate(0, 0, -90);
                }
            }
        }

        // Translate
        if (translation > 0)
        {
            for (int i = 0; i < Mathf.Abs(translation); i++)
            {
                //StartCoroutine(TranslateR());
                transform.position += new Vector3(1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(-1, 0, 0);
            }
        }
        else if (translation < 0)
        {
            for (int i = 0; i < Mathf.Abs(translation); i++)
            {
                //StartCoroutine(TranslateL());
                transform.position += new Vector3(-1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
            }
        }

        // Drop
        for (int i = 0; i < 20; i++)
        {
            //StartCoroutine(Drop());
            // Modify position
            transform.position += new Vector3(0, -1, 0);
            // See if valid
            if (isValidGridPos())
            {
                // It's valid. Update grid.
                updateGrid();
            }
            else
            {
                // It's not valid. revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Grid.deleteFullRows();

                // Spawn next Group
                FindObjectOfType<Spawner>().spawnNext();
                //FindObjectOfType<Spawner>().spawnCollection();
                // Disable script
                enabled = false;
                break;
            }
            //StartCoroutine(Wait());
        }
    }

    // Use this for initialization
    void Start ()
	{
		// Default position not valid? Then it's game over
		if (!isValidGridPos ()) {
			Debug.Log ("GAME OVER");
            print("ROWS COMPLETED: " + rowscompleted);
            Destroy (gameObject);
		}
	}

    //bool movedone = false;
    bool evaluationdone = false;
    int rots, trans;
    move bestmove = new move();
    // Update is called once per frame
    void Update ()
	{
        if (Group.aimode == false)
        { // 1P MODE
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Modify position
                transform.position += new Vector3(1, 0, 0);

                // See if valid
                if (isValidGridPos())
                    // It's valid. Update grid.
                    updateGrid();
                else
                    // It's not valid. revert.
                    transform.position += new Vector3(-1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
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
                if (isValidGridPos())
                    // It's valid. Update grid.
                    updateGrid();
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
            else if (Input.GetKey(KeyCode.DownArrow) || Time.time - lastFall >= 1)
            {
                // Modify position
                transform.position += new Vector3(0, -1, 0);

                // See if valid
                if (isValidGridPos())
                {
                    // It's valid. Update grid.
                    updateGrid();
                }
                else
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);

                    // Clear filled horizontal lines
                    Grid.deleteFullRows();

                    // Spawn next Group
                    FindObjectOfType<Spawner>().spawnNext();

                    // Disable script
                    enabled = false;
                }

                lastFall = Time.time;
            }
        }
        else // AI MODE
        {
            /* if(Spawner.nextGroup == 0 ||
               Spawner.nextGroup == 1 ||
               Spawner.nextGroup == 2 ||
               Spawner.nextGroup == 3 ||
               Spawner.nextGroup == 4 ||
               Spawner.nextGroup == 5 ||
               Spawner.nextGroup == 6)
            {
                //StartCoroutine(Wait());
                move bestmove = ComputeBestMove(Spawner.nextGroup);
                print("bestmove: " + bestmove.rotation + ", " + bestmove.translation);
                DoMove(bestmove.rotation, bestmove.translation);
            }            
            */

            if (Spawner.nextGroup == 0 || Spawner.nextGroup == 1 || Spawner.nextGroup == 2 || Spawner.nextGroup == 3 ||
                Spawner.nextGroup == 4 || Spawner.nextGroup == 5 || Spawner.nextGroup == 6)
            {
                if (!evaluationdone)
                {
                    bestmove = ComputeBestMove(Spawner.nextGroup);
                    print("BEST MOVE: piece: " + bestmove.piece + ", rotation: " + bestmove.rotation + ", translation: " + bestmove.translation + ", landingheight: " + bestmove.landingheight + ", rowseliminated: " + bestmove.rowseliminated + ", rowtrans: " + bestmove.rowtrans + ", coltrans: " + bestmove.coltrans + ", holes: " + bestmove.holes + ", wellsums: " + bestmove.wellsums + ", rating: " + bestmove.rating);
                    print("ROWS COMPLETED: " + rowscompleted);
                    rots = bestmove.rotation;
                    trans = bestmove.translation;
                    evaluationdone = true;
                }
                if (evaluationdone && Time.time - lastFall >= 0.07) // AI Rotate
                {
                    if (rots > 0)
                    {
                        /*transform.Rotate(0, 0, -90);
                        foreach (Transform child in transform)
                        {
                            child.Rotate(0, 0, 90);
                        }

                        if (isValidGridPos())
                            updateGrid();
                        else
                        {
                            transform.Rotate(0, 0, 90);
                            foreach (Transform child in transform)
                            {
                                child.Rotate(0, 0, -90);
                            }
                        }
                        rots--;*/
                        int storedCurrent = currentPermutation;
                        if (currentPermutation != 3) currentPermutation++;
                        else currentPermutation = 0;
                        rotateGroup();

                        if (isValidGridPos())
                            updateGrid();
                        else
                        {
                            if (!kick(storedCurrent, currentPermutation))
                            {
                                if (currentPermutation != 0)
                                    currentPermutation--;
                                else
                                    currentPermutation = 3;
                                rotateGroup();
                            }
                        }
                        rots--;
                        lastFall = Time.time;
                    }

                    if (trans != 0) // AI translate
                    {
                        if (trans < 0)
                        {
                            transform.position += new Vector3(-1, 0, 0);

                            if (isValidGridPos())
                                updateGrid();
                            else
                                transform.position += new Vector3(1, 0, 0);
                            trans++;
                            lastFall = Time.time;
                        }
                        else
                        {
                            transform.position += new Vector3(1, 0, 0);

                            if (isValidGridPos())
                                updateGrid();
                            else
                                transform.position += new Vector3(-1, 0, 0);
                            trans--;
                            lastFall = Time.time;
                        }
                    }

                    if (rots == 0 && trans == 0) // AI Rotated and translated. Ok to drop piece
                    {
                        transform.position += new Vector3(0, -1, 0);

                        if (isValidGridPos())
                        {
                            updateGrid();
                        }
                        else
                        {
                            transform.position += new Vector3(0, 1, 0);
                            Grid.deleteFullRows();
                            FindObjectOfType<Spawner>().spawnNext();
                            enabled = false;
                            evaluationdone = false;
                        }
                        lastFall = Time.time;
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);

                if (isValidGridPos())
                    updateGrid();
                else
                    transform.position += new Vector3(1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                // Modify position
                transform.position += new Vector3(1, 0, 0);

                // See if valid
                if (isValidGridPos())
                    // It's valid. Update grid.
                    updateGrid();
                else
                    // It's not valid. revert.
                    transform.position += new Vector3(-1, 0, 0);
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
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
                if (isValidGridPos())
                    // It's valid. Update grid.
                    updateGrid();
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
            else if (Input.GetKey(KeyCode.DownArrow) || Time.time - lastFall >= 1)
            {
                // Modify position
                transform.position += new Vector3(0, -1, 0);

                // See if valid
                if (isValidGridPos())
                {
                    // It's valid. Update grid.
                    updateGrid();
                }
                else
                {
                    // It's not valid. revert.
                    transform.position += new Vector3(0, 1, 0);

                    // Clear filled horizontal lines
                    Grid.deleteFullRows();

                    // Spawn next Group
                    FindObjectOfType<Spawner>().spawnNext();

                    // Disable script
                    enabled = false;
                }

                lastFall = Time.time;
            }
            
        }
    }
}
