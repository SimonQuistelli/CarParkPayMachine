using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace CarParkPayMachine
{
    public enum Duration
    {
        OneHour,
        TwoHours,
        FourHours,
        SixHours,
        TwelveHours
    }
    public interface ICarParkPayMachine
    {
        bool FeePaid { get; set; }
        decimal Fee { get; set; }
        decimal ChangeDue { get; set; }

        bool FeeCanceled { get; set; }

        int MachineFivePence { get; set; }

        int MachineTenPence { get; set; }

        int MachineTwentyPence { get; set; }

        int MachineFiftyPence { get; set; }
        int MachineOnePound { get; set; }

        int MachineTwoPound { get; set; }

        int UserFivePence { get; set; }

        int UserTenPence { get; set; }

        int UserTwentyPence { get; set; }

        int UserFiftyPence { get; set; }
        int UserOnePound { get; set; }

        int UserTwoPound { get; set; }

        void InsertFivePence();
        void InsertTenPence();
        void InsertTwentyPence();
        void InsertFiftyPence();
        void InsertOnePound();
        void InsertTwoPound();

        bool CanGiveChange();

        void SetFee(Duration duration);

        void ResetUserCoins();

        Change ReturnChange();

        Change ReturnUserCoins();
    }

    public class CarParkPayMachine : ICarParkPayMachine
    {
        public bool FeePaid { get; set; }
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
            FeePaid = false;
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

        public void InsertFivePence()
        {
            UserFivePence++;
            Fee -= 0.05M;
            Fee = decimal.Round(Fee, 2);
        }

        public void InsertTenPence()
        {
            UserTenPence++;
            Fee -= 0.1M;
            Fee = decimal.Round(Fee, 2);
        }

        public void InsertTwentyPence()
        {
            UserTwentyPence++;
            Fee -= 0.2M;
            Fee = decimal.Round(Fee, 2);
        }

        public void InsertFiftyPence()
        {
            UserFiftyPence++;
            Fee -= 0.5M;
            Fee = decimal.Round(Fee, 2);
        }

        public void InsertOnePound()
        {
            UserOnePound++;
            Fee -= 1.0M;
            Fee = decimal.Round(Fee, 2);
        }

        public void InsertTwoPound()
        {
            UserTwoPound++;
            Fee -= 2.0M;
            Fee = decimal.Round(Fee, 2);
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

        public void SetFee(Duration duration)
        {
            switch (duration)
            {
                case Duration.OneHour:
                    Fee = 0.5M;
                    break;
                case Duration.TwoHours:
                    Fee = 1.25M;
                    break;
                case Duration.FourHours:
                    Fee = 3.70M;
                    break;
                case Duration.SixHours:
                    Fee = 6.0M;
                    break;
                case Duration.TwelveHours:
                    Fee = 13.55M;
                    break;
                default:
                    Fee = 0.0M;
                    break;

            }
            Fee = decimal.Round(Fee, 2);
        }

        public void ResetUserCoins()
        {
            MachineFivePence += UserFivePence;
            UserFivePence = 0;
            MachineTenPence += UserTenPence;
            UserTenPence = 0;
            MachineTwentyPence += UserTwentyPence;
            UserTwentyPence = 0;
            MachineFiftyPence += UserFiftyPence;
            UserFiftyPence = 0;
            MachineOnePound += UserOnePound;
            UserOnePound = 0;
            MachineTwoPound += UserTwoPound;
            UserTwoPound = 0;
        }

        public Change ReturnChange()
        {
            Change change = new Change();

            while (ChangeDue > 0.0M)
            {
                if (ChangeDue >= 2.0M & MachineTwoPound > 0)
                {
                    ChangeDue -= 2.0M;
                    MachineTwoPound--;
                    change.TwoPoundCoins++;
                }
                else if (ChangeDue >= 1.0M & MachineOnePound > 0)
                {

                    ChangeDue -= 1.0M;
                    MachineOnePound--;
                    change.OnePoundCoins++;
                }
                else if (ChangeDue >= 0.5M & MachineFiftyPence > 0)
                {

                    ChangeDue -= 0.5M;
                    MachineFiftyPence--;
                    change.FiftyPenceCoins++;
                }
                else if (ChangeDue >= 0.2M & MachineTwentyPence > 0)
                {

                    ChangeDue -= 0.2M;
                    MachineTwentyPence--;
                    change.TwentyPenceCoins++;
                }
                else if (ChangeDue >= 0.1M & MachineTenPence > 0)
                {

                    ChangeDue -= 0.1M;
                    MachineTenPence--;
                    change.TenPenceCoins++;
                }
                else if (ChangeDue >= 0.05M & MachineFivePence > 0)
                {

                    ChangeDue -= 0.05M;
                    MachineFivePence--;
                    change.FivePenceCoins++;
                }

                ChangeDue = decimal.Round(ChangeDue, 2);
            }

            return change;
        }

        public Change ReturnUserCoins()
        {
            Change change = new Change();

            change.FivePenceCoins = UserFivePence;
            change.TenPenceCoins = UserTenPence;
            change.TwentyPenceCoins = UserTwentyPence;
            change.FiftyPenceCoins = UserFiftyPence;
            change.OnePoundCoins = UserOnePound;
            change.TwoPoundCoins = UserTwoPound;

            ResetUserCoins();

            return change;
        }
    }

    public struct Change
    {
        public int FivePenceCoins { get; set; }
        public int TenPenceCoins { get; set; }
        public int TwentyPenceCoins { get; set; }
        public int FiftyPenceCoins { get; set; }
        public int OnePoundCoins { get; set; }
        public int TwoPoundCoins { get; set; }

        public Change()
        {
            FivePenceCoins = 0;
            TenPenceCoins = 0;
            TwentyPenceCoins = 0;
            FiftyPenceCoins = 0;
            OnePoundCoins = 0;
            TwoPoundCoins = 0;
        }
    }
}
