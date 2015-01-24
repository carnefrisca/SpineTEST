﻿using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Spine
{
    public class XnaTextureLoader : TextureLoader
    {
        GraphicsDevice device;
        Texture2D thistexture;

        public XnaTextureLoader(GraphicsDevice device, Texture2D thistexture)
        {
            this.device = device;
            this.thistexture = thistexture;
        }

        public void Load(AtlasPage page, String path)
        {
            Texture2D texture = thistexture;
            page.rendererObject = texture;
            page.width = texture.Width;
            page.height = texture.Height;
        }

        public void Unload(Object texture)
        {
            ((Texture2D)texture).Dispose();
        }
    }
}