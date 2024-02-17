using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CarParkPayMachine
{
    public class CarParkPayMachine
    {
        public IPayMachineState State { get; set; }
        public decimal Fee { get; set; }
        public decimal ChangeDue { get; set; }

        public bool FeeCanceled { get; set; }

        public int MachineFivePence { get; set; }

        public int MachineTenPence { get; set; }

        public int MachineTwentyPence { get; set; }

        public int MachineFiftyPence { get; set; }
        public int MachineOnePound { get; set; }

        public int MachineTwoPound { get; set; }

        public int UserFivePence { get; set; }

        public int UserTenPence { get; set; }

        public int UserTwentyPence { get; set; }

        public int UserFiftyPence { get; set; }
        public int UserOnePound { get; set; }

        public int UserTwoPound { get; set; }

        public CarParkPayMachine()
        {
            State = new SelectHoursState(this);
            Fee = 0.0M;
            ChangeDue = 0.0M;
            FeeCanceled = false;
            MachineFivePence = 0;
            MachineTenPence = 0;
            MachineTwentyPence = 0;
            MachineFiftyPence = 0;
            MachineOnePound = 0;
            MachineTwoPound = 0;
            UserFivePence = 0;
            UserTenPence = 0;
            UserTwentyPence = 0;
            UserFiftyPence = 0;
            UserOnePound = 0;
            UserTwoPound = 0;
        }

        public void RunCarParkPayMachine()
        {
            while (true)
            {
                State.RunState();
            }
        }

        // Machine must have at least £1.95 in change for possible worst case senario

        public bool CanGiveChange()
        {
            bool canGiveChange = false;

            // Must have a least 1 5p and 1 10p
            if (MachineFivePence > 0 & MachineTenPence > 0)
            {
                if ((MachineFivePence * 5 + MachineTenPence * 10 + MachineTwentyPence * 20) >= 195)
                {
                    canGiveChange = true;
                }

                if (MachineFiftyPence == 1)
                {
                    if ((MachineFivePence * 5 + MachineTenPence * 10 + MachineTwentyPence * 20) >= 145)
                    {
                        canGiveChange = true;
                    }
                }

                if (MachineFiftyPence == 2)
                {
                    if ((MachineFivePence * 5 + MachineTenPence * 10 + MachineTwentyPence * 20) >= 95)
                    {
                        canGiveChange = true;
                    }
                }

                if (MachineFiftyPence >= 3)
                {
                    if ((MachineFivePence * 5 + MachineTenPence * 10 + MachineTwentyPence * 20) >= 35)
                    {
                        canGiveChange = true;
                    }
                }

                if (MachineOnePound >= 1)
                {
                    if ((MachineFivePence * 5 + MachineTenPence * 10 + MachineTwentyPence * 20) >= 95)
                    {
                        canGiveChange = true;
                    }
                }

                if (MachineOnePound >= 1 & MachineFiftyPence >= 1)
                {
                    if ((MachineFivePence * 5 + MachineTenPence * 10 + MachineTwentyPence * 20) >= 45)
                    {
                        canGiveChange = true;
                    }
                }
            }

            return canGiveChange;
        }

        public void DisplayCoinsInserted()
        {
            if (UserFivePence > 0) { Console.WriteLine("{0} Five pence coins inserted", UserFivePence); }
            if (UserTenPence > 0) { Console.WriteLine("{0} Ten pence coins inserted", UserTenPence); }
            if (UserTwentyPence > 0) { Console.WriteLine("{0} Twenty pence coins inserted", UserTwentyPence); }
            if (UserFiftyPence > 0) { Console.WriteLine("{0} Fifty pence coins inserted", UserFiftyPence); }
            if (UserOnePound > 0) { Console.WriteLine("{0} One pound coins inserted", UserOnePound); }
            if (UserTwoPound > 0) { Console.WriteLine("{0} Two pound coins inserted", UserTwoPound); }
            Console.WriteLine();
        }
    }

    public interface IPayMachineState
    {
        CarParkPayMachine PayMachine { get; set; }

        void RunState() { }

        void StateChangeCheck() { }
    }

    public class SelectHoursState : IPayMachineState
    {
        public CarParkPayMachine PayMachine { get; set; }
        public SelectHoursState(CarParkPayMachine carParkPayMachine)
        {
            PayMachine = carParkPayMachine;
            PayMachine.UserFivePence = 0;
            PayMachine.UserTenPence = 0;
            PayMachine.UserTwentyPence = 0;
            PayMachine.UserFiftyPence = 0;
            PayMachine.UserOnePound = 0;
            PayMachine.UserTwoPound = 0;
        }
        public SelectHoursState(IPayMachineState state)
        {
            PayMachine = state.PayMachine;
        }

        public void RunState()
        {
            bool optionVaild = false;

            if (PayMachine.CanGiveChange())
            {
                Console.WriteLine("Change is available");
            }
            else
            {
                Console.WriteLine("No change available");
            }
            Console.WriteLine();

            // uncomment this line if you want to display coins in machine
            DisplayMachineCoins();

            Console.WriteLine("Select number of hours");
            Console.WriteLine();

            Console.WriteLine("Option 1 1 hour");
            Console.WriteLine("Option 2 2 hours");
            Console.WriteLine("Option 3 4 hours");
            Console.WriteLine("Option 4 6 hours");
            Console.WriteLine("Option 5 12 hours");
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
                        PayMachine.Fee = 0.5M;
                        break;
                    case "2":
                        optionVaild = true;
                        PayMachine.Fee = 1.25M;
                        break;
                    case "3":
                        optionVaild = true;
                        PayMachine.Fee = 3.70M;
                        break;
                    case "4":
                        optionVaild = true;
                        PayMachine.Fee = 6.00M;
                        break;
                    case "5":
                        optionVaild = true;
                        PayMachine.Fee = 13.55M;
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
            PayMachine.State = new PayFeeState(this);
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

    public class PayFeeState : IPayMachineState
    {
        public CarParkPayMachine PayMachine { get; set; }

        public PayFeeState(IPayMachineState state)
        {
            PayMachine = state.PayMachine;
        }

        public void RunState()
        {
            bool optionValid = false;

            Console.WriteLine("Fee due {0:C}", PayMachine.Fee);
            Console.WriteLine();

            PayMachine.DisplayCoinsInserted();

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
                        PayMachine.Fee -= 0.05M;
                        PayMachine.Fee = decimal.Round(PayMachine.Fee, 2);
                        PayMachine.UserFivePence++;
                        optionValid = true;
                        break;
                    case "2":
                        PayMachine.Fee -= 0.1M;
                        PayMachine.Fee = decimal.Round(PayMachine.Fee, 2);
                        PayMachine.UserTenPence++;
                        optionValid = true;
                        break;
                    case "3":
                        PayMachine.Fee -= 0.2M;
                        PayMachine.Fee = decimal.Round(PayMachine.Fee, 2);
                        PayMachine.UserTwentyPence++;
                        optionValid = true;
                        break;
                    case "4":
                        PayMachine.Fee -= 0.5M;
                        PayMachine.Fee = decimal.Round(PayMachine.Fee, 2);
                        PayMachine.UserFiftyPence++;
                        optionValid = true;
                        break;
                    case "5":
                        PayMachine.Fee -= 1.0M;
                        PayMachine.Fee = decimal.Round(PayMachine.Fee, 2);
                        PayMachine.UserOnePound++;
                        optionValid = true;
                        break;
                    case "6":
                        PayMachine.Fee -= 2.0M;
                        PayMachine.Fee = decimal.Round(PayMachine.Fee, 2);
                        PayMachine.UserTwoPound++;
                        optionValid = true;
                        break;
                    case "7":
                        PayMachine.FeeCanceled = true;
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
            if (PayMachine.FeeCanceled)
            {
                PayMachine.FeeCanceled = false;
                PayMachine.State = new PayFeeCanceled(this);
            }
            else if (PayMachine.Fee <= 0.0M)
            {
                if (PayMachine.Fee < 0.0M)
                    PayMachine.ChangeDue = Math.Abs(PayMachine.Fee);
                PayMachine.State = new FeePaidState(this);
            }
        }
    }

    public class FeePaidState : IPayMachineState
    {
        public CarParkPayMachine PayMachine { get; set; }

        public FeePaidState(IPayMachineState state)
        {
            PayMachine = state.PayMachine;
        }

        public void RunState()
        {
            PayMachine.DisplayCoinsInserted();

            ResetUserCoins();

            if (PayMachine.ChangeDue > 0.0M & PayMachine.CanGiveChange())
            {
                ReturnChange();
            }
            Console.WriteLine("Fee Paid Thank you");
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();

            StateChangeCheck();
        }

        public void ResetUserCoins()
        {
            PayMachine.MachineFivePence += PayMachine.UserFivePence;
            PayMachine.UserFivePence = 0;
            PayMachine.MachineTenPence += PayMachine.UserTenPence;
            PayMachine.UserTenPence = 0;
            PayMachine.MachineTwentyPence += PayMachine.UserTwentyPence;
            PayMachine.UserTwentyPence = 0;
            PayMachine.MachineFiftyPence += PayMachine.UserFiftyPence;
            PayMachine.UserFiftyPence = 0;
            PayMachine.MachineOnePound += PayMachine.UserOnePound;
            PayMachine.UserOnePound = 0;
            PayMachine.MachineTwoPound += PayMachine.UserTwoPound;
            PayMachine.UserTwoPound = 0;
        }

        public void ReturnChange()
        {
            int ChangeFivePenceCoin = 0;
            int ChangeTenPenceCoin = 0;
            int ChangeTwentyPenceCoin = 0;
            int ChangeFiftyPenceCoin = 0;
            int ChangeOnePoundCoin = 0;
            int ChangeTwoPoundCoin = 0;


            while (PayMachine.ChangeDue > 0.0M)
            {
                if (PayMachine.ChangeDue >= 2.0M & PayMachine.MachineTwoPound > 0)
                {
                    PayMachine.ChangeDue -= 2.0M;
                    PayMachine.MachineTwoPound--;
                    ChangeTwoPoundCoin++;
                }
                else if(PayMachine.ChangeDue >= 1.0M & PayMachine.MachineOnePound > 0)
                {

                    PayMachine.ChangeDue -= 1.0M;
                    PayMachine.MachineOnePound--;
                    ChangeOnePoundCoin++;
                }
                else if (PayMachine.ChangeDue >= 0.5M & PayMachine.MachineFiftyPence > 0)
                {

                    PayMachine.ChangeDue -= 0.5M;
                    PayMachine.MachineFiftyPence--;
                    ChangeFiftyPenceCoin++;
                }
                else if (PayMachine.ChangeDue >= 0.2M & PayMachine.MachineTwentyPence > 0)
                {

                    PayMachine.ChangeDue -= 0.2M;
                    PayMachine.MachineTwentyPence--;
                    ChangeTwentyPenceCoin++;
                }
                else if (PayMachine.ChangeDue >= 0.1M & PayMachine.MachineTenPence > 0)
                {

                    PayMachine.ChangeDue -= 0.1M;
                    PayMachine.MachineTenPence--;
                    ChangeTenPenceCoin++;
                }
                else if (PayMachine.ChangeDue >= 0.05M & PayMachine.MachineFivePence > 0)
                {

                    PayMachine.ChangeDue -= 0.05M;
                    PayMachine.MachineFivePence--;
                    ChangeFivePenceCoin++;
                }

                PayMachine.ChangeDue = decimal.Round(PayMachine.ChangeDue, 2);
            }

            if (ChangeFivePenceCoin > 0)
            {
                Console.WriteLine("Five pence coins returned {0}", ChangeFivePenceCoin);
            }

            if (ChangeTenPenceCoin > 0)
            {
                Console.WriteLine("Ten pence coins returned {0}", ChangeTenPenceCoin);
            }

            if (ChangeTwentyPenceCoin > 0)
            {
                Console.WriteLine("Twenty pence coins returned {0}", ChangeTwentyPenceCoin);
            }

            if (ChangeFiftyPenceCoin > 0)
            {
                Console.WriteLine("Fifty pence coins returned {0}", ChangeFiftyPenceCoin);
            }

            if (ChangeOnePoundCoin > 0)
            {
                Console.WriteLine("One pound coins returned {0}", ChangeOnePoundCoin);
            }

            if (ChangeTwoPoundCoin > 0)
            {
                Console.WriteLine("Two pound coins returned {0}", ChangeTwoPoundCoin);
            }
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();
        }

        public void StateChangeCheck()
        {
            PayMachine.State = new SelectHoursState(this);
        }
    }

    public class PayFeeCanceled : IPayMachineState
    {
        public CarParkPayMachine PayMachine { get; set; }
        public PayFeeCanceled(IPayMachineState state)
        {
            PayMachine = state.PayMachine;
        }

        public void RunState()
        {
            Console.WriteLine("Fee canceled");
            Console.WriteLine();

            if (PayMachine.UserFivePence > 0)
            {
                Console.WriteLine("Five pence coins returned {0}", PayMachine.UserFivePence);
                PayMachine.UserFivePence = 0;
            }

            if (PayMachine.UserTenPence > 0)
            {
                Console.WriteLine("Ten pence coins returned {0}", PayMachine.UserTenPence);
                PayMachine.UserTenPence = 0;
            }

            if (PayMachine.UserTwentyPence > 0)
            {
                Console.WriteLine("Twenty pence coins returned {0}", PayMachine.UserTwentyPence);
                PayMachine.UserTwentyPence = 0;
            }

            if (PayMachine.UserFiftyPence > 0)
            {
                Console.WriteLine("Fifty pence coins returned {0}", PayMachine.UserFiftyPence);
                PayMachine.UserFiftyPence = 0;
            }

            if (PayMachine.UserOnePound > 0)
            {
                Console.WriteLine("One pound coins returned {0}", PayMachine.UserOnePound);
                PayMachine.UserOnePound = 0;
            }

            if (PayMachine.UserTwoPound > 0)
            {
                Console.WriteLine("Two pound coins returned {0}", PayMachine.UserTwoPound);
                PayMachine.UserTwoPound = 0;
            }
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine();

            StateChangeCheck();
        }

        public void StateChangeCheck()
        {
            PayMachine.State = new SelectHoursState(this);
        }
    }
}
