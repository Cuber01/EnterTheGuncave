using System.Collections.Generic;
using EnterTheGuncave.Entities;
using EnterTheGuncave.Entities.Allies;
using EnterTheGuncave.General;
using EnterTheGuncave.General.ContentHandling.Assets;
using EnterTheGuncave.General.ContentHandling.Rooms;
using EnterTheGuncave.General.DungeonGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheGuncave
{
    public class EnterTheGuncave : Game
    {
        public static readonly List<Entity> entities = new List<Entity>();
        public static readonly List<Entity> entitiesToBeSpawned = new List<Entity>();
        public static readonly List<Entity> entitiesToBeKilled = new List<Entity>();

        public const int roomWidth = 19;
        public const int roomHeight = 11;
        public static int[,] currentRoom = new int[roomWidth, roomHeight];
        
        public const int tileSize = 16;
        public const int scale = 4;

        private const int windowWidth = roomWidth * tileSize * scale;
        private const int windowHeight = roomHeight * tileSize * scale;

        private readonly Matrix scaleMatrix = Matrix.CreateScale(scale, scale, 1.0f);
        private readonly GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        DrawUtils draw;

        public EnterTheGuncave()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            graphics.PreferredBackBufferWidth  = windowWidth;  
            graphics.PreferredBackBufferHeight = windowHeight; 
            graphics.ApplyChanges();
            
            base.Initialize();
            
            draw = new DrawUtils(GraphicsDevice, spriteBatch);
        }

        // 1. Load rooms
        // 2. Generate dungeon
        // 3. Add player
        // 4. Play a room
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetLoader assetLoader = new AssetLoader(Content);
            
            assetLoader.loadTextures();
           
            RoomLoader.loadAllRooms();
            
            DungeonGenerator.generate();
            
            entities.Add(new Player(new Vector2(50, 100)));
            
            // entities.Add(new WalkingEnemy(new Vector2(128, 80)));
            // entities.Add(new Stone(new Vector2(100, 100)));
            // RoomLoader.placeWalls();
           
            RoomLoader.playRoom(DungeonGenerator.floorMap[entities[0].mapPosition.X, entities[0].mapPosition.Y].roomInfo.roomIndex);
            RoomLoader.placeDoors();

        }

        protected override void Update(GameTime gameTime)
        {
            
            Input.updateKeyboardState();
            Input.updateMouseState();


            foreach (Entity spawn in entitiesToBeSpawned)
            { 
                entities.Add(spawn);
            }

            entitiesToBeSpawned.Clear();


            foreach (Entity entity in entities) 
            {
                if (entity.dead)
                {
                    entitiesToBeKilled.Add(entity); 
                }
                
                entity.update();    
                
                if (RoomLoader.changingRoom)
                {
                    break;
                }
            }

            foreach (Entity victim in entitiesToBeKilled)
            {
                    entities.Remove(victim);
            }

            entitiesToBeKilled.Clear();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: scaleMatrix);
            
            draw.drawGrid(Color.Gray);
            
            foreach( Entity entity in entities )
            {
                entity.draw();
                entity.collider.draw(draw);
            }
            
            spriteBatch.End();
            
            base.Draw(gameTime);
            
        }
    }
}