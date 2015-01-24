using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Spine;

namespace SpineTEST
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SkeletonMeshRenderer skeletonRenderer;
        Skeleton skeleton;
        AnimationState state;

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            IsMouseVisible = true;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            skeletonRenderer = new SkeletonMeshRenderer(GraphicsDevice);
            skeletonRenderer.PremultipliedAlpha = true;
            
            String name = "anim_dude01";
            Texture2D texture = Content.Load<Texture2D>("anim_dude01");

            AtlasPage atlasPage = new AtlasPage();
            atlasPage.rendererObject = texture;
            atlasPage.width = texture.Width;
            atlasPage.height = texture.Height;
            Atlas atlas = new Atlas(@"Content\" + name + ".atlas", new XnaTextureLoader(GraphicsDevice,texture));
            
            SkeletonJson json = new SkeletonJson(atlas);
            json.Scale = 1.0f;
            
            skeleton = new Skeleton(json.ReadSkeletonData(@"Content\" + name + ".json"));
            skeleton.SetSkin("default");

            AnimationStateData stateData = new AnimationStateData(skeleton.Data);
            state = new AnimationState(stateData);
            state.SetAnimation(0, "anim_dude01_idle", true);          
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            state.Apply(skeleton);
            state.Update(gameTime.ElapsedGameTime.Milliseconds / 1000f);
            
            skeleton.X = 400.0f;
            skeleton.Y = 300.0f;

            skeleton.UpdateWorldTransform();
            skeletonRenderer.Begin();
            skeletonRenderer.Draw(skeleton);
            skeletonRenderer.End();

            base.Draw(gameTime);
        }
    }
}
