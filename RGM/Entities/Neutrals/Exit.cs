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

namespace RGM.Entities.Neutrals
{
    public class Exit : Entity
    {
        
        public Exit(Vector2 position, dDirection direction, bool isOpen)
        {
            // Set direction and position
            this.position = position;

            // Get texture
            this.texture = AssetLoader.textures[dTextureKeys.exit];

            // Calculate tile position
            this.myWidth = RGM.tileSize;
            this.myHeight = RGM.tileSize;
            this.tilePosition = Util.pixelPositionToTilePosition(position, myWidth, myHeight);

            // Setup collider and colliding teams
            this.team = dTeam.neutrals;
            this.collider = new Hitbox(position, myWidth, myHeight);

        }
        
        public override void onPlayerCollision()
        {
        }

    }
}