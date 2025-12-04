using Scripts.Main;
using UnityEngine;

namespace Scripts.Entity
{
    public static class CalculateProcessor
    {
        public static GridElement[] GetCornerNearGridElements(int width, int height, GridElement[] gridElements,
            Coord currentCoord)
        {
            GridElement[] returnGridElements = new GridElement[8];

            int x = currentCoord.X;
            int xMinusOne = currentCoord.X - 1;

            int y = currentCoord.Y;
            int yMinusOne = currentCoord.Y - 1;

            int z = currentCoord.Z;
            int zMinusOne = currentCoord.Z - 1;

            if (y < height)
            {
                if (x < width && z < width)
                {
                    //Upper NorthEast
                    returnGridElements[0] = getGridByXYZ(gridElements, width, x, y, z);
                }

                if (x > 0 && z < width)
                {
                    //Upper NorthWest
                    returnGridElements[1] = getGridByXYZ(gridElements, width, xMinusOne, y, z);
                }

                if (x > 0 && z > 0)
                {
                    //Upper SouthWest
                    returnGridElements[2] = getGridByXYZ(gridElements, width, xMinusOne, y, zMinusOne);
                }

                if (x < width && z > 0)
                {
                    //Upper SouthWest
                    returnGridElements[3] = getGridByXYZ(gridElements, width, x, y, zMinusOne);
                }
            }

            if (y > 0)
            {
                if (x < width && z < width)
                {
                    //Lower NorthEast
                    returnGridElements[4] = getGridByXYZ(gridElements, width, x, yMinusOne, z);
                }

                if (x > 0 && z < width)
                {
                    //Lower NorthWest
                    returnGridElements[5] = getGridByXYZ(gridElements, width, xMinusOne, yMinusOne, z);
                }

                if (x > 0 && z > 0)
                {
                    //Lower SouthWest
                    returnGridElements[6] = getGridByXYZ(gridElements, width, xMinusOne, yMinusOne, zMinusOne);
                }

                if (x < width && z > 0)
                {
                    //Lower SouthWest
                    returnGridElements[7] = getGridByXYZ(gridElements, width, x, yMinusOne, zMinusOne);
                }
            }

            return returnGridElements;
        }

        public static int GetBitMaskValue(GridElement[] cornerNearGridElements)
        {
            int bitMask = 0;

            if (cornerNearGridElements[0] != null)
            {
                if (cornerNearGridElements[0].GetEnable())
                {
                    bitMask += 1;
                }
            }

            if (cornerNearGridElements[1] != null)
            {
                if (cornerNearGridElements[1].GetEnable())
                {
                    bitMask += 2;
                }
            }

            if (cornerNearGridElements[2] != null)
            {
                if (cornerNearGridElements[2].GetEnable())
                {
                    bitMask += 4;
                }
            }

            if (cornerNearGridElements[3] != null)
            {
                if (cornerNearGridElements[3].GetEnable())
                {
                    bitMask += 8;
                }
            }

            if (cornerNearGridElements[4] != null)
            {
                if (cornerNearGridElements[4].GetEnable())
                {
                    bitMask += 16;
                }
            }

            if (cornerNearGridElements[5] != null)
            {
                if (cornerNearGridElements[5].GetEnable())
                {
                    bitMask += 32;
                }
            }

            if (cornerNearGridElements[6] != null)
            {
                if (cornerNearGridElements[6].GetEnable())
                {
                    bitMask += 64;
                }
            }

            if (cornerNearGridElements[7] != null)
            {
                if (cornerNearGridElements[7].GetEnable())
                {
                    bitMask += 128;
                }
            }

            return bitMask;
        }

        public static CornerElement[] GetGridForCornerElements(int widthPlusOne, int heightPlusOne, CornerElement[] cornerElements, Coord currentCoord)
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

        public static void ProcessCornerElementsPosition(Bounds bounds, CornerElement[] targetCornerElements)
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


        public static GridElement GetProcessGrid(int width, int height, GridElement[] gridElements, Coord currentCoord,
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

        private static GridElement getGridByCoord(GridElement[] gridElements, int width, Coord targetCoord)
        {
            return getGridByXYZ(gridElements, width, targetCoord.X, targetCoord.Y, targetCoord.Z);
        }

        private static GridElement getGridByXYZ(GridElement[] gridElements, int width, int x, int y, int z)
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

        private static CornerElement getCornerByXYZ(CornerElement[] cornerElements, int width, int x, int y, int z)
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

        private static int getIndex(int width, int x, int y, int z)
        {
            return x + (width * (z + width * y));
        }
    }
}