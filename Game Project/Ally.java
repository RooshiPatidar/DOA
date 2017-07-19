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
public class Ally
{
    private BufferedImage allyImage;
    private int x;
    private int y;
    private int count;
    
    public Ally(int playerx, int playery)
    {
        try{
            allyImage = ImageIO.read(new File("Ally.png"));
        } catch (Exception ex){}
        x = playerx;
        y = playery;
    }

    public void addX()
    {
        x+=2;       
    }

    public void subX()
    {
        x-=2;
    }

    public void addY()
    {
        y+=2;
    }

    public void subY()
    {
        y-=2;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }

    public BufferedImage getAllyImage()
    {
        return allyImage;
    }
}