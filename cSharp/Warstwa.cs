class Warstwa
{
    Neuron neurony[];

    public Warstwa()
    {
        neurony = null;
    }

    public Warstwa(int liczbaNeuronow )
    {
        neurony = new Neuron[liczbaNeuronow];
    }
}