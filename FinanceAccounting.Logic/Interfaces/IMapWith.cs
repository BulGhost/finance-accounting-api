using AutoMapper;

namespace FinanceAccounting.Logic.Interfaces
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile) =>
            profile.CreateMap(typeof(T), GetType());
    }
}
