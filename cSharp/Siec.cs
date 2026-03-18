class Siec
{
    Warstwa[] warstwy;
    public int liczbaWarstw;

    // liczbaWejsc -> to jest ile jest neuronow w warstwie wejsciowej -> ustawiane jest to dla 1 warsty ukrytej
    // ileWarstw -> to jest ile jest warstw ukrytych + wyjsciowa
    // ileNeuronowWWarstwie -> ile neuronów w warstwach ukrytych i wyjsciowej
    public Siec(int liczbaWejsc,int ileWarstw, int[] ileNeuronowWWarstwie)
    {
        this.liczbaWarstw = ileWarstw;
        warstwy = new double[ileWarstw];
        for (int i = 0; i < ileWarstw; i++)
        {
            if(i == 0)
            {
                warstwy[i] = new Warstwa(liczbaWejsc, ileNeuronowWWarstwie[i]);
            }
            else
            {
                warstwy[i] = new Warstwa(liczbaWejsc[ileNeuronowWWarstwie[i-1]], ileNeuronowWWarstwie[i]);
            }
        }
    }

    // Dla Pierwszego wejscia wejscia to będą warstwa początkowa u nas 8x8 grid painta
    public double[] ObliczWyjscie(double[] wejscia)
    {
        double[] wyjscie = null;
        for(int i = 0; i < liczbaWarstw ; i++)
        {
            // ustawiamy wyjscie po obliczeniu warstwy 0 -> czyli warstwy wejsciowej
            wyjscie = warstwy[i].ObliczWyjscie(wejscia);
            // ustawiamy wejscia do następnej warstwy (warstwy 1 ukrytej) wyjscie z warstwy 0
            wejscia = wyjscie;
        }
        // Dostajemy tablice wartosci warstwy wyjsciowej
        return wyjscie;
    }

    // wejscie -> nasz paint 8x8
    // target -> czyli jaka to litere jest (to co zaznaczylismy)
    // eta - > współczynnik uczenia
    public void Ucz(double[] wejscie, double[] target, double eta)
    {
        // Inicjalizacja wag następuje podczas tworzenia sieci

        // Forward propagation -> wynik uczenia
        ObliczWyjscie(wejscie);


        // Obliczenie delty dla warstwy wyjsciowej
        // liczbaWarstw - 1 -> ponieważ liczymy od 0 w tablicach
        Warstwa wyjsciowa = warstwy[liczbaWarstw - 1];

        for (int i = 0; i < wyjsciowa.liczbaNeuronow; i++)
        {
            // Bierzymy z warstwy wyjsciowej neuron i zapisujemy referencje do niego
            Neuron neuronObecny = wyjsciowa[i];

            // zapisujemy jego wyjscie
            double wyjscieNeuronu = neuronObecny.wyjscie;

            // zapisujemy w tym neuronie jego delte
            neuronObecny.delta = (target[i] - wyjscieNeuronu) * neuronObecny.pochodna();

            // wyjsciowa[i].delta = (target[i] - wyjsciowa[i].wyjscie) * wyjsciowa[i].pochodna();
        }


        // Obliczenie delty dla warstw ukrytych

        // liczbaWarstw - 2 -> ponieważ obliczyliśmy ostatnia warstwe (wyjsciową) dlatego -1
        for (int warstwa = liczbaWarstw - 2; warswa >= 0; warstwa--)
        {
            Warstwa warstwaObecna = warstwy[warstwa];
            Warstwa warstwaNastepna = warstwy[warstwa+1];

            for (int i = 0; i < warstwaObecna.liczbaNeuronow; i++)
            {
                Neuron neuronObecny = warstwaObecna[i];
                double suma = 0;

                for (int j = 0; j < warstwaNastepna.liczbaNeuronow; j++)
                {
                    // Suma -> to jest błąd (jak bardzo obwinia nas ten neuron o zły wynik) razy wage dla naszego neuronu
                    suma += warstwaNastepna.neuron[j].delta * warstwaNastepna.neuron[j].wagi[i+1];
                }
                neuronObecny.delta = neuronObecny.pochodna() * suma;
            }
        }
        // Backpropagation -> liczenie delty (błedu) dla każdego neuronu
            // poprzednia warstwa liczy delte z warstwy następnej
            // czyli warstwa ukryta 2 liczy delte dla każdego neuronu z warstwy wyjsciowej

        // Aktualizacja wag
        for (int warstwa = 0; warstwa < liczbaWarstw; warstwa++)
        {
            Warstwa warstwaObecna = warstwy[warstwa];
            // warstwaObecna.neurony[0]
            for (int neuron = 0; neuron < warstwaObecna.liczbaNeuronow; neuron++)
            {
                Neuron neuronObecny = warstwaObecna.neurony[neuron];
                
                // Dla każdego neuronu w każdej warstwie updatujemy bias który jest w wagi[0]
                neuronObecny.wagi[0] += eta * neuronObecny.delta;
                
                // aktualizujemy wagi dla każdego neuronu warstwy poprzedniej
                for (int waga = 1; waga < length; waga++)
                {
                    neuron.wagi[waga] += eta * neuron.delta * neuron.ostatnieWejscia[waga];
                }
            }
        }
    }
    
}