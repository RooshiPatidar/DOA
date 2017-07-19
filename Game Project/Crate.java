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
public class Crate
{
    // instance variables - replace the example below with your own
    private int x;
    private int y;
    private BufferedImage crate;
    private int time;
    private int buff;
    private Random gen = new Random();
    public Crate(int yes, int no)
    {
        try{
            crate = ImageIO.read(new File("crate.png"));
        } catch (Exception lol){}
        x = yes;
        y = no;
        time = 0;
        buff = gen.nextInt(3);
    }
    
    public int buffUsed()
    {
        return buff;
    }
    
    public BufferedImage getImage()
    {
        return crate;
    }
    
    public int getX()
    {
        return x;
    }
    
    public int getY()
    {
        return y;
    }
    
    public boolean okay()
    {
        time++;
        if(time<1000)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public Rectangle getBounds()
    {
        return new Rectangle(x,y,crate.getWidth(),crate.getHeight());
    }
}