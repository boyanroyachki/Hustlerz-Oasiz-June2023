using AutoMapper;

namespace HustlerzOasiz.Services.Mapping.Interfaces
{
    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}
