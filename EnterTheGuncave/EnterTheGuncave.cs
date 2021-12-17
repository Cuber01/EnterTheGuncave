using System.Collections.Generic;
using EnterTheGuncave.Content;
using EnterTheGuncave.Entities;
using EnterTheGuncave.Entities.Allies;
using EnterTheGuncave.Entities.Baddies;
using EnterTheGuncave.Entities.Neutrals;
using EnterTheGuncave.General;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace EnterTheGuncave
{
    public class EnterTheGuncave : Game
    {
        
        public static readonly List<Entity> entities = new List<Entity>();
        public static readonly List<Entity> entitiesToBeSpawned = new List<Entity>();
        private static readonly List<Entity> entitiesToBeKilled = new List<Entity>();

        public const int roomWidth = 18;
        public const int roomHeight = 11;

        public const int tileSize = 16;
        public const int scale = 4;

        private const int windowWidth = roomWidth * tileSize * scale;
        private const int windowHeight = roomHeight * tileSize * scale;

        private readonly Matrix scaleMatrix = Matrix.CreateScale(scale, scale, 1.0f);
        private readonly GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

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
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetLoader assetLoader = new AssetLoader(Content);
            
            assetLoader.loadTextures();
            
            entities.Add(new Player(new Vector2(50, 50)));
           // entities.Add(new WalkingEnemy(new Vector2(128, 80)));
            entities.Add(new Stone(new Vector2(100, 100)));
        }

        protected override void Update(GameTime gameTime)
        {

            Input.updateKeyboardState();
            Input.updateMouseState();
            
            
            // foreach (Entity spawn in entitiesToBeSpawned)
            // { 
            //     entities.Add(spawn);
            // }
            // entitiesToBeSpawned.Clear();
            //     
            //     
            // foreach( Entity entity in entities )
            // { 
            //     if (entity.dead)
            //     {
            //         entitiesToBeKilled.Add(entity);
            //     }
            //     
            //     entity.update();
            // }
            //
            // foreach ( Entity victim in entitiesToBeKilled )
            // {
            //     entities.Remove(victim);
            // }
            // entitiesToBeKilled.Clear();
            //     


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: scaleMatrix);

            DrawUtils draw = new DrawUtils(GraphicsDevice, spriteBatch);
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