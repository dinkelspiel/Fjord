namespace Proj.Modules.Tools {
    public class tilemap {

        public string asset_pack;

        public int[,] map;
        public int grid_w, grid_h;
        public int w, h;

        public List<string> textures = new List<string>();

        public tilemap(int w, int h, int grid_w, int grid_h) {
            map = new int[w,h];
            this.grid_w = grid_w;
            this.grid_h = grid_h;
            this.w = w;
            this.h = h;
        }
    }
}