using WhenAndWhere.DTO;

namespace WhenAndWhere.BL.Interfaces;

public interface IOptionService
{
    Task<List<OptionDTO>> GetAllOptions();

    Task<OptionDTO> GetOption(int id);

    Task CreateOption(OptionDTO optionDto);

    Task UpdateOption(OptionDTO optionDto);

    Task DeleteOption(int id);
}