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
        private readonly List<Dictionary<Rectangle, int>> animation;
        private readonly ComplexAnimator animator;

        private readonly dDirection direction;
        
        private int openingTimer = 20;
        private bool isOpen;
        private bool isOpening;

        public Door(Vector2 position, dDirection direction, bool isOpen)
        {
            // Set direction and position
            this.position = position;
            this.direction = direction;

            // Get texture
            this.texture = AssetLoader.textures[dTextureKeys.door];

            // Choose a set of animations depending on our direction
            switch (direction)
            {
                case dDirection.left:
                    animation = animations_left;
                    break;
                case dDirection.right:
                    animation = animations_right;
                    break;
                case dDirection.up:
                    animation = animations_up;
                    break;
                case dDirection.down:
                    animation = animations_down;
                    break;
            }

            // Create an animator
            this.animator = new ComplexAnimator(texture, animation);

            // Calculate tile position
            this.myWidth = RGM.tileSize;
            this.myHeight = RGM.tileSize;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);
            
            // Setup open state
            this.isOpen = isOpen;
            this.animator.changeAnimation(isOpen ? 0 : 1);

            // Setup collider and colliding teams
            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);

            // Subscribe for roomClear event, if it happens, doors shall open
            GEventHandler.subscribe(open, dEvents.roomClear);
        }

        private void open(dEvents e)
        {
            isOpening = true;
            animator.changeAnimation(2);
        }

        public override void draw()
        {
            if (isOpening) openingTimer--;

            if (openingTimer <= 0)
            {
                isOpen = true;
                animator.changeAnimation(0);
            }
            
            animator.draw(position);

        }

        public override void onPlayerCollision()
        {

            // TODO smth is potentially fucked up here
            if (isOpen == false)
            {
                return;
            }

            Entity player = entities[0];
            Point oldMapPosition = player.mapPosition;

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
                DungeonGenerator.floorMap[player.mapPosition.X, player.mapPosition.Y].roomInfo.roomType, direction
            );
            
            RoomLoader.exitRoom(
                DungeonGenerator.floorMap[oldMapPosition.X, oldMapPosition.Y].roomInfo.roomIndex,
                DungeonGenerator.floorMap[oldMapPosition.X, oldMapPosition.Y].roomInfo.roomType
            );
            
            RoomLoader.changingRoom = true;
        }

        /* ---------------------- ANIMATION ------------------- */
        // This should be in json goddamn it...

        private static readonly Dictionary<Rectangle, int> closed_up    = new Dictionary<Rectangle, int>()  { { new Rectangle(0, 0, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> closed_down  = new Dictionary<Rectangle, int>()  { { new Rectangle(0, 8, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> closed_right = new Dictionary<Rectangle, int>()  { { new Rectangle(0, 16, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> closed_left  = new Dictionary<Rectangle, int>()  { { new Rectangle(0, 24, 8, 8), Int32.MaxValue } };

        private static readonly Dictionary<Rectangle, int> opened_up    = new Dictionary<Rectangle, int>      { { new Rectangle(24, 0, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> opened_down  = new Dictionary<Rectangle, int>() { { new Rectangle(24, 8, 8, 8 ), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> opened_right = new Dictionary<Rectangle, int>() { { new Rectangle(24, 16, 8, 8), Int32.MaxValue } };
        private static readonly Dictionary<Rectangle, int> opened_left  = new Dictionary<Rectangle, int>() { { new Rectangle(24, 24, 8, 8), Int32.MaxValue } };
        
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
            opened_up,
            closed_up,
            anim_up
        };
        
        private static readonly List<Dictionary<Rectangle, int>> animations_down = new List<Dictionary<Rectangle, int>>()
        {
            opened_down,
            closed_down,
            anim_down
        };
        
        private static readonly List<Dictionary<Rectangle, int>> animations_right = new List<Dictionary<Rectangle, int>>()
        {
            opened_right,
            closed_right,
            anim_right
        };
        
        private static readonly List<Dictionary<Rectangle, int>> animations_left = new List<Dictionary<Rectangle, int>>()
        {
            opened_left,
            closed_left,
            anim_left
        };

        
    }
}