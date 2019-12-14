using System.Drawing;

namespace TankzClient.Game
{
    public abstract class TerrainTemplate
    {
        public abstract void SetBaseColor();
        public abstract void SetTopColor();
        public virtual bool TopLayerStyle(int depth) { return true; }
        public virtual void SpawnProps() { }
        public virtual Bitmap TerrainShape()
        {
            const string terrainPath = "../../res/terrain_bitmap.bmp";
            return new Bitmap(Image.FromFile(terrainPath));
        }

        protected Terrain terrain;

        public Terrain GenerateTerrain()
        {
            terrain = new Terrain();
            Bitmap mask = TerrainShape();
            SetBaseColor();
            SetTopColor();
            terrain.GenerateColoredTerrain(mask, TopLayerStyle);
            SpawnProps();
            return terrain;
        }
    }
}