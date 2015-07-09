namespace JarheadsButtonMaker
{
    public class VoxelSpriteChunk
    {
        public int X_SIZE = 32, Y_SIZE = 32, Z_SIZE = 32;
        public VoxelSpriteVoxel[, ,] Voxels; 

        public void Init(int xs, int ys, int zs)
        {
            X_SIZE = xs;
            Y_SIZE = ys;
            Z_SIZE = zs;

            Voxels = new VoxelSpriteVoxel[X_SIZE,Y_SIZE,Z_SIZE];
        }

        public void SetVoxel(int x, int y, int z, bool active, Color col)
        {
            if (x < 0 || y < 0 || z < 0 || x >= X_SIZE || y >= Y_SIZE || z >= Z_SIZE) return;

            Voxels[x, y, z].Active = active;
            Voxels[x, y, z].Color = col;
        }

    }
}
