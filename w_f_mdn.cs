using System;
using System.Windows.Forms;
using System.Drawing;

public class BisectionForm : Form
{
    TextBox tbA, tbB, tbEps;
    Button btnFind;
    Label lblResult;

    public BisectionForm()
    {
        this.Text = "Метод ділення навпіл";
        this.ClientSize = new Size(420, 160);

        Label la = new Label() { Text = "a:", Location = new Point(10, 15), AutoSize = true };
        tbA = new TextBox() { Location = new Point(30, 12), Width = 100, Text = "1.0" };

        Label lb = new Label() { Text = "b:", Location = new Point(140, 15), AutoSize = true };
        tbB = new TextBox() { Location = new Point(160, 12), Width = 100, Text = "2.0" };

        Label le = new Label() { Text = "eps:", Location = new Point(10, 45), AutoSize = true };
        tbEps = new TextBox() { Location = new Point(50, 42), Width = 100, Text = "1e-6" };

        btnFind = new Button() { Text = "Знайти корінь", Location = new Point(260, 20), Width = 130 };
        btnFind.Click += BtnFind_Click;

        lblResult = new Label() { Text = "Результат: ", Location = new Point(10, 80), AutoSize = true };

        this.Controls.AddRange(new Control[] { la, tbA, lb, tbB, le, tbEps, btnFind, lblResult });
    }

    private void BtnFind_Click(object sender, EventArgs e)
    {
        if (!double.TryParse(tbA.Text, out double a) ||
            !double.TryParse(tbB.Text, out double b) ||
            !double.TryParse(tbEps.Text, out double eps))
        {
            MessageBox.Show("Будь ласка, введіть числа (a, b, eps).", "Некоректні дані", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            int it;
            double root = Bisection(a, b, eps, 1000, out it);
            lblResult.Text = $"Результат: x ≈ {root:F8}    f(x) = {f(root):G5}    ітерацій = {it}";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    static double f(double x) => x*x - 2*x - 6;

    static double Bisection(double a, double b, double eps, int maxIter, out int iterations)
    {
        double fa = f(a), fb = f(b);
        iterations = 0;
        if (fa * fb > 0) throw new ArgumentException("f(a) та f(b) повинні мати різні знаки (f(a)*f(b) < 0).");

        double c = a;
        while ((b - a) / 2.0 > eps && iterations < maxIter)
        {
            c = (a + b) / 2.0;
            double fc = f(c);

            if (fc == 0.0) { iterations++; return c; }

            if (fa * fc < 0) { b = c; fb = fc; }
            else { a = c; fa = fc; }
            iterations++;
        }
        return (a + b) / 2.0;
    }

    [STAThread]
    public static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new BisectionForm());
    }
}
