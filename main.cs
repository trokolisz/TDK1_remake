using Google.Protobuf.WellKnownTypes;
using System.Xml.Serialization;
using TDK1_remake;



public class SolarPanelPlacement
{
    static async void Main(string[] args)
    {
        int N = 5; // Sorok maximális száma
        int M = 10; // Oszlopok maximális száma
        double w = 1.0; // Panel szélessége
        double h = 1.0; // Panel magassága
        int P = 9; // Panelek maximális száma

        Models.Solution1(N, M, w, h, P);


        Console.WriteLine("\n!\n");


        Models.Solution2(N, M, w, h, P);


        Console.WriteLine("\n!\n");


        Models.Solution1_with_symmetry(N, M, w, h, P);

        Console.WriteLine("\n!\n");


        Models.Model_with_rectangular_symmetry(N, M, w, h, P);


        Console.ReadKey();

        //await Program.Main(args);
        
    }
}
