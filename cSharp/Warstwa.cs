class Warstwa
{
    Neuron[] neurony;

    public Warstwa()
    {
        neurony = null;
    }

    public Warstwa(int liczbaWejsc, int liczbaNeuronow)
    {
        neurony = new Neuron[liczbaNeuronow];
        for(int i = 0; i < liczbaNeuronow; i++)
        {
            neurony[i] = new Neuron(liczbaWejsc);
        }
    }

    public double[] ObliczWyjscie(double[] wejscia)
    {
        double wyjscie = new double[neurony.Length];
        for (int i = 0; i < neurony.Length; i++)
        {
            wyjscie[i] = neurony[i].ObliczWyjscie(wejscia);
        }
        return wyjscie;
    }
}