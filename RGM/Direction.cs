namespace RGM
{
    public enum dDirection
    {
        up = 0,
        down = 1,
        right = 2,
        left = 3,
        
        center = 4, // Well it's not quite a direction but I need it
        none = 5
    }
    
    public struct dSides
    {
        public dSides(bool top, bool bottom, bool right, bool left)
        {
            this.top = top;
            this.bottom = bottom;
            this.right = right;
            this.left = left;
        }
        
        public bool top;
        public bool bottom;
        public bool right;
        public bool left;
    }

}