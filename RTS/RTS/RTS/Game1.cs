using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Threading;
using System.IO;

namespace RTS
{
    public enum GameState
    {
        Menu,
        Play,
        Pause,
    }
   
    public enum Animations
    {
        None,
        StartGameselected,
        Loading,
    }


    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        VirtualScreen virtualScreen;
        SpriteBatch spriteBatch;
        GameState gs;

        Random rand;
        
        Animations anim;

        Stack<Animations> lastAnims;

        Texture2D cursorTexture;
        Rectangle cursorRect;
        int mouseX;
        int mouseY;
        int actualMouseX;
        int actualMouseY;
        KeyboardState oldKBState;
        MouseState oldMState;
        int viewX;
        int viewY;
        int fullScreenWidth;
        int fullScreenHeight;
        Rectangle mapRect;
        Texture2D mapTexture;
        int mapHeight;
        int mapWidth;
        Tile[,] map;
        String input;
        Rectangle currentView;

        Rectangle drag;

        int originalDragX;
        int originalDragY;
        Texture2D knightTest;
        Texture2D red;
        Texture2D blue;
        List<Actor> selected;
        Texture2D pixel;

        Command peasantCommand = new Command("Peasant");
        Command CallToArmsCommand = new Command("CallToArms");
        Command BackToWorkCommand = new Command("BackToWork");


        //HUD
        Texture2D HUDTexture;
        Rectangle HUDRect;

        Texture2D brick;
        Dictionary<Int32, Command> commands;

        List<Building> buildingList;

        Texture2D logo;
        Rectangle logoRect;
        Texture2D background;
        Rectangle backgroundRect;

        List<Button> buttonList;
        Button StartGameButton;
        Button OptionsButton;
        Button ExitButton;
        Button VideoButton;
        Button SoundButton;
        Button GameplayButton;
        Button BackButton;

        int buttonPosition;
        Boolean animateButtons;

        Building commandGridBuilding;
        List<Actor> commandsGridActors;
        Dictionary<Int32, Point> commandLocations = new Dictionary<int, Point>();
        int commandHeight = 40;
        int commandWidth = 40;

        //TODO: move textures to class
        

        Texture2D peasant;
        Texture2D knight;
        Texture2D archer;
        Texture2D Mage;

        //Units
        List<Knight> knights;
        List<Peasant> peasants;
        List<Archer> archers;
        List<Mage> mages;
        List<Footman> footmans;
        //Buildings
        TownHall townHall;
        List<Farm> farms;
        List<Church> churches;
        List<Barracks> barrackses;
        List<Mine> mines;
        List<ScoutTower> scoutTowers;


        List<Knight> EKnights;
        List<Peasant> EPeasants;
        List<Archer> EArchers;
        List<Mage> EMages;
        List<Footman> EFootmans;

        int gold;
        int resources;

        //animations
        Texture2D black;
        Rectangle fadeRect;
        int fade;
        float fadeOpacity;
        int optionsselected;
        Texture2D loading1;
        Texture2D loading2;
        Texture2D loading3;
        Rectangle loading1Rect;
        Rectangle loading2Rect;
        Rectangle loading3Rect;
        int loadingCounter;
        int loadingPic;
        Thread backgroundThread;



        Texture2D grass;
        SpriteFont font;

        public Game1()
        {
            Mouse.WindowHandle = Window.Handle;

            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;

            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";


        }


        protected override void Initialize()
        {
            rand = new Random();
            viewX = 0;
            viewY = 580;
            
            virtualScreen = new VirtualScreen(960, 540, GraphicsDevice);
            currentView = new Rectangle(0, 0, virtualScreen.VirtualWidth, virtualScreen.VirtualHeight);
            Mouse.SetPosition(virtualScreen.VirtualWidth / 2, virtualScreen.VirtualHeight / 2);

            mapHeight = 35;
            mapWidth = 50;
            map = new Tile[mapHeight, mapWidth];
            input = File.ReadAllText("map.txt");
            fullScreenHeight = mapHeight * 32;
            fullScreenWidth = mapWidth * 32;
            mapRect = new Rectangle(0, 0, fullScreenWidth, fullScreenHeight);


            logoRect = new Rectangle(280, 50, 400, 102);
            backgroundRect = new Rectangle(0, 0, 1162, 633);

            buttonList = new List<Button>();
            StartGameButton = new Button(new Rectangle(380, 202, 200, 60), true);
            OptionsButton = new Button(new Rectangle(380, 275, 200, 60), true);
            ExitButton = new Button(new Rectangle(380, 350, 200, 60), true);
            VideoButton = new Button(new Rectangle(virtualScreen.VirtualWidth, 202, 160, 60), false);
            SoundButton = new Button(new Rectangle(virtualScreen.VirtualWidth, 277, 160, 60), false);
            GameplayButton = new Button(new Rectangle(virtualScreen.VirtualWidth, 352, 160, 60), false);
            BackButton = new Button(new Rectangle(100, virtualScreen.VirtualHeight, 106, 40), false);

            addButtonPosition();
            buttonPosition = 1;
            animateButtons = false;

            buttonList.Add(StartGameButton);
            buttonList.Add(OptionsButton);
            buttonList.Add(ExitButton);
            buttonList.Add(VideoButton);
            buttonList.Add(SoundButton);
            buttonList.Add(GameplayButton);
            buttonList.Add(BackButton);


            gs = GameState.Menu;
            anim = Animations.None;

            knights = new List<Knight>();
            peasants = new List<Peasant>();
            archers = new List<Archer>();
            mages = new List<Mage>();
            footmans = new List<Footman>();

            EKnights = new List<Knight>();
            EPeasants = new List<Peasant>();
            EArchers = new List<Archer>();
            EMages = new List<Mage>();
            EFootmans = new List<Footman>();

            
            farms = new List<Farm>();
            churches = new List<Church>();
            barrackses = new List<Barracks>();
            mines = new List<Mine>();
            scoutTowers = new List<ScoutTower>();

            commandGridBuilding = null;
            commandsGridActors = new List<Actor>();

            gold = 500;
            resources = 50;

            selected = new List<Actor>();
            cursorRect = new Rectangle(50, 50, 30, 29);
            oldMState = Mouse.GetState();
            drag = Rectangle.Empty;

            buildingList = new List<Building>();

            //HUD
            commands = new Dictionary<Int32, Command>();
            HUDRect = new Rectangle(0, virtualScreen.VirtualHeight - 175, virtualScreen.VirtualWidth, 175);

            //animation
            fade = 200;
            fadeRect = new Rectangle(0, 0, 960, 540);
            fadeOpacity = 0f;
            optionsselected = 30;
            lastAnims = new Stack<Animations>();
            loading1Rect = new Rectangle(280, 150, 380, 116);
            loading2Rect = new Rectangle(280, 150, 411, 116);
            loading3Rect = new Rectangle(280, 150, 443, 116);
            loadingCounter = 30;
            loadingPic = 1;

            commandLocations.Add(1, new Point(745, 395));
            commandLocations.Add(2, new Point(796, 395));
            commandLocations.Add(3, new Point(849, 395));
            commandLocations.Add(4, new Point(901, 395));
            commandLocations.Add(5, new Point(744, 442));
            commandLocations.Add(6, new Point(796, 442));
            commandLocations.Add(7, new Point(849, 442));
            commandLocations.Add(8, new Point(901, 442));
            commandLocations.Add(9, new Point(744, 490));
            commandLocations.Add(10, new Point(797, 490));
            commandLocations.Add(11, new Point(849, 490));
            commandLocations.Add(12, new Point(901, 490));


            mouseX = virtualScreen.VirtualWidth / 2 + viewX;
            mouseY = virtualScreen.VirtualHeight / 2 + viewY;
            base.Initialize();
        }


        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cursorTexture = this.Content.Load<Texture2D>("cursor");
            //mapTexture = this.Content.Load<Texture2D>("map");
            knightTest = this.Content.Load<Texture2D>("TestKnight");
            red = this.Content.Load<Texture2D>("red");
            blue = this.Content.Load<Texture2D>("blue");
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });
            //brick = this.Content.Load<Texture2D>("brick");
            background = this.Content.Load<Texture2D>("background");

            logo = this.Content.Load<Texture2D>("Start Screen\\logo");
            StartGameButton.unpressed = this.Content.Load<Texture2D>("Start Screen\\Start Game");
            OptionsButton.unpressed = this.Content.Load<Texture2D>("Start Screen\\Options Button");
            ExitButton.unpressed = this.Content.Load<Texture2D>("Start Screen\\Exit Button");
            VideoButton.unpressed = this.Content.Load<Texture2D>("Start Screen\\VideoUnpressed");
            VideoButton.pressed = this.Content.Load<Texture2D>("Start Screen\\VideoPressed");
            SoundButton.unpressed = this.Content.Load<Texture2D>("Start Screen\\SoundUnpressed");
            SoundButton.pressed = this.Content.Load<Texture2D>("Start Screen\\SoundPressed");
            GameplayButton.unpressed = this.Content.Load<Texture2D>("Start Screen\\GameplayUnpressed");
            GameplayButton.pressed = this.Content.Load<Texture2D>("Start Screen\\GameplayPressed");
            BackButton.unpressed = this.Content.Load<Texture2D>("Start Screen\\BackUnpressed");
            BackButton.pressed = this.Content.Load<Texture2D>("Start Screen\\BackPressed");

            //HUD
            HUDTexture = this.Content.Load<Texture2D>("HUD\\HUD");

            //Building Textures
            TownHall.halfCompleted = this.Content.Load<Texture2D>("Buildings\\TownHallHalf");
            TownHall.completed = this.Content.Load<Texture2D>("Buildings\\TownHall");

            //Actor Textures
            Peasant.spriteSheet = this.Content.Load<Texture2D>("Actors\\peasant");
            Peasant.icon = this.Content.Load<Texture2D>("Icons\\peasantIcon");

            //Actor Sounds
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pspissd1"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pspissd2"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pspissd3"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pspissd4"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pspissd5"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pspissd6"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pspissd7"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pswhat1"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pswhat2"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pswhat3"));
            Peasant.sounds.Add(this.Content.Load<SoundEffect>("Sounds\\Peasant\\Pswhat4"));
            Peasant.readyToWork = this.Content.Load<SoundEffect>("Sounds\\Peasant\\Psready");
            
            //animations
            black = this.Content.Load<Texture2D>("black");
            loading1 = this.Content.Load<Texture2D>("Start Screen\\loading1");
            loading2 = this.Content.Load<Texture2D>("Start Screen\\loading2");
            loading3 = this.Content.Load<Texture2D>("Start Screen\\loading3");

            grass = this.Content.Load<Texture2D>("grasstest");
            font = this.Content.Load<SpriteFont>("font");

            peasantCommand.image = this.Content.Load<Texture2D>("Icons\\peasantIcon");
            CallToArmsCommand.image = this.Content.Load<Texture2D>("Icons\\CallToArmsIcon");
            BackToWorkCommand.image = this.Content.Load<Texture2D>("Icons\\BackToWorkIcon");
        }


        protected override void UnloadContent()
        {

        }

        public void addButtonPosition()
        {
            StartGameButton.addPosition(1, new Position(380, 202, 15, true));
            StartGameButton.addPosition(2, new Position(0 - StartGameButton.rect.Width, 202, 15, false));
            StartGameButton.addPosition(3, new Position(0 - StartGameButton.rect.Width, 202, 15, false));
            StartGameButton.addPosition(4, new Position(0 - StartGameButton.rect.Width, 202, 15, false));
            StartGameButton.addPosition(5, new Position(0 - StartGameButton.rect.Width, 202, 15, false));

            OptionsButton.addPosition(1, new Position(380, 275, 15, true));
            OptionsButton.addPosition(2, new Position(80, 275, 10, false));
            OptionsButton.addPosition(3, new Position(0 - OptionsButton.rect.Width, 275, 10, false));
            OptionsButton.addPosition(4, new Position(0 - OptionsButton.rect.Width, 275, 10, false));
            OptionsButton.addPosition(5, new Position(0 - OptionsButton.rect.Width, 275, 10, false));

            ExitButton.addPosition(1, new Position(380, 350, 15, true));
            ExitButton.addPosition(2, new Position(0 - ExitButton.rect.Width, 350, 15, false));
            ExitButton.addPosition(3, new Position(0 - ExitButton.rect.Width, 350, 15, false));
            ExitButton.addPosition(4, new Position(0 - ExitButton.rect.Width, 350, 15, false));
            ExitButton.addPosition(5, new Position(0 - ExitButton.rect.Width, 350, 15, false));

            VideoButton.addPosition(1, new Position(virtualScreen.VirtualWidth, 202, 15, false));
            VideoButton.addPosition(2, new Position(virtualScreen.VirtualWidth - 450, 202, 15, true));
            VideoButton.addPosition(3, new Position(80, 202, 15, false));
            VideoButton.addPosition(4, new Position(0 - VideoButton.rect.Width, 202, 15, false));
            VideoButton.addPosition(5, new Position(0 - VideoButton.rect.Width, 202, 15, false));

            SoundButton.addPosition(1, new Position(virtualScreen.VirtualWidth, 277, 15, false));
            SoundButton.addPosition(2, new Position(virtualScreen.VirtualWidth - 450, 277, 15, true));
            SoundButton.addPosition(4, new Position(80, 277, 15, false));
            SoundButton.addPosition(3, new Position(0 - SoundButton.rect.Width, 277, 15, false));
            SoundButton.addPosition(5, new Position(0 - SoundButton.rect.Width, 277, 15, false));

            GameplayButton.addPosition(1, new Position(virtualScreen.VirtualWidth, 353, 15, false));
            GameplayButton.addPosition(2, new Position(virtualScreen.VirtualWidth - 450, 353, 15, true));
            GameplayButton.addPosition(5, new Position(80, 353, 15, false));
            GameplayButton.addPosition(4, new Position(0 - SoundButton.rect.Width, 353, 15, false));
            GameplayButton.addPosition(3, new Position(0 - SoundButton.rect.Width, 353, 15, false));

            BackButton.addPosition(1, new Position(100, virtualScreen.VirtualHeight + 20, 2, false));
            BackButton.addPosition(2, new Position(100, virtualScreen.VirtualHeight - 60, 2, true));
            BackButton.addPosition(3, new Position(100, virtualScreen.VirtualHeight - 60, 2, true));
            BackButton.addPosition(4, new Position(100, virtualScreen.VirtualHeight - 60, 2, true));
            BackButton.addPosition(5, new Position(100, virtualScreen.VirtualHeight - 60, 2, true));
        }


        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            virtualScreen.Update();



            UpdateInput();
            if (gs == GameState.Menu)
            {
                animations();
            }
            if (gs == GameState.Play)
            {
                updateHUD();
                updateActors();

                
            }


            base.Update(gameTime);
        }

        public void updateHUD()
        {

        }

        public void updateActors()
        {
            foreach (Peasant a in townHall.peasants)
            {
                a.updateAngle();
                a.updateMovement();
                
            }
        }


        public List<Actor> getEnemiesNearby(Actor a)
        {

            List<Actor> enemiesNearby = new List<Actor>();
            foreach (Knight b in EKnights)
            {
                if (getDistance(a.XLoc, a.YLoc, b.XLoc, b.YLoc) <= a.awareness)
                {
                    enemiesNearby.Add(b);
                }
            }
            foreach (Archer b in EArchers)
            {
                if (getDistance(a.XLoc, a.YLoc, b.XLoc, b.YLoc) <= a.awareness)
                {
                    enemiesNearby.Add(b);
                }
            }
            foreach (Peasant b in EPeasants)
            {
                if (getDistance(a.XLoc, a.YLoc, b.XLoc, b.YLoc) <= a.awareness)
                {
                    enemiesNearby.Add(b);
                }
            }
            foreach (Mage b in EMages)
            {
                if (getDistance(a.XLoc, a.YLoc, b.XLoc, b.YLoc) <= a.awareness)
                {
                    enemiesNearby.Add(b);
                }
            }
            foreach (Footman b in EFootmans)
            {
                if (getDistance(a.XLoc, a.YLoc, b.XLoc, b.YLoc) <= a.awareness)
                {
                    enemiesNearby.Add(b);
                }
            }
            return enemiesNearby;
        }

        public void animations()
        {
             if (anim == Animations.StartGameselected)
             {
                 fade--;
                 fadeOpacity += .01f;
                 if (fade <= 0)
                 {
                     //gs = GameState.Play;
                     anim = Animations.Loading;
                     backgroundThread = new Thread(generateMap);
                     backgroundThread.Start();
                     anim = Animations.Loading;
                     fade = 200;
                    mouseX = virtualScreen.VirtualWidth / 2 + viewX;
                    mouseY = virtualScreen.VirtualHeight / 2 + viewY;
                }
             }
            
             if (anim == Animations.Loading)
             {
                 loadingCounter--;
                 if (loadingCounter <= 0)
                 {
                     loadingCounter = 30;
                     loadingPic++;
                 }

                 if (loadingPic >=4)
                 {
                     loadingPic = 1;
                 }
             }
            
            if (animateButtons)
            {
                int count = 0;
                foreach (Button button in buttonList)
                {
                    button.animate(buttonPosition);
                    if (button.finishedMoving)
                    {
                        count++;
                    }
                }
                if (count == buttonList.Count)
                {
                    animateButtons = false;
                    foreach (Button button in buttonList)
                    {
                        button.finishedMoving = false;
                    }
                }
            }
        }

        public void setCommandGrid()
        {
            if (commandGridBuilding != null)
            {
                if (commandGridBuilding is TownHall)
                {
                    commands.Add(1, peasantCommand);
                    commands.Add(11, CallToArmsCommand);
                    commands.Add(12, BackToWorkCommand);
                }
            }
            else if (commandsGridActors.Count() > 0)
            {
                
            }
           
        }

        public void addBuilding(String buildingType)
        {
            if (buildingType == "TownHall")
            {
                townHall = new TownHall(new Tile(28, 7, 0));
                    for (int i = 28; i < 28 + TownHall.tileWidth; i++) {
                        for (int j = 7; j < 7 + TownHall.tileHeight; j++) {
                            map[i, j].buildable = false;
                        }
                    }
                    
                }
            }
        

        public void UpdateInput()
        {
            KeyboardState newKBState = Keyboard.GetState();
            MouseState newMState = Mouse.GetState();

            //Keyboard
            if (newKBState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            oldKBState = newKBState;


            //Mouse

            actualMouseX = Mouse.GetState().X;
            actualMouseY = Mouse.GetState().Y;

            mouseX += (actualMouseX - virtualScreen.VirtualWidth / 2);
            mouseY += (actualMouseY - virtualScreen.VirtualHeight / 2);

            if (gs == GameState.Menu)
            {
                if (mouseX <= currentView.X)
                {
                    mouseX = currentView.X;
                }
                if (mouseX >= currentView.Right - 5)
                {
                    mouseX = currentView.Right - 5;
                }

                if (mouseY <= currentView.Y)
                {
                    mouseY = currentView.Y;
                }
                if (mouseY >= currentView.Bottom - 5)
                {
                    mouseY = currentView.Bottom - 5;
                }

                if (newMState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)
                {
                   
                    int oldpos = buttonPosition;

                    if (StartGameButton.isClicked(mouseX, mouseY, buttonPosition))
                    {

                        anim = Animations.StartGameselected;

                    }
                    else if (OptionsButton.isClicked(mouseX, mouseY, buttonPosition))
                    {
                        buttonPosition = 2;
                    }
                    else if (ExitButton.isClicked(mouseX, mouseY, buttonPosition))
                    {
                        Exit();
                    }
                    else if (VideoButton.isClicked(mouseX, mouseY, buttonPosition))
                    {
                        buttonPosition = 3;
                    }
                    else if (SoundButton.isClicked(mouseX, mouseY, buttonPosition))
                    {
                        buttonPosition = 4;
                    }
                    else if (GameplayButton.isClicked(mouseX, mouseY, buttonPosition))
                    {
                        buttonPosition = 5;
                    } 
                    else if (BackButton.isClicked(mouseX, mouseY, buttonPosition)) 
                    {
                        if (buttonPosition == 2)
                        {
                            buttonPosition = 1;
                        }
                        else if (buttonPosition == 3 || buttonPosition == 4 || buttonPosition == 5)
                        {
                            buttonPosition = 2; 
                        }
                    }
                    if (oldpos != buttonPosition)
                    {
                        animateButtons = true;
                    }

                }
            }
            else if (gs == GameState.Play)
            {
                
                if (newMState.RightButton == ButtonState.Pressed)
                {
                    foreach (Actor a in selected)
                    {
                        a.move(mouseX, mouseY);
                    }
                }
                 
                if (newMState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Released)
                {

                    if (mouseY > virtualScreen.VirtualHeight - HUDRect.Height + viewY)
                    {

                      foreach (KeyValuePair<Int32, Command> command in commands)
                        {
                            if (new Rectangle(commandLocations[command.Key].X, commandLocations[command.Key].Y, commandWidth, commandHeight).Contains(mouseX - viewX, mouseY - viewY))
                            {
                                doCommand(command.Value.name);
                            }
                        }


                    }
                    else
                    {

                        drag = new Rectangle(mouseX, mouseY, 0, 0);
                        originalDragX = drag.X;
                        originalDragY = drag.Y;


                        commands.Clear();
                        if (towerClicked())
                        {
                            setCommandGrid();
                        }
                        else if (actorClicked())
                        {

                        }

                    }
                }
                else if (newMState.LeftButton == ButtonState.Pressed && oldMState.LeftButton == ButtonState.Pressed)
                {

                    if (mouseY > virtualScreen.VirtualHeight - HUDRect.Height + viewY)
                    {

                    }
                    else
                    {

                        if (mouseX > originalDragX + 5 || mouseX < originalDragX - 5 || mouseY > originalDragY + 5 || mouseY < originalDragY - 5)
                        {

                            Boolean flippedX = false;
                            Boolean flippedY = false;

                            if (mouseX < originalDragX)
                            {
                                flippedX = true;
                            }

                            if (mouseY < originalDragY)
                            {
                                flippedY = true;
                            }

                            if (flippedX && flippedY)
                            {
                                drag = new Rectangle(mouseX, mouseY, originalDragX - mouseX, originalDragY - mouseY);
                            }
                            else if (flippedX)
                            {
                                drag = new Rectangle(mouseX, originalDragY, originalDragX - mouseX, mouseY - originalDragY);
                            }
                            else if (flippedY)
                            {
                                drag = new Rectangle(originalDragX, mouseY, mouseX - originalDragX, originalDragY - mouseY);
                            }
                            else
                            {
                                drag = new Rectangle(originalDragX, originalDragY, mouseX - originalDragX, mouseY - originalDragY);
                            }
                        }
                    }
                    



                    if (newKBState.IsKeyUp(Keys.LeftControl))
                    {
                        selected.Clear();
                    }

                    foreach (Knight a in knights)
                    {
                        if (a.rect.Intersects(drag))
                        {
                            selected.Add(a);
                        }
                    }
                    foreach (Archer a in archers)
                    {
                        if (a.rect.Intersects(drag))
                        {
                            selected.Add(a);
                        }
                    }
                    
                    foreach (Peasant a in townHall.peasants)
                    {
                        if (a.rect.Intersects(drag))
                        {
                            selected.Add(a);
                        }
                    }
                   
                    foreach (Mage a in mages)
                    {
                        if (a.rect.Intersects(drag))
                        {
                            selected.Add(a);
                        }
                    }

                }
                else if (newMState.LeftButton == ButtonState.Released && oldMState.LeftButton == ButtonState.Pressed)
                {
                    drag = Rectangle.Empty;
                    foreach (Actor a in selected)
                    {
                        if (a is Peasant)
                        {
                            Peasant.playSound();
                            break;
                        }
                    }
                }
                

                //View
                if (mouseX <= currentView.X)
                {
                    mouseX = currentView.X - 5;

                    if (viewX <= 0)
                    {
                        viewX = 0;
                    }
                    else
                    {
                        viewX -= 5;
                    }


                }
                else if (mouseX >= currentView.Right)
                {
                    mouseX = currentView.Right + 5;

                    if (viewX >= fullScreenWidth - virtualScreen.VirtualWidth)
                    {
                        viewX = fullScreenWidth - virtualScreen.VirtualWidth;

                    }
                    else
                    {
                        viewX += 5;
                    }

                }

                if (mouseY <= currentView.Y)
                {
                    mouseY = currentView.Y - 5;

                    if (viewY <= 0)
                    {
                        viewY = 0;
                    }
                    else
                    {
                        viewY -= 5;
                    }


                }
                else if (mouseY >= currentView.Bottom)
                {
                    mouseY = currentView.Bottom + 5;
                    if (viewY >= fullScreenHeight - virtualScreen.VirtualHeight + HUDRect.Height)
                    {
                        viewY = fullScreenHeight - virtualScreen.VirtualHeight + HUDRect.Height;
                    }
                    else
                    {
                        viewY += 5;
                    }

                }
                currentView.X = viewX;
                currentView.Y = viewY;
            }

            oldMState = newMState;

            cursorRect.X = mouseX;
            cursorRect.Y = mouseY;

            Mouse.SetPosition(virtualScreen.VirtualWidth / 2, virtualScreen.VirtualHeight / 2);
        }

        public void doCommand(String commandName)
        {
            if (commandName == "Peasant")
            {
                spawnPeasant();
            }
        }

        public void spawnPeasant()
        {
            townHall.peasants.Add(new Peasant(getRandPointAroundBuilding(townHall), false));
            Peasant.readyToWork.Play();
        }

        public Point getRandPointAroundBuilding(Building building)
        {
            double random = rand.NextDouble();
            int x;
            int y;

            if (random < .25)
            {
                x = rand.Next(building.rect.Left - 50, building.rect.Left-40);
                y = rand.Next(building.rect.Top, building.rect.Bottom);
            } else if (random > .25 && random < .5)
            {
                x = rand.Next(building.rect.Right, building.rect.Right + 10);
                y = rand.Next(building.rect.Top, building.rect.Bottom);
            }
            else if(random > .5 && random < .75)
            {
                x = rand.Next(building.rect.Left, building.rect.Right);
                y = rand.Next(building.rect.Top - 50, building.rect.Top - 40);
            } else
            {
                x = rand.Next(building.rect.Left, building.rect.Right);
                y = rand.Next(building.rect.Bottom, building.rect.Bottom + 10);
            }
            


                return new Point(x, y);
        }

        public Boolean actorClicked()
        {
            foreach (Peasant peasant in townHall.peasants)
            {
                if (peasant.rect.Contains(new Point(mouseX, mouseY)))
                {
                    commandsGridActors.Add(peasant);
                    
                }
            }
            if (commandsGridActors.Count > 0)
            {
                return true;
            }
            return false;
        }

        public Boolean towerClicked()
        {
            
            
                if (townHall.rect.Contains(new Point(mouseX, mouseY)))
                {
                    commandGridBuilding = townHall;
                    return true;
                }
            
            
            return false;
        }

        public void generateMap()
        {
            int x = 0;
            int y = 0;

            foreach (var row in input.Split('\n'))
            {
                y = 0;
                foreach (var col in row.Trim().Split(','))
                {

                    map[x, y] = new Tile(x, y, int.Parse(col.Trim()));
                    y++;
                }
                x++;
            }


            addBuilding("TownHall");

            anim = Animations.None;
            gs = GameState.Play;

            backgroundThread.Abort(); 
        }


        protected override void Draw(GameTime gameTime)
        {
            virtualScreen.BeginCapture();
            GraphicsDevice.Clear(Color.DarkSlateGray);

            if (gs == GameState.Menu && anim != Animations.Loading)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(background, backgroundRect, Color.White);
                spriteBatch.Draw(logo, logoRect, Color.White);
                spriteBatch.Draw(StartGameButton.unpressed, StartGameButton.rect, Color.White * .85f);
                spriteBatch.Draw(OptionsButton.unpressed, OptionsButton.rect, Color.White * .85f);
                spriteBatch.Draw(ExitButton.unpressed, ExitButton.rect, Color.White * .85f);
                spriteBatch.Draw(VideoButton.unpressed, VideoButton.rect, Color.White * .85f);
                spriteBatch.Draw(SoundButton.unpressed, SoundButton.rect, Color.White * .85f);
                spriteBatch.Draw(GameplayButton.unpressed, GameplayButton.rect, Color.White * .85f);
                spriteBatch.Draw(BackButton.unpressed, BackButton.rect, Color.White * .85f);

                spriteBatch.Draw(cursorTexture, cursorRect, Color.White);
                if (anim == Animations.StartGameselected)
                {
                    spriteBatch.Draw(black, fadeRect, Color.White * fadeOpacity);
                }
                spriteBatch.End();
            }
            else if (anim == Animations.Loading)
            {
                spriteBatch.Begin();
                if (loadingPic == 1)
                {
                    spriteBatch.Draw(loading1, loading1Rect, Color.White);
                }
                else if (loadingPic == 2)
                {
                    spriteBatch.Draw(loading2, loading2Rect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(loading3, loading3Rect, Color.White);
                }

                spriteBatch.End();
            }
            else if (gs == GameState.Play)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateTranslation(-viewX, -viewY, 0));

                

                for (int rows = 0; rows < map.GetLength(0); rows++)
                {
                    for (int cols = 0; cols < map.GetLength(1); cols++)
                    {
                        Tile tile = map[rows, cols];
                        if (tile.buildable)
                        {
                            spriteBatch.Draw(grass, tile.rect, Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(grass, tile.rect, Color.White);
                        }


                    }
                }


               

                //hella good code

                spriteBatch.End();
                drawActors();
                drawBuildings();

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateTranslation(-viewX, -viewY, 0));
                if (drag != Rectangle.Empty)
                {
                    DrawBorder(drag, 3, new Color(52, 152, 219));
                }
                foreach (Actor a in selected)
                {
                    DrawBorder(new Rectangle(a.rect.X - 3, a.rect.Y - 6, a.rect.Width + 6, a.rect.Height + 6), 2, Color.Red);
                }
                spriteBatch.End();
                drawHUD();
                drawCommands();


                //Draw Cursor on Top of everything
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateTranslation(-viewX, -viewY, 0));
                spriteBatch.Draw(cursorTexture, cursorRect, Color.White);
                spriteBatch.End();
            }



            virtualScreen.EndCapture();

            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();
            virtualScreen.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void drawBuildings()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateTranslation(-viewX, -viewY, 0));

           
                spriteBatch.Draw(townHall.getTexture(), townHall.rect, Color.White);
            
           
            spriteBatch.End();
        }

        public void drawCommands()
        {
            spriteBatch.Begin(); 
                
            foreach(KeyValuePair<int, Command> command in commands) {
                spriteBatch.Draw(command.Value.image, new Rectangle(commandLocations[command.Key].X, commandLocations[command.Key].Y, 40, 40), Color.White);
            }

            spriteBatch.End();
        }

        public void drawActors()
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.CreateTranslation(-viewX, -viewY, 0));


            foreach (Peasant peasant in townHall.peasants)
            {
                if (peasant.angle == Angle.DownLeft || peasant.angle == Angle.Left || peasant.angle == Angle.UpLeft)
                {
                    spriteBatch.Draw(peasant.getTexture(), peasant.rect, peasant.getSourceRect(), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.None, 0.5f);
                } else
                {
                    spriteBatch.Draw(peasant.getTexture(), peasant.rect, peasant.getSourceRect(), Color.White, 0.0f, new Vector2(0, 0), SpriteEffects.FlipHorizontally, 0.5f);
                }
            }
            spriteBatch.End();
        }

        public void drawHUD()
        {
           
       

            spriteBatch.Begin();

            
            spriteBatch.Draw(HUDTexture, HUDRect, Color.White);

            List<Actor> HUDActorList = new List<Actor>();
            foreach (Actor a in selected)
            {
                if (HUDActorList.Count < 8)
                {
                    HUDActorList.Add(a);
                } else
                {
                    break;
                }
            }
            int x = 400;
            int y = 420;
            int actorCount = 0;
            foreach (Actor a in HUDActorList)
            {
                spriteBatch.Draw(Peasant.icon, new Rectangle(x, y, 40, 40), Color.White);
                actorCount++;
                x += 50;
                if (actorCount == 4)
                {
                    y += 30;
                    x = 400;
                }
            }

            spriteBatch.End();
        }

        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }

        public double getDistance(double X1, double Y1, double X2, double Y2)
        {
            return Math.Sqrt(Math.Pow((X2 - X1), 2) + Math.Pow((Y2 - Y1), 2));
        }
    }
}
