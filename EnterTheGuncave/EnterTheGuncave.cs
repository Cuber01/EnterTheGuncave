using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace EnterTheGuncave
{
    public class EnterTheGuncave : Game
    {

        private List<Entity> entities = new List<Entity>();

        private int windowWidth  = 500;
        private int windowHeight = 500;

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
        }

        protected override void Update(GameTime gameTime)
        {

            Input.updateKeyboardState();
            
            foreach( Entity entity in entities)
            {
                entity.update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();
            
            foreach( Entity entity in entities )
            {
                entity.draw();
            }
            
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}