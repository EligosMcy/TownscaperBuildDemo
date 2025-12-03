using System;
using Scripts.Main;
using UnityEngine;

namespace Scripts.Entity
{
    public class CalculateProcessor
    {
        public CornerElement[] GetGridCornerElements(int widthPlusOne, int heightPlusOne, CornerElement[] cornerElements, Coord currentCoord)
        {
            CornerElement[] returnCornerElements = new CornerElement[8];

            int x = currentCoord.X;
            int xPlusOne = currentCoord.X + 1;

            int y = currentCoord.Y;
            int yPlusOne = currentCoord.Y + 1;

            int z = currentCoord.Z;
            int zPlusOne = currentCoord.Z + 1;

            returnCornerElements[0] = getCornerByXYZ(cornerElements, widthPlusOne, x, y, z);

            returnCornerElements[1] = getCornerByXYZ(cornerElements, widthPlusOne, xPlusOne, y, z);

            returnCornerElements[2] = getCornerByXYZ(cornerElements, widthPlusOne, x, y, zPlusOne);

            returnCornerElements[3] = getCornerByXYZ(cornerElements, widthPlusOne, xPlusOne, y, zPlusOne);

            returnCornerElements[4] = getCornerByXYZ(cornerElements, widthPlusOne, x, yPlusOne, z);

            returnCornerElements[5] = getCornerByXYZ(cornerElements, widthPlusOne, xPlusOne, yPlusOne, z);

            returnCornerElements[6] = getCornerByXYZ(cornerElements, widthPlusOne, x, yPlusOne, zPlusOne);

            returnCornerElements[7] = getCornerByXYZ(cornerElements, widthPlusOne, xPlusOne, yPlusOne, zPlusOne);

            return returnCornerElements;
        }

        public void ProcessCornerElementsPosition(Bounds bounds, CornerElement[] targetCornerElements)
        {
            targetCornerElements[0].SetPosition(bounds.min.x, bounds.min.y, bounds.min.z);
            targetCornerElements[1].SetPosition(bounds.max.x, bounds.min.y, bounds.min.z);
            targetCornerElements[2].SetPosition(bounds.min.x, bounds.min.y, bounds.max.z);
            targetCornerElements[3].SetPosition(bounds.max.x, bounds.min.y, bounds.max.z);
            targetCornerElements[4].SetPosition(bounds.min.x, bounds.max.y, bounds.min.z);
            targetCornerElements[5].SetPosition(bounds.max.x, bounds.max.y, bounds.min.z);
            targetCornerElements[6].SetPosition(bounds.min.x, bounds.max.y, bounds.max.z);
            targetCornerElements[7].SetPosition(bounds.max.x, bounds.max.y, bounds.max.z);
        }


        public GridElement GetProcessGrid(int width, int height, GridElement[] gridElements, Coord currentCoord,
            GridEventEnum gridEventEnum)
        {
            GridElement targetGridElement = null;

            Coord targetCoord = new Coord(currentCoord.X, currentCoord.Y, currentCoord.Z);

            switch (gridEventEnum)
            {
                case GridEventEnum.Remove:
                    if (currentCoord.Y > 0)
                    {
                        targetGridElement = getGridByCoord(gridElements, width, targetCoord);
                    }
                    break;
                case GridEventEnum.AddXPos:
                    if (currentCoord.X < width - 1)
                    {
                        targetCoord.X += 1;

                        targetGridElement = getGridByCoord(gridElements, width, targetCoord);
                    }

                    break;
                case GridEventEnum.AddXNeg:
                    if (currentCoord.X > 0)
                    {
                        targetCoord.X -= 1;

                        targetGridElement = getGridByCoord(gridElements, width, targetCoord);
                    }

                    break;
                case GridEventEnum.AddZPos:
                    if (currentCoord.Z < width - 1)
                    {
                        targetCoord.Z += 1;

                        targetGridElement = getGridByCoord(gridElements, width, targetCoord);
                    }

                    break;
                case GridEventEnum.AddZNeg:
                    if (currentCoord.Z > 0)
                    {
                        targetCoord.Z -= 1;

                        targetGridElement = getGridByCoord(gridElements, width, targetCoord);
                    }

                    break;
                case GridEventEnum.AddYPos:
                    if (currentCoord.Y < height - 1)
                    {
                        targetCoord.Y += 1;

                        targetGridElement = getGridByCoord(gridElements, width, targetCoord);
                    }

                    break;
            }

            return targetGridElement;
        }

        private GridElement getGridByCoord(GridElement[] gridElements, int width, Coord targetCoord)
        {
            return getGridByXYZ(gridElements, width, targetCoord.X, targetCoord.Y, targetCoord.Z);
        }

        private GridElement getGridByXYZ(GridElement[] gridElements, int width, int x, int y, int z)
        {
            int gridIndex = getIndex(width, x, y, z);

            if (gridIndex < gridElements.Length)
            {
                return gridElements[gridIndex];
            }
            else
            {
                Debug.LogError(gridIndex);

                return null;
            }
        }

        private CornerElement getCornerByXYZ(CornerElement[] cornerElements, int width, int x, int y, int z)
        {
            int index = getIndex(width, x, y, z);

            if (index < cornerElements.Length)
            {
                return cornerElements[index];
            }
            else
            {
                Debug.LogError(index);

                return null;
            }
        }

        private int getIndex(int width, int x, int y, int z)
        {
            return x + (width * (z + width * y));
        }
    }
}