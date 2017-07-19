import java.awt.Color;
import java.awt.Graphics;
import java.awt.Graphics2D;
import java.awt.Toolkit;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.geom.AffineTransform;
import java.awt.event.ActionListener;
import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;
import java.awt.image.*;
import javax.swing.JPanel;
import javax.swing.Timer;
import java.util.concurrent.*;
import sun.audio.*;
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import java.awt.Image;
import java.io.*;
import javax.imageio.*;
import java.util.*;
/**
 * Write a description of class Enemy here.
 * 
 * @author (your name) 
 * @version (a version number or a date)
 */
public class Enemy 
{
    private BufferedImage enemyImage;
    private double health;
    private int x;
    private int y;
    private int drop;
    private int move;
    private Random gen = new Random();
    public int pistolcount;
    public int mgcount;
    public int snipercount;
    public Enemy(double health)
    {
        this.health = health;
        x = gen.nextInt(1600);
        y = gen.nextInt(200);
        drop = gen.nextInt(50);
        try{
            enemyImage = ImageIO.read(new File("Zleft.png"));
        } catch (Exception ex){}
    }

    public boolean drop()
    {
        if(drop == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void damage(int damage)
    {
        health -= damage;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public void addX(boolean plox)
    {
        move++;
        if(plox && move%3==0){
            x++;
        }
        else if(!plox){
            x++;
        }
        //ImageIcon i = new ImageIcon("Zright.png"); 
        //image = i.getImage();
        if(move == 100000)
        {
            move = 0;
        }
    }

    public void subX(boolean plox)
    {
        move++;
        if(plox && move%3==0){
            x--;
        }
        else if(!plox){
            x--;
        }
        //ImageIcon i = new ImageIcon("Zleft.png"); 
        //image = i.getImage();
        if(move == 100000)
        {
            move = 0;
        }
    }

    public void addY(boolean plox)
    {
        move++;
        if(plox && move%3==0){
            y++;
        }
        else if(!plox){
            y++;
        }
        //ImageIcon i = new ImageIcon("Zdown.png"); 
        //image = i.getImage();
        if(move == 100000)
        {
            move = 0;
        }
    }

    public void subY(boolean plox)
    {
        move++;
        if(plox && move%3==0){
            y--;
        }
        else if(!plox){
            y--;
        }
        //ImageIcon i = new ImageIcon("Zup.png"); 
        //image = i.getImage();
        if(move == 100000)
        {
            move = 0;
        }
    }

    public BufferedImage getEnemyImage()
    {
        return enemyImage;
    }

    public double getHealth()
    {
        return health;
    }

    public Rectangle getBounds()
    {
        return new Rectangle(x,y,enemyImage.getWidth(),enemyImage.getHeight());
    }
}
