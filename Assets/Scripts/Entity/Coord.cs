namespace Scripts.Entity
{
    public class Coord
    {
        public int X, Y, Z = 0;

        public Coord(int x, int y, int z)
        {
            X = x; 
            Y = y; 
            Z = z;
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}, {nameof(Z)}: {Z}";
        }
    }
}