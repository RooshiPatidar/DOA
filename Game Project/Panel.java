import javax.swing.*;
import java.awt.*;
import java.io.*;
import java.util.*;
import javax.imageio.*;
import java.awt.image.*;
import java.awt.event.*;
import sun.audio.*;
import java.applet.*;
import java.io.File;
import javax.sound.sampled.AudioInputStream;
import javax.sound.sampled.AudioSystem;
import javax.sound.sampled.Clip;
import javax.swing.*;
import javax.sound.sampled.*;
import javax.sound.sampled.AudioSystem;
import java.io.*;
import sun.audio.*;
import java.awt.geom.AffineTransform;
@SuppressWarnings("serial")
public class Panel extends JPanel   {
    //player
    private Player player1; 
    private Image hero; 

    private ArrayList<Enemy> enemies;
    private Ally ally1;
    //special needs
    private boolean start;
    private int endScreen;
    private int counter;

    //Counters
    private int endCount;
    private int pistolcount;
    private int mgcount;
    private int snipercount;
    private int count;
    private int kills;
    private int mouseX;
    private int mouseY;
    private boolean ally;

    //Images
    private BufferedImage lose;
    private BufferedImage background;
    private BufferedImage crosshair;
    private BufferedImage flash;

    //Sounds
    //private AudioListener listener;
    private AudioInputStream gunAIS;
    private Clip gunshot;
    private AudioInputStream regAIS;
    private Clip register;
    private AudioInputStream moanAIS;
    private Clip moan;
    private AudioInputStream growlAIS;
    private Clip growl;
    private AudioInputStream brainsAIS;
    private Clip brains;
    private AudioInputStream grenAIS;
    private Clip grenadeWAV;
    //Cursor
    private Cursor blankCursor;
    private Toolkit toolkit;
    private Image cursorImage;
    private Cursor cursor;

    //guns
    private boolean pistol;
    private boolean MG;
    private boolean sniper;

    //Listeners and their variables
    private KeyListener keyListener;
    private boolean up;  
    private boolean left; 
    private boolean down; 
    private boolean right;

    private MouseListener mouseListener;
    private boolean click;

    private MouseWheelListener mouseWheelListener;
    private Enemy allyTarget;
    private int steps;
    private int allyTimer;
    private int startHealth;
    private int startSpeed;

    private boolean title;
    private boolean shop;
    private BufferedImage shopImage;
    private BufferedImage titleImage;
    private boolean enter;
    private int allyAttack;
    private boolean unlockMG;
    private boolean unlockSniper;
    private boolean running;
    private int tick;

    private boolean bootOnce;
    private int bootCounter;

    private String kInput;
    private int flashCounter;
    private boolean instructions;
    private BufferedImage instructionPage;

    private BufferedImage pistolIcon;
    private BufferedImage MGIcon;
    private BufferedImage sniperIcon;
    private boolean nuke;
    private boolean konami;
    private boolean nukeAnimation;
    private int nukeCounter;
    private boolean nukeUse;
    private BufferedImage empty;
    private boolean emptyBackground;
    public boolean usedNuke;
    private int grenades;
    private int gCount;
    private BufferedImage grenadeImage;
    private int gX;
    private int gY;
    private boolean pos;
    private boolean grenade;
    private ArrayList<Enemy> gKills;
    private BufferedImage greenTint;
    private BufferedImage NukeBG;
    private boolean FMJ;
    private boolean dropped;
    private ArrayList<Crate> drops;
    private Crate takenAway;
    private Crate putIn;
    private boolean instakill;
    private boolean money2x;
    private boolean zombieSlow;
    private int goodIntName;
    private int badIntName;
    private int otherGoodIntName;
    public Panel() throws Exception{
        //JPanel
        setFocusable(true);
        setPreferredSize(new Dimension(1660, 980));
        setBackground(Color.white);

        //player
        player1 = new Player();
        ImageIcon i = new ImageIcon("Hero.png");
        hero = i.getImage();

        //enemies
        enemies = new ArrayList<Enemy>();

        //ally
        ally1 = new Ally(player1.getX(), player1.getY());

        //special needs
        start = true;
        endScreen = 0;
        endCount = 0;

        //counters
        count = 0;
        kills = 0;
        counter = 0;

        //mice
        mouseX = (int)MouseInfo.getPointerInfo().getLocation().getX();
        mouseY = (int)MouseInfo.getPointerInfo().getLocation().getY();

        //images
        try{
            shopImage = ImageIO.read(new File("shop.png"));
        } catch (Exception lol){}
        try{
            titleImage = ImageIO.read(new File("title.png"));
        } catch (Exception lol){}
        try{
            background = ImageIO.read(new File("Road2.jpg"));
        } catch (Exception lol){}
        try{
            crosshair = ImageIO.read(new File("crosshair.png"));
        } catch (Exception lol){}
        try{
            lose = ImageIO.read(new File("lose.png"));
        } catch (Exception lol){}
        try{
            flash = ImageIO.read(new File("flash.png"));
        } catch (Exception lol){}
        try{
            instructionPage = ImageIO.read(new File("instructionPage.png"));
        } catch (Exception lol){}

        //icons
        try{
            pistolIcon = ImageIO.read(new File("OPistol.png"));
        } catch (Exception lol){}
        try{
            MGIcon = ImageIO.read(new File("TMG.png"));
        } catch (Exception lol){}
        try{
            sniperIcon = ImageIO.read(new File("TSniper.png"));
        } catch (Exception lol){}

        //gunshot
        gunAIS = AudioSystem.getAudioInputStream(new File("gunshot2.wav"));
        gunshot = AudioSystem.getClip();
        try {
            gunshot.open(gunAIS);
        } catch (Exception e) {}

        //register
        regAIS = AudioSystem.getAudioInputStream(new File("register.wav"));
        register = AudioSystem.getClip();
        try {
            register.open(regAIS);
        } catch (Exception e) {}

        //moan
        moanAIS = AudioSystem.getAudioInputStream(new File("moan.wav"));
        moan = AudioSystem.getClip();
        try {
            moan.open(moanAIS);
        } catch (Exception e) {}

        //growl
        growlAIS = AudioSystem.getAudioInputStream(new File("growl.wav"));
        growl = AudioSystem.getClip();
        try {
            growl.open(gunAIS);
        } catch (Exception e) {}

        //brains
        brainsAIS = AudioSystem.getAudioInputStream(new File("brains.wav"));
        brains = AudioSystem.getClip();
        try {
            brains.open(brainsAIS);
        } catch (Exception e) {}

        grenAIS = AudioSystem.getAudioInputStream(new File("grenade.wav"));
        grenadeWAV = AudioSystem.getClip();
        try {
            grenadeWAV.open(grenAIS);
        } catch (Exception e) {}
        //Cursor
        toolkit = Toolkit.getDefaultToolkit();
        cursorImage = createImage(new MemoryImageSource(1, 1, new int[2], 0, 0));
        blankCursor = toolkit.createCustomCursor(cursorImage, new java.awt.Point(0,0),"Transparent");
        cursor = new Cursor(Cursor.HAND_CURSOR);

        //shop
        unlockMG = false;
        unlockSniper = false;
        nuke = false;
        konami = false;

        //guns
        pistol = true;
        MG = false;
        sniper = false;

        //Listeners and their variables
        keyListener = new MyKeyListener();
        addKeyListener(keyListener);
        up = false;
        left = false;
        down = false;
        right = false;

        mouseListener = new MouseEventDemo();
        addMouseListener(mouseListener);
        click = false;
        steps = 1;

        startHealth = 120;
        startSpeed = 250;

        title = true;
        enter = false;

        running = true;
        bootOnce = false;
        bootCounter = 0;
        flashCounter = 1;
        instructions = false;
        kInput = "";
        nukeAnimation = false;
        nukeCounter = 0;
        nukeUse = false;
        emptyBackground = false;
        usedNuke = false;
        grenades = 0;
        gCount = 0;
        try{
            grenadeImage = ImageIO.read(new File("grenade.png"));
        } catch (Exception lol){}
        pos = true;
        grenade = false;
        gKills = new ArrayList<Enemy>();
        try{
            greenTint = ImageIO.read(new File("NukeBG.png"));
        } catch (Exception lol){}
        FMJ = false;

        dropped = false;
        drops = new ArrayList<Crate>();
        instakill = false;
        money2x = false;
        zombieSlow = false;
        goodIntName = 0;
        otherGoodIntName = 0;
        badIntName = 0;
    }

    public void paintComponent(Graphics g)
    {
        try {
            Thread.sleep(8);
        } catch (Exception ex) {}
        Graphics2D g2d = (Graphics2D) g;

        super.paintComponent(g);
        g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING,RenderingHints.VALUE_ANTIALIAS_ON);
        if(instructions) {
            g.drawImage(instructionPage, 0, 0, this);
        } else if (title) {
            g.drawImage(titleImage, 0, 0, this);
        } else if (player1.getHealth() <= 0)
        {
            g.drawImage(lose, 0, 0, this);
            g.setFont(new Font("Stencil", Font.PLAIN, 60));
            g.drawString(Integer.toString(player1.getScore()),677,812);
            if (enter) {
                restart();
            }
        } else if (shop){
            g.setFont(new Font("Stencil", Font.PLAIN, 20));
            setCursor(cursor);
            g.drawImage(shopImage, 0, 0, this);
            g.setColor(Color.green);
            g.drawString("Money: $"+ player1.getMoney(),100,980);
            updateMouse();
            if (kInput.contains("UpUpDownDownLeftRightLeftRightBA")) {
                konami = true;
                kInput = "";
            }
            if(!unlockMG && click && mouseX>=60 && mouseY>=580 && mouseX<=340 && mouseY<=720 && player1.getMoney()>=150)
            {
                unlockMG = true;
                player1.subMoney(150);
                register.setFramePosition(0);
                register.start();
            }
            if(!unlockSniper && click && mouseX>=60 && mouseY>=780 && mouseX<=340 && mouseY<=930 && player1.getMoney()>=400)
            {
                unlockSniper = true;
                player1.subMoney(400);
                register.setFramePosition(0);
                register.start();
            }
            if(!ally && click && mouseX>=715 && mouseY>=560 && mouseX<=910 && mouseY<=740 && player1.getMoney()>=600)
            {
                ally = true;
                player1.subMoney(600);
                register.setFramePosition(0);
                register.start();
            }
            if(!nuke && click && mouseX>=720 && mouseY>=760 && mouseX<=905 && mouseY<=940 && player1.getMoney()>=800)
            {
                nuke = true;
                player1.subMoney(800);
                register.setFramePosition(0);
                register.start();
            }
            if(!FMJ && click && mouseX>=1224 && mouseY>=760 && mouseX<=1406 && mouseY<=940 && player1.getMoney()>=1000)
            {
                FMJ = true;
                player1.subMoney(1000);
                register.setFramePosition(0);
                register.start();
            }

            if (bootCounter == 1) {
                player1.subMoney(200);
                register.setFramePosition(0);
                register.start();
                player1.setSpeed(4);
                bootOnce = true;
                bootCounter++;
            } else if (bootCounter == 3) {
                player1.saran = true;
                bootCounter = 4;
            }
        } else  {
            setCursor(blankCursor);

            if (nukeUse){
                usedNuke = true;
                emptyBackground = !emptyBackground;
                if (emptyBackground)
                {
                }else if (nukeCounter >= 20 && !emptyBackground)
                {
                    nukeUse = false;
                    try{
                        background = ImageIO.read(new File("Road2.png"));
                    } catch (Exception lol){}

                } else {
                    g.drawImage(background, 0, 0, this);
                    drawIcons(g);
                    drawPlayer(g, g2d);
                    drawEnemies(g, g2d);
                    drawHUD(g);
                    if (ally) {
                        ally(g2d,g);
                    }

                }
                nukeCounter++;
            }else{
                g.drawImage(background, 0, 0, this);
                drawIcons(g);
                drawPlayer(g, g2d);
                drawEnemies(g, g2d);
                drawHUD(g);
                if (ally) {
                    ally(g2d,g);
                }
            }
            updateMouse();
            playerMove();
            //flash
            flashCounter++;
            if (flashCounter%3 == 0) {
                player1.setPlayerImage();
            }
            moveEnemies(g);
            damagePlayer();
            //drawEnemies(g, g2d);
            gunshot();
            zombieSounds();

            if (kills%16==0)
            {
                startHealth += 20;
                if (startSpeed >= 50) {
                    startSpeed -= 10;
                }
                kills++;
                enemySpawn(startHealth, startSpeed);
                spawnBoss(startHealth*1.75);
            } else {
                enemySpawn(startHealth, startSpeed);
            }
        }
        if (!shop) {
            grenade(g);
        }
        //Removing Enemies
        if(!shop){
            if(dropped)
            {
                for(Crate l : drops)
                {
                    if(player1.getBounds().intersects(l.getBounds()))
                    {
                        takenAway = l;
                        if(l.buffUsed() == 1)
                        {
                            instakill = true;
                            badIntName-=2000;
                        }
                        else if(l.buffUsed() == 2)
                        {
                            money2x = true;
                            goodIntName-=2000;
                        }
                        else if(l.buffUsed() == 0)
                        {
                            zombieSlow = true;
                            otherGoodIntName-=2000;
                        }
                    }
                    else if(l.okay())
                    {
                        g.drawImage(l.getImage(), l.getX(), l.getY(), this);
                    }
                    else
                    {
                        takenAway = l;
                    }
                }
                if(takenAway!=null)
                {
                    drops.remove(takenAway);
                }
                if(drops.size()==0)
                {
                    dropped = false;
                }
            }

            //Removing Enemies
            if(zombieSlow)
            {
                g.setColor(Color.white);
                g.setFont(new Font("Stencil", Font.PLAIN, 20));
                g.drawString("Slow Zombies", 5, 790);
                otherGoodIntName++;
            }
            if(otherGoodIntName>1999)
            {
                zombieSlow = false;
            }

            if(instakill)
            {
                g.setColor(Color.white);
                g.setFont(new Font("Stencil", Font.PLAIN, 20));
                g.drawString("Instakill", 5, 820);
                badIntName++;
            }
            if(badIntName>1999)
            {
                instakill = false;
            }

            if(money2x)
            {
                g.setColor(Color.white);
                g.setFont(new Font("Stencil", Font.PLAIN, 20));
                g.drawString("2X Money", 5, 850);
                goodIntName++;
            }
            if(goodIntName>1999)
            {
                money2x = false;
            }
        }
        Iterator<Enemy> x = enemies.iterator();
        while (x.hasNext())
        {
            Enemy d = x.next();
            if (d.getHealth() <=0)
            {
                if(d.drop())
                {
                    dropped = true;
                    putIn = new Crate(d.getX(), d.getY());
                    drops.add(putIn);
                }
                x.remove();
                player1.addScore(100);
                if(money2x)
                {
                    player1.addMoney(10);
                }
                else
                {
                    player1.addMoney(5);
                }
                kills++;
            }
        }
        g.dispose();
        repaint();
    }

    public void grenade(Graphics g)
    {
        if (grenade && gCount < 200) {
            if (pos)
            {
                gX = player1.getX();
                gY = player1.getY();
                pos = false;
            }
            g.drawImage(grenadeImage,gX + 20, gY + 50,this);
            gCount++;
            if(gCount == 199)
            {
                int index = enemies.size()-1;
                while (index >= 0) {
                    Enemy b = enemies.get(index);
                    int distance = (int)Math.sqrt(Math.pow(b.getX()-gX, 2)+Math.pow(b.getY()-gY, 2));

                    if(distance <= 250)
                    {
                        enemies.remove(index);
                        player1.addScore(100);
                        player1.addMoney(10);
                    }
                    index--;
                }
                grenadeWAV.setFramePosition(0);
                grenadeWAV.start();
                gCount = 0;
                pos = true;
                grenade = false;
                grenades --;
            }
        }
    }

    public void damagePlayer()
    {
        for (Enemy b : enemies)
        {
            if(b.getBounds().intersects(player1.getBounds()))
            {
                player1.damage();
            }
        }
    }

    public void nuke()
    {
        player1.addMoney(enemies.size()*5);
        enemies.clear();
        nukeUse = true;
    }

    public void drawIcons(Graphics g)
    {
        g.drawImage(pistolIcon, 1200, 900, this);
        g.drawImage(MGIcon, 1300, 900, this);
        g.drawImage(sniperIcon, 1400, 900, this);
    }

    public void opaquePistol()
    {
        try{
            pistolIcon = ImageIO.read(new File("OPistol.png"));
        } catch (Exception lol){}
        try{
            MGIcon = ImageIO.read(new File("TMG.png"));
        } catch (Exception lol){}
        try{
            sniperIcon = ImageIO.read(new File("Tsniper.png"));
        } catch (Exception lol){}
    }

    public void opaqueMG()
    {
        try{
            pistolIcon = ImageIO.read(new File("TPistol.png"));
        } catch (Exception lol){}
        try{
            MGIcon = ImageIO.read(new File("OMG.png"));
        } catch (Exception lol){}
        try{
            sniperIcon = ImageIO.read(new File("Tsniper.png"));
        } catch (Exception lol){}
    }

    public void opaqueSniper()
    {
        try{
            pistolIcon = ImageIO.read(new File("TPistol.png"));
        } catch (Exception lol){}
        try{
            MGIcon = ImageIO.read(new File("TMG.png"));
        } catch (Exception lol){}
        try{
            sniperIcon = ImageIO.read(new File("Osniper.png"));
        } catch (Exception lol){}
    }

    public void moveEnemies(Graphics g)
    {
        for (Enemy b : enemies) {
            if (player1.getX()+43 < b.getX()+34) {
                b.subX(zombieSlow);
                //b.move();
            }
            if (player1.getX()+43 > b.getX()+34) {
                b.addX(zombieSlow);
                //b.move();
            }
            if (player1.getY()+43 < b.getY()+34) {
                b.subY(zombieSlow);
                //b.move();
            }
            if (player1.getY()+43 > b.getY()+34){
                b.addY(zombieSlow);
                //b.move();
            }
            g.fill3DRect((b.getX()+28)-(int)(b.getHealth()/5), b.getY() - 10, (int)(b.getHealth()*.5), 5, true);
        }
    }

    public void gunshot()
    {
        if (!shop) {
            for (Enemy b : enemies)
            {
                if (click && mouseX >= b.getX() && mouseX <= b.getX()+68 && mouseY >= b.getY() && mouseY <= b.getY()+68)
                {
                    if (pistol ) {
                        pistol(b);
                    }
                    else if (MG) {
                        MG(b);
                    }
                    else if(sniper) {
                        sniper(b);
                    }

                }

            }
            if (click && pistol) {
                counter++;
                if (counter%25 == 0) {
                    gunshot.setFramePosition(0);
                    gunshot.start();
                    player1.setPlayerFlash();
                }
            } else if (click && MG) {
                counter++;
                if (counter%10 == 0) {
                    gunshot.setFramePosition(0);
                    gunshot.start();
                    player1.setPlayerFlash();
                }
            } else if (click && sniper) {
                counter++;
                if (counter%70 == 0) {
                    gunshot.setFramePosition(0);
                    gunshot.start();
                    player1.setPlayerFlash();
                }
            }
        }
    }

    public void drawEnemies(Graphics g, Graphics2D g2d)
    {
        for (Enemy b: enemies) {
            int drawLocationX = b.getX();
            int drawLocationY = b.getY();

            // Rotation information
            double rotation = Math.atan2(drawLocationY - player1.getY(), drawLocationX - player1.getX());

            //double rotationRequired = Math.atan2(drawLocationX - mouseY, drawLocationY - mouseX) - Math.PI / 2;
            double locationX = b.getEnemyImage().getWidth()/2;
            double locationY = b.getEnemyImage().getHeight()/2;
            AffineTransform tx = AffineTransform.getRotateInstance(rotation, locationX, locationY);
            AffineTransformOp op = new AffineTransformOp(tx, AffineTransformOp.TYPE_BILINEAR);

            // Drawing the rotated image at the required drawing locations
            g2d.drawImage(op.filter(b.getEnemyImage(), null), drawLocationX, drawLocationY, null);
        }
    }

    public void ally(Graphics2D g2d, Graphics g)
    {
        int closest = 10000;
        for (Enemy b : enemies) {
            int distance = (int)Math.sqrt(Math.pow(b.getX()-ally1.getX(), 2)+Math.pow(b.getY()-ally1.getY(), 2));

            if(distance<closest)
            {
                closest = distance;
                allyTarget = b;
            }
            if (allyTarget != null) {
                MGAlly(allyTarget);
            }
        }
        allyTimer++;
        if (allyTimer > 1700) {
            ally = false;
            allyTimer = 0;
        }
        if(ally1.getX()+70<player1.getX()){
            ally1.addX();
        }
        else if(ally1.getX()>player1.getX()+70){
            ally1.subX();
        }
        if(ally1.getY()+70<player1.getY()){
            ally1.addY();
        }
        else if(ally1.getY()>player1.getY()+70){
            ally1.subY();
        }
        //g.drawImage(ally1.getAllyImage(), ally1.getX(), ally1.getY(), this);

        int drawLocationX = ally1.getX();
        int drawLocationY = ally1.getY();

        // Rotation information
        double rotation = Math.atan2(ally1.getY() - allyTarget.getY(), ally1.getX() - allyTarget.getX());

        double locationX = ally1.getAllyImage().getWidth()/2;
        double locationY = ally1.getAllyImage().getHeight()/2;
        AffineTransform tx = AffineTransform.getRotateInstance(rotation, locationX, locationY);
        AffineTransformOp op = new AffineTransformOp(tx, AffineTransformOp.TYPE_BILINEAR);

        // Drawing the rotated image at the required drawing locations
        g2d.drawImage(op.filter(ally1.getAllyImage(), null), drawLocationX, drawLocationY, null);

    }

    public void zombieSounds() 
    {
        double one = Math.random()*10000;
        if (one <= enemies.size()*3) {

            moan.setFramePosition(0);
            moan.start();
        }
        double two = Math.random()*10000;  
        if (two <= enemies.size()*5) {
            growl.setFramePosition(0);
            growl.start();
        }
        double three = Math.random()*10000;  
        if (three <= enemies.size()) {
            brains.setFramePosition(0);
            brains.start();
        }
    }

    public void restart()
    {
        start = true;
        title = true;

        //counters
        count = 0;
        kills = 0;
        player1.subScore(player1.getScore());
        player1.setX(500);
        player1.setY(500);
        player1.subMoney(player1.getMoney());
        player1.addHealth(100);

        //guns
        pistol = true;
        MG = false;
        sniper = false;

        counter = 0;
        startHealth = 100;
        startSpeed = 250;

        enemies.clear();
        endCount = 0;
        endScreen = 0;
        running = true;
        bootOnce = false;
        bootCounter = 0;
        flashCounter = 1;
        instructions = false;
        kInput = "";
        nukeAnimation = false;
        nukeCounter = 0;
        nukeUse = false;
        emptyBackground = false;
        usedNuke = false;
        grenades = 0;
        gCount = 0;

        pos = true;
        grenade = false;
        gKills = new ArrayList<Enemy>();
        FMJ = false;
        dropped = false;
        drops = new ArrayList<Crate>();
        instakill = false;
        money2x = false;
        zombieSlow = false;
        goodIntName = 0;
        otherGoodIntName = 0;
        badIntName = 0;
        bootCounter = 0;
        player1.setSpeed(2);
    }

    public void drawPlayer(Graphics g, Graphics2D g2d)
    {
        int drawLocationX = player1.getX();
        int drawLocationY = player1.getY();

        // Rotation information
        double rotation = Math.atan2(drawLocationY - mouseY, drawLocationX - mouseX);

        double locationX = player1.getPlayerImage().getWidth()/2;
        double locationY = player1.getPlayerImage().getHeight()/2;
        AffineTransform tx = AffineTransform.getRotateInstance(rotation, locationX, locationY);
        AffineTransformOp op = new AffineTransformOp(tx, AffineTransformOp.TYPE_BILINEAR);

        // Drawing the rotated image at the required drawing locations
        g2d.drawImage(op.filter(player1.getPlayerImage(), null), drawLocationX, drawLocationY, null);
    }

    public void MGAlly(Enemy b)
    {
        allyAttack++;
        if (allyAttack%15 == 0) {
            b.damage(12);
        }
    }

    public void drawHUD(Graphics g)
    {
        //Crosshair
        g.drawImage(crosshair, mouseX-20, mouseY-20, this);

        //HUD
        g.setColor(Color.white);
        g.setFont(new Font("Stencil", Font.PLAIN, 35));
        g.drawString("Score: "+ player1.getScore(),100,950);
        g.setColor(Color.green);
        g.drawString("Money: $"+ player1.getMoney(),100,900);
        g.setColor(Color.red);
        g.fill3DRect(585, 950, player1.getHealth()*5, 10, true);
    }

    public void updateMouse()
    {
        mouseX = (int)MouseInfo.getPointerInfo().getLocation().getX()-8;
        mouseY = (int)MouseInfo.getPointerInfo().getLocation().getY() - 30;
    }

    public void enemySpawn(int health, int speed)
    {
        count++;
        if (count% speed == 0 && enemies.size() < 50) 
        {   
            Enemy a = new Enemy(health);
            enemies.add(a);
        }
    }

    public void spawnBoss(double health)
    {
        Enemy a = new Enemy(health);
        enemies.add(a);
    }

    public void pistol(Enemy b)
    {
        if (instakill) {
            pistolcount++;
            if (pistolcount%25 == 0) {
                b.damage(9001);
            }
        }else if (FMJ) {
            pistolcount++;
            if (pistolcount%25 == 0) {
                b.damage(70);
            }
        } else if (konami) {
            pistolcount++;
            if (pistolcount%15 == 0) {
                b.damage(30);
            }
        }else {
            pistolcount++;
            if (pistolcount%25 == 0) {
                b.damage(20);
            }
        }
    }

    public void MG(Enemy b)
    {
        if (instakill) {
            mgcount++;
            if (mgcount%10 == 0) {
                b.damage(9001);
            }
        }
        else if (FMJ) {
            mgcount++;
            if (mgcount%10 == 0) {
                b.damage(70);
            }
        }
        else if (konami) {
            mgcount++;
            if (mgcount%8 == 0) {
                b.damage(50);
            }
        } else {
            mgcount++;
            if (mgcount%10 == 0) {
                b.damage(15);
            }
        }
    }

    public void sniper(Enemy b)
    {
        if (konami) {
            snipercount++;
            if (snipercount%40 == 0) {
                b.damage(9001);
                gunshot.setFramePosition(0);
                gunshot.start();
            }
        } else {
            snipercount++;
            if (snipercount%70 == 0) {
                b.damage(9001);
                gunshot.setFramePosition(0);
                gunshot.start();
            }
        }
    }

    public void playerMove()
    {

        if (up == true && player1.getY() > 0){            
            player1.moveUp();           
        }
        if (left == true && player1.getX() > 0){            
            player1.moveLeft();            
        }
        if (down == true && player1.getY() < 980-86){           
            player1.moveDown();            
        }
        if (right == true && player1.getX() < 1660-86){            
            player1.moveRight();
        }
    } 

    public static void main(String[] args) throws Exception {
        JFrame frame = new JFrame("DOA");

        Panel panel = new Panel();
        frame.add(panel);

        frame.setSize(1000, 1000);
        frame.setVisible(true);
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
    }

    public class MyKeyListener implements KeyListener  {
        @Override
        public void keyTyped(KeyEvent e) {}

        @Override
        public void keyPressed(KeyEvent e) {

            if (KeyEvent.getKeyText(e.getKeyCode()).equals("W")) {
                //player1.setImageUp();
                up = true;
                kInput += KeyEvent.getKeyText(e.getKeyCode());
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("A")) {
                //player1.setImageLeft();
                left = true;
                kInput += KeyEvent.getKeyText(e.getKeyCode());
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("S")) {
                //player1.setImageDown();
                down = true;
                kInput += KeyEvent.getKeyText(e.getKeyCode());
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("D")) {
                //player1.setImageRight();
                right = true;
                kInput += KeyEvent.getKeyText(e.getKeyCode());
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("P")){
                shop = !shop;
                kInput += KeyEvent.getKeyText(e.getKeyCode());
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("Enter")){
                enter = true;
                kInput += KeyEvent.getKeyText(e.getKeyCode());
            } else {
                kInput += KeyEvent.getKeyText(e.getKeyCode());

            }

        }

        @Override
        public void keyReleased(KeyEvent e) {
            if (KeyEvent.getKeyText(e.getKeyCode()).equals("W")) {
                up = false;
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("A")) {
                left = false;
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("S")) {
                down = false;
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("D")) {
                right = false;
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("1")) {
                pistol = true;
                MG = false;
                sniper = false;
                opaquePistol();
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("2")&&unlockMG) {
                pistol = false;
                MG = true;
                sniper = false;
                opaqueMG();
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("3")&&unlockSniper) {
                pistol = false;
                MG = false;
                sniper = true;
                opaqueSniper();
            }else if (KeyEvent.getKeyText(e.getKeyCode()).equals("Enter")){
                enter = false;
            }else if (KeyEvent.getKeyText(e.getKeyCode()).equals("I")){
                instructions = !instructions;
            }else if (KeyEvent.getKeyText(e.getKeyCode()).equals("Q") && nuke){
                nuke();
            } else if (KeyEvent.getKeyText(e.getKeyCode()).equals("G") && grenades >=1) {
                grenade = true;
            }
        }
    }

    public class MouseEventDemo  implements MouseListener {

        public void mousePressed(MouseEvent e) {
            click = true;
            title = false;
        }

        public void mouseReleased(MouseEvent e) {
            click = false;
            if(shop && mouseX>=1220 && mouseY>=360 && mouseX<=1410 && mouseY<=540 && player1.getMoney()>=200)
            {
                bootCounter++;
            } 
            if(shop && grenades <= 2 && mouseX>=712 && mouseY>=351 && mouseX<=903 && mouseY<=538 && player1.getMoney()>=100)
            {
                grenades += 1;
                player1.subMoney(100);
                register.setFramePosition(0);
                register.start();
            }
            if(shop && mouseX>=1220 && mouseY>=550 && mouseX<=1410 && mouseY<=740 && player1.getMoney()>=75 && player1.getHealth() <=90 )
            {
                player1.subMoney(75);
                register.setFramePosition(0);
                register.start();
                if (player1.getHealth() > 70 && player1.getHealth() < 90) {
                    player1.addHealth(100-player1.getHealth());
                } else if (player1.getHealth() < 90){
                    player1.addHealth(30);
                }
            }
        }

        public void mouseEntered(MouseEvent e) {
        }

        public void mouseExited(MouseEvent e) {
        }

        public void mouseClicked(MouseEvent e) {

        }
    }
}

