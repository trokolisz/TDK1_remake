using System.Xml.Serialization;
using TDK1_remake;



public class SolarPanelPlacement
{
    static void Main()
    {
        int N = 5; // Sorok maximális száma
        int M = 10; // Oszlopok maximális száma
        double w = 1.0; // Panel szélessége
        double h = 1.0; // Panel magassága
        int P = 9; // Panelek maximális száma

        SolarPanelPlacement1.Solution(N, M, w, h, P);


        Console.WriteLine("\n!\n");


        SolarPanelPlacement2.Solution(N, M, w, h, P);




        Console.ReadKey();
    }
}
