namespace CarParkPayMachine
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CarParkPayMachineState PayMachineState = new CarParkPayMachineState();
            PayMachineState.RunCarParkPayMachine();
        }
    }
}
