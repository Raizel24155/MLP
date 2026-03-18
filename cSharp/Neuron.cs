class Neuron
{
    // wagi dla neuronów z poprzedniej warstwy
    public double[] wagi;
    // Ile jest neuronow w poprzedniej warstwie
    public int liczbaWejsc;

    // Wejscia do którego wyjscie obliczyliśmy
    public double[] ostatnieWejscia;
    // Wyjscie neuronu, inaczej jego wartosc obliczona
    public double wyjscie;
    public double delta; // róźnica pomiędzy tym co chcemy a tym co uzyskaliśmy


    public Neuron()
    {
        liczbaWejsc = 0;
        wagi = null;
    }


    // liczbaWejsc + 1 -> tworzy tablica wag, +1 -> to jest bias który znajduje się w 0 elemencie
    public Neuron(int liczbaWejsc)
    {
        this.liczbaWejsc = liczbaWejsc;
        wagi = new double[liczbaWejsc + 1];
        Generuj();
    }


    // Pierwszy raz podczas tworzenia tworzy wagi dla każdego neuronu w warstwie poprzedniej
    public void Generuj()
    {
        Random random;
        for (int i = 0; i < liczbaWejsc; i++)
        {
            //  zmienia zakres od -1 do 1
            wagi[i] = (random.NextDouble() - 0.5) * 2 * 0.01; 
        }
    }

    public double[] ObliczWyjscie(double[] wejscia)
    {
        ostatnieWejscia = wejscia;
        // ustawiamy wartosc jako bias 
        double wartosc = wagi[0];
        for (int i = 1; i <= wejscia.Length; i++)
        {
            // dodajemy do biasu wartość dla każdego neuronu * jego wage
            wartosc += wagi[i] * wejscia[i-1];
        }

        // zamienia wartość uzyskana na od 0 do 1 
        wyjscie = 1.0/(1.0+Math.Exp(-wartosc));

        // zwracamy wartosc którą obliczyliśmy
        return wyjscie;
    }

    // zwracamy wartosc pochodnej naszego wyniku
    public double pochodna()
    {
        return wyjscie  * (1 - wyjscie);
    }
}