using System;
using AutoMapper;

namespace TestService
{
    #region int?与int 互转
    public class IntNullConverter : ITypeConverter<int?, int>
    {
        public int Convert(int? source, int destination, ResolutionContext context)
        {
            if (source == null)
            {
                destination = Int32.MinValue;
            }
            else
            {
                destination = source.Value;
            }
            return destination;
        }
    }
    public class IntConverter : ITypeConverter<int, int?>
    {
        public int? Convert(int source, int? destination, ResolutionContext context)
        {
            if (source == Int32.MinValue)
            {
                destination = null;
            }
            else
            {
                destination = source;
            }
            return destination;
        }
    }
    #endregion

    #region String类型Null与GRPCNull转换
    public class StringNullConverter : ITypeConverter<string, string>
    {
        public string Convert(string source, string destination, ResolutionContext context)
        {
            if (source == null)
            {
                destination = "GrpcNull";
            }
            else if (source == "GrpcNull")
            {
                destination = null;
            }
            else
            {
                destination = source;
            }
            return destination;
        }
    }
    #endregion

}
