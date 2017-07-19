import javax.swing.ImageIcon;
import java.awt.*;
import java.awt.image.BufferedImage;
import java.io.*;
import javax.imageio.ImageIO;
/**
 * Write a description of class PLayer here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Player 
{
    private int x;
    private int y;
    private int health;
    private int score;
    private int money;
    private Image image; 
    private int count;
    private int speed;
    private BufferedImage heroImage;
    private BufferedImage flashImage;
    public boolean saran;

    /**
     * Constructor for objects of class PLayer
     */
    public Player()
    {
        x = 500;
        y = 500;
        health = 100;
        score = 0;
        count = 0;
        money = 10000;
        speed = 2;
        try{
            heroImage = ImageIO.read(new File("HeroLeft.png"));
        } catch (Exception ex){}
    }

    public int getHealth()
    {
        return health;    
    }

    public void addHealth(int moar)
    {
        health += moar;    
    }

    public void damage()
    {
        count++;
        if (count%20 == 0) {
            health -= 1;   
        }
    }

    public void subMoney(int money)
    {
        this.money -= money;
    }

    public void addMoney(int money)
    {
        this.money += money;
    }

    public int getMoney()
    {
        return money;
    }

    public int getScore()
    {
        return score;
    }

    public void subScore(int score)
    {
        this.score -= score;
    }

    public void addScore(int score)
    {
        this.score += score;
    }

    
    public void setImageSaran()
    {
        try{
            heroImage = ImageIO.read(new File("saran.png"));
        } catch (Exception ex){}
    }

    public Image getImageHero()
    {
        return image;
    }

    public int moveUp()
    {
        y -= speed;
        return y;
    }

    public int moveLeft()
    {
        x -=speed;
        return x;
    }

    public int moveDown()
    {
        y +=speed;
        return y;
    }

    public int moveRight()
    {
        x +=speed;
        return x;
    }

    public void setPlayerImage()
    {
        if (saran) {
            try{
                heroImage = ImageIO.read(new File("saran.png"));
            } catch (Exception ex){}
        } else {
            try{
                heroImage = ImageIO.read(new File("HeroLeft.png"));
            } catch (Exception ex){}
        }
    }

    public void setPlayerFlash()
    {
        try{
            heroImage = ImageIO.read(new File("HeroFlash.png"));
        } catch (Exception lol){}
    }

    public BufferedImage getPlayerImage() 
    {
        return heroImage;
    }

    public int getX() {
        return x;
    }

    public int getY() {
        return y;
    }

    public void setX(int x) {
        this.x = x;
    }

    public void setY(int y) {
        this.y = y;
    }

    public void setSpeed(int speed){
        this.speed = speed;
    }
    
    public Rectangle getBounds()
    {
        return new Rectangle(x,y,heroImage.getWidth(),heroImage.getHeight());
    }
}
