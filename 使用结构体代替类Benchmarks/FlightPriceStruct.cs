using System.Runtime.InteropServices;

namespace 使用结构体代替类Benchmarks
{
    [StructLayout(LayoutKind.Auto)]
    public struct FlightPriceStruct : IEquatable<FlightPriceStruct>
    {
        public string Airline { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public string FlightNo { get; set; }

        public string Cabin { get; set; }

        public decimal Price { get; set; }


        public DateOnly DepDate { get; set; }


        public TimeOnly DepTime { get; set; }


        public DateOnly ArrDate { get; set; }


        public TimeOnly ArrTime { get; set; }


        public bool EqualsAirline(string airline)
        {
            return Airline == airline;
        }

        public bool EqualsStart(string start)
        {
            return Start == start;
        }

        public bool EqualsEnd(string end)
        {
            return End == end;
        }

        public bool EqualsFlightNo(string flightNo)
        {
            return FlightNo == flightNo;
        }

        public bool IsCabin(string cabin)
        {
            return Cabin == cabin;
        }

        public bool IsPriceLess(decimal min)
        {
            return Price == min;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(FlightPriceStruct left, FlightPriceStruct right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FlightPriceStruct left, FlightPriceStruct right)
        {
            return !(left == right);
        }

        public bool Equals(FlightPriceStruct other)
        {
            return base.Equals(other);
        }

        //public static bool EqualsAirLine(ref FlightPriceStruct item,string airline)
        //{
        //    return item.Airline == airline;
        //}


    }
}