class Neuron
{
    public double wartosc;
    public double[] wagi;

    public Neuron(double wartosc)
    {
        this.wartosc = wartosc;
        wagi = null;
    }

    public Neuron(double wagi[], double wartosci[])
    {
        wartosc = wagi[0];
        for (int i = 0; i < wartosci.length; i++)
        {
            wartosc += wagi[i]*wartosci[i]; 
        }
    }
}