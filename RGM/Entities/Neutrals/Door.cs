using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using RGM.Entities.Projectiles;
using RGM.General.Animation;
using RGM.General.Collision;
using RGM.General.ContentHandling.Assets;
using RGM.General.ContentHandling.Rooms;
using RGM.General.DungeonGenerator;
using RGM.General.EventHandling;

// Static imports. ono.
using static RGM.RGM;

namespace RGM.Entities.Neutrals
{
    public class Door : Entity
    {
        private readonly Dictionary<Rectangle, int> animation;
        private readonly SimpleAnimator _simpleAnimator;

        private readonly dDirection direction;
        private bool isOpen;

        private static readonly Rectangle[] spritesheetPositions =
        {
            new Rectangle(0, 0, 8, 8),
            new Rectangle(0, 8, 8, 8),
            new Rectangle(0, 16, 8, 8),
            new Rectangle(0, 24, 8, 8),

            new Rectangle(RGM.tileSize * 2, 0, RGM.tileSize, RGM.tileSize) // Closed.
        };

        public Door(Vector2 position, dDirection direction, bool isOpen)
        {
            this.position = position;
            this.direction = direction;

            this.texture = AssetLoader.textures[dTextureKeys.arrow];

            switch (direction)
            {
                case dDirection.left:
                    animation = anim_left;
                    break;
                case dDirection.right:
                    animation = anim_right;
                    break;
                case dDirection.up:
                    animation = anim_up;
                    break;
                case dDirection.down:
                    animation = anim_down;
                    break;
            }


            this._simpleAnimator = new SimpleAnimator(texture, animation);

            this.myWidth = RGM.tileSize;
            this.myHeight = RGM.tileSize;

            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            this.isOpen = isOpen;

            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);

            GEventHandler.subscribe(open, dEvents.roomClear);
        }

        private void open(dEvents e)
        {
            isOpen = true;
        }

        public override void draw()
        {
            // RGM.spriteBatch.Draw(texture, position,
            //     isOpen ? spritesheetPositions[(int)direction] : 
            //              spritesheetPositions[4], 
            //     Color.White);
            _simpleAnimator.draw(position);
        }

        public override void onPlayerCollision()
        {

            // TODO smth is potentially fucked up here
            if (isOpen == false)
            {
                return;
            }

            Entity player = entities[0];

            switch (direction)
            {
                case dDirection.up:
                    player.mapPosition.Y -= 1;
                    break;

                case dDirection.down:
                    player.mapPosition.Y += 1;
                    break;

                case dDirection.right:
                    player.mapPosition.X += 1;
                    break;

                case dDirection.left:
                    player.mapPosition.X -= 1;
                    break;
            }

            RoomLoader.playRoom(
                DungeonGenerator.floorMap[player.mapPosition.X, player.mapPosition.Y].roomInfo.roomIndex,
                DungeonGenerator.floorMap[player.mapPosition.X, player.mapPosition.Y].roomInfo.roomType, direction);
            RoomLoader.changingRoom = true;
        }

        /* ---------------------- ANIMATION ------------------- */
        // This should be in json goddamn it...

        private static readonly Dictionary<Rectangle, int> closed_up    = new Dictionary<Rectangle, int>()  { { new Rectangle(0, 0, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> closed_down  = new Dictionary<Rectangle, int>()  { { new Rectangle(0, 8, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> closed_right = new Dictionary<Rectangle, int>()  { { new Rectangle(0, 16, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> closed_left  = new Dictionary<Rectangle, int>()  { { new Rectangle(0, 24, 8, 8), Int32.MaxValue } };

        private static readonly Dictionary<Rectangle, int> open_up = new Dictionary<Rectangle, int>      { { new Rectangle(24, 0, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> open_down  = new Dictionary<Rectangle, int>() { { new Rectangle(24, 8, 8, 8 ), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> open_right = new Dictionary<Rectangle, int>() { { new Rectangle(24, 16, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> open_left  = new Dictionary<Rectangle, int>() { { new Rectangle(24, 24, 8, 8), Int32.MaxValue } };
        
        private static readonly Dictionary<Rectangle, int> anim_up = new Dictionary<Rectangle, int>()
        {
            {
                new Rectangle(0, 0, 8, 8), 5
            },
            {
                new Rectangle(8, 0, 8, 8), 5
            },
            {
                new Rectangle(16, 0, 8, 8), 5
            },
            {
                new Rectangle(24, 0, 8, 8), 5
            }
        };

        private static readonly Dictionary<Rectangle, int> anim_down = new Dictionary<Rectangle, int>()
        {
            {
                new Rectangle(0, 8, 8, 8), 5
            },
            {
                new Rectangle(8, 8, 8, 8), 5
            },
            {
                new Rectangle(16, 8, 8, 8), 5
            },
            {
                new Rectangle(24, 8, 8, 8), 5
            }
        };

        private static readonly Dictionary<Rectangle, int> anim_right = new Dictionary<Rectangle, int>()
        {
            {
                new Rectangle(0, 16, 8, 8), 5
            },
            {
                new Rectangle(8, 16, 8, 8), 5
            },
            {
                new Rectangle(16, 16, 8, 8), 5
            },
            {
                new Rectangle(24, 16, 8, 8), 5
            }
        };
        
        private static readonly Dictionary<Rectangle, int> anim_left = new Dictionary<Rectangle, int>()
        {
            {
                new Rectangle(0, 24, 8, 8), 5
            },
            {
                new Rectangle(8, 24, 8, 8), 5
            },
            {
                new Rectangle(16, 24, 8, 8), 5
            },
            {
                new Rectangle(24, 24, 8, 8), 5
            }
        };
        
        private static readonly List<Dictionary<Rectangle, int>> animations_up = new List<Dictionary<Rectangle, int>>()
        {
            open_up,
            closed_up,
            anim_up
        };

        
    }
}