using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RGM.Entities;
using RGM.Entities.Allies;
using RGM.General;
using RGM.General.ContentHandling.Assets;
using RGM.General.ContentHandling.Rooms;
using RGM.General.DungeonGenerator;
using RGM.General.EventHandling;
using RGM.General.Graphics;
using RGM.General.Input;
using RGM.General.Sound;
using RGM.Items;

namespace RGM
{
    public class RGM : Game
    {
        public static Entity Player;
        private textInfo playerHP;

        public static dGameState gameState = dGameState.playing;

        public static readonly List<Entity> entities = new List<Entity>();
        public static readonly List<Entity> entitiesToBeSpawned = new List<Entity>();
        public static readonly List<Entity> entitiesToBeKilled = new List<Entity>();

        public const int roomWidth = 19;
        public const int roomHeight = 11;
        
        public static readonly int[,] currentRoom = new int[roomWidth, roomHeight];
        public static int enemiesInRoom;
        
        public const int tileSize = 8;
        public const int scale = 8;

        private const int windowWidth = roomWidth * tileSize * scale;
        private const int windowHeight = roomHeight * tileSize * scale;

        public const int windowYMiddle = (windowWidth / 2);
        public const int windowXMiddle = (windowWidth / 2);

        public static readonly List<Type> allItems = new List<Type>()
        {
            // typeof(BloodChalice),
            // typeof(Arrow),
            // typeof(Medkit),
            // typeof(Determination),
            typeof(Knife),
            // typeof(ShotgunItem),
            // typeof(GunRing)
        };
        
        private readonly Matrix scaleMatrix = Matrix.CreateScale(scale, scale, 1.0f);
        private readonly GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        private static DrawUtils draw;

        public RGM()
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
            
            GEventResponse.subscribeGeneralMethods();
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
            assetLoader.loadSounds();
            assetLoader.loadFonts();
            
            SoundManager.init();
           
            RoomLoader.loadAllRooms();
            
            DungeonGenerator.generate();
            
            entities.Add(new Player(new Vector2(50, 100)));
            Player = entities[0];

            RoomLoader.playRoom(DungeonGenerator.floorMap[entities[0].mapPosition.X, entities[0].mapPosition.Y].roomInfo.roomIndex, 
                                DungeonGenerator.floorMap[entities[0].mapPosition.X, entities[0].mapPosition.Y].roomInfo.roomType, 
                                dDirection.center);

            playerHP = new textInfo(Player.stats.hitpoints.ToString(),
                1, true,
                new Vector2(15 * scale, 5 * scale),
                dFontKeys.pico8_big, Color.Red
            );
            
        }

        protected override void Update(GameTime gameTime)
        {

            switch (gameState)
            {
                
                case dGameState.playing:
                {
                    updateGame();
                    break;
                }
                
                case dGameState.game_over:
                {
                    updateGameOver();
                    break;
                }
                
                case dGameState.won:
                {
                    updateGameWon();
                    break;
                }
                
            }

            base.Update(gameTime);
            
        }

        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            
            switch (gameState)
            {
                
                case dGameState.playing:
                {
                    drawGame();
                    break;
                }
                
                case dGameState.game_over:
                {
                    drawGameOver();
                    break;
                }
            
                case dGameState.won:
                {
                    drawGameWon();
                    break;
                }
                
            }
                
            base.Draw(gameTime);
            
        }

        private void updateGame()
        {
            Input.updateKeyboardState();
            Input.updateMouseState();

            GEventHandler.update();
            GEventHandler.clearEvents();

            //spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: scaleMatrix);

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
                    RoomLoader.changingRoom = false;
                    break;
                }
            }

            foreach (Entity victim in entitiesToBeKilled)
            {
                entities.Remove(victim);
            }

            playerHP.text = Player.stats.hitpoints.ToString();

            entitiesToBeKilled.Clear();

            FontRenderer.dissappearingTextQueue.Add(playerHP);
            FontRenderer.textQueue.Add(playerHP);

            //spriteBatch.End();

        }

        private void drawGame()
        {
            // Main
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: scaleMatrix);
            
            //draw.drawGrid(Color.Gray);
            
            foreach( Entity entity in entities )
            {
                entity.draw();
                
                //entity.collider.draw(draw);
            }
            
            // Heart GUI
            spriteBatch.Draw(AssetLoader.textures[dTextureKeys.heart], new Vector2(2, 2), Color.White);

            spriteBatch.End();
            
            // Fonts
            spriteBatch.Begin();
            
            FontRenderer.renderQueue();
            
            spriteBatch.End();
        }
        
        

        private void drawGameOver()
        {
            // Fonts
            spriteBatch.Begin();
            
            FontRenderer.renderQueue();
            
            spriteBatch.End();
        }
        
        private void updateGameOver()
        {
            FontRenderer.textQueue.Add(new textInfo("Game Over!",
                1, true,new Vector2(RGM.windowXMiddle, RGM.windowYMiddle - 300),
                dFontKeys.pico8_big, Color.White));
            
            FontRenderer.textQueue.Add(new textInfo("Restart to try again.",
                1, true,new Vector2(RGM.windowXMiddle, RGM.windowYMiddle - 250),
                dFontKeys.pico8_small, Color.White));
            
            FontRenderer.textQueue.Add(new textInfo("(No, I really don't have restart implemented, go away.)",
                1, true, new Vector2(RGM.windowXMiddle, RGM.windowYMiddle - 200), 
                dFontKeys.pico8_small, Color.White));
        }
    }
}