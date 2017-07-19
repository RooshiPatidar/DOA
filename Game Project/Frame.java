import java.awt.*;
import javax.swing.*;
public class Frame extends JFrame
{
    public Frame()throws Exception
    {
        setTitle("DOA");
        setSize(1680, 1010);
        setLocation(0, 0);
        setDefaultCloseOperation(EXIT_ON_CLOSE);

        Panel panel = new Panel();
        Player first = new Player();

        Container c = getContentPane();
        c.add(panel);

        pack();
        setVisible(true);
        
        
    }
}

