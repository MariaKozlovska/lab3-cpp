using System;
using System.Windows.Forms;

public class HelloForm
{
    public static int Main()
    {
        Form fm = new Form();
        fm.Text = "Моя перша форма";
        fm.Width = 400;
        fm.Height = 300;
        fm.ShowDialog();
        return 0;
    }
}
