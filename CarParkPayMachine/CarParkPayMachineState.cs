using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CarParkPayMachine
{
    public class CarParkPayMachineState
    {
        public CarParkPayMachine PayMachine { get; set; }
        public IPayMachineState State { get; set; }
 
        public CarParkPayMachineState()
        {
            PayMachine = new CarParkPayMachine();
            State = new SelectHoursState(this);
        }

        public void RunCarParkPayMachine()
        {
            while (true)
            {
                State.RunState();
            }
        }

        public void DisplayCoinsInserted()
        {
            if (PayMachine.UserFivePence > 0) { Console.WriteLine("{0} Five pence coins inserted", PayMachine.UserFivePence); }
            if (PayMachine.UserTenPence > 0) { Console.WriteLine("{0} Ten pence coins inserted", PayMachine.UserTenPence); }
            if (PayMachine.UserTwentyPence > 0) { Console.WriteLine("{0} Twenty pence coins inserted", PayMachine.UserTwentyPence); }
            if (PayMachine.UserFiftyPence > 0) { Console.WriteLine("{0} Fifty pence coins inserted", PayMachine.UserFiftyPence); }
            if (PayMachine.UserOnePound > 0) { Console.WriteLine("{0} One pound coins inserted", PayMachine.UserOnePound); }
            if (PayMachine.UserTwoPound > 0) { Console.WriteLine("{0} Two pound coins inserted", PayMachine.UserTwoPound); }
            Console.WriteLine();
        }

        public void DisplayMachineCoins()
        {
            Console.WriteLine("Machine five pence {0}", PayMachine.MachineFivePence);
            Console.WriteLine("Machine ten pence {0}", PayMachine.MachineTenPence);
            Console.WriteLine("Machine twenty pence {0}", PayMachine.MachineTwentyPence);
            Console.WriteLine("Machine fifty pence {0}", PayMachine.MachineFiftyPence);
            Console.WriteLine("Machine one pound {0}", PayMachine.MachineOnePound);
            Console.WriteLine("Machine two pound {0}", PayMachine.MachineTwoPound);
            Console.WriteLine();
        }
    }

    public interface IPayMachineState
    {
        CarParkPayMachineState PayMachineState { get; set; }

        void RunState() { }

        void StateChangeCheck() { }
    }

    public class SelectHoursState : IPayMachineState
    {
        public CarParkPayMachineState PayMachineState { get; set; }
        public SelectHoursState(CarParkPayMachineState carParkPayMachineState)
        {
            PayMachineState = carParkPayMachineState;
            PayMachineState.PayMachine.ResetUserCoins();
        }
        public SelectHoursState(IPayMachineState state)
        {
            PayMachineState = state.PayMachineState;
        }

        public void RunState()
        {
            bool optionVaild = false;

            if (PayMachineState.PayMachine.CanGiveChange())
            {
                Console.WriteLine("Change is available");
            }
            else
            {
                Console.WriteLine("No change available");
            }
            Console.WriteLine();

            // uncomment this line if you want to display coins in machine
            PayMachineState.DisplayMachineCoins();

            Console.WriteLine("Select number of hours");
            Console.WriteLine();

            Console.WriteLine("Option 1 1 hour        50p");
            Console.WriteLine("Option 2 2 hours    £1.25");
            Console.WriteLine("Option 3 4 hours    £3.70");
            Console.WriteLine("Option 4 6 hours    £6.00");
            Console.WriteLine("Option 5 12 hours  £13.55");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();

            while (!optionVaild)
            {
                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        optionVaild = true;
                        PayMachineState.PayMachine.SetFee(Duration.OneHour);
                        break;
                    case "2":
                        optionVaild = true;
                        PayMachineState.PayMachine.SetFee(Duration.TwoHours);
                        break;
                    case "3":
                        optionVaild = true;
                        PayMachineState.PayMachine.SetFee(Duration.FourHours);
                        break;
                    case "4":
                        optionVaild = true;
                        PayMachineState.PayMachine.SetFee(Duration.SixHours);
                        break;
                    case "5":
                        optionVaild = true;
                        PayMachineState.PayMachine.SetFee(Duration.TwelveHours); ;
                        break;
                    default:
                        Console.WriteLine("Invalid Option Please try again");
                        break;
                }
            }

            StateChangeCheck();
        }

        public void StateChangeCheck()
        {
            PayMachineState.State = new PayFeeState(this);
        }
    }

    public class PayFeeState : IPayMachineState
    {
        public CarParkPayMachineState PayMachineState { get; set; }

        public PayFeeState(IPayMachineState state)
        {
            PayMachineState = state.PayMachineState;
        }

        public void RunState()
        {
            bool optionValid = false;

            PayMachineState.DisplayCoinsInserted();

            Console.WriteLine("Fee due {0:C}", PayMachineState.PayMachine.Fee);
            Console.WriteLine();
            Console.WriteLine("Option 1 insert five pence coin");
            Console.WriteLine("Option 2 insert ten pence coin");
            Console.WriteLine("Option 3 insert twenty pence coin");
            Console.WriteLine("Option 4 insert fifty pence coin");
            Console.WriteLine("Option 5 insert one pound coin");
            Console.WriteLine("Option 6 insert two pound coin");
            Console.WriteLine("Option 7 cancel fee and return coins");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();

            while (!optionValid)
            {
                string option = Console.ReadLine();
                Console.WriteLine();

                switch (option)
                {
                    case "1":
                        PayMachineState.PayMachine.InsertFivePence();
                        optionValid = true;
                        break;
                    case "2":
                        PayMachineState.PayMachine.InsertTenPence();
                        optionValid = true;
                        break;
                    case "3":
                        PayMachineState.PayMachine.InsertTwentyPence();
                        optionValid = true;
                        break;
                    case "4":
                        PayMachineState.PayMachine.InsertFiftyPence();
                        optionValid = true;
                        break;
                    case "5":
                        PayMachineState.PayMachine.InsertOnePound();
                        optionValid = true;
                        break;
                    case "6":
                        PayMachineState.PayMachine.InsertTwoPound();
                        optionValid = true;
                        break;
                    case "7":
                        PayMachineState.PayMachine.FeeCanceled = true;
                        optionValid = true;
                        break;
                    default:
                        Console.WriteLine("Invalid Option Please try again");
                        break;
                }
            }

            StateChangeCheck();
        }

        public void StateChangeCheck()
        {
            if (PayMachineState.PayMachine.FeeCanceled)
            {
                PayMachineState.PayMachine.FeeCanceled = false;
                PayMachineState.State = new PayFeeCanceled(this);
            }
            else if (PayMachineState.PayMachine.Fee <= 0.0M)
            {
                if (PayMachineState.PayMachine.Fee < 0.0M)
                    PayMachineState.PayMachine.ChangeDue = Math.Abs(PayMachineState.PayMachine.Fee);
                PayMachineState.State = new FeePaidState(this);
            }
        }
    }

    public class FeePaidState : IPayMachineState
    {
        public CarParkPayMachineState PayMachineState { get; set; }

        public FeePaidState(IPayMachineState state)
        {
            PayMachineState = state.PayMachineState;
        }

        public void RunState()
        {
            PayMachineState.DisplayCoinsInserted();
            PayMachineState.PayMachine.ResetUserCoins();

            if (PayMachineState.PayMachine.ChangeDue > 0.0M & PayMachineState.PayMachine.CanGiveChange())
            {
                ReturnChange();
            }
            Console.WriteLine("Fee Paid Thank you");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();

            StateChangeCheck();
        }

        public void ReturnChange()
        {
            Change change = PayMachineState.PayMachine.ReturnChange();

            if (change.FivePenceCoins > 0)
            {
                Console.WriteLine("Five pence coins returned {0}", change.FivePenceCoins);
            }

            if (change.TenPenceCoins > 0)
            {
                Console.WriteLine("Ten pence coins returned {0}", change.TenPenceCoins);
            }

            if (change.TwentyPenceCoins > 0)
            {
                Console.WriteLine("Twenty pence coins returned {0}", change.TwentyPenceCoins);
            }

            if (change.FiftyPenceCoins > 0)
            {
                Console.WriteLine("Fifty pence coins returned {0}", change.FiftyPenceCoins);
            }

            if (change.OnePoundCoins > 0)
            {
                Console.WriteLine("One pound coins returned {0}", change.OnePoundCoins);
            }

            if (change.TwoPoundCoins > 0)
            {
                Console.WriteLine("Two pound coins returned {0}", change.TwoPoundCoins);
            }
            Console.WriteLine();
        }

        public void StateChangeCheck()
        {
            PayMachineState.State = new SelectHoursState(this);
        }
    }

    public class PayFeeCanceled : IPayMachineState
    {
        public CarParkPayMachineState PayMachineState { get; set; }
        public PayFeeCanceled(IPayMachineState state)
        {
            PayMachineState = state.PayMachineState;
        }

        public void RunState()
        {
            Console.WriteLine("Fee canceled");
            Console.WriteLine();

            Change change = PayMachineState.PayMachine.ReturnUserCoins();

            if (change.FivePenceCoins > 0)
            {
                Console.WriteLine("Five pence coins returned {0}", change.FivePenceCoins);
            }

            if (change.TenPenceCoins > 0)
            {
                Console.WriteLine("Ten pence coins returned {0}", change.TenPenceCoins);
            }

            if (change.TwentyPenceCoins > 0)
            {
                Console.WriteLine("Twenty pence coins returned {0}", change.TwentyPenceCoins);
            }

            if (change.FiftyPenceCoins > 0)
            {
                Console.WriteLine("Fifty pence coins returned {0}", change.FiftyPenceCoins);
            }

            if (change.OnePoundCoins > 0)
            {
                Console.WriteLine("One pound coins returned {0}", change.OnePoundCoins);
            }

            if (change.TwoPoundCoins > 0)
            {
                Console.WriteLine("Two pound coins returned {0}", change.TwoPoundCoins);
            }

            PayMachineState.PayMachine.ResetUserCoins();

            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();

            StateChangeCheck();
        }

        public void StateChangeCheck()
        {
            PayMachineState.State = new SelectHoursState(this);
        }
    }
}
